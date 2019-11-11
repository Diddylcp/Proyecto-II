using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    public PlayerController player;
    public TMP_Text text;
    string strMoney;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        strMoney = player.userMoney.ToString();
        text.text = strMoney + ('$');
    }
}
