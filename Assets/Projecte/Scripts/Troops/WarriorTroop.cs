using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTroop : Troop
{
    // Start is called before the first frame update
    public WarriorTroop()
    {
        stats.movSpeed = 1f;
        stats.health = 350;   // Vida original 700
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 50;
        stats.range = 0.5f;
        stats.attackSpeed = 1f;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
    }
}
