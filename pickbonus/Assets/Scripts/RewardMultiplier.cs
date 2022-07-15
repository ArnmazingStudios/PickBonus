using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 *<summary> Arn Mesa 06-24-2022
 */

public class RewardMultiplier : MonoBehaviour
{
    [SerializeField] Sprite pooperImage;
    [SerializeField] Sprite rewardImage;
    [SerializeField] GameObject[] chestButtons;
    [SerializeField] float CurrentBalance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float RewardCalculator()
    {
       return 0.0f;
    }

    public Sprite GetPooperImage()
    {
        return pooperImage;
    }


}
