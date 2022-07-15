using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public static Split split;

    private void Awake()
    {
        if (split != null && split != this)
        {
            Destroy(gameObject);
        }
        else
        {
            split = this;
        }
    }

    #region Private Variables 
    [SerializeField] private GameManger gameManager;
    [SerializeField] private float winningBalance;
    [SerializeField] private int splitNumber;
    [SerializeField] private List<float> winningValues = new List<float>();
    [SerializeField] private int distributeNumber;
    [SerializeField] private float[] splitArray;
    [SerializeField] private double[] checkArray;
    [SerializeField] private bool recalculate;
    [SerializeField] private bool recalcDone;
    [SerializeField] private bool winningChecked;
    int x = 0;
    #endregion
    public bool calcSplit;

    //[SerializeField] bool winValuesDetermined;
    // Start is called before the first frame update

    private void Start()
    {

    }    

    public void SplitWinnings()
    {
        if(calcSplit)
        {
            Debug.Log("Calculating Split");
            if(winningValues.Count != 0)
            {
                winningValues.Clear();
            }
            SplitFunciton();
        }
        calcSplit = false;
        if(recalculate)
        {
            Debug.Log("ReCalculating Split");
            SplitFunciton();
        }
        //Debug.Log("Before Check");
        CheckWinning();
    }

    #region Split Function
    /**
     * <summary> Function that splits winning balance between 1 and 9
     * 
     */
    void SplitFunciton()
    {
        /**
         * <summary> If winningBalance % Splitnumber == 0 
         * then the minimum difference is 0 and all numbers are winningBalance / Splitnumber
         */
        if (GetWinBalance() > 0)
        {
            splitNumber = Random.Range(1,9);
            //Debug.LogError("Splitnum Version" + x + ":" + splitNumber);
            if (winningBalance % splitNumber == 0)
            {
                for(int i=0;i<splitNumber;i++)
                {
                    Debug.Log((" Winning and Split Divided " + winningBalance/splitNumber)+" ");
                    if(splitArray[i] <= 0)
                    {
                        splitArray[i] = winningBalance/splitNumber;
                    }
                }
            }
            else
            {
                /** 
                 * <summary> upto winninBalance - (winninBalance % SplitNumber) the values
                 * will be winninBalance / SplitNumber
                 * after that the values
                 * will be winninBalance / SplitNumber + 1
                 */
                float a = splitNumber - (winningBalance % splitNumber);
                float b = winningBalance/splitNumber;
                for(int i=0; i < splitNumber;i++)
                {
                    if(i >= a)
                    {
                        if(splitArray[i] <= 0)
                        {
                            splitArray[i] = b;
                        }
                        
                    }
                    else
                    {
                        if(splitArray[i] <= 0)
                        {
                            splitArray[i] = b;
                        }
                    }
                }
            }
            for(int i = 0; i < splitArray.Length; i++ )
            {
                //Debug.Log(x + " Split Array Values " + splitArray[i]);
                checkArray[i] = System.Math.Round((double)splitArray[i], 2);
                splitArray[i] = 0.0f; 
            }
            x++;
        }
    }
    #endregion

    #region CheckWinning Function
    /**
     * <summary> Checks if the divided winning balance is an increment of at least 0.05
     * if not then recalculate is true then loops to check again 
     * until it finds a number that is divisible by at least 0.05
     */
    void CheckWinning()
    {
        for(int i = 0; i < checkArray.Length; i++)
        {
            //Debug.Log("Remainder 1: " + System.Math.Round(dblWinnings[i] % 0.05, 2) + ", element " + i);
            double roundedWinnings = System.Math.Round(checkArray[i] % 0.05, 2);
            if(roundedWinnings < 0.05 && roundedWinnings != 0)
            {
                //Debug.Log("Checking");
                //Debug.Log("Recalculating Element: " + i + " Value: " +roundedWinnings);
                recalculate = true;
                break;
            }
            else
            {
                //Debug.Log("Recalculating Done");
                //Debug.Log("Element: " + i + " Value: " + roundedWinnings + " is good");
                if (recalculate)
                {
                    Mathf.Floor((float)checkArray[i]);
                    SetWinningValues();
                    recalculate = false;
                    recalcDone = true;
                    break;
                }

            }
        }
    }
    #endregion

    #region Setter and Getter Functions
    public float GetDistNumber()
    {
        distributeNumber = splitNumber;
        return distributeNumber;
    }

    void SetWinningValues()
    {
        for(int i = 0; i < checkArray.Length; i++)
        {
            if(checkArray[i] != 0)
            {
                winningValues.Add((float)checkArray[i]);
            }
        }
    }

    void SetZeroPooper()
    {
        winningValues.Add(0.0f);
    }

    public List<float> GetWinningValues()
    {
        return winningValues;
    }

    public float GetWinBalance()
    {
        return winningBalance;
    }

    public void SetWinBalance(float value)
    {
        winningBalance = value;
    }

    public bool RecalcDone()
    {
        return recalcDone;
    }

    public void SetRecalc(bool condition)
    {
        recalcDone = condition;
    }
    #endregion 

    /**
     * Clears the list winningValues Whenever pooper is found
     * Called in GameManager ResetCalc function
     */
    public void ClearValues()
    {
        winningValues.Clear();
    }

    /**
     * <summary> Unused Code
    void OnEnable()
    {

         <summary> Unused Code
         Debug.Log("Rounded " + System.Math.Round(1.445, 2));
           SplitFunciton();
           if(winningValues.Count != 0)
           {
                winningValues.Clear();
            }
            SplitFunciton();

    }
    void Update()
    {

        <summary> Unused Code
        CheckWinning();
        if(recalculate)
        {
            SplitFunciton();
        }
        else
        {
            SetWinningValues();
            gameObject.SetActive(false);

        }

    }
    */


}
