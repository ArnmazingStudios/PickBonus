using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ChestButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] chestHoverClip;
    [SerializeField] AudioClip[] chestClickClip;
    [SerializeField] AudioClip pooperClip;
    [SerializeField] ChestInfo chestInfo;
    [SerializeField] Image chestImage;
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] bool buttonEntered;
    [SerializeField] bool buttonClicked;
    [SerializeField] bool isPooper;
    [SerializeField] bool chestOpen;
    [SerializeField] float volume;
    [SerializeField] float clickDelay;
    [SerializeField] float TimerConst;
    [SerializeField] float reward;
    [SerializeField] int chestNum;
    [SerializeField] Vector3[] chestSize;
    LeanTweenManager tweenManager = new LeanTweenManager();
    AudioHandler audioHandler = new AudioHandler();
    // Start is called before the first frame update
    void Start()
    {
        isPooper = chestInfo.isPooper;
        clickDelay = TimerConst;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(buttonClicked)
        {
           // DelayClick();
        }
        */
        if(chestOpen)
        {
            ChestLogic();
        }
    }

    public void OpenChest()
    {
        chestOpen = true;
        //ChestLogic();
    }

    #region ChestLogic Function
    /**
     * <summary> Sets the appropriate image when chest is opened
     * Sets the button uninteractable when opened
     */
    void ChestLogic()
    {
        if (reward > 0 && !GetIsPooper())
        {
            chestImage.sprite = chestInfo.rewardImage;
        }
        if (GetIsPooper() && reward <= 0)
        {
            chestImage.sprite = chestInfo.pooperImage;
        }
        //rewardText.transform.position = this.transform.position;
        //rewardText.text = "$" + GetReward().ToString();
        button.interactable = false;
    }
    #endregion

    #region EventSystem Functions
    /**
     * <summary> Tracks wether player hover or click buttons
     * Tweens and sets booleans to appropriate conditions when Player does
     */
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.LogError("Entered");
        buttonEntered = true;
        ChestAnticipation(1, 0.1f);
        audioHandler.PlayAudioRandomOneShot(chestHoverClip, audioSource, volume);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        buttonEntered = false;
        ChestAnticipation(0, 0.1f);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.LogError("Clicked");
        buttonClicked = true;
        if (chestInfo.isPooper)
        {
            audioHandler.PlayAudioOneShot(pooperClip, audioSource, volume);
        }
        else
        {
            audioHandler.PlayAudioRandomOneShot(chestClickClip, audioSource, volume);
        }

    }
    #endregion

    #region Setter and Getter Functions
    /**
     * <summary> Functions used to access and modify private variables
     */
    public void SetChestOpen(bool condition)
    {
        chestOpen = condition;
    }

    public void SetEntered(bool condition)
    {
        buttonEntered = condition;
    }

    public void SetClicked(bool condition)
    {
        buttonClicked = condition;
    }

    public void SetReward(float amount)
    {
        chestInfo.reward = amount;
    }

    public bool GetChestOpen()
    {
        return chestOpen;
    }

    public int GetChestNum()
    {
        return chestNum;
    }

    public float GetReward()
    {
        return chestInfo.reward;
    }

    public bool GetEntered()
    {
        return buttonEntered;
    }

    public bool GetClicked()
    {
        return buttonClicked;
    }

    public bool GetIsPooper()
    {
        return chestInfo.isPooper;
    }

    public void SetPooper(bool condition)
    {
        isPooper = condition;
        chestInfo.isPooper = condition;
    }

    public void SetRewardFinal(float amount)
    {
        reward = amount;
    }

    public float GetRewardFinal()
    {
        return reward;
    }
    #endregion

    #region Unused Timer Function
    void DelayClick()
    {
        clickDelay -= Time.deltaTime;

        if(clickDelay <= 0)
        {
            Debug.LogError("Delay UnClicked");
            buttonClicked = false;
            chestOpen = false;
            clickDelay = TimerConst;
        }
    }
    #endregion

    #region ResetChest Function
    /**
     * <summary> Resets the chest when play is clicked
     * sets images and booleans appropriately
     * called by ResetOnPlay function in GameManager
     */
    public void ResetChest()
    {
        chestImage.sprite = chestInfo.defaultImage;
        chestOpen = false;
        chestInfo.isPooper = false;
        chestInfo.reward = 0.0f;
        reward = 0.0f;
        buttonClicked = false;
        if(!button.interactable)
        {
            button.interactable = true;
        }
    }
    #endregion ChestAnticipation

    #region ChestAnticipation Function
    /**
     * <summary> Dotween chests by calling overloaded function ChestAnticipation
     */
    void ChestAnticipation(int index, float timeValue)
    {
        tweenManager.ScaleObjects(this.gameObject, chestSize[index], timeValue);
    }
    #endregion








}
