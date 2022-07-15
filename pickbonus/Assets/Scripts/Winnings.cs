using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 *<summary> Arn Mesa 06-24-2022
 */
public class Winnings : MonoBehaviour
{
    #region Private Variables
    [SerializeField] private Text winningText;
    [SerializeField] private float winningAmount;
    [SerializeField] private GameObject winDisplayObject;
    [SerializeField] private float delayTimer;
    [SerializeField] private float tweenTimer;
    [SerializeField] private const float tweenTimerConst = 0.5f;
    [SerializeField] private const float delayTimerConst = 1.3f;
    [SerializeField] private bool tweenText;
    [SerializeField] private Vector3[] textSize;
    [SerializeField] private float tweenTime;
    private LeanTweenManager leanTween = new LeanTweenManager();
    #endregion

    void Start()
    {
        tweenTimer = tweenTimerConst;
        delayTimer = delayTimerConst;
    }

    void Update()
    {

        if(tweenText)
        {
            TweenDelay();
        }
    }

    #region Setter and Getter Functions
    /**
     *<summary> Functions that can access and set private Variabels
     */

    public Text GetWinningText()
    {
        return winningText;
    }

    /**
     * <summary> Sets the winning text to the winning amount
     */
    public void SetWinningText(float amount)
    {
        winningAmount += amount;
        winningText.text = "$" + winningAmount.ToString();
        leanTween.ScaleObjects(winningText.gameObject, textSize[1], tweenTime);
        tweenText = true;
        Debug.LogError("Called ");
    }

    public float GetWinningAmount()
    {
        return winningAmount;
    }

    public void SetWinningAmount(float value)
    {
        winningAmount += value;
    }

    public GameObject GetWinningDisplay()
    {
        return winDisplayObject;
    }
    #endregion


    #region ResetWinningAmount Funtion
    /**
     *<summary> Reset Texts to default amount and tweens text gameObject
     */
    public void ResetWinningAmount(float amount)
    {
        winningAmount = amount;
        winningText.text = "$" + amount.ToString();
        leanTween.ScaleObjects(winningText.gameObject, textSize[1], tweenTime);
        tweenText = true;
    }
    #endregion

    #region TweenDelay Function
    /**
     *<summary> Tweens GameObject Scaling back to its default size
     */
    void TweenDelay()
    {
        tweenTimer -= Time.deltaTime;

        if (tweenTimer <= 0)
        {
            leanTween.ScaleObjects(winningText.gameObject, textSize[0], tweenTime);
            tweenText = false;
            tweenTimer = tweenTimerConst;
        }
    }
    #endregion





}
