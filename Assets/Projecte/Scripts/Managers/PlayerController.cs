using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int userMoney;

    bool playerWithTower = false;

    private void Start()
    {
        playerWithTower = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) userMoney += 500;
    }

    public void SumMoney(int moneyToSum)
    {
        userMoney += moneyToSum;
    }
    public int GetMoney()
    {
        return userMoney;
    }

    //Si el jugador esta mirant les compres de la torra aixo estara a true;
    public void SetPlayerWithTower(bool a)
    {
        playerWithTower = a;
    }
    public bool GetPlayerWithTower()
    {
        return playerWithTower;
    }
}
