using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_IA : MonoBehaviour
{
    PlayerController playerController;
    Vector3 selectPosition;
    Vector3 referencePointSpawn;
    [SerializeField] GameObject warrior;
    [SerializeField] GameObject mage;
    [SerializeField] GameObject archer;
    System.Random rnd;
    int troopSpawned;
    int warriorCost = 190;
    int mageCost = 250;
    int archerCost = 210;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player;
        rnd = new System.Random();
        referencePointSpawn = new Vector3(-58, 140, 211);   // Pos utilizada para encontrar la torre enemiga más cercana al campo aliado
        player = GameObject.Find("EnemyEconomy");
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(Spawn());
    }

    public GameObject TowerSpawning()
    {
        GameObject[] gosTower;
        gosTower = GameObject.FindGameObjectsWithTag("EnemyTower");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gosTower)
        {
            if (go.GetComponent<TowerScript>().stats.health < 1000) return go;
        }
        foreach (GameObject go in gosTower)
        {
            float curDistance = Vector3.Distance(referencePointSpawn, go.transform.position);
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    IEnumerator Spawn()
    {
        troopSpawned = rnd.Next(0, 2);
        switch (troopSpawned)
        {
            case 0:
                if (playerController.GetMoney() > warriorCost)
                {
                    Instantiate(warrior, TowerSpawning().transform.position, Quaternion.Euler(-90, 0, 0));
                    playerController.SumMoney(-warriorCost);
                    yield return new WaitForSeconds(4);
                    StartCoroutine(Spawn());
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Spawn());
                }
                break;
            case 1:
                if (playerController.GetMoney() > mageCost)
                {
                    Instantiate(mage, TowerSpawning().transform.position, Quaternion.Euler(-90, 0, 0));
                    playerController.SumMoney(-mageCost);
                    yield return new WaitForSeconds(4);
                    StartCoroutine(Spawn());
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Spawn());
                }
                break;
            case 2:
                if (playerController.GetMoney() > archerCost)
                {
                    Instantiate(archer, TowerSpawning().transform.position, Quaternion.Euler(-90, 0, 0));
                    playerController.SumMoney(-archerCost);
                    yield return new WaitForSeconds(4);
                    StartCoroutine(Spawn());
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Spawn());
                }
                break;
            default:
                yield return new WaitForSeconds(0.5f);
                break;
        }
    }
}
