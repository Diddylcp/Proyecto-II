using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int userMoney;

    public void SumMoney(int moneyToSum)
    {
        userMoney += moneyToSum;
    }
    public int GetMoney()
    {
        return userMoney;
    }
}
