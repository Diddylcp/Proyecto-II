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
        //base.Start();
        pathRequest = new GraphPathfinder();
        swordAudio = GetComponent<AudioSource>();
        startHealth = stats.health;
        troopObjective = DetectClosestEnemy();
        currNode = findClosestNode();
    }

    void Update()
    {
       switch(troopState)
       {
            case TroopState.INIT:
                if (pathRequest.findPath(currNode, towerToMove) && !StillInRange(troopObjective))   
                     troopState = TroopState.MOVING;
                else if (troopObjective != null)
                     if(StillInRange(troopObjective))
                        troopState = TroopState.ATTACKING;

                break;

            case TroopState.MOVING:
                if (!AmIAlive())
                {
                    isMoving = false;
                    StopCoroutine(FollowPath());
                    troopState = TroopState.DYING;
                }
                else if (StillInRange(troopObjective))
                {
                    isMoving = false;
                    StopCoroutine(FollowPath());
                    troopState = TroopState.ATTACKING;
                }
                else
                {
                    if (!isMoving)
                        StartCoroutine(FollowPath());
                    else
                    {
                        
                    }
                }

                break;

            case TroopState.ATTACKING:
                if (!AmIAlive())
                {
                    isAttacking = false;
                    StopCoroutine(Attack());
                    troopState = TroopState.DYING;
                }
                else if (!StillInRange(troopObjective))
                {
                    isAttacking = false;
                    StopCoroutine(Attack());
                    troopState = TroopState.MOVING;
                }
                else
                {

                }

                break;

            case TroopState.DYING:
                StopAllCoroutines();
                Destroy(this.gameObject);
                break;
       }
        currNode = findClosestNode();
        /*
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
        }*/
        //barraVida.transform.forward = cam.transform.forward;
        
    }   
}
