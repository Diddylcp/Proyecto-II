using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTutorial : Troop
{
    // Start is called before the first frame update
    public WarriorTutorial()
    {
        stats.movSpeed = 0.75f;
        stats.health = 500;   // Vida original 700
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 100;
        stats.range = 2f;
        stats.attackSpeed = 1f;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
    }

    void Update()
    {
        if (!AmIAlive())
        {
            troopState = TroopState.DYING;
            myAnimator.SetBool("Dead", true);
            if (this.tag == "EnemyTroop") GameObject.Find("AllyEconomy").GetComponent<PlayerController>().SumMoney(stats.dropedCoins);
            else GameObject.Find("EnemyEconomy").GetComponent<PlayerController>().SumMoney(stats.dropedCoins);
            Destroy(this.gameObject, 4);
        }
    }
}
