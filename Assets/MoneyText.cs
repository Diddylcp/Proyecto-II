using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyText : MonoBehaviour
{

    Vector3 posInitial;
    Vector3 posFinal;
    public float speed;
    TextMeshPro textMoney;
    byte alphaCounter;
    public byte alphaSpeed;

    // Start is called before the first frame update
    void Start()
    {
       
        posInitial = new Vector3(0, 7f, -3.63f);
        posFinal = new Vector3(0, 20, -3.63f);
        textMoney = GetComponent<TextMeshPro>();
        textMoney.faceColor = new Color32(251, 243, 36, 255);
        alphaCounter = 255;
        gameObject.transform.localScale = new Vector3(1, 1, 1);

        this.transform.localPosition = posInitial;
        this.transform.localRotation = Quaternion.Euler(30, 0, 0);
        Destroy(gameObject, 1.5f);



    }

    // Update is called once per frame
    void Update()
    {
        StartFloating();
    }

    void StartFloating()
    {
        if (alphaCounter > 10)
        {
            alphaCounter -= alphaSpeed;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, posFinal, speed * Time.deltaTime);
            textMoney.faceColor = new Color32(251, 243, 36, alphaCounter);

        }
    }

}
