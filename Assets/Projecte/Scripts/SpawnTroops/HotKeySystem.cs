using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HotKeySystem : MonoBehaviour
{
    [SerializeField] private GameObject warriorPrefab;
    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject archerPrefab;
    [SerializeField] private GameObject warriorImage;
    [SerializeField] private GameObject mageImage;
    [SerializeField] private GameObject archerImage;
    [SerializeField] private int warriorCost;
    [SerializeField] private int mageCost;
    [SerializeField] private int archerCost;
    private bool mageHotKey;
    private bool warriorHotKey;
    private bool archerHotKey;

    private GameObject soldierImageInstanciated;
    PlayerController playerController;
    private RectTransform rectTransform;
    private Vector3 soldierPos = Vector3.zero;
    Collider2D col;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player;
        mageHotKey = false;
        warriorHotKey = false;
        archerHotKey = false;
        if (tag == "AllyTroop") player = GameObject.Find("AllyEconomy");
        else player = GameObject.Find("EnemyEconomy");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // HotKeys
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (warriorHotKey || mageHotKey || archerHotKey)
                Destroy(soldierImageInstanciated);
            warriorHotKey = true;
            mageHotKey = false;
            archerHotKey = false;
            if(playerController.GetMoney() > warriorCost)
            {
                //soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                soldierPos = Input.mousePosition;
                soldierImageInstanciated = Instantiate(warriorImage, soldierPos, Quaternion.identity);
                //soldierImageInstanciated.transform.SetParent(this.transform);


            }
            else
            {
                GameObject aux = GameObject.Find("WarriorButton");
                aux.GetComponent<scriptOnDrag>().ChangeColor();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (warriorHotKey || mageHotKey || archerHotKey)
                Destroy(soldierImageInstanciated);
            warriorHotKey = false;
            mageHotKey = true;
            archerHotKey = false;
            if (playerController.GetMoney() > mageCost)
            {
                soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                soldierImageInstanciated = Instantiate(mageImage, soldierPos, Quaternion.identity);

            }
            else
            {
                GameObject aux = GameObject.Find("MageButton");
                aux.GetComponent<scriptOnDrag>().ChangeColor();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if(warriorHotKey || mageHotKey || archerHotKey)
                Destroy(soldierImageInstanciated);
            warriorHotKey = false;
            mageHotKey = false;
            archerHotKey = true;
            if (playerController.GetMoney() > mageCost)
            {
                soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                soldierImageInstanciated = Instantiate(archerImage, soldierPos, Quaternion.identity);
            }
            else
            {
                GameObject aux = GameObject.Find("ArcherButton");
                aux.GetComponent<scriptOnDrag>().ChangeColor();
            }
        }
        if (soldierImageInstanciated != null)
        {
            soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soldierImageInstanciated.transform.position = soldierPos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Spawn
            if (warriorHotKey)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Destroy(soldierImageInstanciated);

                if (Physics.Raycast(ray, out hit, 4000))
                {
                    if (hit.transform.tag == "Respawn")
                    {
                        soldierPos = hit.point;
                       // soldierPos.y += 1;
                        Instantiate(warriorPrefab, soldierPos, Quaternion.identity);
                       
                   
                    }
                }
                /*Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
                foreach (Collider2D collision in cols)
                {
                    if (collision.tag == "Respawn" && playerController.GetMoney() > warriorCost)
                    {
                        Instantiate(warriorPrefab, soldierPos, Quaternion.identity);
                        playerController.SumMoney(-warriorCost);
                    }
                } */
            }
            else if (mageHotKey)
            {
                Destroy(soldierImageInstanciated);
                Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
                foreach (Collider2D collision in cols)
                {
                    if (collision.tag == "Respawn" && playerController.GetMoney() > mageCost)
                    {
                        Instantiate(magePrefab, soldierPos, Quaternion.identity);
                        playerController.SumMoney(-mageCost);
                    }
                }
            }
            else if (archerHotKey)
            {
                Destroy(soldierImageInstanciated);
                Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
                foreach (Collider2D collision in cols)
                {
                    if (collision.tag == "Respawn" && playerController.GetMoney() > archerCost)
                    {
                        Instantiate(archerPrefab, soldierPos, Quaternion.identity);
                        playerController.SumMoney(-archerCost);
                    }
                }
            }
        }
    }


}
