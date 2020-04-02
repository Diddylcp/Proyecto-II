using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTroop : Troop
{
    // Start is called before the first frame update
    public ArcherTroop()
    {
        stats.movSpeed = 2f;
        stats.health = 275; // Vida original 350
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 60;
        stats.range = 2f;
        stats.attackSpeed = 0.95f;
        stats.dropedCoins = 50;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
        troopObjective = DetectClosestEnemy();
    }
}
