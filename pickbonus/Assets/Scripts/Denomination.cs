using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Arn Mesa
 * 6/24/2022
 */
public class Denomination : MonoBehaviour
{


    #region Enum used to determine which betting system is used
    /**
     *<summary> Enum used to determine which betting system is used
     * A random betting which denominations is randomly selected 
     * A manual one which is manually set by the player using buttons 
     */
    public enum BetMode
    {
        randomBet = 0,
        manualBet = 1,
    }
    #endregion

    #region Serialized Private Variables
    /**
     * <summary>Exposed private variables to easily modify within the Unity Editor 
     */
    [SerializeField] private GameObject betObj;
    [SerializeField] private BetMode betMode;
    [SerializeField] private Money[] denom;
    [SerializeField] private Money defaultBet;
    [SerializeField] private Text currentBetText;
    [SerializeField] private Text selectedDenomText;
    [SerializeField] private Text addToBetText;
    [SerializeField] private float maxBet;
    [SerializeField] private float selectedDenom;
    [SerializeField] private float currentBet;
    [SerializeField] private float tweenTimer;
    [SerializeField] private float tweenTime;
    [SerializeField] private float tempBet;
    [SerializeField] private const float tweenTimerConst = 0.2f;
    [SerializeField] private bool tweenText;
    [SerializeField] private bool betOverLoad;
    [SerializeField] private bool betLow;
    [SerializeField] private bool isBetting;
    [SerializeField] private Vector3[] textSize;
    #endregion

    #region LeanTweenManager Object
    private LeanTweenManager leanTween = new LeanTweenManager();
    #endregion

    #region Start
    /**
     * <summary> tweenTimer at the start is set to a constant
     * GetMaxBet is checked at the start to make sure it is not zero or less
     * If it is then it is corrected by setting the max bet to the initial default value
     * using SetMaxBet function that takes in a float argument
     */
    void Start()
    {
        tweenTimer = tweenTimerConst;
        if (GetMaxBet() <= 0.0f)
        {
            SetMaxBet(10.0f);
        }

        if (betMode == BetMode.manualBet)
        {
            betObj.SetActive(true);
        }
        else
        {
            betObj.SetActive(false);
        }
    }
    #endregion

    #region Update
    /**
     * <summary> Nothing much is called in update
     * CheckCurrentBet function to always check if bets are correct
     * TweenDelay to time the tweening properly
     */
    void Update()
    {
        //CheckCurrent Bet is called in Update to constantly check wether bets are within min or max
        CheckCurrentBet();

        #region Text Tweening
        //When true TweenDelay runs until timer is zero
        //tweenText is set to true within the overloaded functions SelectDenom and also in function SetCurrentBet
        if (tweenText)
        {
            TweenDelay();
        }
        #endregion
    }
    #endregion


    #region AddToBet Function
    /**
     * <summary> Adds bet using accumultor whenever a button is pressed
     * if else statement determines how currentBet is being added 
     * Depending on which bet mode is being used Manual/Random
     * if random betting is selected then RandomBetFunction is called
     * with an argument of true which adds values inside the RandomFunc function
     */
    public void AddToBet()
    {
        if (betMode == BetMode.manualBet)
        {
            addToBetText.text = "Add to Bet";
            currentBet += selectedDenom;
            SetCurrentBet(currentBet);
        }
        else
        {
            addToBetText.text = "Added to Bet";
            RandomBetFunc(true);
            SetCurrentBet(currentBet);
        }
    }
    #endregion

    #region SubtractFromBet Function
    /**
     * <summary> Subtracts bet using decumulator whenever a button is pressed
     * if else statement determines how currentBet will be subtracted
     * Depending on which bet mode is being used Manual/Random
     * if random betting is selected then RandomBetFunction is called
     * with an argument of false which subtracts values inside the RandomFunc function
    */
    public void SubtractFromBet()
    {
        if (currentBet > 0)
        {
            if (betMode == BetMode.manualBet)
            {
                addToBetText.text = "Subtract from Bet";
                currentBet -= selectedDenom;
                SetCurrentBet(currentBet);
            }
            else
            {
                addToBetText.text = "Subtracted from Bet";
                RandomBetFunc(false);
                SetCurrentBet(currentBet);
            }
        }
    }
    #endregion

    #region RandomBet Function
    /**
     * <summary> Randomizes bet
     * local int vriable get a random number and is then used 
     * to find a a random float value within the denom array
     * if condition is true then current bet is accumulated
     * if condition is false then current bet is decumulated;
     */
    void RandomBetFunc(bool condition)
    {
        int index = Random.Range(0, 4);
        tempBet = denom[index].money;
        if (condition)
        {
            currentBet += tempBet;
        }
        else
        {
            currentBet -= tempBet;
        }
        SelectDenom();
    }
    #endregion

    #region CheckCurrentBet Function
    /**
     * <summary> Checks if player bet is zero or less than that
     */
    public void CheckCurrentBet()
    {
        if(currentBet <= 0)
        {
            SetBetLow(true);
        }
        else
        {
            SetBetLow(false);
        }
        if (currentBet < 0)
        {
            Debug.Log(" Current Bet is less than 0, adjusting to 0 ");
            SetCurrentBet(0);
        }
    }
    #endregion

    #region SelectDenom Overloaded Functions
    /**
     * <summary> SelectDenom function that is used when manual betting mode system is picked
     * called when a button is pressed
     * the int parameter represents which index of the denom array is being picked when the button is pressed
     */
    public void SelectDenom(int amount)
    {
        selectedDenom = denom[amount].money;
        selectedDenomText.text = "$ " + selectedDenom.ToString();
        leanTween.ScaleObjects(selectedDenomText.gameObject, textSize[3], tweenTime);
        tweenText = true;
    }
    /**
     * <summary> SelectDenom function that is used when random betting mode system is picked
     * called in RandomBetting function
     */
    public void SelectDenom()
    {
        selectedDenomText.text = "$ " + tempBet.ToString();
        leanTween.ScaleObjects(selectedDenomText.gameObject, textSize[3], tweenTime);
        tweenText = true;
    }
    #endregion

    #region Getter and Setter Functions
    /**
     *<summary> Setters and getters to access private variables within the class
     */
    public bool GetBetLow()
    {
        return betLow;
    }

    public void SetBetLow(bool condition)
    {
        betLow = condition;
    }
    public float GetCurrentBet()
    {
        return currentBet;
    }

    public bool GetBetOverLoad()
    {
        return betOverLoad;
    }

    public void SetBetOverLoad(bool condition)
    {
        betOverLoad = condition;
    }

    public bool GetIsBetting()
    {
        return isBetting;
    }

    public void SetIsBetting(bool condition)
    {
        isBetting = condition;
    }

    public float GetMaxBet()
    {
        return maxBet;
    }

    public void SetMaxBet(float amount)
    {
        maxBet = amount;
    }
    #endregion

    #region SetCurrentBet Function
    /**
     * <summary>Function that displays and tweens the current bet
     */
    void SetCurrentBet(float amount)
    {
        currentBet = amount;
        currentBetText.text = "$ " +  currentBet.ToString();
        leanTween.ScaleObjects(currentBetText.gameObject, textSize[2], tweenTime);
        tweenText = true;
    }
    #endregion

    #region TweenDelay Function
    /**
     * <summary> Scales Text back to their original scale when timer is 0 or less
     */
    void TweenDelay()
    {
        tweenTimer -= Time.deltaTime;

        if (tweenTimer <= 0)
        {
            leanTween.ScaleObjects(currentBetText.gameObject, textSize[0], tweenTime);
            leanTween.ScaleObjects(selectedDenomText.gameObject, textSize[1], tweenTime);
            tweenText = false;
            tweenTimer = tweenTimerConst;
        }
    }
    #endregion


}
