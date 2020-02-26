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
        if (tower.player.GetMoney() >= 600)
        {
            buttonWizard.GetComponent<Image>().color = Color.green;
            buttonWarrior.GetComponent<Image>().color = Color.green;
            buttonArcher.GetComponent<Image>().color = Color.green;
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
            buttonSpeed.GetComponent<Image>().color = Color.green;
            buttonAvailable[2] = true;
        }
        else
        {
            buttonSpeed.GetComponent<Image>().color = Color.red;
            buttonAvailable[2] = false;
        }

        if (tower.player.GetMoney() >= 450)
        {
            buttonEconomy.GetComponent<Image>().color = Color.green;
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
            tower.player.SumMoney(-600);
            tower.type = TowerType.MAGE_TOWER;
            tower.stats.SetStats(tower.type);
            tower.GetComponent<MeshFilter>().mesh = tower.towerWizard;
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
            
        }

    }
    public void ClickArcher()
    {
        if (buttonAvailable[1])
        {
            tower.player.SumMoney(-600);
            tower.type = TowerType.ARCHER_TOWER;
            tower.stats.SetStats(tower.type);
            tower.GetComponent<MeshFilter>().mesh = tower.towerArcher;
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
        }

    }
    public void ClickSpeed()
    {
        if (buttonAvailable[2])
        {
            tower.player.SumMoney(-400);
            tower.type = TowerType.SPEED_TOWER;
            tower.stats.SetStats(tower.type);
            tower.GetComponent<MeshFilter>().mesh = tower.towerVelocity;
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
        }

    }
    public void ClickEconomy()
    {
        if (buttonAvailable[3])
        {
            tower.player.SumMoney(-450);
            tower.type = TowerType.GOLD_TOWER;
            tower.stats.SetStats(tower.type);
            tower.GetComponent<MeshFilter>().mesh = tower.towerEconomy;
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
        }
    }
    public void ClickWarrior()
    {
        if (buttonAvailable[4])
        {
            tower.player.SumMoney(-600);
            tower.type = TowerType.WARRIOR_TOWER;
            tower.stats.SetStats(tower.type);
            tower.GetComponent<MeshFilter>().mesh = tower.towerWarrior;
            tower.selected.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject);
            tower.player.SetPlayerWithTower(false);
            
        }
    }
}