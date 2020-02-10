﻿using System.Collections;
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
    private Vector2 soldierPos = Vector2.zero;
    Collider2D col;

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
            soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soldierImageInstanciated = Instantiate(warriorImage, soldierPos, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (warriorHotKey || mageHotKey || archerHotKey)
                Destroy(soldierImageInstanciated);
            warriorHotKey = false;
            mageHotKey = true;
            archerHotKey = false;
            soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soldierImageInstanciated = Instantiate(mageImage, soldierPos, Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if(warriorHotKey || mageHotKey || archerHotKey)
                Destroy(soldierImageInstanciated);
            warriorHotKey = false;
            mageHotKey = false;
            archerHotKey = true;
            soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soldierImageInstanciated = Instantiate(archerImage, soldierPos, Quaternion.identity);
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
                Destroy(soldierImageInstanciated);
                Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
                foreach (Collider2D collision in cols)
                {
                    if (collision.tag == "Respawn" && playerController.GetMoney() > warriorCost)
                    {
                        Instantiate(warriorPrefab, soldierPos, Quaternion.identity);
                        playerController.SumMoney(-warriorCost);
                    }
                }
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
                        scriptOnDrag aux = collision.GetComponent<scriptOnDrag>();
                        Debug.Log("Entrar Hotkey");
                        aux.ChangeColor();
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
