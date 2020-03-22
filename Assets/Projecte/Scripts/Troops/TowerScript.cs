using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TowerType { NORMAL, MAGE_TOWER, ARCHER_TOWER, WARRIOR_TOWER, GOLD_TOWER, SPEED_TOWER, COUNT };


public struct TowerStates
{
    public float health;
    public float startHealth;
    public float area;
    public int damage;
    public float range;
    public float attackSpeed;
    public int moneyPerSecond;

    public void SetStats(TowerType tipo)
    {
        switch (tipo)
        {
            case TowerType.NORMAL:
                health = 1500;       // Vida original 1500
                damage = 130;
                attackSpeed = 0.8f;
                range = 2.4f; //11
                moneyPerSecond = 0; // abans 30
                area = 0;
                break;
            case TowerType.MAGE_TOWER:
                health = 1800;
                damage = 40;
                attackSpeed = 1.6f;
                range = 2.4f;
                moneyPerSecond = 05; // abans 30
                area = 2;
                break;
            case TowerType.ARCHER_TOWER:
                health = 1800;
                damage = 30;
                attackSpeed = 2.1f;
                range = 2.7f;
                moneyPerSecond = 05; // abans 30
                area = 0;       
                break;

            case TowerType.GOLD_TOWER:
                health = 1800;
                damage = 30;
                attackSpeed = 1.6f;
                range = 2.4f;
                moneyPerSecond = 30;
                area = 0;
                break;

            case TowerType.SPEED_TOWER:
                health = 1800;
                damage = 30;
                attackSpeed = 2f;
                range = 2.4f;
                moneyPerSecond = 05; // abans 30
                area = 0;
                break;

            case TowerType.WARRIOR_TOWER:
                health = 2000;
                damage = 30;
                attackSpeed = 1.6f;
                range = 2.4f;
                moneyPerSecond = 05;
                area = 0;
                break;
        }
        startHealth = health;
    }
}

public class TowerScript : MonoBehaviour
{
    public PlayerController player;// jugador el que controla
    [SerializeField] PlayerController allyPlayer, enemyPlayer;
    public TowerStates stats;
    private float speed;
    public TowerType type;
    GameObject objective;  //Al que atacara
    Vector2 pos;
    Vector2 posMouse;
    public GameObject respawnArea;
    bool isClicked = false;
    public MilloresTower enhanceButtons;
    MilloresTower hudEnhance;
    Vector3 positionToShowEnhance;
    public GameObject selected;
    public Image HealthBar;

    public Mesh towerNormal;
    public Mesh towerArcher;
    public Mesh towerWizard;
    public Mesh towerWarrior;
    public Mesh towerVelocity;
    public Mesh towerEconomy;

    public CreateFloatingMoney objToCreateFloatingMoney;

    // Start is called before the first frame update
    void Start()
    {
        positionToShowEnhance = new Vector3(454.3f, 36.9375f, 0);
        //type = TowerType.NORMAL;
        objective = AnyoneToAttack();
        if (tag == "AllyTower") player = GameObject.Find("AllyEconomy").GetComponent<PlayerController>();
        else player = player = GameObject.Find("EnemyEconomy").GetComponent<PlayerController>();
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
        if(tag == "AllyTower")
            objToCreateFloatingMoney.AddFloatingText(stats.moneyPerSecond);
        

    }

    IEnumerator WaitSec()
    {
        // wait for 1 second
        DropCoin();
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(WaitSec());
    }
    //Al clickar
    public void SetIsClicked(bool a)
    {
        isClicked = a;
    }

    private void OnMouseDown()
    {
        if (tag == "AllyTower" && this.type == TowerType.NORMAL)
        { 
            if (!player.GetPlayerWithTower())
            {
                player.SetPlayerWithTower(true);
                hudEnhance = Instantiate(enhanceButtons, positionToShowEnhance, Quaternion.identity, GameObject.Find("ButtonsHUD").transform);
                hudEnhance.tower = this;
                hudEnhance.tower.selected.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                hudEnhance = GameObject.FindObjectOfType<MilloresTower>();
                hudEnhance.tower.selected.GetComponent<SpriteRenderer>().enabled = false;
                hudEnhance.tower = this;
                hudEnhance.tower.selected.GetComponent<SpriteRenderer>().enabled = true;
            }
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
        yield return new WaitForSeconds(stats.attackSpeed);
        StartCoroutine(AttackEnemy());
    }

    public void TakeDamage(int damage)
    {
        stats.health -= damage;
        HealthBar.fillAmount = stats.health / stats.startHealth;
        if (stats.health <= 0)
        {
            ChangeTeam();
            type = TowerType.NORMAL;
            this.GetComponent<MeshFilter>().mesh = towerNormal;
        }
    }

    public void ChangeTeam()
    {
        if(team == "AllyTower")
        {
            this.tag = "EnemyTower";
            this.GetComponent<MeshRenderer>().material = MaterialEnemigo;
            stats.health = 1500;
            HealthBar.fillAmount = stats.health / stats.startHealth;
            player = enemyPlayer;
            respawnArea.tag = "EnemyRespawn";
            
        }
        else if(team == "EnemyTower")
        {
            this.tag = "AllyTower";
            this.GetComponent<MeshRenderer>().material = MaterialAliado;
            stats.health = 1500;
            HealthBar.fillAmount = stats.health / stats.startHealth;
            player = allyPlayer;
            respawnArea.tag = "Respawn";
        }
        team = this.tag;
        
    }
}
