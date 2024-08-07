using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraControll : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] private float roll;
    [SerializeField] private float pitch;
    [SerializeField] private float fov;
    [SerializeField] private float mouseSpeed;
    [SerializeField] public bool isMove = true;

    private void Start()
    {
        isMove = true;
      fov = _camera.fieldOfView;
    }
    void Update()
    {
        if (isMove)
        {
            if (Input.GetMouseButton(1))
            {
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");

                roll -= mouseY * Time.deltaTime * mouseSpeed;
                pitch -= mouseX * Time.deltaTime * mouseSpeed;
            }

            _camera.transform.eulerAngles = new Vector3(roll, pitch, 0);
        }
    }
}
