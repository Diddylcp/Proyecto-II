﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int userMoney;

    private void Start()
    {
        userMoney = 0;
        Debug.Log(userMoney);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) userMoney += 100;
    }

    public void SumMoney(int moneyToSum)
    {
        userMoney += moneyToSum;
    }
    public int GetMoney()
    {
        return userMoney;
    }
}
