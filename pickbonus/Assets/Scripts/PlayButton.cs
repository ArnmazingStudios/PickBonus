using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *<summary> Arn Mesa 06-24-2022
 */

public class PlayButton : MonoBehaviour
{
    [SerializeField] bool isPlaying;

    #region Play Function
    /**
     *<summary> Sets isPlaying to true when PlayButton is pressed
     */
    public void Play()
    {
        isPlaying = true;
    }
    #endregion

    #region Setter and Getter Functions
    public bool GetPlay()
    {
        return isPlaying;
    }

    public void SetPlay(bool condition)
    {
        isPlaying = condition;
    }
    #endregion


}
