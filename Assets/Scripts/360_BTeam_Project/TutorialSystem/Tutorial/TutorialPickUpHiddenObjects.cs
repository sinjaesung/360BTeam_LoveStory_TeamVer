using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPickUpHiddenObjects : TutorialBase
{
    [SerializeField] private Ray ray;
    [SerializeField] private Vector3 click_worldPosition;

    [SerializeField] public GameObject ClickPosAreaIcon;

    [SerializeField] private LayerMask PickUpLayer;

    [SerializeField] public int catchObjectCount = 6;//찾아야되는개수
    [SerializeField] public int catchCount = 0;

    [SerializeField]
    private GameObject[] objectList;
    [SerializeField]
    private string tagName;

    [SerializeField]
    private ParticleSystem True_Particle;
    [SerializeField]
    private ParticleSystem false_Particle;

    public Player3d_Planet player;

    public override void Enter()
    {
        Debug.Log("TutorialPickUpHiddenObjects Enter>>");
        player = FindObjectOfType<Player3d_Planet>();

        // 파괴해야할 오브젝트들을 활성화
        for (int i = 0; i < objectList.Length; ++i)
        {
            objectList[i].SetActive(true);
        }
    }

    public override void Execute(TutorialController controller)
    {
        /*
		/// 거리 기준
		if ( (triggerObject.position - playerController.transform.position).sqrMagnitude < 0.1f )
		{
			controller.SetNextTutorial();
		}*/
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);

        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 왼쪽 버튼을 눌렀을 때의 처리    
            Debug.Log("TutorialPickUpHiddenObjects 마우스 왼쪽 누름");
 
            ClickPosAreaIcon.SetActive(true);
            ScreenToWorld();
        }

        if (catchCount >= catchObjectCount && (objects.Length == 0))
        {
            controller.SetNextTutorial();
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
        if (Physics.Raycast(GetMouseHitRay(), out hitObject, 1000, PickUpLayer))
        {
            Debug.Log("해당 감지 모든 물리형 레이캐스트 콜라이더 타깃>" +
                hitObject.transform.name + "," + hitObject.point);

            ClickPosAreaIcon.SetActive(true);
            Debug.Log("해당 위치로 클릭위치 아이콘 표시>" + (new Vector3(hitObject.point.x, 0, hitObject.point.z) + new Vector3(0, 1.2f, 0)));
            ClickPosAreaIcon.transform.position = new Vector3(hitObject.point.x, 0, hitObject.point.z) + new Vector3(0, 1.2f, 0);
            click_worldPosition = new Vector3(hitObject.point.x, 0, hitObject.point.z) + new Vector3(0, 1.2f, 0);

            if (hitObject.transform.CompareTag(tagName))
            {
                Debug.Log("올바른 물체를 선택한경우> 점수(유대감)증가" + hitObject.transform.name);
                Destroy(hitObject.transform.gameObject);
                Instantiate(True_Particle, hitObject.point, Quaternion.identity);
                catchCount++;
                player.LoveScore += 10;
            }
            else
            {
                Debug.Log("다른 물체를 선택한경우> 점수(유대감)감소" + hitObject.transform.name);
                Destroy(hitObject.transform.gameObject);
                Instantiate(false_Particle, hitObject.point, Quaternion.identity);
                player.LoveScore -= 20;
                player.SetHealth(-1);
            }
        }
    }
    public override void Exit()
    {
        Debug.Log("TutorialPickUpHiddenObjects Exit>>");
    }
}
