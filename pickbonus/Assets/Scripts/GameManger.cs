using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 *<summary> Arn Mesa 06-24-2022
 */
public class GameManger : MonoBehaviour //IPointerEnterHandler, IPointerExitHandler
{
    /**
     * <summary> used for Singleton
     */
    public static GameManger gamemanagerInstance;

    #region Private Variables
    private const int ChestArraySize = 9;
    [SerializeField] private ChestButton[] chestButtons = new ChestButton[ChestArraySize];
    [SerializeField] private Button[] chests = new Button[ChestArraySize];
    [SerializeField] private Image[] chestImage = new Image[ChestArraySize];
    [SerializeField] private Image[] playButtonImage;
    [SerializeField] private Image[] BetButtonImage;
    [SerializeField] private ChanceObject chanceObject;
    [SerializeField] private CurrentBalance currentBalance;
    [SerializeField] private Winnings win;
    [SerializeField] private Split splitter;
    [SerializeField] private Denomination denom;
    [SerializeField] private PlayButton playButton;
    [SerializeField] private Button[] PlayButtons; //Play Button Button Class
    [SerializeField] private Button[] BetButtons;
    [SerializeField] private List<float> winningValues = new List<float>();
    [SerializeField] private bool pooperFound;
    [SerializeField] private bool adjustValue;
    [SerializeField] private bool toggleTimerObjects;
    [SerializeField] private bool resetRewardTracker;
    [SerializeField] private bool calculatingSplitDone;
    [SerializeField] private float multiplier;
    [SerializeField] private float timerDelay;
    [SerializeField] private const float timerDelayConst = 5.0f;
    [SerializeField] private float trackerTimer;
    [SerializeField] private const float trackerTimerConst = 1.2f;
    [SerializeField] private int rewardIndex = -1;
    [SerializeField] private int chestIndex = -1;
    #endregion

     /**
     * <summary> Only one instance of GameManager can exists On Play
     */
    private void Awake()
    {
        if(gamemanagerInstance != null && gamemanagerInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gamemanagerInstance = this;
        }
    }

    /**
    * <summary> timerDelay and MaxBet is set to their appropriate values
    */
    void Start()
    {
        timerDelay = timerDelayConst;
        denom.SetMaxBet(currentBalance.GetDefaultBalance());
    }

     /**
     * <summary> Gameloop logic is in update to set how the loop happens sequentially
     */
    void Update()
    {
        DenomOverBalance();
        OnPlayLogic();
        if (toggleTimerObjects)
        {
            DelayFunction();
        }
        if (splitter.RecalcDone() && playButton.GetPlay())
        {
            toggleTimerObjects = false;
            calculatingSplitDone = true;
        }
        CheckPooper();
        PooperHandler();
    }

    #region Functions

        #region CheckBet Function
        /**
         * <summary> CheckBet tracks wether bet is too high or too low
         * it then sets the play button.interactable & Image.rayCastarget to either true or false
         */
        void CheckBet()
        {
            if (denom.GetBetOverLoad() || denom.GetBetLow())
            {

                    ToggleButtonArray(PlayButtons, false);
                    ToggleButtonRaycast(playButtonImage, false);
            }
            else
            {
                    ToggleButtonArray(PlayButtons, true);
                    ToggleButtonRaycast(playButtonImage, true);
            }
        }
        #endregion

        #region TrackRewards Function
        /**
         * <summary> TrackRewards distributes the winnings predeterminedly 
         * TrackRewards Identifies which chest the player is hovering over and assigning chestNum to chestIndex
         * Once chestIndex is assigned a value tt then determines which function to run relative to winningValues' count
         */
        void TrackRewards()
        {
            for (int i = 0; i < chestButtons.Length; i++)
            {
                if (chestButtons[i].GetEntered())
                {
                    chestIndex = chestButtons[i].GetChestNum();
                }
            }
            if (winningValues.Count > 0)
            {
                RewardHandler();
            }
            else
            {
                PooperSetter();
            }
        }
        #endregion

        #region PooperSetter Function
        /**
         * <summary>Determines which chest is going to be the pooper
         * When winninValues is empty or when multiplier is 0
         * Pooper is set To whatever chest the player is hovering and then clicking on using chestIndex
         * The chestIndex is determined by TrackRewards Function
         * PooperSetter is also called inside TrackRewards
         */
        void PooperSetter()
        {
            if (chestIndex > -1 && !chestButtons[chestIndex].GetEntered())
            {
                chestButtons[chestIndex].SetReward(0.0f);
                chestButtons[chestIndex].SetPooper(false);
                chestIndex = -1;
            }
            if (chestIndex > -1 && chestButtons[chestIndex].GetEntered())
            {
                chestButtons[chestIndex].SetReward(0.0f);
                chestButtons[chestIndex].SetRewardFinal(0.0f);
                if (chestButtons[chestIndex].GetClicked())
                {
                    chestButtons[chestIndex].SetPooper(true);
                    Debug.Log("Chest " + chestButtons[chestIndex].GetChestNum() + "Clicked" + "@ Index " + chestIndex);
                }
            }
        }
        #endregion

        #region Handles the predetermined distribution of rewards via chest buttons
        /**
         * <summary>Distributes Player winnings
         * Once chestIndex is identified
         * And winningValues list is not empty
         * and rewardIndex is less than the winningValues contents
         * RewardHandler will distribute the divided player winnings into whichever 
         * chest the player is hovering over until all the elements in winningValues list
         * is set to -1 and rewardIndex is equals to the winningValues length
         * once those conditions are not met, then RewardHandler will set
         * any chest the player is hovering over to the pooper
         */
        void RewardHandler()
        {
            if (chestIndex > -1 && !chestButtons[chestIndex].GetEntered()) 
            {
                chestButtons[chestIndex].SetReward(0.0f);
                Debug.Log("Exit Chest");
                if (rewardIndex == winningValues.Count)
                {
                        //Debug.Log("Set Reward ");
                        chestButtons[chestIndex].SetPooper(false);
                }
            }
            else if (chestIndex > -1 && chestButtons[chestIndex].GetEntered())
            {
                if (rewardIndex < winningValues.Count)
                {
                    //Debug.Log("Set Reward ");
                    chestButtons[chestIndex].SetReward(winningValues.ElementAt(rewardIndex));
                }
                else if (rewardIndex >= winningValues.Count)
                {
                    if (chestButtons[chestIndex].GetRewardFinal() <= 0 && chestButtons[chestIndex].GetClicked())
                    {
                        chestButtons[chestIndex].SetPooper(true);
                        float roundWinnings = currentBalance.GetDefaultBalance() + win.GetWinningAmount();
                        currentBalance.SetCurrentBalance(roundWinnings);

                    }

                }
            }
        }
        #endregion

        #region  CalculateWinnings Function
        /**
         * <summary> multiplies the multiplier and denom totaling to the total winning
         * returns the multiplied value
         */
        float CalculateWinnings()
        {
            float winnings = denom.GetCurrentBet() * multiplier;
            return winnings;
        }
        #endregion

        #region OnPlayLogic Function
        /**
         * <summary>Function that contains the logic when Play button is pressed
         * Determines wether play button is pressed via playButtonGetPlay function
         * which returns a bool value when Play Button is pressed
         * Once play is determined it will then run all the functions sequentially
         * This is where the player winnings is calculated and the splitting of it is called
         * OnPlayLogic also toggle buttons appropriately
         */
        void OnPlayLogic()
        {
            if (playButton.GetPlay())
            {
                if (pooperFound)
                {
                    pooperFound = false;
                    ResetOnPlay();
                }

                multiplier = chanceObject.GetMultiplier();
                ToggleButtonArray(PlayButtons, false);
                ToggleButtonArray(BetButtons, false);
                ToggleButtonRaycast(playButtonImage, false);
                ToggleButtonRaycast(BetButtonImage, false);
                float winnings = CalculateWinnings();

                if (multiplier != 0 && winningValues.Count() == 0)
                {
                    splitter.SetWinBalance(winnings);
                    splitter.calcSplit = true;
                    if (!calculatingSplitDone || !splitter.RecalcDone())
                    {
                        toggleTimerObjects = true;
                        splitter.SplitWinnings();
                    }
                    else
                    {
                        winningValues.AddRange(splitter.GetWinningValues());

                    }
                }
                TrackRewards();
                CheckOpenChests();
            }
            else
            {
                CheckBet();
                ToggleButtonArray(BetButtons, true);
                ToggleButtonRaycast(BetButtonImage, true);
                ToggleButtonArray(chests, false);
                ToggleButtonRaycast(chestImage, false);
            }
        }
        #endregion

        #region BalanceAdjustment Function
        /**
         * <summary> Function that adjusts Current balance according to the bet made once play is pressed
         */
        public void BalanceAdjustment()
        {
            if (!denom.GetBetOverLoad())
            {
                Debug.Log("Balance Adjusted");
                float adjustBalance = currentBalance.GetDefaultBalance() - denom.GetCurrentBet();
                currentBalance.SetCurrentBalance(adjustBalance);
                adjustValue = false;
            }
        }
        #endregion

        #region DenomOverBalance Function
        /**
         * <summary> Function that checks if denomination is less than current balance
         */
        void DenomOverBalance()
        {
            if (denom.GetCurrentBet() <= currentBalance.GetDefaultBalance())
            {
                denom.SetBetOverLoad(false);
            }
            else
            {
                Debug.Log("Bet is higher than CurrentBalance");
                denom.SetBetOverLoad(true);
            }
        }
        #endregion

        #region ToggleButtonArray Function
        /**
         * <summary> Function To Toggle Buttons' Interactable
         */
        void ToggleButtonArray(Button[] button, bool condition)
        {
            for (int i = 0; i < button.Length; i++)
            {
                button[i].interactable = condition;
            }
        }
        #endregion

        #region ToggleButtonRaycast Function
        /**
         * <summary> Function that toggles images RayCastTarget
         */
        void ToggleButtonRaycast(Image[] image, bool condition)
        {
            for (int i = 0; i < image.Length; i++)
            {
                image[i].raycastTarget = condition;
            }
        }
        #endregion

        #region DelayFunction Function
        /**
         * <summary> DelayFunction is used to give calculations enough time to 
         * calculate and check if the calculations meets the required conditions
         */
        void DelayFunction()
        {
            timerDelay -= Time.deltaTime;

            if (timerDelay <= 0)
            {
                calculatingSplitDone = true;
                toggleTimerObjects = false;
                timerDelay = timerDelayConst;
            }
        }
        #endregion

        #region UnusedCode
        /**
         *<summary> Unused Code
        void RewardTrackerTimer()
        {
            trackerTimer -= Time.deltaTime;

            if (trackerTimer <= 0)
            {
                trackerTimer = trackerTimerConst;
                resetRewardTracker = false;
            }
        }
        */
        #endregion

        #region RewardIndexIncrement Funtion
        /**
         * <summary> Increments rewardIndex whenever a chest is clicked or Opened
         * helps RewardHandler to distribute winnings by identifying at which element
         * of winningValues list has a value and also sets that element value into -1 to
         * represent no value so that RewardHandler knows not to assign the value or anymore
         * values into a chest
         * Reward Index also displays the winnings when a chest is opened and has a value
         */
        public void RewardIndexInc()
        {
            if (winningValues.Count > 0 && rewardIndex < winningValues.Count && rewardIndex >= 0)
            {
                win.SetWinningText(winningValues.ElementAt(rewardIndex));
                if (rewardIndex < winningValues.Count)
                {
                    chestButtons[chestIndex].SetRewardFinal(winningValues.ElementAt(rewardIndex));
                    winningValues[rewardIndex] = -1;
                    rewardIndex++;
                }
                else if (rewardIndex >= winningValues.Count)
                {
                    rewardIndex = 0;
                }
            }
        }
        #endregion

        #region CheckOpenChest Function
        /**
         * <summary> Checks if any chests are open and set interactable and raycasTarget to false
         */
        void CheckOpenChests()
        {
            for (int i = 0; i < chestButtons.Length; i++)
            {
                if (chestButtons[i].GetChestOpen())
                {
                    chestImage[i].raycastTarget = false;
                    chests[i].interactable = false;
                }
            }
        }
        #endregion

        #region ActivateChestButtons Function
        /**
         * <summary> Activates chests and set interactable and raycasTarget to true
         * allows chests to be clickable when play button is pressed
         * called by play button
         */
        public void ActivateChestButtons()
        {
            ToggleButtonArray(chests, true);
            ToggleButtonRaycast(chestImage, true);
        }
        #endregion

        #region CheckPooper Function
        /**
        * <summary> Check if any of the chests is set to pooper
        */
        void CheckPooper()
        {
        
            for (int i = 0; i < chestButtons.Length; i++)
            {
                if (chestButtons[i].GetIsPooper() && chestButtons[i].GetRewardFinal() <= 0)
                {
                    pooperFound = true;
                }
            }

        }
        #endregion

        #region Setter and Getter functions
        /**
        * <summary> allows access to private float List winningValues
        */
        public List<float> GetWinningValues()
        {
            return winningValues;
        }

        public void SetWinningValues(List<float> values)
        {
            winningValues.AddRange(values);
        }
        #endregion

        #region PooperHandler Function
        /**
        * <summary> Appropriate actions when pooper is found
        */
        void PooperHandler()
        {
            if(pooperFound)
            {
                playButton.SetPlay(false);
                ToggleButtonArray(chests, false);
                ToggleButtonRaycast(chestImage, false);
                winningValues.Clear();
                Resetcalc();
            }
        }
        #endregion

        #region Resetcalc Function
        /**
        * <summary> Reset necessary bools
        * clears Lists
        * and set Indexes to default value of zero
        * called in PooperHandler
        */
        void Resetcalc()
        {
            rewardIndex = 0;
            splitter.calcSplit = false;
            calculatingSplitDone = false;
            splitter.ClearValues();
            splitter.SetRecalc(false);
        }
        #endregion

        #region ResetOnPlay Function
        /**
        * <summary> Reset winning display text to 0
        * reset all chests to default values and images
        */
        void ResetOnPlay()
        {
            win.ResetWinningAmount(0.0f);
            for (int i = 0; i < chestButtons.Length; i++)
            {
                chestButtons[i].ResetChest();
            }
        }
        #endregion

    #endregion


}