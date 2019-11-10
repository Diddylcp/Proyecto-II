using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    [SerializeField]
    public enum troopType {MAGE, ARCHER, WARRIOR, PRIEST, COUNT};
    public struct ability
    {
        public troopType tipus;
        public int health;
        public float movSpeed;
        public float area;
        public int residualDamage;
        public int damage;
        public float range;
        public float attackSpeed;
        public void SetStats(troopType tipo)
        {
            tipus = tipo;
            switch (tipus)
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
    [SerializeField] private Material MaterialTropaEnemigo;
    [SerializeField] private Material MaterialTropaAliado;
    
    // Start is called before the first frame update
    void Start()
    {
        stats.tipus = troopType.ARCHER;
        stats.SetStats(stats.tipus);
        pos = transform.position;
        tag = "AllyTroop";
        team = tag;
        if(tag == "EnemyTroop") this.GetComponent<MeshRenderer>().material = MaterialTropaEnemigo;
        else this.GetComponent<MeshRenderer>().material = MaterialTropaAliado;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        troopObjective = null;

    }

    // Update is called once per frame
    void Update()
    {
        troopObjective = DetectClosestEnemy();
        Debug.Log(StillInRange(troopObjective));
        if (StillInRange(troopObjective))
        {
            if (troopObjective.GetComponent<Troop>() != null)
            {
                AttackEnemy(troopObjective);
            }
            else if (troopObjective.GetComponent<TowerScript>() != null)
            {
                //Debug.Log("AtacandoTorre");
                AttackTower(troopObjective);
            }
            //Debug.Log("En RANGO" + this.name);
        }
        else
        {
            //FindPath(troopObjective);
        }
        Debug.Log(stats.tipus);
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
    
    public void FindPath(GameObject objective)
    {

    //     Vector2 dir = objective.pos - pos;
    }

    private void AnyoneToAttack()
    {
       /*
        Vector2 dir;
        dir.x = objective.transform.position.x - pos.x;
        dir.y = objective.transform.position.y - pos.y;
        dir.x *= 0.2f;//stats.movSpeed;
        dir.y *= 0.2f;//stats.movSpeed;
        
        rb2D.AddForce(dir , ForceMode2D.Impulse);
        */
    }
    

    private bool StillInRange(GameObject objective)
    {
        bool inRange;
        if (Mathf.Abs(pos.x - objective.transform.position.x) < stats.range && Mathf.Abs(pos.y - objective.transform.position.y) < stats.range)
            inRange = true;
       else inRange = false;
       return inRange;
    }

    private void AttackEnemy(GameObject enemy)
    {
        enemy.GetComponent<Troop>().TakeDamage(stats.damage);       
    }

    private void AttackTower(GameObject tower)
    {
        tower.GetComponent<TowerScript>().TakeDamage(stats.damage);
    }

    private void AmIAlive()
    {
        if(stats.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void CapturingTower(GameObject _tower)
    {

    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        Debug.Log("DAMAGEAO");
    } 
}
