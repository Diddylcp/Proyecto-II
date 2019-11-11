using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troop : MonoBehaviour
{
    public enum troopType {MAGE, ARCHER, WARRIOR, PRIEST, COUNT};
    public troopType tipus;
    public struct ability
    {
        public int health;
        public float movSpeed;
        public float area;
        public int residualDamage;
        public int damage;
        public float range;
        public float attackSpeed;
        public void SetStats(troopType _tipus)
        {
            switch (_tipus)
            {
                case troopType.MAGE:
                    movSpeed = 1.25f;
                    health = 400;
                    area = 2f;
                    residualDamage = 0;
                    damage = 60;
                    range = 8f;
                    attackSpeed = 0.8f;
                    break;
                case troopType.ARCHER:
                    movSpeed = 1f;
                    health = 350;
                    area = 1f;
                    residualDamage = 0;
                    damage = 20;
                    range = 5f;
                    attackSpeed = 2f;
                    break;
                case troopType.WARRIOR:
                    movSpeed = 0.75f;
                    health = 700;
                    area = 1f;
                    residualDamage = 0;
                    damage = 30;
                    range = 3f;
                    attackSpeed = 1f;
                    break;
                case troopType.PRIEST:
                    movSpeed = 1f;
                    health = 450;
                    area = 1f;
                    residualDamage = 0;
                    damage = 20;
                    range = 10f;
                    attackSpeed = 0.5f;
                    break;
                default:
                    movSpeed = 0f;
                    health = 0;
                    area = 0f;
                    residualDamage = 0;
                    damage = 0;
                    range = 0f;
                    attackSpeed = 0f;
                    break;

            }
        }
    };
    public Vector2 pos;
    public string team;
    //public GameObject player;
    public ability stats;
    public GameObject troopObjective;
    public Rigidbody2D rb2D;
    public NavMeshAgent agent;
    [SerializeField] private Material MaterialTropaEnemigo;
    [SerializeField] private Material MaterialTropaAliado;
    
    // Start is called before the first frame update
    void Start()
    {
        tipus = troopType.ARCHER;       // Cuando se creen las demás tropas, simplemente hace falta pasar por referencia en el boton, la tropa deseada
        stats.SetStats(tipus);
        pos = transform.position;
        team = tag;
        if(tag == "EnemyTroop") this.GetComponent<MeshRenderer>().material = MaterialTropaEnemigo;
        else this.GetComponent<MeshRenderer>().material = MaterialTropaAliado;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        troopObjective = DetectClosestEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        // Si pongo aqui el DetectClosestEnemy en cuanto haya alguien más cerca, aunque este atacando cambia de objetivo
        Debug.Log("A rango: " + StillInRange(troopObjective));
        if (StillInRange(troopObjective))
        {
            Debug.Log(troopObjective.tag);
            agent.isStopped = true;                             // Stops the troop because he is attacking
            if (troopObjective.GetComponent<Troop>() != null)
            {
                AttackEnemy(troopObjective);
            }
            else if (troopObjective.GetComponent<TowerScript>() != null)
            {
                AttackTower(troopObjective);
            }
            if ((this.tag == "AllyTroop" && troopObjective.GetComponent<TowerScript>().tag == "AllyTower") || (this.tag == "EnemyTroop" && troopObjective.GetComponent<TowerScript>().tag == "EnemyTower") ||
                (this.tag == troopObjective.GetComponent<Troop>().tag))
            {
                troopObjective = DetectClosestEnemy();
                FindPath(troopObjective);
            }
        }
        else
        {
            agent.isStopped = false;
            Debug.Log("Buscando enemigo");
            troopObjective = DetectClosestEnemy();              // While not attacking, finds the nearest enemy
            FindPath(troopObjective);                           // Moves towards the closest enemy
        }  
    }

    public GameObject DetectClosestEnemy()
    {
        GameObject[] gos;
        if(tag == "AllyTroop") gos = GameObject.FindGameObjectsWithTag("EnemyTower");
        else gos = GameObject.FindGameObjectsWithTag("AllyTower");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        pos = transform.position;
        foreach(GameObject go in gos)
        {
            Vector2 diff;
            diff.x = go.transform.position.x - pos.x;
            diff.y = go.transform.position.y - pos.y;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void FindPath( GameObject _troopObjective)  // Move the troop to the objective
    {
        agent.SetDestination(_troopObjective.transform.position);
    }


    private bool StillInRange(GameObject objective)     // Checks if troop is still in range of the enemy
    {
        bool inRange;
        if (Mathf.Abs(pos.x - objective.transform.position.x) < stats.range && Mathf.Abs(pos.y - objective.transform.position.y) < stats.range)
            inRange = true;
       else inRange = false;
       return inRange;
    }

    private void AttackEnemy(GameObject enemy)          // Attacks the enemy
    {
        enemy.GetComponent<Troop>().TakeDamage(stats.damage);       
    }

    private void AttackTower(GameObject tower)          // Attacks the tower
    {
        tower.GetComponent<TowerScript>().TakeDamage(stats.damage);
    }

    private void AmIAlive()                             // Checks if he is alive
    {
        if(stats.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void CapturingTower(GameObject _tower)      // Captures the tower
    {

    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
    } 
}
