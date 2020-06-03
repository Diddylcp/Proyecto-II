﻿using System.Collections;
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
        troopState = TroopState.INIT;
    }

    void Update()
    {
        if (!AmIAlive())
        {
            myAnimator.SetBool("Running", false);
            myAnimator.SetBool("Attack", false);
            myAnimator.SetBool("Dead", true);
            troopState = TroopState.DYING;
            Destroy(this.gameObject, 4);
        }
    }
}
