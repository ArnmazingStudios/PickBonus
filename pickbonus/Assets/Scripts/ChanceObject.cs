using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *<summary> Arn Mesa 06-24-2022
 */
public class ChanceObject : MonoBehaviour
{
    /**
     * <summary> used for singleton
     */
    public static ChanceObject chanceObj;

    /**
     * <summary> Only need one instance of ChanceObject
     */
    private void Awake()
    {
        if (chanceObj != null && chanceObj != this)
        {
            Destroy(gameObject);
        }
        else
        {
            chanceObj = this;

        }
    }

    #region Private Variables
    private int chanceNumber;
    [SerializeField] private float moneyMultiplier = 0;
    [SerializeField] private int multiplier;
    [SerializeField] private float[] multiplierOne;
    [SerializeField] private float[] multiplierTwo;
    [SerializeField] private float[] multiplierThree;
    #endregion

    #region Test loop
    /*
    void ChanceSimulation(int LoopNumber)
    {
        for(int i = 0; i < LoopNumber; i++)
        {
            chanceNumber = Random.Range(0,101);
            if(chanceNumber >= 50)
            {
                Debug.Log("Chance 50% you won nothing");
            }
            else if(chanceNumber >= 20 && chanceNumber < 50)
            {
                int randomMult = Random.Range(1, 10);
                //multiplierOne[randomMult];
                moneyMultiplier = multiplierOne[randomMult];
                Debug.Log("Chance 15% Multiplier = " + moneyMultiplier);
                
            }
            else if(chanceNumber >= 5 && chanceNumber < 20)
            {
                 int randomMult = Random.Range(0,5);
                 moneyMultiplier = multiplierTwo[randomMult];
                 Debug.Log("Chance 15% Multiplier = " + moneyMultiplier);
            }
            else if(chanceNumber >= 0 && chanceNumber < 5)
            {
                int randomMult = Random.Range(0,4);
                moneyMultiplier = multiplierThree[randomMult];
                Debug.Log("Chance 5% Multiplier = " + moneyMultiplier);
            }
            else
            {
                 Debug.Log("chance number: " + chanceNumber + "Percentage = " + "0%");
            }
            if(i == LoopNumber)
             {
                this.gameObject.SetActive(false);
             }
        }
    }
    */
    #endregion

    #region CalculateMultiplier Function
    /**
     * <summary> Function that calculates the chances of getting the multiplier
     * Using Random.Range function with a min of 0 and a max of 100
     * to determine the  multiplier belonging to a specifc chance array.
     * Once the chance array is determined, another random number is generated 
     * It is then used to find a random value belonging to the relative array
     */
    public void CalculateMultiplier()
    {
        chanceNumber = Random.Range(1,100); 

        if(chanceNumber >= 51)
        {
            Debug.Log("Chance 50% you won nothing");
            moneyMultiplier = 0;
        }
        else if(chanceNumber >= 21 && chanceNumber < 50)
        {
            int randomMult = Random.Range(0, 10);
            //Debug.Log("Chance 30% Multiplier = " + multiplierOne);
            moneyMultiplier = multiplierOne[randomMult];
            Debug.Log("Chance 30% Multiplier = " + moneyMultiplier);
        }
        else if(chanceNumber >= 6 && chanceNumber < 20)
        {
            int randomMult = Random.Range(0,5);
            //Debug.Log("Chance 15% Multiplier = " +multiplierTwo[randomMult]);
            moneyMultiplier = multiplierTwo[randomMult];
            Debug.Log("Chance 15% Multiplier = " + moneyMultiplier);
        }
        else if(chanceNumber >= 1 && chanceNumber < 5)
        {
            int randomMult = Random.Range(0,4);
            //Debug.Log("Chance 5% Multiplier = " + multiplierThree[randomMult]);
            moneyMultiplier = multiplierThree[randomMult];
            Debug.Log("Chance 5% Multiplier = " + moneyMultiplier);
        }
        else
        {
            Debug.Log("chance number: " + chanceNumber + "Percentage = " + "0%");
        }
    }
    #endregion

    #region GetMultplier Function
    /**
     * <summary> Functions that returns the private float variable moneyMultiplier 
     */
    public float GetMultiplier()
    {
       //Debug.Log("Multiplier is " + moneyMultiplier);
        return moneyMultiplier;
    }
    #endregion

}
