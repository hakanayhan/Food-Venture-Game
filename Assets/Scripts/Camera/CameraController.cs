using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 dragOrigin;
    Camera mainCamera;
    [SerializeField] private CameraSettings _settings;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        HandleMovementWithWASD();
        HandleMovementClickAndDrag();
    }

    void HandleMovementWithWASD()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Scroll up
            float z = transform.position.z;
            z += _settings.moveSpeed * Time.deltaTime;
            z = Mathf.Min(_settings.maxZ, z);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Scroll down
            float z = transform.position.z;
            z -= _settings.moveSpeed * Time.deltaTime;
            z = Mathf.Max(_settings.minZ, z);
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
    }

    void HandleMovementClickAndDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0))
            return;

        Vector3 pos = mainCamera.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(0, 0, -pos.y * _settings.dragSpeed);

        transform.Translate(move, Space.World);

        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, _settings.minZ, _settings.maxZ));
    }
}
