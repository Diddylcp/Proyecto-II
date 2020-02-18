using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Troop : MonoBehaviour
{
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
    [SerializeField]protected float startHealth;
    public Vector2 pos;
    public string team;
    //public GameObject player;
    public ability stats;
    public GameObject troopObjective;
    protected Rigidbody2D rb2D;
    //[SerializeField] private Material MaterialTropaEnemigo;
    //[SerializeField] private Material MaterialTropaAliado;
    public Transform barraVida;
    public Transform barraVidaFill;
    public GameObject projectile;

    Vector2[] path;
    int targetIndex;

    protected Transform cam;
    
    void Awake()
    {
        myAnimator = GetComponentInChildren<Animator>();
        pos = transform.position;
        team = tag;
        troopObjective = DetectClosestEnemy();
        //if(tag == "EnemyTroop") this.GetComponent<MeshRenderer>().material = MaterialTropaEnemigo;
        //else this.GetComponent<MeshRenderer>().material = MaterialTropaAliado;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        troopObjective = DetectClosestEnemy();
        StartCoroutine(Attack());
        cam = Camera.main.transform;
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
        pos = transform.position;
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
            }
        }
        return closest;
    }

    public void OnPathFound(Vector2[] newPath, bool success)
    {
        if (success)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }


    protected bool StillInRange(GameObject objective)     // Checks if troop is still in range of the enemy
    {
        float distance;
        distance = Vector3.Distance(pos, objective.transform.position);
        return (Mathf.Abs(distance) < stats.range);
    }

    protected void AttackEnemy(GameObject enemy)          // Attacks the enemy
    {
        myAnimator.SetBool("Attack", true);
        enemy.GetComponent<Troop>().TakeDamage(stats.damage);
    }

    protected void AttackTower(GameObject tower)          // Attacks the tower
    {
        myAnimator.SetBool("Attack", true);
        tower.GetComponent<TowerScript>().TakeDamage(stats.damage);
        
    }

    protected void AmIAlive()                             // Checks if he is alive
    {
        if(stats.health <= 0)
        {
            Destroy(this.gameObject);
        }
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

    IEnumerator Attack()
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
                    PathRequestManager.RequestPath((Vector2)transform.position, (Vector2)troopObjective.transform.position, OnPathFound);
                }
            } 
        }
        yield return new WaitForSeconds(this.stats.attackSpeed);
        StartCoroutine(Attack());
    }

    IEnumerator FollowPath()
    {
        Vector2 currWaypoint = path[0];

        while (!StillInRange(troopObjective))
        {
            if ((Vector2)transform.position == currWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currWaypoint = path[targetIndex];
            }
            transform.position = Vector2.MoveTowards((Vector2)transform.position, currWaypoint, stats.movSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected void ShootProjectile()
    {
        Vector3 vectorToEnemy = (Vector2)troopObjective.transform.position - this.pos;
        GameObject projectileSpawned = Instantiate(projectile, this.transform.position, Quaternion.LookRotation(vectorToEnemy)) as GameObject;
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine((Vector2)transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }

}
