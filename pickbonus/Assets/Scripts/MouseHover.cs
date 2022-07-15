using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 *<summary>Arn Mesa
 * 6/24/2022
 */

public class MouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioClip[] clickClips;
    [SerializeField] float volume;
    AudioHandler audioHandler = new AudioHandler();
    //S Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region EventSystem Functions
    /**
     * <summary> Tracks mouse hovers, clicks 
     * Plays an audio when mouse hovers or clicks
     */
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        audioHandler.PlayAudioRandomOneShot(audioClips, audioSource, volume);
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        audioHandler.PlayAudioRandomOneShot(audioClips, audioSource, volume);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        audioHandler.PlayAudioRandomOneShot(clickClips, audioSource, volume);
    }
    #endregion
}
