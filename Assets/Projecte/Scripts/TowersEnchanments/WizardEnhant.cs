using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnhant : MonoBehaviour
{
    GameObject player;
    PlayerController playerVar;

    private void Start()
    {
        player = GameObject.Find("/GameManager/AllyEconomy");
        playerVar = player.GetComponent<PlayerController>();
    }

    void OnClick()
    {
        print(playerVar);
    }

}
