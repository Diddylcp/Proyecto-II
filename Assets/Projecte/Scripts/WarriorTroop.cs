using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTroop : Troop
{
    private AudioSource swordAudio;
    // Start is called before the first frame update
    public WarriorTroop()
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
        swordAudio = GetComponent<AudioSource>();
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
                agent.isStopped = false;
                troopObjective = DetectClosestEnemy();            // While not attacking, finds the nearest enemy
                FindPath(troopObjective);                           // Moves towards the closest enemy
            }
        }
        AmIAlive();
        barraVida.transform.forward = cam.transform.forward;
    }

    
}
