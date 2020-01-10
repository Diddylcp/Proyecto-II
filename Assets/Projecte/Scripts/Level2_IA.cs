using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_IA : MonoBehaviour
{
    GameObject player;
    PlayerController playerController;
    Vector3 selectPosition;
    Vector3 referencePointSpawn;
    public int moneyCost;
    public GameObject soldier;

    void Start()
    {
        referencePointSpawn = new Vector3(-58, 140, 211);   // Pos utilizada para encontrar la torre enemiga más cercana al campo aliado
        moneyCost = 190;
        player = GameObject.Find("EnemyEconomy");
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(Spawn());
    }

    void BuySoldier()
    {
        playerController.SumMoney(-moneyCost);
    }

    public GameObject TowerSpawning()
    {
        GameObject[] gosTower;
        gosTower = GameObject.FindGameObjectsWithTag("EnemyTower");
        GameObject closest = null;
        float distance = Mathf.Infinity;
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

    void SetSoldierPosition()
    {
        Instantiate(soldier, TowerSpawning().transform.position, Quaternion.identity);
    }

    IEnumerator Spawn()
    {
        if(playerController.GetMoney() > moneyCost)
        {
            SetSoldierPosition();
            BuySoldier();
            yield return new WaitForSeconds(4);
            StartCoroutine(Spawn());
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(Spawn());
        }
        
    }
}
