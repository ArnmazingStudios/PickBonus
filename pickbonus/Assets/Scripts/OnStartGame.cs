using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *<summary> Arn Mesa 06-24-2022
 */

public class OnStartGame : MonoBehaviour
{
    public static OnStartGame onStartGame;

    #region Private variables
    [SerializeField] private AudioClip[] clips;
    private LeanTweenManager tweenManager = new LeanTweenManager();
    [SerializeField] private GameObject[] gameObj;
    [SerializeField] private Vector3[] dimension;
    [SerializeField] private float volume;
    [SerializeField] private float onStartTimer;
    [SerializeField] private float timerSec;
    [SerializeField] private bool  delayDeactivate;
    private AudioHandler audioHandler = new AudioHandler();
    #endregion

    /**
     * <summary> singleton
     * only need one instance of OnStartGame
     */
    private void Awake()
    {
        if (onStartGame != null && onStartGame != this)
        {
            Destroy(gameObject);
        }
        else
        {
            onStartGame = this;
        }
    }


    /**
     * <summary>StartCoroutine On start
     */
    void Start()
    {
        StartCoroutine(OnStart());
    }

    void Update()
    {
        OnStartfunction();
    }

    #region Coroutine OnStart()
    /**
     * <summary> Coroutine called on Start function 
     * appropriately timing the scaling and activating of game objects
     * timing needs to happen independent of Update
     */
    IEnumerator OnStart()
    {
        tweenManager.ScaleObjects(gameObj, dimension[0], 2.0f);
        yield return new WaitForSeconds(onStartTimer);
        tweenManager.ScaleObjects(gameObj, dimension[1], 1.0f);
        timerSec = 1.0f;
        delayDeactivate = true;
        StopCoroutine(OnStart());
    }
    #endregion

    #region ToggleGameObjects Function
    /**
     * <summary> Toggles Game objects using for loop
     * reusable function
     * called in OnStartFunction
     */
    void ToggleGameObjects(GameObject[] gmeObj, int index, int length, bool condition)
    {
        for(; index <= length; index++)
        {
            gmeObj[index].SetActive(condition);
        }
    }
    #endregion

    #region ObjTimer Function
    /**
     * <summary> A timer function
     * returns a float value that represents when the timer ends
     * called in OnStartfunction to time objects toggling
     */
    float ObjTimer()
    {
        timerSec -= Time.deltaTime;

        if (timerSec <= 0)
        {
            timerSec = 0.0f;
            return 0.0f;
        }
        else
        {
            return 1.0f;
        }
    }
    #endregion

    #region OnStartFunction
    /**
     * <summary>Handles the toggling and the scaling of canvas objects
     * Called in Update and only runs when delayDeactivate is set to true
     * only need to Run once after ObjTimer returns a zero or less
     */
    void OnStartfunction()
    {
        if (delayDeactivate)
        {
            if(ObjTimer() <= 0.0f)
            {
                ToggleGameObjects(gameObj, 0, 1, false);
                ToggleGameObjects(gameObj, 2, gameObj.Length -1, true);
                StartCoroutine(ScaleCanvasObjects());
                delayDeactivate = false;
            }
        }
    }
    #endregion

    #region Coroutine ScaleCanvasObjects
    /**
     * <summary> Coroutine that appropriately scales canvas objects after 0.1 seconds
     * runs independent of Update
     * Stops Itself after scaling of objects is done
     */
    IEnumerator ScaleCanvasObjects()
    {
        yield return new WaitForSeconds(0.1f);
        tweenManager.ScaleObjects(gameObj, dimension, 0.15f, 0.2f);
        StopCoroutine(ScaleCanvasObjects());
    }
    #endregion
}
