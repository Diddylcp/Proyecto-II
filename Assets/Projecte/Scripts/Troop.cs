﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Troop : MonoBehaviour
{
   
   // public enum troopType {MAGE, ARCHER, WARRIOR, PRIEST, COUNT};
   // public troopType tipus;
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
    [SerializeField]protected float startHealth;
    public Vector3 pos;
    public string team;
    //public GameObject player;
    public ability stats;
    public GameObject troopObjective;
    protected Rigidbody2D rb2D;
    public NavMeshAgent agent;
    [SerializeField] private Material MaterialTropaEnemigo;
    [SerializeField] private Material MaterialTropaAliado;
    public Transform barraVida;
    public Transform barraVidaFill;
    public GameObject projectile;

    public ProjectileMovement projectileAttack;
    protected Transform cam;
    
    // Start is called before the first frame update
    void Awake()
    {      
        
        pos = transform.position;
        team = tag;
        if(tag == "EnemyTroop") this.GetComponent<MeshRenderer>().material = MaterialTropaEnemigo;
        else this.GetComponent<MeshRenderer>().material = MaterialTropaAliado;
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

    protected void FindPath( GameObject _troopObjective)  // Move the troop to the objective
    {
        agent.SetDestination(_troopObjective.transform.position);
    }


    protected bool StillInRange(GameObject objective)     // Checks if troop is still in range of the enemy
    {
        float distance;
        distance = Vector3.Distance(pos, objective.transform.position);
        return (Mathf.Abs(distance) < stats.range);
    }

    protected void AttackEnemy(GameObject enemy)          // Attacks the enemy
    {
        enemy.GetComponent<Troop>().TakeDamage(stats.damage);
    }

    protected void AttackTower(GameObject tower)          // Attacks the tower
    {
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
            agent.isStopped = true;                             // Stops the troop because he is attacking
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
                    FindPath(troopObjective);
                }
            } 
        }
        yield return new WaitForSeconds(this.stats.attackSpeed);
        StartCoroutine(Attack());
    }

    protected void ShootProjectile()
    {
        Vector3 vectorToEnemy = troopObjective.transform.position - this.pos;
        GameObject projectileSpawned = Instantiate(projectile, this.transform.position, Quaternion.LookRotation(vectorToEnemy)) as GameObject;
    }
}
