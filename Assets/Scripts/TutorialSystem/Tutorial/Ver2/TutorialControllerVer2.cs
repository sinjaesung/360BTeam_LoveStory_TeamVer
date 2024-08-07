using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialControllerVer2 : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBaseVer2> tutorials;

    public TutorialBaseVer2 currentTutorial = null;
    public int currentIndex = -1;

    public bool isCompleted = false;

    public Player3d_Planet player;

    //SerializeField] private bool isDynamicScene = false;
    //튜토리얼객체 단위별 리스트가 모두 마무리시에 씬 이동할지 역할여부
    //[SerializeField] private bool isSceneMoveByCompleted = false;
    //[SerializeField] private string HappyEndingSceneName;
    //[SerializeField] private string SadEndingSceneName;
    [SerializeField] GameObject Target_Dialogue;

    public LoveGameManager lovegameManager;

    private void Start()
    {
        player = FindObjectOfType<Player3d_Planet>();

        lovegameManager = FindObjectOfType<LoveGameManager>();
    }

    private void Update()
    {
        if (currentTutorial != null)
        {
            currentTutorial.Execute(this);
        }
    }

    //이 객체의 외부에서 이것을 직접적으로 호출하는경우(다른 클래스에서 호출해야하는경우)
    public void SetNextTutorial()
    {
        Debug.Log("Conversation<->[[TutorialControllerVer2]] SetNextTutorial >> " + currentIndex);
        // 현재 튜토리얼의 Exit() 메소드 호출
        if (currentTutorial != null)
        {
            currentTutorial.Exit();
        }

        // 마지막 튜토리얼을 진행했다면 CompletedAllTutorials() 메소드 호출
        if (currentIndex >= tutorials.Count - 1)
        {
            CompletedAllTutorials();
            return;
        }

        // 다음 튜토리얼 과정을 currentTutorial로 등록
        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        // 새로 바뀐 튜토리얼의 Enter() 메소드 호출
        currentTutorial.Enter();
    }

    public void CompletedAllTutorials()
    {
        currentTutorial = null;
        isCompleted = true;
        // 행동 양식이 여러 종류가 되었을 때 코드 추가 작성
        // 현재는 씬 전환

        Debug.Log("Conversation<->[[TutorialControllerVer2]][CompletedAllTutorials]Complete All >> 다음 타깃 씬 이동>>>");//1회(맨 마지막 도달시 첫 한번만 실행)   

        /* if (isSceneMoveByCompleted)
         {
             if (isDynamicScene)
             {
                 if (player.LoveScore >= 10)
                 {
                     Debug.Log("[CompletedAllTutorials]해피앤딩씬으로 이동,씬별로 지정해둔 씬Name(happyScene)로 이동" + HappyEndingSceneName);
                     SceneManager.LoadScene(HappyEndingSceneName);
                 }
                 else if (player.LoveScore < -8)
                 {
                     Debug.Log("[CompletedAllTutorials]새드앤딩씬으로 이동,씬별로 지정해둔 씬Name(SadScene)로 이동" + SadEndingSceneName);
                     SceneManager.LoadScene(SadEndingSceneName);
                 }
             }
         }*/

        if (Target_Dialogue)
        {
            //Ver2에선,Ver1과 다르게 TutorialController안에서 다이아로그까지 전체 관리하는형태가 아닌
            //TutorialController따로, 다이아로그(Conversation대화) 따로 독립적으로 존재하여 이와같이 조치
            Debug.Log("Conversation<->[[TutorialControllerVer2]]Ver2Scene에서 임의 튜토리얼Controller 통 한개 (미니게임)의 마무리시마다, " +
                "연결된 다이아로그(Conversation) 연결"+Target_Dialogue.transform.name);
            Target_Dialogue.SetActive(true);
            Debug.Log("액션 튜토리얼통Wrapper 모두 끝나고 타깃 다음 다이아로그 넘어감 activeConversationIndex증가>>");
            lovegameManager.activeConversationIndex++;
            //튜토리얼->다이아로그 케이스
        }
    }
}


