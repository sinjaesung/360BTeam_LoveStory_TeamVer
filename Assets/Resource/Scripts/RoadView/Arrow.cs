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

    //[SerializeField] private int loadSceneIndex = 5;
    //Arrow는 클릭시 해당 방향으로(해당 Arrow의 근방위치로 카메라를 이동시키는연산 수행)
    //Arrow별 이동시키는 방향벡터 x,z방향 벡터를 지정해두고 클릭시 그 방향으로 이동하게끔(카메라)
    [SerializeField] private Vector3 move_direction = new Vector3(0, 0, 0);

    [SerializeField] private Camera playerCamera;

    //[SerializeField] private int moveSceneIndex = 0;
    [SerializeField] private string moveSceneName;
    [SerializeField] private int plantIndex = 0;
    [SerializeField] private FadeInOut fadeinout;

    [SerializeField] CameraMoveTest cameraMove;

    [SerializeField] private PlanetUI planetui;

    private void Start()
    {
        planetui = FindObjectOfType<PlanetUI>();
        fadeinout = FindObjectOfType<FadeInOut>();
        playerCamera = FindObjectOfType<Camera>();
        cameraMove = playerCamera.GetComponent<CameraMoveTest>();
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
       Debug.Log("Arrow요소 클릭한 경우!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("충돌 부딪힌 대상체>>" + other.transform.name);
        // objMessage.SetActive(true);
        objMessage.anchoredPosition = new Vector2(0, 0);
        TextMessage.text = $"{name} 행성으로 진입 하실거에요?";
        planetui.activePlanetIndex = plantIndex;
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
        Debug.Log("[[Planet Scene Move]]행성 씬이동>>" + moveSceneName);
        //SceneManager.LoadScene(moveSceneName);
        StartCoroutine(SceneLoad());
    }
    IEnumerator SceneLoad()
    {
        cameraMove.CanControl = false;
        float waitTime = fadeinout.GetFadeTime();
        fadeinout.StartFadeOut();
        yield return new WaitForSeconds(waitTime);
        fadeinout.StartFadeIn();
        yield return new WaitForSeconds(waitTime);
        Debug.Log("fadeinout효과가 모두 끝난 waitTime후에 씬 전환" + waitTime);
        Debug.Log($"해당 {transform.name}행성 관련 씬으로 이동 [[Planet Scene Move]]>>" + moveSceneName);
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
