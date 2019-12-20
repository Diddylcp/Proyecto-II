using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Troop
{
    public Archer()
    {
        stats.movSpeed = 1f;
        stats.health = 350; // Vida original 350
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 70;
        stats.range = 4f;
        stats.attackSpeed = 0.5f;
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
