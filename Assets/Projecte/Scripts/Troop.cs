using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    [SerializeField]
    public enum troopType {MAGE, ARCHER, WARRIOR, PRIEST, COUNT};
    public int health;
    public float movSpeed;
    public Vector3 pos;
    public int team;
    public struct ability
    {
        public float area;
        public int residualDamage;
        public int damage;
        public int range;
        public float attackSpeed;
    };
    public ability attackEfect;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
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
            Vector3 diff = go.transform.position - pos;
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

    }

    private bool StillInRange(GameObject objective)
    {
        bool inRange;
      //  if ((pos.x - objective.pos.x) < attackEfect.range && (pos.y - objective.pos.y) < attackEfect.range)
            inRange = true;
       // else inRange = false;
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
