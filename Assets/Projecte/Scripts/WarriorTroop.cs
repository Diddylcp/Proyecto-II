using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTroop : Troop
{
    private AudioSource swordAudio;
    // Start is called before the first frame update
    public WarriorTroop()
    {
        stats.movSpeed = 3f;
        stats.health = 500;   // Vida original 700
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 100;
        stats.range = 1f;
        stats.attackSpeed = 1f;
    }

    void Start()
    {
        swordAudio = GetComponent<AudioSource>();
        startHealth = stats.health;
        troopObjective = DetectClosestEnemy();
    }

    void Update()
    {
        AmIAlive();
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
        
        PathRequestManager.RequestPath((Vector2)transform.position, (Vector2)troopObjective.transform.position, OnPathFound);
       // barraVida.transform.forward = cam.transform.forward;
    }

    
}
