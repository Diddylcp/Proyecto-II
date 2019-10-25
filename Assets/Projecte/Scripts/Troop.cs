using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    [SerializeField]
    public enum troopType {MAGE, ARCHER, WARRIOR, PRIEST, COUNT};
    public struct ability
    {
        troopType tipus;
        public int health;
        public float movSpeed;
        public float area;
        public int residualDamage;
        public int damage;
        public int range;
        public float attackSpeed;
        ability(troopType tipo)
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
                    range = 8;
                    attackSpeed = 0.8f;
                    break;
                case troopType.ARCHER:
                    movSpeed = 1f;
                    health = 350;
                    area = 1f;
                    residualDamage = 0;
                    damage = 20;
                    range = 9;
                    attackSpeed = 2f;
                    break;
                case troopType.WARRIOR:
                    movSpeed = 0.75f;
                    health = 700;
                    area = 1f;
                    residualDamage = 0;
                    damage = 30;
                    range = 3;
                    attackSpeed = 1f;
                    break;
                case troopType.PRIEST:
                    movSpeed = 1f;
                    health = 450;
                    area = 1f;
                    residualDamage = 0;
                    damage = 20;
                    range = 10;
                    attackSpeed = 0.5f;
                    break;
                default:
                    movSpeed = 0f;
                    health = 0;
                    area = 0f;
                    residualDamage = 0;
                    damage = 0;
                    range = 0;
                    attackSpeed = 0f;
                    break;

            }
        }
    };
    public Vector2 pos;
    public string team;
    public ability stats;
    //public GameObject player;
    public GameObject troopObjective;
    public Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        tag = "AllyTower";
        team = tag;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        troopObjective = null;

    }

    // Update is called once per frame
    void Update()
    {
        troopObjective = DetectClosestTower();
        FindPath(troopObjective);
    }

    public GameObject DetectClosestTower()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("EnemyTower");
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
        Vector2 dir;
        dir.x = objective.transform.position.x - pos.x;
        dir.y = objective.transform.position.y - pos.y;
        dir.x *= 0.2f;//stats.movSpeed;
        dir.y *= 0.2f;//stats.movSpeed;
        Debug.Log(dir.ToString());
        rb2D.AddForce(dir , ForceMode2D.Impulse);
    }

    private void AnyoneToAttack()
    {

    }

    private bool StillInRange(GameObject objective)
    {
        bool inRange;
        if ((pos.x - objective.transform.position.x) < stats.range && (pos.y - objective.transform.position.y) < stats.range)
            inRange = true;
       else inRange = false;
        return inRange;
    }

    private void AttackEnemy()
    {

    }

    private void AmIAlive()
    {

    }

    private void CapturingTower(GameObject tower)
    {

    }
}
