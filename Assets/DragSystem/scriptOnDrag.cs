using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptOnDrag : MonoBehaviour
{

    public GameObject SoldierImage;
    // Start is called before the first frame update
    public void AddImageOnHUD()
    {
        Instantiate(SoldierImage, new Vector3(0, 1, -19), Quaternion.identity);
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
