using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadView : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    [SerializeField] private float fov = 0;

    [SerializeField] private float wheelSpeed = 10f;

    [SerializeField] private float roll;
    [SerializeField] private float pitch;
    [SerializeField] private float mouseSpeed = 10f;

    [SerializeField] private float autoTime = 0;

    [SerializeField] private bool isDebugMode = false;

    private void Awake()
    {
        fov = camera.fieldOfView;
    }
    private void Update()
    {
        //마우스 스크롤휠을 통한 카메라 화면 줌인아웃효과
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if(Mathf.Abs(wheel) > 0)
        {
            Debug.Log("Wheel");
            fov -= wheel * wheelSpeed;
            fov = Mathf.Clamp(fov, 25, 80);//25~80범위
            camera.fieldOfView = fov;
        }

        //마우스를 누르고 있는 상태에서 좌우로 움직여서 회전한다.카메라를 회전해야한다.
        if (Input.GetMouseButton(1))
        {
            autoTime = 0;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            roll -= mouseY * Time.deltaTime * mouseSpeed;//카메라 x축 회전(위아래회전)
            pitch -= mouseX * Time.deltaTime * mouseSpeed;//카메라 좌우 회전

            roll = Mathf.Clamp(roll, -30, 60);//위아래 회전 범위
        }
        else
        {
            autoTime += Time.deltaTime;
        }

        if(autoTime >= 5.0f && isDebugMode == false)
        {
            //5초가 지나면 자동으로 y축회전(좌우회전)되게끔
            pitch += Time.deltaTime * 10f;
        }

        if(pitch >= 360f)
        {
            pitch -= 360f;
        }
        else
        {
            pitch += 360f;
        }

        camera.transform.eulerAngles = new Vector3(roll, pitch, 0);//x축회전,y축회전 반영
        //rtCompass.rotation = Quaternion.Euler(0, 0, pitch);//화면의 좌우회전(카메라y축회전) pitch값 그대로 반영 나침반
    }
}
