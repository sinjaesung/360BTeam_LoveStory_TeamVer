using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//**(240730_PSB) 마우스를 눌렀을때, 마우스를 눌럿다 떗을때, 누르고있을때
public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    //====================
    // 조이스틱의 기능
    // 1. 마우스를 누르고 있는 상태에서 움직이면 스틱도 움직이게 하면된다.
    // 2. 움직일수 있는 거리를 제한해야한다.
    // 3. 마우스를 누르고 있다가 때면 원래 위치로 돌아가야 합니다.
    // 4. (옵션) 마우스 드래그앤 드랍 기능도 포함하도록 한다.
    //====================

    //**(240730_PSB) 움직이여야 할 스틱 변수
    [SerializeField] private RectTransform rtStick;

    //**(240730_PSB) 시작위치
    [SerializeField] private Vector2 startPos;
    
    //**(240730_PSB) 스틱이 움직이여야 할 크기와 방향
    [SerializeField] private Vector2 dir;

    //======================
    // 이해를 돕기위해 public으로 선언
    //======================
    //**(240730_PSB) 조이스틱이 동작하는 동안은 로드뷰 마우스 이동은 하지 않게 하기 위한 트리거
    public bool isDragLock = false;


    private void Start()
    {
        //=================
        // 초기화
        //=================

        startPos = rtStick.position;
    }

    public Vector2 GetDir()
    {
        //**(240730_PSB) dir 값을 리턴받는 함수
        return dir;
    }


    public void OnPointerDown(PointerEventData _eventData)
    {
        Debug.Log("눌렀을때");
        isDragLock = true;
    }

    public void OnPointerUp(PointerEventData _eventData)
    {
        Debug.Log("눌렀다 떗을때");
        isDragLock = false;
        //**(240730_PSB) 마우스를 눌렀다 땟을 면 처음위치로 돌아간다.
        rtStick.position = startPos;
        dir = new Vector2(0, 0);
    }

    public void OnDrag(PointerEventData _eventData)
    {
        Debug.Log("누르고 움직일때");
        //**(240730_PSB) 마우스를 드래그 한 위치를 스틱으로 할당한다.
        rtStick.position = _eventData.position;
        
        Vector2 drag = _eventData.position - startPos;
        //**(240730_PSB) 40 : 최대 거리 값
        if(drag.magnitude > 40)
        {
            //정규화
            drag.Normalize();
            rtStick.position = startPos + drag * 40;
        }

        dir = drag;
    }

}
