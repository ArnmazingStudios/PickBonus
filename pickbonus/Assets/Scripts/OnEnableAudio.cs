using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableAudio : MonoBehaviour
{
    #region PlayMode Enum
    /**
     * <summary> Allow easy way to change from multiple clip to single
     */
    public enum PlayMode
    {
        playSingleClip = 0,
        playRandomClips = 1
    }
    #endregion

    #region Private variables
    private AudioHandler audioHandler = new AudioHandler();
    [SerializeField] private PlayMode playMode;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private float volume;
    [SerializeField] float timer;
    #endregion


    /**
     *<summary> Calls DelayAudio Function
     */
    void Update()
    {
        DelayAudio();
    }


    #region AudioMode function
    /**
     * <summary>Picks which PlayAudioOneShot funciton to call
     * Depending on what enum is picked
    */
    void AudioMode()
    {
        if (playMode == PlayMode.playSingleClip)
        {
            audioHandler.PlayAudioOneShot(audioClip[0], audioSource, volume);
        }
        else if(playMode == PlayMode.playRandomClips)
        {
            audioHandler.PlayAudioRandomOneShot(audioClip, audioSource, volume);
        }
    }
    #endregion

    #region DelayAudio Function
    /**
     *<summary>Timer function that calls AudioMode whenver timer is zero
     */
    void DelayAudio()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            AudioMode();
            this.enabled = false;
        }
    }
    #endregion
}
