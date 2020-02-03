using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TowerType { NORMAL, MAGE_TOWER, ARCHER_TOWER, WARRIOR_TOWER, GOLD_TOWER, SPEED_TOWER, COUNT };


public struct TowerStates
{
    public int health;
    public float area;
    public int damage;
    public int range;
    public float attackSpeed;
    public int moneyPerSecond;

    public void SetStats(TowerType tipo)
    {
        switch (tipo)
        {
            case TowerType.NORMAL:
                health = 1000;       // Vida original 1500
                damage = 130;
                attackSpeed = 0.8f;
                range = 12;
                moneyPerSecond = 10;
                area = 0;
                break;
            case TowerType.MAGE_TOWER:
                health = 1800;
                damage = 40;
                attackSpeed = 1.6f;
                range = 12;
                moneyPerSecond = 10;
                area = 2;
                break;
            case TowerType.ARCHER_TOWER:
                health = 1800;
                damage = 30;
                attackSpeed = 2.1f;
                range = 16;
                moneyPerSecond = 10;
                area = 0;
                break;

            case TowerType.GOLD_TOWER:
                health = 1800;
                damage = 30;
                attackSpeed = 1.6f;
                range = 12;
                moneyPerSecond = 15;
                area = 0;
                break;

            case TowerType.SPEED_TOWER:
                health = 1800;
                damage = 30;
                attackSpeed = 2f;
                range = 12;
                moneyPerSecond = 10;
                area = 0;
                break;

            case TowerType.WARRIOR_TOWER:
                health = 2000;
                damage = 30;
                attackSpeed = 1.6f;
                range = 12;
                moneyPerSecond = 10;
                area = 0;
                break;
        }
    }
}

public class TowerScript : MonoBehaviour
{
    private PlayerController player;// jugador el que controla
    [SerializeField] PlayerController allyPlayer, enemyPlayer;
    TowerStates stats;
    private float speed;
    TowerType type;
    GameObject objective;  //Al que atacara
    Vector2 pos;
    Vector2 posMouse;
    public GameObject respawnArea;
    bool isClicked = false;


    // Start is called before the first frame update
    void Start()
    {
        type = TowerType.NORMAL;
        objective = AnyoneToAttack();
        if (tag == "AllyTower") player = allyPlayer;
        else player = enemyPlayer;
        stats.SetStats(type);
        StartCoroutine(AttackEnemy());
        StartCoroutine(WaitSec());
    }

    [SerializeField] private Material MaterialEnemigo;
    [SerializeField] private Material MaterialAliado;
    [SerializeField] private string team;

    // Update is called once per frame
    void Update()
    {
        if(objective == null)
        {
            objective = AnyoneToAttack();
        }
        else
        {
            if (!StillInRange())
            {
                objective = AnyoneToAttack();
            }
        }
    }

    //Si segueix en rang enemic
    bool StillInRange()
    {
        float distance;
        distance = Vector3.Distance(pos, objective.transform.position);
        return (Mathf.Abs(distance) < stats.range);
    }

    //Augmenta el money
    void DropCoin()
    {
        player.SumMoney(stats.moneyPerSecond);

    }

    IEnumerator WaitSec()
    {
        // wait for 1 second
        yield return new WaitForSeconds(1.0f);
        DropCoin();
        StartCoroutine(WaitSec());
    }
    //Al clickar
    private void OnMouseDown()
    {
        if (isClicked)
        {
            isClicked = false;
        }
        else
        {
            isClicked = true;
        }
    }
    private GameObject AnyoneToAttack()
    {
        GameObject[] gos;
        if (tag == "AllyTower") gos = GameObject.FindGameObjectsWithTag("EnemyTroop");
        else gos = GameObject.FindGameObjectsWithTag("AllyTroop");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        pos = transform.position;
        foreach (GameObject go in gos)
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

    IEnumerator AttackEnemy()
    {
        if(objective != null)
        {
            if (StillInRange())
            {
                if ((objective.tag == "AllyTroop" && this.tag == "EnemyTower") || (objective.tag == "EnemyTroop" && this.tag == "AllyTower"))
                {
                    objective.GetComponent<Troop>().TakeDamage(stats.damage);
                }
            }                
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(AttackEnemy());
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        if (stats.health <= 0)
        {
            ChangeTeam();
        }
    }

    public void ChangeTeam()
    {
        if(team == "AllyTower")
        {
            this.tag = "EnemyTower";
            this.GetComponent<MeshRenderer>().material = MaterialEnemigo;
            stats.health = 1500;
            player = enemyPlayer;
            respawnArea.tag = "EnemyRespawn";
            
        }
        else if(team == "EnemyTower")
        {
            this.tag = "AllyTower";
            this.GetComponent<MeshRenderer>().material = MaterialAliado;
            stats.health = 1500;
            player = allyPlayer;
            respawnArea.tag = "Respawn";
        }
        team = this.tag;
        
    }
}
