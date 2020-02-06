using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTroop : Troop
{
    // Start is called before the first frame update
    public MageTroop()
    {
        stats.movSpeed = 1.25f;
        stats.health = 400;
        stats.area = 2f;
        stats.residualDamage = 0;
        stats.damage = 150;
        stats.range = 3f;
        stats.attackSpeed = 0.8f;
    }

    void Start()
    {
        startHealth = stats.health;
    }

    void Update()
    {
        PathRequestManager.RequestPath((Vector2)transform.position, (Vector2)troopObjective.transform.position, OnPathFound);
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
