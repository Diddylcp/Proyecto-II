using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTroop : Troop
{
    // Start is called before the first frame update
    public MageTroop()
    {
        stats.movSpeed = 1.35f;
        stats.health = 400;
        stats.area = 2f;
        stats.residualDamage = 0;
        stats.damage = 150;
        stats.range = 2f;
        stats.attackSpeed = 1.75f;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
    }
}
