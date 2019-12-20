using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Troop
{ 
    public Warrior()
    {
        stats.movSpeed = 0.75f;
        stats.health = 500;   // Vida original 700
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 100;
        stats.range = 2f;
        stats.attackSpeed = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //stats.SetStats(tipus);
        startHealth = stats.health;
        pos = transform.position;
        team = tag;
        if (tag == "EnemyTroop") this.GetComponent<MeshRenderer>().material = MaterialTropaEnemigo;
        else this.GetComponent<MeshRenderer>().material = MaterialTropaAliado;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        troopObjective = DetectClosestEnemy();
        StartCoroutine(Attack());

        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.health > startHealth) stats.health = startHealth;
        if (troopObjective == null)
        {
            troopObjective = DetectClosestEnemy();
        }
        else
        {
            if (!StillInRange(troopObjective))
            {
                agent.isStopped = false;
                troopObjective = DetectClosestEnemy();            // While not attacking, finds the nearest enemy
                FindPath(troopObjective);                           // Moves towards the closest enemy
            }
        }
        AmIAlive();
        barraVida.transform.forward = cam.transform.forward;
    }
}
