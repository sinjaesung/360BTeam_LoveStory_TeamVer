using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMoveTest : MonoBehaviour
{
    [SerializeField] private Ray ray;
    [SerializeField] private BoxCollider XZPlane_Collider;
    [SerializeField] private Vector3 click_worldPosition;

    [SerializeField] public GameObject ClickPosAreaIcon;

    [SerializeField] public bool isMoving = false;//이동중 여부
    [SerializeField] public Vector3 moveDir = Vector3.zero;
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private LayerMask RaycastMovePosLayer;

    [SerializeField] public bool CanControl = true;

    [SerializeField] public ParticleSystem moveEffect;

    public CameraMoveTest Instance;
    void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        if (CanControl)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 마우스 왼쪽 버튼을 눌렀을 때의 처리    
                Debug.Log("마우스 왼쪽 누름");

                if (!isMoving)
                {
                    ClickPosAreaIcon.SetActive(true);
                    ScreenToWorld();
                    Debug.Log("카메라를 해당 클릭한 스크린->XZ평면 raycast투영 월드위치로 이동시킨다" + click_worldPosition);
                }
                else
                {
                    Debug.Log("카메라가 이동중일때는 대기한다");
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("마우스를 Up했을때 관련 인터렉션 아이콘 감춘다");

                // ClickPosAreaIcon.SetActive(false);
            }
        }
    }

    private Ray GetMouseHitRay()
    {
        Debug.Log("Camera.main.ScreenPointToRay(Input.mousePosition)>" + Camera.main.ScreenPointToRay(Input.mousePosition));
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    void ScreenToWorld()
    {
        // ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitObject;
        if(Physics.Raycast(GetMouseHitRay(),out hitObject, 1000, RaycastMovePosLayer))
        {
            Debug.Log("해당 감지 모든 물리형 레이캐스트 콜라이더 타깃>" +
                hitObject.transform.name + "," + hitObject.point);

            ClickPosAreaIcon.SetActive(true);
            Debug.Log("해당 위치로 클릭위치 아이콘 표시>" +(new Vector3(hitObject.point.x, 0, hitObject.point.z) + new Vector3(0, 1.2f, 0)) );
            ClickPosAreaIcon.transform.position = new Vector3(hitObject.point.x, 0, hitObject.point.z) + new Vector3(0, 1.2f, 0);
            click_worldPosition = new Vector3(hitObject.point.x, 0, hitObject.point.z) + new Vector3(0, 1.2f, 0);
            //Move_Transform_Lerp(click_worldPosition);
            StartCoroutine(Move_Transform_Coroutine(click_worldPosition));
        }
    }

    IEnumerator Move_Transform_Coroutine(Vector3 destination)
    {
        moveEffect.Play();
        isMoving = true;
        Vector3 start = transform.position;

        transform.LookAt(destination);
        //movement.MoveTo((destination - start).normalized);
        var moveDirection = (destination - start).normalized;
        moveDir = moveDirection;

        while (isMoving)
        {
            Debug.Log("Move_Transform_Coroutine>>" + transform.position + "," + destination);
            //var distance = Vector3.Distance(transform.position, destination);
            var distance = Vector3.Magnitude(transform.position-destination);
            Debug.Log("목표위치와 카메라간의 Distance값 조회>" + distance);
            if (distance <= (moveSpeed * 0.04f))//50*0.04 = 2
            {
                Debug.Log("목표 위치에 도달완료>" + transform.position + "," + destination+",distance:>"+ distance);
                isMoving = false;
                transform.position = destination;
                StopAllCoroutines();
                moveEffect.Stop();
                yield break;
            }
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }
    void Move_Transform_Lerp(Vector3 destination)
    {
        Debug.Log("Move_Transform_Lerp>" + destination);
        transform.LookAt(destination);
        transform.position = Vector3.Lerp(transform.position, destination, 1f);
    }

    void Move_Transform_Slerp(Vector3 destination)
    {
        Debug.Log("Move_Transform_Slerp>" + destination);
        transform.LookAt(destination);
        transform.position = Vector3.Slerp(transform.position, destination, 1f);
    }
}
