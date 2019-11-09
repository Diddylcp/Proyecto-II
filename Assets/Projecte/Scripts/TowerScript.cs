using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    public enum TowerType { NORMAL, MAGE_TOWER, ARCHER_TOWER, WARRIOR_TOWER, GOLD_TOWER, SPEED_TOWER, COUNT};
    public struct TowerStats
    {
        public int health;
        public float area;
        public int damage;
        public int range;
        public float attackSpeed;
        public int moneyPerSecond;
        public void SetStats(TowerType tipo)
        {
            switch (tipo)
            {
                case TowerType.NORMAL:
                    health = 1500;
                    damage = 30;
                    attackSpeed = 1.6f;
                    range = 12;
                    moneyPerSecond = 10;
                    area = 0;
                    break;

                case TowerType.MAGE_TOWER:
                    health = 1800;
                    damage = 40;
                    attackSpeed = 1.6f;
                    range = 12;
                    moneyPerSecond = 10;
                    area = 2;
                    break;
                case TowerType.ARCHER_TOWER:
                    health = 1800;
                    damage = 30;
                    attackSpeed = 2.1f;
                    range = 16;
                    moneyPerSecond = 10;
                    area = 0;
                    break;

                case TowerType.GOLD_TOWER:
                    health = 1800;
                    damage = 30;
                    attackSpeed = 1.6f;
                    range = 12;
                    moneyPerSecond = 15;
                    area = 0;
                    break;

                case TowerType.SPEED_TOWER:
                    health = 1800;
                    damage = 30;
                    attackSpeed = 2f;
                    range = 12;
                    moneyPerSecond = 10;
                    area = 0;
                    break;

                case TowerType.WARRIOR_TOWER:
                    health = 2000;
                    damage = 30;
                    attackSpeed = 1.6f;
                    range = 12;
                    moneyPerSecond = 10;
                    area = 0;
                    break;
            }
        }
    };
    [SerializeField] private Material MaterialEnemigo;
    [SerializeField] private Material MaterialAliado;

    public TowerType type;
    public Vector2 pos;
    public float speed;
    public TowerStats stats;
    private GameObject towerObjective;
    [SerializeField] private string team;
    // Start is called before the first frame update
    void Start()
    {
        type = TowerType.NORMAL;
        team = this.tag;
        stats.SetStats(type);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private GameObject AnyoneToAttack()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("AllyTower");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        pos = transform.position;
        foreach (GameObject go in gos)
        {
            Vector2 diff;
            diff.x = go.transform.position.x - pos.x;
            diff.y = go.transform.position.y - pos.y;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void AttackEnemy(GameObject objective)
    {
        objective.GetComponent<Troop>().TakeDamage(stats.damage);
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            ChangeTeam();
            Debug.Log("Changed team");
        }
            Debug.Log("DAMAGEAO - " + stats.health);
    }

    public void ChangeTeam()
    {
        
            if(team == "AllyTower")
            {
                this.tag = "EnemyTower";
                this.GetComponent<MeshRenderer>().material = MaterialEnemigo;
            }
            else if(team == "EnemyTower")
            {
                this.tag = "AllyTower";
                this.GetComponent<MeshRenderer>().material = MaterialAliado;
            }
            team = this.tag;
        
    }
}
