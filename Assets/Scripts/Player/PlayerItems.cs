using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Amounts")]
    public int totalWood;
    public int carrots;
    public float currentWater;
    public int fishes;

    [Header("Limits")]
    public float waterLimit = 50;
    public float woodLimit = 4;
    public float carrotsLimit = 8;
    public float fishesLimit = 3f;
    public void WaterLimit(int water)
    {
        if (currentWater <= waterLimit)
        {
            currentWater += water;
        }

    }

    public void For() 
    {

    }
}
