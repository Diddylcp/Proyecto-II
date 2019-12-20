using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : Troop
{
    public float healPower;
    public Priest()
    {
        healPower = 20f; 
        stats.movSpeed = 1f;
        stats.health = 450f;
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 20;
        stats.range = 10f;
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

    private new GameObject DetectClosestEnemy()
    {
        GameObject[] gosTroops, gosTower;
        if (tag == "AllyTroop")
        {
            gosTower = GameObject.FindGameObjectsWithTag("EnemyTower");
            gosTroops = GameObject.FindGameObjectsWithTag("EnemyTroop");

            gosTower = GameObject.FindGameObjectsWithTag("AllyTower");
        }
        else
        {
            gosTower = GameObject.FindGameObjectsWithTag("AllyTower");
            gosTroops = GameObject.FindGameObjectsWithTag("AllyTroop");

            gosTroops = GameObject.FindGameObjectsWithTag("EnemyTroop");
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        pos = transform.position;
        foreach (GameObject go in gosTroops)
        {
            float curDistance = Vector2.Distance(pos, go.transform.position);
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        foreach (GameObject go in gosTower)
        {
            float curDistance = Vector2.Distance(pos, go.transform.position);
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void HealAlly(GameObject Ally)
    {
        if(Ally.GetComponent<Troop>().stats.health < Ally.GetComponent<Troop>().startHealth)
            Ally.GetComponent<Troop>().stats.health += healPower;
    }

    private IEnumerator Heal()
    {
        if (StillInRange(troopObjective) && troopObjective.tag == this.tag)
        {
            agent.isStopped = true;                             // Stops the troop because he is attacking
            if (troopObjective.GetComponent<Troop>() != null)
            {
                HealAlly(troopObjective);
                // Debug.Log("Tropa que cura: " + tipus + " Mi vida: " + this.stats.health);
            }
            else if (troopObjective.GetComponent<TowerScript>() != null)
            {
                AttackTower(troopObjective);
                if ((this.tag == "AllyTroop" && troopObjective.GetComponent<TowerScript>().tag == "AllyTower") || (this.tag == "EnemyTroop" && troopObjective.GetComponent<TowerScript>().tag == "EnemyTower"))
                {
                    troopObjective = DetectClosestEnemy();
                    FindPath(troopObjective);
                }
            }
        }
        yield return new WaitForSeconds(this.stats.attackSpeed);
        StartCoroutine(Heal());
    }
}
