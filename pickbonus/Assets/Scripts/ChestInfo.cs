using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Chest", menuName = "Pick Bonus/Chest", order = 0)]
public class ChestInfo : ScriptableObject
{
    /**
     * <summary>Scriptable Object To store all data of Chests
     */
    public float reward;
    public Sprite defaultImage;
    public Sprite pooperImage;
    public Sprite rewardImage;
    public bool isPooper = false;
}
