using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 1f;
    public float panBorderThickness = 10f;
    public Vector2 panMaxLimit;
    public Vector2 panMinLimit; 

    /*public float scrollSpeed = 20f;
    public float maxZ = -6f;
    public float minZ = -2f;*/

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        /*float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos.z += scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);*/
        pos.x = Mathf.Clamp(pos.x, panMinLimit.x, panMaxLimit.x);
        pos.y = Mathf.Clamp(pos.y, panMinLimit.y, panMaxLimit.y);

        transform.position = pos;
    }
}
