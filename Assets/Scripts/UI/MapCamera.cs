using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public float camSpeed = 2f;
    public float zoomSpeed = 10f; 
    public float minZoom = 10f; 
    public float maxZoom = 50;  

    public Transform player;
    public Camera cam;

    void Update()
    {
        float moveSpeed = cam.orthographicSize * camSpeed;
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.A))
            horizontalInput = -1f;
        if (Input.GetKey(KeyCode.D))
            horizontalInput = 1f;
        if (Input.GetKey(KeyCode.W))
            verticalInput = 1f;
        if (Input.GetKey(KeyCode.S))
            verticalInput = -1f;

        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized * moveSpeed * Time.unscaledDeltaTime;
        cam.transform.Translate(moveDirection, Space.World);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float newSize = cam.orthographicSize - scrollInput * zoomSpeed;

        cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);

        if (Input.GetKeyDown(KeyCode.C) && player != null)
        {
            cam.transform.position = new Vector3(player.position.x, player.position.y, cam.transform.position.z);
            cam.orthographicSize = 20;
        }
    }
}
