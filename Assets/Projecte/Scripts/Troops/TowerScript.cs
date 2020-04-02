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
                damage = 20;
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
    public Canvas enhanceButtons;
    MilloresTower hudEnhance;
    Vector3 positionToShowEnhance;
    public GameObject selected;
    public Image HealthBar;

    [SerializeField] Color allyHealthBar;
    [SerializeField] Color enemyHealthBar;

    public GameObject towerNormal;
    public GameObject towerArcher;
    public GameObject towerWizard;
    public GameObject towerWarrior;
    public GameObject towerVelocity;
    public GameObject towerEconomy;

    private GameObject actualTower;

    [SerializeField] private AudioSource capturedAudio;
    [SerializeField] private AudioSource updateAudio;

    public CreateFloatingMoney objToCreateFloatingMoney;

    // Start is called before the first frame update
    void Start()
    {
        positionToShowEnhance = new Vector3(-21.4f, -72.9f, -6);
        actualTower = Instantiate(towerNormal, this.transform);
        positionToShowEnhance = new Vector3(454.3f, 36.9375f, 0);
        objective = AnyoneToAttack();
        if (tag == "AllyTower") player = GameObject.Find("AllyEconomy").GetComponent<PlayerController>();
        else player = player = GameObject.Find("EnemyEconomy").GetComponent<PlayerController>();
        ChangeMaterial();
        stats.SetStats(type);
        StartCoroutine(AttackEnemy());
        StartCoroutine(WaitSec());
    }

    [SerializeField] private Material MaterialTorreAliado;
    [SerializeField] private Material MejoraTorreAliado;
    [SerializeField] private Material FlagAliado;

    [SerializeField] private Material MaterialTorreEnemigo;
    [SerializeField] private Material MejoraTorreEnemigo;
    [SerializeField] private Material FlagEnemigo;
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
        //GameObject imageSoldier = GameObject.Find("imageSoldier");
        if (tag == "AllyTower" && this.type == TowerType.NORMAL)
        { 
            if (!player.GetPlayerWithTower())
            {
                player.SetPlayerWithTower(true);
                Instantiate(enhanceButtons);
                hudEnhance = (MilloresTower)GameObject.FindObjectOfType<MilloresTower>();// ("/EnhanceTowerCanvas/MilloresTower");
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
            updateAudio.Play();
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
            type = TowerType.NORMAL;

            Destroy(actualTower);
            actualTower = Instantiate(towerNormal, this.transform);
            ChangeTeam();
            ChangeMaterial();
        }
    }

    public void ChangeTeam()
    {
        if(tag == "AllyTower")
        {
            this.tag = "EnemyTower";
            foreach (Transform tower in transform)
            this.transform.Find("Canvas").transform.Find("HealthBG").transform.Find("HealthBar").GetComponent<Image>().color = enemyHealthBar;
            stats.health = 1500;
            HealthBar.fillAmount = stats.health / stats.startHealth;
            player = enemyPlayer;
            respawnArea.tag = "EnemyRespawn";
            
        }
        else if(tag == "EnemyTower")
        {
            this.tag = "AllyTower";
            this.transform.Find("Canvas").transform.Find("HealthBG").transform.Find("HealthBar").GetComponent<Image>().color = allyHealthBar;
            stats.health = 1500;
            HealthBar.fillAmount = stats.health / stats.startHealth;
            player = allyPlayer;
            respawnArea.tag = "Respawn";
        }
        capturedAudio.Play();
        team = this.tag;
    }

    public void ChangeTower()
    {
        Destroy(actualTower);
        if (type == TowerType.NORMAL) actualTower = Instantiate(towerNormal, this.transform);
        else if (type == TowerType.ARCHER_TOWER) actualTower = Instantiate(towerArcher, this.transform);
        else if (type == TowerType.GOLD_TOWER) actualTower = Instantiate(towerEconomy, this.transform);
        else if (type == TowerType.MAGE_TOWER) actualTower = Instantiate(towerWizard, this.transform);
        else if (type == TowerType.WARRIOR_TOWER) actualTower = Instantiate(towerWarrior, this.transform);
        else if (type == TowerType.SPEED_TOWER) actualTower = Instantiate(towerVelocity, this.transform);
        ChangeMaterial();
    }

    private void ChangeMaterial()
    {
        if (tag == "EnemyTower")
        {
            if (type == TowerType.NORMAL)
                foreach (Transform tower in transform)
                {
                    if (tower.tag == "Tower") foreach (Transform child in tower)
                    {
                        if (child.tag == "Tower") child.GetComponent<MeshRenderer>().material = MaterialTorreEnemigo;
                        else if (child.tag == "Flag") child.GetComponent<MeshRenderer>().material = FlagEnemigo;
                    }
                }
            else
                foreach (Transform tower in transform)
                {
                    if (tower.tag == "Tower") foreach (Transform child in tower)
                    {
                        if (child.tag == "Tower") child.GetComponent<MeshRenderer>().material = MejoraTorreEnemigo;
                        else if (child.tag == "Flag") child.GetComponent<MeshRenderer>().material = FlagEnemigo;
                    }
                }
        }
        else
        {
            if (type == TowerType.NORMAL)
                foreach (Transform tower in transform)
                {
                    if (tower.tag == "Tower") foreach (Transform child in tower)
                        {
                            if (child.tag == "Tower") child.GetComponent<MeshRenderer>().material = MaterialTorreAliado;
                            else if (child.tag == "Flag") child.GetComponent<MeshRenderer>().material = FlagAliado;
                        }
                }
            else
                foreach (Transform tower in transform)
                {
                    if (tower.tag == "Tower") foreach (Transform child in tower)
                        {
                            if (child.tag == "Tower") child.GetComponent<MeshRenderer>().material = MejoraTorreAliado;
                            else if (child.tag == "Flag") child.GetComponent<MeshRenderer>().material = FlagAliado;
                        }
                }
        }
    }
}
