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

    PlayerController playerController;
    private GameObject soldierImageInstanciated;
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
        if(soldierImageInstanciated != null)
        {
            soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soldierImageInstanciated.transform.position = soldierPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //USAR Ray2D
            RaycastHit hit; //USAR RaycastHit2D
            Destroy(soldierImageInstanciated);
            if (Physics.Raycast(ray, out hit, 4000)) //ESTO NO ES 2D SE TIENE QUE USAR Physics2D.Raycast
            {
                if (hit.transform.tag == "Respawn" && playerController.GetMoney() > soldierCost)
                {
                    soldierPos = hit.point;
                    //soldierPos.y += 1;
                    Instantiate(soldierPrefab, soldierPos, Quaternion.Euler(-90, 0, 0)); 
                    playerController.SumMoney(-soldierCost);
                }

                /*Collider2D[] cols = Physics2D.OverlapCircleAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
                foreach(Collider2D collision in cols)
                { 
                    if (collision.tag == "Respawn" && playerController.GetMoney() > soldierCost)
                    {
                        Instantiate(soldierPrefab, soldierPos, Quaternion.identity);
                        playerController.SumMoney(-soldierCost);
                    }  
                } */
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f);
        if (/*col != null && */playerController.GetMoney() > soldierCost)
        {
            soldierPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            soldierImageInstanciated = Instantiate(soldierImage, soldierPos, Quaternion.identity);
        }
        if (/*col != null && */playerController.GetMoney() < soldierCost)
        {
            ChangeColor();
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
    {
        for (float i=0f; i<= 1f; i += 0.02f)
        {
            Color c= me.color;
            c.r += i;
            c.b -= i;
            me.color = c;
            yield return new WaitForEndOfFrame();
            //yield  return new WaitForSeconds(0.01f);
        }
        //StopCoroutine("ChangingRed");
        StartCoroutine("ChangingBlue");
    }

    IEnumerator ChangingBlue()
    {
        for (float i = 1f; i >= 0f; i -= 0.02f)
        {
            Color c = me.color;
            c.r -= i;
            c.b += i;
            me.color = c;
            yield return new WaitForEndOfFrame();
            //yield return new WaitForSeconds(0.01f);
        }
        StopCoroutine("ChangingBlue");
    }
}
