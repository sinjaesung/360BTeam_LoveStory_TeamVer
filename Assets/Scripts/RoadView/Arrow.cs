using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.SceneView;

/*
 * 씬을 이동하는 용도의 Arrow
   클릭시에 관련 연결된 씬으로 페이드아웃되며 이동한다.
 */

public class Arrow : MonoBehaviour
{
    [SerializeField] private RectTransform objMessage;
    [SerializeField] private TMP_Text TextMessage;

    [SerializeField] private Vector3 offset = new Vector3(0, 1, 0);

    [SerializeField] private new string name = string.Empty;

    private bool isHover = false;
    //public bool[] isClear = new bool[3];

    //[SerializeField] private int loadSceneIndex = 5;
    //Arrow는 클릭시 해당 방향으로(해당 Arrow의 근방위치로 카메라를 이동시키는연산 수행)
    //Arrow별 이동시키는 방향벡터 x,z방향 벡터를 지정해두고 클릭시 그 방향으로 이동하게끔(카메라)
    [SerializeField] private Vector3 move_direction = new Vector3(0, 0, 0);

    [SerializeField] private Camera playerCamera;

    //[SerializeField] private int moveSceneIndex = 0;
    [SerializeField] private string moveSceneName;
    [SerializeField] public int plantIndex = 0;
    [SerializeField] private FadeInOut fadeinout;

    [SerializeField] CameraMoveTest cameraMove;

    [SerializeField] private PlanetUI planetui;

    public Maintain_GameDatamanager maintain_gameDatamanager;

    private void Start()
    {
        planetui = FindObjectOfType<PlanetUI>();
        fadeinout = FindObjectOfType<FadeInOut>();
        playerCamera = FindObjectOfType<Camera>();
        cameraMove = playerCamera.GetComponent<CameraMoveTest>();
        maintain_gameDatamanager = FindObjectOfType<Maintain_GameDatamanager>();
    }
    private void OnMouseEnter()
    {
        isHover = true;
    }
    private void OnMouseExit()
    {
        isHover = false;
    }

    private void OnMouseUp()
    {
       //Debug.Log("Arrow요소 클릭한 경우!");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("충돌 부딪힌 대상체>>" + other.transform.name);
        // objMessage.SetActive(true);
        if (!maintain_gameDatamanager.visit_planetList[plantIndex])
        {
            objMessage.anchoredPosition = new Vector2(0, 0);
            TextMessage.text = $"{name} 행성으로 진입 하실거에요?";
            planetui.activePlanetIndex = plantIndex;
        }

        if (maintain_gameDatamanager.visit_planetList[plantIndex])
        {
            objMessage.anchoredPosition = new Vector2(0, 0);
            TextMessage.text = $"행성에 진입할 수 없습니다.";
            planetui.activePlanetIndex = plantIndex;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // objMessage.SetActive(false);
        objMessage.anchoredPosition = new Vector2(0, 1200);
    }
    /*public void UI_MoveScene_Close()
    {
        objMessage.SetActive(false);
    }*/
    public void SceneMove()
    {
        //Debug.Log("[[Planet Scene Move]]행성 씬이동>>" + moveSceneName);
        //SceneManager.LoadScene(moveSceneName);
        if (!maintain_gameDatamanager.visit_planetList[plantIndex])
        {
            Debug.Log("행성 방문한적없는 경우 행성 정상적 방문>");
            StartCoroutine(SceneLoad());
        }
        else
        {
            Debug.Log("행성 방문한적있는 경우 행성 들어갈수없다");
        }
    }
    IEnumerator SceneLoad()
    {
        cameraMove.CanControl = false;
        float waitTime = fadeinout.GetFadeTime();
        fadeinout.StartFadeOut();
        yield return new WaitForSeconds(waitTime);
        fadeinout.StartFadeIn();
        yield return new WaitForSeconds(waitTime);
        //Debug.Log("fadeinout효과가 모두 끝난 waitTime후에 씬 전환" + waitTime);
        //Debug.Log($"해당 {transform.name}행성 관련 씬으로 이동 [[Planet Scene Move]]>>" + moveSceneName);
        SceneManager.LoadScene(moveSceneName);
    }

    /*private void Update()
    {
        if(isHover == true)
        {
            //Arrow월드좌표를 화면 스크린좌표로 바꿔준다.
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);

            //나의 위치를 스크린좌표로 변경한 후 message의 위치를 변경
            objMessage.transform.position = screenPos;
        }
    }*/
}
