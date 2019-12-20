using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Troop
{

    public Mage()
    {
        stats.movSpeed = 1.25f;
        stats.health = 400;
        stats.area = 2f;
        stats.residualDamage = 0;
        stats.damage = 150;
        stats.range = 8f;
        stats.attackSpeed = 0.8f;
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
