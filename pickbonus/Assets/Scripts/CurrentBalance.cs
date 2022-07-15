using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentBalance : MonoBehaviour
{
    /**
    * <summary> Used for singleton
    */
    public static CurrentBalance currentBalance;

    #region Private Variables
    [SerializeField] Money defaultBalance;
    [SerializeField] Text balanceText;
    [SerializeField] float adjustedBalance;
    [SerializeField] float tweenTime;
    [SerializeField] float tweenTimer;
    [SerializeField] const float tweenTimerConst = 0.2f;
    [SerializeField] bool adjustBalance;
    [SerializeField] bool tweenText;
    [SerializeField] Vector3[] textSize;
    LeanTweenManager leanTween = new LeanTweenManager();
    #endregion

    #region Singleton
    /**
     * <summary> Only one instance of CurrentBalance can exist on runtime
     */
    private void Awake()
    {
        if (currentBalance != null && currentBalance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            currentBalance = this;
        }
    }
    #endregion

    /**
    * <summary> default values assigned to TweenTimer, defaultBalance, adjustedBalance and Balance text
    */
    void Start()
    {
        tweenTimer = tweenTimerConst;
        if (defaultBalance.money < 10.0f || defaultBalance.money > 10.0f)
        {
            defaultBalance.money = 10.0f;
        }
        adjustedBalance = defaultBalance.money;
        balanceText.text = "$" + defaultBalance.money.ToString();
    }

    /**
    * <summary> TweenDelay is called on update and runs whenever tweenText is true
    */
    void Update()
    {
        if (tweenText)
        {
            TweenDelay();
        }
    }


    #region Setter and getter functions
    public float GetDefaultBalance()
    {
        float balance = 0;
        return balance = defaultBalance.money;
    }

    /**
    * <summary> Updates the currentbalance text and also tweens it
    */
    public void SetCurrentBalance(float balance)
    {
        defaultBalance.money = balance;
        balanceText.text = "$" + defaultBalance.money.ToString();
        leanTween.ScaleObjects(balanceText.gameObject, textSize[1], tweenTime);
        tweenText = true;
    }
    #endregion

    #region TweenDelay Function
    /**
    * <summary> Tweens objects after a certain amount of time
    */
    void TweenDelay()
    {
        tweenTimer -= Time.deltaTime;

        if (tweenTimer <= 0)
        {
            leanTween.ScaleObjects(balanceText.gameObject, textSize[0], tweenTime);
            tweenText = false;
            tweenTimer = tweenTimerConst;
        }
    }
    #endregion

}
