using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenManager
{

    #region ScaleObject Overloaded Functions
    /**
     * <summary>Overloaded Functions that tween gameObjects
     */

    public void ScaleObjects(GameObject[] objectsToScale, Vector3 values, float tweenTime)
    {
        for (int i = 0; i < objectsToScale.Length; i++)
        {
            if (objectsToScale[i] != null && objectsToScale[i].activeInHierarchy)
            {
                LeanTween.scale(objectsToScale[i], values, tweenTime);
            }
        }
    }

    public void ScaleObjects(GameObject objectsToScale, Vector3 values, float tweenTime)
    {
        if (objectsToScale != null && objectsToScale.activeInHierarchy)
        {
            LeanTween.scale(objectsToScale, values, tweenTime);
        }
    }


    /**
     * <summary>Overloaded ScaleObject Function that tween gameObjects sequentially
     */
    public void ScaleObjects(GameObject[] objectsToScale, Vector3[] values, float tweenTime, float intervals)
    {
        var seq = LeanTween.sequence();
        seq.append(1f);
        seq.append(() => { // fire an event before start
            Debug.Log("I have started");
        });


        for (int i = 0; i < objectsToScale.Length; i++)
        {
            if (objectsToScale[i] != null && objectsToScale[i].activeInHierarchy)
            {
                seq.append(intervals);
                seq.append(() => { // fire an event before start
                    Debug.Log("I have started");
                });
                seq.append(LeanTween.scale(objectsToScale[i], values[i], tweenTime));
                seq.append(() => { // fire event after tween
                    Debug.Log("We are done now");
                }); ;
            }
        }
    }
    #endregion

    /**
 * <summary> Unused code
public void ScaleObjects(GameObject[] objectsToScale, Vector3 values, float tweenTime, bool condition)
{
    for (int i = 0; i < objectsToScale.Length; i++)
    {
        if (objectsToScale[i] != null)
        {
            if (condition)
            {
                LeanTween.scale(objectsToScale[i], values, tweenTime);
            }
        }
    }
}
*/
    /**
 * <summary> Unused Funtion
public void ScaleObjects(GameObject[] objectsToScale, Vector3[] values, float tweenTime)
{
    for (int i = 0; i < objectsToScale.Length; i++)
    {
        if (objectsToScale[i] != null && objectsToScale[i].activeInHierarchy)
        {
            LeanTween.scale(objectsToScale[i], values[i], tweenTime);
        }
    }
}
*/
}
