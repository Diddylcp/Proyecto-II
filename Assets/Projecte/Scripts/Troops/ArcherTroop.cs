using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTroop : Troop
{
    // Start is called before the first frame update
    public ArcherTroop()
    {
        stats.movSpeed = 2f;
        stats.health = 350; // Vida original 350
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 70;
        stats.range = 2.2f;
        stats.attackSpeed = 0.75f;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
        troopObjective = DetectClosestEnemy();
    }
}
