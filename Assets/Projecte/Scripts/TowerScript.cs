using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public enum TowerType { MAGE_TOWER, ARCHER_TOWER, WARRIOR_TOWER, GOLD_TOWER, SPEED_TOWER, COUNT};
    public struct TowerStats
    {
        public int health;
        public float area;
        public int damage;
        public int range;
        public float attackSpeed;
        public int moneyPerSecond;
    };
    public Vector2 pos;
    public float speed;
    public TowerStats stats;
    private GameObject towerObjective;
    // Start is called before the first frame update
    void Start()
    {
        
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
        damage -= stats.health;
        Debug.Log("DAMAGEAO");
    }
}
