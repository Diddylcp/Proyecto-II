using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Troop : MonoBehaviour
{
    protected enum TroopState { INIT, MOVING, ATTACKING, DYING, COUNT};
    public struct ability
    {
        [SerializeField]public float health;
        public float movSpeed;
        public float area;
        public int residualDamage;
        public int damage;
        public float range;
        public float attackSpeed;
    };
    protected TroopState troopState;
    [SerializeField]protected float startHealth;
    public Vector2 pos;
    public string team;
    public ability stats;
    public GameObject troopObjective;
    protected MyNode towerToMove;
    public Transform barraVida;
    public Transform barraVidaFill;
    public GameObject projectile;
    protected GraphPathfinder pathRequest;
    protected MyNode currNode;

    protected bool isAttacking = false, isMoving = false;

    int targetIndex = 0;

    protected Transform cam;
    
    protected void Start()
    {
        troopState = TroopState.INIT;
        pos = transform.position;
        team = tag;
        troopObjective = DetectClosestEnemy();
        troopObjective = DetectClosestEnemy();
        StartCoroutine(Attack());
        cam = Camera.main.transform;
    }

    public MyNode findClosestNode()
    {
        GameObject[] gosNodes;
        gosNodes = GameObject.FindGameObjectsWithTag("Node");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach(GameObject go in gosNodes)
        {
            float currDistance = Vector2.Distance(this.transform.position, go.transform.position);
            if(currDistance < distance)
            {
                closest = go;
                distance = currDistance;
            }
        }
        return closest.GetComponent<MyNode>();
    }

    public GameObject DetectClosestEnemy()
    {
        GameObject[] gosTroops, gosTower;
        if (tag == "AllyTroop")
        {
            gosTower = GameObject.FindGameObjectsWithTag("EnemyTower");
            gosTroops = GameObject.FindGameObjectsWithTag("EnemyTroop");    
        }
        else
        {
            gosTower = GameObject.FindGameObjectsWithTag("AllyTower");
            gosTroops = GameObject.FindGameObjectsWithTag("AllyTroop");
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        pos = this.transform.position;
        foreach(GameObject go in gosTroops)
        {
            float curDistance = Vector3.Distance(pos, go.transform.position);
            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        foreach (GameObject go in gosTower)
        {
            float curDistance = Vector3.Distance(pos, go.transform.position);
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                towerToMove = go.GetComponentInChildren<MyNode>();
            }
        }
        return closest;
    }

    protected bool StillInRange(GameObject objective)     // Checks if troop is still in range of the enemy
    {
        float distance;
        distance = Vector2.Distance(pos, (Vector2)objective.transform.position);
        return (distance < stats.range);
    }

    protected void AttackEnemy(GameObject enemy)          // Attacks the enemy
    {
        enemy.GetComponent<Troop>().TakeDamage(stats.damage);
    }

    protected void AttackTower(GameObject tower)          // Attacks the tower
    {
        Debug.Log("Atacando");
        tower.GetComponent<TowerScript>().TakeDamage(stats.damage);
        
    }

    protected bool AmIAlive()                             // Checks if he is alive
    {
        if(stats.health <= 0)
        {
            return false;
        }
        return true;
    }

    protected void CapturingTower(GameObject _tower)      // Captures the tower
    {

    }

    public void TakeDamage(int _damage)
    {
        stats.health -= _damage;
        barraVidaFill.localScale = new Vector2(stats.health / startHealth, barraVidaFill.localScale.y);
    }

    public Quaternion vectorRotation(Vector3 thisPos, Vector3 targetPos)
    {
        return Quaternion.LookRotation(targetPos) * Quaternion.Inverse(Quaternion.LookRotation(thisPos));
    }

    protected IEnumerator Attack()
    {
        if (StillInRange(troopObjective))
        {
            if (troopObjective.GetComponent<Troop>() != null)
            {
                AttackEnemy(troopObjective);
                ShootProjectile();
            }
            else if (troopObjective.GetComponent<TowerScript>() != null)
            {
                AttackTower(troopObjective);
                ShootProjectile();
                if ((this.tag == "AllyTroop" && troopObjective.GetComponent<TowerScript>().tag == "AllyTower") || (this.tag == "EnemyTroop" && troopObjective.GetComponent<TowerScript>().tag == "EnemyTower"))
                {
                    troopObjective = DetectClosestEnemy();
                }
            } 
        }
        yield return new WaitForSeconds(this.stats.attackSpeed);
        StartCoroutine(Attack());
    }

    protected IEnumerator FollowPath()
    {
        if (targetIndex < pathRequest.waypoints.Length)
        {
            Vector2 currWaypoint = pathRequest.waypoints[targetIndex];
            if (towerToMove != null)
            {
                if (!StillInRange(troopObjective))
                {
                    if (Vector2.Distance(transform.position, currWaypoint) < 0.1f*Time.deltaTime)
                    {
                        targetIndex++;
                    }
                    transform.position = Vector2.MoveTowards((Vector2)transform.position, currWaypoint, stats.movSpeed * Time.deltaTime);
                    yield return null;
                }
            }
        }
    }

    protected void ShootProjectile()
    {
        Vector3 vectorToEnemy = (Vector2)troopObjective.transform.position - this.pos;
        GameObject projectileSpawned = Instantiate(projectile, this.transform.position, Quaternion.LookRotation(vectorToEnemy)) as GameObject;
    }

    public void OnDrawGizmos()
    {
        if (pathRequest != null)
        {
            for (int i = targetIndex; i < pathRequest.waypoints.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(pathRequest.waypoints[i], new Vector2(0.12f, 0.12f));

                if (i == targetIndex)
                {
                    Gizmos.DrawLine((Vector2)transform.position, pathRequest.waypoints[i]);
                }
                else
                {
                    Gizmos.DrawLine(pathRequest.waypoints[i - 1], pathRequest.waypoints[i]);
                }
            }
        }
    }

}
