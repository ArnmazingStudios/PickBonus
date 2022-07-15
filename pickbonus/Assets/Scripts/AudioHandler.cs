using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler
{
    #region PlayAudioOneShot Function
    /**
     * <summary> Plays a single clip of audio
     */
    public void PlayAudioOneShot(AudioClip clip, AudioSource audioSource, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
    #endregion

    #region PlayAudioRandomOneShot Function
    /**
     * <summary>Plays a random clip inside an array of clip
     */
    public void PlayAudioRandomOneShot(AudioClip[] clip, AudioSource audioSource, float volume)
    {
        int clipNum = AudioRandomizer(0, clip.Length);

        audioSource.PlayOneShot(clip[clipNum], volume);
    }
    #endregion

    #region AudioTandomizer Function
    /**
     * <summary>Selects a random int that will be used as the element of the audio clip to play
     */
    public int AudioRandomizer(int min, int max)
    {
        int clipNum = Random.Range(min, max);

        return clipNum;
    }
    #endregion

}
