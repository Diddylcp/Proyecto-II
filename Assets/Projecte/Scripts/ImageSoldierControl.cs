﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSoldierControl : MonoBehaviour
{
    /*GameObject player;
    PlayerController playerController;
    Vector3 selectPosition;
    Vector2 pos;
    public int moneyCost;
    public GameObject canvas;
    public GameObject soldier; */

    // Start is called before the first frame update
    void Start()
    {
        /* player = GameObject.FindGameObjectWithTag("GameController");
         playerController = player.GetComponent<PlayerController>(); */

    }
    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPos = hit.point;
            transform.position = newPos;
        }


    }
}
        /* pos = Input.mousePosition;
         transform.position = pos; //new Vector3(posPant.x, posPant.y, 0);
         if (Input.GetMouseButtonDown(0))
         {
             SetSoldierPosition();
         }


         if (Input.GetMouseButtonDown(1))
         {
             Destroy(canvas);
         }


         if (Input.GetMouseButtonDown(2))
             Debug.Log("Pressed middle click.");

     }

     void BuySoldier()
     {
         if (tag == "AllyTroop") player = GameObject.Find("PlayerEconomy");
         else player = GameObject.Find("EnemyEconomy");
         playerController = player.GetComponent<PlayerController>();
         playerController.SumMoney(-moneyCost);
     }

     void SetSoldierPosition()
     {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;

         if (Physics.Raycast(ray, out hit, 1000))
         {
             if (hit.transform.tag == "Respawn")
             {
                 selectPosition = hit.point;
                 selectPosition.y += 1;               
                 Instantiate(soldier, selectPosition, Quaternion.identity);
                 BuySoldier();
                 Destroy(canvas);
             }
         }
     }
 } */
    