using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class scriptOnDrag : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject soldierImage;
    [SerializeField] private int soldierCost;


    private GameObject soldierImageInstanciated;
    PlayerController playerController;
    private RectTransform rectTransform;
    private Vector2 soldierPos = Vector2.zero;
    Collider2D col;
    Image me;

    void Start()
    {
        me = this.GetComponent<Image>();
        GameObject player;
        if (tag == "AllyTroop") player = GameObject.Find("AllyEconomy");
        else player = GameObject.Find("EnemyEconomy");
        playerController = player.GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    public void AddImageOnHUD()
    {
        GameObject player;
        if (tag == "AllyTroop") player = GameObject.Find("AllyEconomy");
        else player = GameObject.Find("EnemyEconomy");
        playerController = player.GetComponent<PlayerController>();
        if (playerController.GetMoney() > soldierCost)
            Instantiate(soldierImage, new Vector3(0, 1, -19), Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        soldierImageInstanciated.transform.position = soldierPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        foreach(Collider2D collision in cols)
        { 
            if (collision.tag == "Respawn" && playerController.GetMoney() > soldierCost)
            {
                Instantiate(soldierPrefab, soldierPos, Quaternion.identity);
                playerController.SumMoney(-soldierCost);
            }  
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        if (col != null)
        {
            soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soldierImageInstanciated = Instantiate(soldierImage, soldierPos, Quaternion.identity);
        }
        rectTransform = soldierImage.GetComponent<RectTransform>();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Destroy(soldierImageInstanciated);
    }

    public void ChangeColor()
    {
        StartCoroutine("ChangingRed");
    }

    IEnumerator ChangingRed()
    {/*
                                                                            //Algo así
        for (float i = 1f; i >= fadeToBlueAmount; i -= 0.05f)
		{

			// Getting access to Color options
			Color c = rend.material.color;

			// Setting values for Red and Green channels
			c.r = i;
			c.g = i;

			// Set color to Sprite Renderer
			rend.material.color = c;

			// Pause to make color be changed slowly
			yield return new WaitForSeconds (fadingSpeed);
		}
         */
        for (float i=0f; i>= 1f; i -= 0.1f)
        {
            Color c= me.color;
            c.r = i;
            yield  return new WaitForSeconds(0.2f);
        }
    }
}
