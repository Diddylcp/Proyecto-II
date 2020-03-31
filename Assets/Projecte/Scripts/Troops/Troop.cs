using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

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
    protected Animator myAnimator;
    [SerializeField]protected TroopState troopState;
    [SerializeField]protected float startHealth;
    public string team;
    public ability stats;
    public GameObject troopObjective;
    protected MyNode towerToMove;
    [SerializeField] private Image healthBar;
    public GameObject projectile;
    protected GraphPathfinder pathRequest;
    protected MyNode currNode;

    protected bool isAttacking = false, isMoving = false;
    protected int targetIndex = 0;


    
    protected void Start()
    {
        myAnimator = GetComponentInChildren<Animator>();
        troopState = TroopState.INIT;
        VelocityEnhance(FindVelocityTower()); //Mira si existeix torre de velocitat, i aplica la millora
        team = tag;
        troopObjective = DetectClosestEnemy();
        currNode = findClosestNode();
        pathRequest = new GraphPathfinder();
        StartCoroutine(Attack());
    }

    void Update()
    {
        troopObjective = DetectClosestEnemy();
        switch (troopState)
        {
            case TroopState.INIT:
                if (pathRequest.findPath(currNode, towerToMove) && !StillInRange(troopObjective))
                {
                    troopState = TroopState.MOVING;
                    myAnimator.SetBool("Running", true);
                    myAnimator.SetBool("Attack", false);
                }
                else if (troopObjective != null)
                    if (StillInRange(troopObjective))
                    {
                        troopState = TroopState.ATTACKING;
                        myAnimator.SetBool("Running", false);
                        myAnimator.SetBool("Attack", true);
                        //StartCoroutine(Attack());
                    }
                break;

            case TroopState.MOVING:
                if (!AmIAlive())
                {
                    isMoving = false;
                    troopState = TroopState.DYING;
                }
                else if (StillInRange(troopObjective))
                {
                    isMoving = false;
                    troopState = TroopState.ATTACKING;
                    myAnimator.SetBool("Attack", true);
                    myAnimator.SetBool("Running", false);
                    targetIndex = 0;
                    //StartCoroutine(Attack());
                }
                else
                {                   
                    isMoving = true;
                    if(tag == "AllyTroop" && towerToMove.tag == "AllyTower")
                    {
                        pathRequest.findPath(currNode, towerToMove);
                        DetectClosestTower();
                    }
                    pathRequest.findPath(currNode, towerToMove);
                    FollowPath();
                }
                break;

            case TroopState.ATTACKING:
                if (!AmIAlive())
                {
                    isAttacking = false;
                    //StopCoroutine(Attack());
                    troopState = TroopState.DYING;
                }
                else if (!StillInRange(troopObjective) && pathRequest.findPath(currNode, towerToMove))
                {
                    
                    //StopCoroutine(Attack());
                    isAttacking = false;
                    myAnimator.SetBool("Running", true);
                    myAnimator.SetBool("Attack", false);
                    troopState = TroopState.MOVING;
                }
                break;

            case TroopState.DYING:
                StopAllCoroutines();
                Destroy(this.gameObject);
                break;
        }
        currNode = findClosestNode();
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
        GameObject[] gosTroops;
        if (tag == "AllyTroop")
        {
            gosTroops = GameObject.FindGameObjectsWithTag("EnemyTroop");    
        }
        else
        {
            gosTroops = GameObject.FindGameObjectsWithTag("AllyTroop");
        }
        GameObject closest = DetectClosestTower();
        float distance = Vector3.Distance(this.transform.position, closest.transform.position);
        foreach (GameObject go in gosTroops)
        {
            float curDistance = Vector3.Distance(this.transform.position, go.transform.position);
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public GameObject DetectClosestTower()
    {
        GameObject[] gosTower;
        if (tag == "AllyTroop")
        {
            gosTower = GameObject.FindGameObjectsWithTag("EnemyTower");
        }
        else
        {
            gosTower = GameObject.FindGameObjectsWithTag("AllyTower");
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gosTower)
        {
            float curDistance = Vector3.Distance(this.transform.position, go.transform.position);
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        towerToMove = closest.GetComponentInChildren<MyNode>();
        return closest;
    }

    public List<GameObject> DetectAllClosestEnemies()
    {
        
        GameObject[] gosTroops;
        GameObject[] gosTowers;
        if (tag == "AllyTroop")
        {
            gosTroops = GameObject.FindGameObjectsWithTag("EnemyTroop");
            gosTowers = GameObject.FindGameObjectsWithTag("EnemyTower");
        }
        else
        {
            gosTroops = GameObject.FindGameObjectsWithTag("AllyTroop");
            gosTowers = GameObject.FindGameObjectsWithTag("AllyTower");
        }
        List<GameObject> closest = new List<GameObject>();
        foreach (GameObject go in gosTroops)
        {
            float curDistance = Vector3.Distance(this.transform.position, go.transform.position);
            if (curDistance < stats.range)
            {
                closest.Add(go);
            }
        }
        foreach (GameObject go in gosTowers)
        {
            float curDistance = Vector3.Distance(this.transform.position, go.transform.position);
            if (curDistance < stats.range)
            {
                closest.Add(go);
            }
        }
        return closest;
    }

    protected bool StillInRange(GameObject objective)     // Checks if troop is still in range of the enemy
    {
        float distance;
        if (objective == null) return false;
        distance = Vector2.Distance(this.transform.position, objective.transform.position);
        return (distance < stats.range);
    }

    protected void AttackEnemy(GameObject enemy)          // Attacks the enemy
    {
        enemy.GetComponent<Troop>().TakeDamage(stats.damage);

        
        if (enemy.GetComponent<WarriorTroop>())
        {
            //enhance tower 20%
            if (enemy.GetComponent<WarriorTroop>().returnDamage)
                TakeDamage(enemy.GetComponent<Troop>().stats.damage * 20 / 100);
           
        }
    }

    protected void AttackTower(GameObject tower)          // Attacks the tower
    {
        if(tag == "AllyTroop" && tower.tag != "AllyTower" || tag == "EnemyTroop" && tower.tag != "EnemyTower")
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
        healthBar.fillAmount = stats.health / startHealth;
    }

    public Quaternion vectorRotation(Vector3 thisPos, Vector3 targetPos)
    {
        return Quaternion.LookRotation(targetPos) * Quaternion.Inverse(Quaternion.LookRotation(thisPos));
    }

    protected IEnumerator Attack()
    {   
        if(GetComponent<MageTroop>())
        {
            if (GetComponent<MageTroop>().areaAttack)
            {
                List<GameObject> Objectives = DetectAllClosestEnemies();
                foreach (GameObject go in Objectives)
                {
                    troopObjective = go;
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
                }
            }
            else
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
            }
        }
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
        StartCoroutine("Attack");
    }

    protected void FollowPath()
    {
        if (targetIndex < pathRequest.waypoints.Length)
        {
            Vector2 currWaypoint = pathRequest.waypoints[targetIndex];
            if (towerToMove != null)
            {
                if (!StillInRange(troopObjective))
                {
                    if (Vector2.Distance((Vector2)transform.position, currWaypoint) < 0.01f*Time.deltaTime)
                    {
                        targetIndex++;
                    }
                    transform.LookAt(currWaypoint);
                    transform.rotation = Quaternion.Euler(this.transform.eulerAngles.x, this.transform.eulerAngles.y, 90*transform.right.z);
                    transform.position = Vector2.MoveTowards(transform.position, currWaypoint, stats.movSpeed * Time.deltaTime);
                }
                else
                {
                    return;
                }
            }
        }
    }

    protected void ShootProjectile()
    {
        Vector3 vectorToEnemy = troopObjective.transform.position - this.transform.position;
        GameObject projectileSpawned = Instantiate(projectile, this.transform.position, Quaternion.LookRotation(vectorToEnemy)) as GameObject;
    }


    bool FindVelocityTower()
    {
        if (CompareTag("AllyTroop"))
        {
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("AllyTower"))
            {
                if (tower.GetComponent<TowerScript>().type == TowerType.SPEED_TOWER)
                {
                    return true;
               
                }
            }
        }
        else if (CompareTag("EnemyTroop"))
        {
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("EnemyTower"))
            {
                if (tower.GetComponent<TowerScript>().type == TowerType.SPEED_TOWER)
                {
                    return true;
                    
                }
            }
        }
        return false;
    }

    void VelocityEnhance(bool existTower)
    {
        if(existTower)
        { 
            stats.attackSpeed += stats.attackSpeed * 20 / 100;
            
        }
    }

}
