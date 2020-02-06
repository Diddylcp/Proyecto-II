using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScriptOnDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    GameObject player;
    PlayerController playerController;
    [SerializeField] private GameObject soldierImage;
    [SerializeField] private int soldierCost;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    public void AddImageOnHUD()
    {
        if (tag == "AllyTroop") player = GameObject.Find("PlayerEconomy");
        else player = GameObject.Find("EnemyEconomy");
        playerController = player.GetComponent<PlayerController>();
        if (playerController.GetMoney() > soldierCost)
            Instantiate(soldierImage, new Vector3(0, 1, -19), Quaternion.identity);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector2 soldierPos;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            soldierPos = (Vector2)hit.point;
            Instantiate(soldierImage, new Vector3(soldierPos.x, soldierPos.y, 0), Quaternion.identity);
        }
        //if (playerController.GetMoney() > soldierCost)
        //Instantiate(soldierImage, this.transform);
        Debug.Log("OnPointerDown");
        
    }
}
