﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTroop : Troop
{
    public ParticleSystem MageTowerFX;

    public bool areaAttack = false;
    // Start is called before the first frame update
    public MageTroop()
    {
        stats.movSpeed = 1.35f;
        stats.health = 290;
        stats.area = 2f;
        stats.residualDamage = 0;
        stats.damage = 80;
        stats.range = 1.5f;
        stats.attackSpeed = 2.0f;
        stats.dropedCoins = 70;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;
        SearchMageTower();
    }

    void SearchMageTower()
    {
        if (CompareTag("AllyTroop"))
        {
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("AllyTower"))
            {
                if (tower.GetComponent<TowerScript>().type == TowerType.MAGE_TOWER)
                {
                    areaAttack = true;
                    
                    break;
                }
            }
        }
        else if (CompareTag("EnemyTroop"))
        {
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("EnemyTower"))
            {
                if (tower.GetComponent<TowerScript>().type == TowerType.MAGE_TOWER)
                {
                    areaAttack = true;
                    break;
                }
            }
        }

        if (areaAttack)
        {
            MageTowerFX = Instantiate(MageTowerFX, this.transform);

        }
    }
}
