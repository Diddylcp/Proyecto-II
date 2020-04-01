using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorTroop : Troop
{
    public ParticleSystem WarriorTowerFX;
    

    public bool returnDamage = false;
    

    // Start is called before the first frame update
    public WarriorTroop()
    {
        stats.movSpeed = 1f;
        stats.health = 350;   // Vida original 700
        stats.area = 1f;
        stats.residualDamage = 0;
        stats.damage = 50;
        stats.range = 0.3f;
        stats.attackSpeed = 1f;
        stats.dropedCoins = 30;
    }

    void Start()
    {
        base.Start();
        startHealth = stats.health;

        SearchWarriorTower();
    }


    void SearchWarriorTower()
    {
        if (CompareTag("AllyTroop"))
        {
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("AllyTower"))
            {
                if (tower.GetComponent<TowerScript>().type == TowerType.WARRIOR_TOWER)
                {
                    returnDamage = true;
                    break;
                }
            }
        }
        else if (CompareTag("EnemyTroop"))
        {
            foreach (GameObject tower in GameObject.FindGameObjectsWithTag("EnemyTower"))
            {
                if (tower.GetComponent<TowerScript>().type == TowerType.WARRIOR_TOWER)
                {
                    returnDamage = true;
                    break;
                }
            }
        }

        if (returnDamage)
        {
            WarriorTowerFX = Instantiate(WarriorTowerFX, this.transform);

        }
    }
}
