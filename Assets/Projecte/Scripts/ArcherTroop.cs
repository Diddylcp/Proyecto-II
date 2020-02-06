using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTroop : Troop
{
    // Start is called before the first frame update
    public ArcherTroop()
    {
        stats.movSpeed = 1f;
        stats.health = 350; // Vida original 350
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 70;
        stats.range = 3f;
        stats.attackSpeed = 0.5f;
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
