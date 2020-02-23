using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTroop : Troop
{
    // Start is called before the first frame update
    public WarriorTroop()
    {
        stats.movSpeed = 3f;
        stats.health = 500;   // Vida original 700
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 100;
        stats.range = 1f;
        stats.attackSpeed = 0.5f;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
    }
}
