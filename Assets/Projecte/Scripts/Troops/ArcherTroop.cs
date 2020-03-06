using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTroop : Troop
{
    // Start is called before the first frame update
    public ArcherTroop()
    {
        stats.movSpeed = 1.4f;
        stats.health = 350; // Vida original 350
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 45;
        stats.range = 1.5f;
        stats.attackSpeed = 0.75f;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
        troopObjective = DetectClosestEnemy();
    }
}
