﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    bool firstText = false, firstUpgrade = false, secondText = false, thirdText = false, firstUnit = false;
    [SerializeField] GameObject buttonsHud, areaSpawn, mageButton, archerButton, otherButtons, redCircle, upgradeMenu;

    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!firstText) {
                firstText = true;
                GameObject.Find("TutorialCanvas").SetActive(false);
                buttonsHud.SetActive(true);
                areaSpawn.SetActive(true);
            }
            else if (!secondText)
            {
                secondText = true;
                Time.timeScale = 1;
                areaSpawn.SetActive(false);
                Destroy(redCircle);
            }
            else if (!thirdText)
            {
                buttonsHud.SetActive(true);
                otherButtons.SetActive(false);
                Time.timeScale = 1;
            }
            if (firstUpgrade)
            {
                upgradeMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if(GameObject.Find("AllyTroopWarrior Variant(Clone)") != null && !firstUnit)
        {
            StartCoroutine("ShowMageButton");
            firstUnit = true;
        }
        if(GameObject.Find("EnhanceTowerCanvas(Clone)") != null && !firstUpgrade)
        {
            upgradeMenu.SetActive(true);
            firstUpgrade = true;
            Time.timeScale = 0;
        }
    }

    IEnumerator ShowMageButton()
    {
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        otherButtons.SetActive(true);
        mageButton.SetActive(true);
        archerButton.SetActive(true);
        buttonsHud.SetActive(false);
    }
}
