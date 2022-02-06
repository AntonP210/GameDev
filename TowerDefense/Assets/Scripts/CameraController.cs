using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float initialCameraLocation = 72f;
    public float minY = 30f, maxY = 80f, maxZ = 39f, minZ = 75f, maxX = 45f, minX = 104f;

    private void Update()
    {
        //The controls are inverted in the code from what actually happens
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            if (transform.position.z > minZ) //39f
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }

        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            if (transform.position.z < maxZ)//75f
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            if (transform.position.x > maxX)//45f
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            if (transform.position.x < minX)//104f
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
        }


        //Camera zoom into game , TODO: make it look better and not just zoom.
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY); //limits the area where to zoom in\out.

        transform.position = pos;
    }
}
