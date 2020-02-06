using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestTroop : Troop
{
    // Start is called before the first frame update
    public PriestTroop()
    {
        stats.movSpeed = 1f;
        stats.health = 450;
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 20;
        stats.range = 10f;
        stats.attackSpeed = 0.5f;
    }

    void Start()
    {
        startHealth = stats.health;
    }

    void Update()
    {
        if (troopObjective == null)
        {
            troopObjective = DetectClosestEnemy();
        }
        else
        {
            if (!StillInRange(troopObjective))
            {
                troopObjective = DetectClosestEnemy();            // While not attacking, finds the nearest enemy
            }
        }
        AmIAlive();
        barraVida.transform.forward = cam.transform.forward;
    }
}
