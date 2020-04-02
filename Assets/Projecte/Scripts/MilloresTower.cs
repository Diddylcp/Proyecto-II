using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilloresTower : MonoBehaviour
{
    public TowerScript tower;
    public Button buttonWizard;
    public Button buttonWarrior;
    public Button buttonArcher;
    public Button buttonEconomy;
    public Button buttonSpeed;

    // Si té prous diners per la millora estara Available
    bool[] buttonAvailable; // 0 = Wizard; 1 = Archer; 2 = Speed; 3 = Economy; 4 = Warrior


    // Start is called before the first frame update
    void Start()
    {
        
        buttonAvailable = new bool[5];

    }

    // Update is called once per frame
    void Update()
    {
        if (tower.player.GetMoney() >= 550)
        {
            buttonWizard.GetComponent<Image>().color = Color.white;
            buttonWarrior.GetComponent<Image>().color = Color.white;
            buttonArcher.GetComponent<Image>().color = Color.white;
            buttonAvailable[0] = true;
            buttonAvailable[1] = true;
            buttonAvailable[4] = true;
        }
        else
        {
            buttonWizard.GetComponent<Image>().color = Color.red;
            buttonWarrior.GetComponent<Image>().color = Color.red;
            buttonArcher.GetComponent<Image>().color = Color.red;
            buttonAvailable[0] = false;
            buttonAvailable[1] = false;
            buttonAvailable[4] = false;
        }

        if (tower.player.GetMoney() >= 400)
        {
            buttonSpeed.GetComponent<Image>().color = Color.white;
            buttonAvailable[2] = true;
        }
        else
        {
            buttonSpeed.GetComponent<Image>().color = Color.red;
            buttonAvailable[2] = false;
        }

        if (tower.player.GetMoney() >= 600)
        {
            buttonEconomy.GetComponent<Image>().color = Color.white;
            buttonAvailable[3] = true;
        }
        else
        {
            buttonEconomy.GetComponent<Image>().color = Color.red;
            buttonAvailable[3] = false;
        }



    }

    public void ClickWizard()
    {
        if (buttonAvailable[0])
        {
            tower.player.SumMoney(-550);
            tower.type = TowerType.MAGE_TOWER;
            tower.stats.SetStats(tower.type);
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
            EnhanceAllWizard();
            tower.ChangeTower();
        }

    }
    public void ClickArcher()
    {
        if (buttonAvailable[1])
        {
            tower.player.SumMoney(-550);
            tower.type = TowerType.ARCHER_TOWER;
            tower.stats.SetStats(tower.type);
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
            tower.ChangeTower();
        }

    }
    public void ClickSpeed()
    {
        if (buttonAvailable[2])
        {
            tower.player.SumMoney(-400);
            tower.type = TowerType.SPEED_TOWER;
            tower.stats.SetStats(tower.type);
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
            EnhanceAllTroopsVelocity();
            tower.ChangeTower();
        }

    }
    public void ClickEconomy()
    {
        if (buttonAvailable[3])
        {
            tower.player.SumMoney(-600);
            tower.type = TowerType.GOLD_TOWER;
            tower.stats.SetStats(tower.type);
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
            tower.ChangeTower();
        }
    }
    public void ClickWarrior()
    {
        if (buttonAvailable[4])
        {
            tower.player.SumMoney(-600);
            tower.type = TowerType.WARRIOR_TOWER;
            tower.stats.SetStats(tower.type);
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
            EnhanceAllWarriors();
            tower.ChangeTower();
        }
    }


    //Fa que els guerrers tinguin returnDamage true per poder retornar el 20% quan hi hagi una WwarriorTower
    void EnhanceAllWarriors()
    {
        if (tower.CompareTag("AllyTower"))
        {
            foreach (GameObject troop in GameObject.FindGameObjectsWithTag("AllyTroop"))
            {
                if (troop.GetComponent<WarriorTroop>())
                {
                    troop.GetComponent<WarriorTroop>().returnDamage = true;
                    troop.GetComponent<WarriorTroop>().WarriorTowerFX = Instantiate(troop.GetComponent<WarriorTroop>().WarriorTowerFX, troop.GetComponent<WarriorTroop>().transform);
                }
            }
        }
        else if (tower.CompareTag("EnemyTower"))
        {
            foreach (GameObject troop in GameObject.FindGameObjectsWithTag("EnemyTroop"))
            {
                if (troop.GetComponent<WarriorTroop>())
                {
                    troop.GetComponent<WarriorTroop>().returnDamage = true;
                    troop.GetComponent<WarriorTroop>().WarriorTowerFX = Instantiate(troop.GetComponent<WarriorTroop>().WarriorTowerFX, troop.GetComponent<WarriorTroop>().transform);
                }
            }
        }
    }

    //Wizard
    void EnhanceAllWizard()
    {
        if (tower.CompareTag("AllyTower"))
        {
            foreach (GameObject troop in GameObject.FindGameObjectsWithTag("AllyTroop"))
            {
                if (troop.GetComponent<MageTroop>())
                {
                    troop.GetComponent<MageTroop>().areaAttack = true;
                    troop.GetComponent<MageTroop>().MageTowerFX = Instantiate(troop.GetComponent<MageTroop>().MageTowerFX, troop.GetComponent<MageTroop>().transform);
                }
            }
        }
        else if (tower.CompareTag("EnemyTower"))
        {
            foreach (GameObject troop in GameObject.FindGameObjectsWithTag("EnemyTroop"))
            {
                if (troop.GetComponent<MageTroop>())
                {
                    troop.GetComponent<MageTroop>().areaAttack = true;
                    troop.GetComponent<MageTroop>().MageTowerFX = Instantiate(troop.GetComponent<MageTroop>().MageTowerFX, troop.GetComponent<MageTroop>().transform);
                }
            }
        }
    }

    //Velocity 
    void EnhanceAllTroopsVelocity()
    {
        if (tower.CompareTag("AllyTower"))
        {
            foreach (GameObject troop in GameObject.FindGameObjectsWithTag("AllyTroop"))
            {
                if (troop.GetComponent<Troop>())
                {
                    troop.GetComponent<Troop>().stats.attackSpeed += troop.GetComponent<Troop>().stats.attackSpeed * 20 / 100;
                    troop.GetComponent<Troop>().VelocityTowerFX = Instantiate(troop.GetComponent<Troop>().VelocityTowerFX, troop.GetComponent<Troop>().transform);
                }
            }
        }
        else if (tower.CompareTag("EnemyTower"))
        {
            foreach (GameObject troop in GameObject.FindGameObjectsWithTag("EnemyTroop"))
            {
                if (troop.GetComponent<Troop>())
                {
                    troop.GetComponent<Troop>().stats.attackSpeed += troop.GetComponent<Troop>().stats.attackSpeed * 20 / 100;
                }
            }
        }
    }
}