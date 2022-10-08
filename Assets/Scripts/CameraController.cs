using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // TODO:
    //public bool moveCameraWhenMouseOnEdge = false;
    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        List<RuntimePlatform> platforms = new List<RuntimePlatform> { RuntimePlatform.Android, RuntimePlatform.IPhonePlayer };
        if (platforms.Contains(Application.platform))
        {
            Vector3 position = new Vector3(28f, 117f, -70.5f);
            Quaternion rotation = new Quaternion(65f, 0f, 0f, 0f);
            transform.SetPositionAndRotation(position, rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GameIsOver){
            this.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            Vector3 currentPosition = transform.position;

            currentPosition.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
            currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

            transform.position = currentPosition;
        }
    }
}
