using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public Sprite cg;
    // public Image name;
    public Sprite speakerShape;//화자마다 다른 색깔의 화살표,테두리,이름
    [TextArea]
    public string dialogue;
    
    //public Sprite leftcg;
    //스크립트에서 동적으로 바뀌는건지(대화UI Sprite이미지),
    //Inspector에서 입력한 정보로 수동적으로 바뀌는건지여부
    public bool isDynamicCg = false;
}
public class Converstation : MonoBehaviour
{
    [SerializeField] Image sprite_CG;//캐릭터 대화창 몬스터sprite이미지(LoveGameManager에서 접근)
    [SerializeField] Image sprite_Box;
    [SerializeField] Image img_Speaker;//화자박스(테두리,화살표,이름)
    [SerializeField] TMP_Text txt_Dialogue;
    //[SerializeField] GameObject nameBox;
    [SerializeField] Image thinkCloud;//명령(미니게임)내릴때 사용되는 말풍선이미지
    [SerializeField] bool isNext = false;
    [SerializeField] int dialogueCnt = 0;
    [SerializeField] Dialogue[] dialogue;
    [SerializeField] public Player3d_Planet playercameracontroll;//대화가 진행중에는 카메라(플레이어)컨트롤 여부 지정
    public GameObject GameOverPanel;
    public GameObject goodEndingPanel;

    //말풍선(숨은그림찾기명령 말풍선)이 보이기 시작할 Conversation index값
    [SerializeField] private int ViewThinkCloud_startIndex;
    [SerializeField] private bool isCompleted = false;

    //클릭활성화 시킬 오브젝트(다이아로그 끝날시에 특수 활성화시킬)
    [SerializeField] public ClickEvent targetactive_ClickEvent;

    [SerializeField] public bool isCompleted_NextDialog = false;
    [SerializeField] GameObject Target_Dialogue;//다이아로그에서 -> 다음 다이아로그

    [SerializeField] public bool isAction_TargetTutorial = false;
    [SerializeField] public TutorialControllerVer2 TargetTutorial;//대화 끝났을시나 or 특정상황에 진행할 타깃 튜토리얼
    [SerializeField] public GameObject OXQuizPanel;

    [SerializeField] public bool isLastDialog = false;//다음씬(씬 엔딩,result)를 결정짓기 전의 마지막 대화 다이아로그 인지 여부

    [SerializeField] public bool isPlayerSpeak=true;//플레이어가 Inspector에서 지정한 Sprite(수동지정)으로 말하고있는경우,몬스터 등 다른캐릭터가 말할땐 Sprite동적지정
    [SerializeField] public string NowSpeakername = "";//이 개체에서 현재 말하고 있는 대상(화자)

    public int ConversationIndex = 0;//해당 객체의 인댁스값(이 개체 자체의 index 값) 여러 Conversation개체간의 번호 순번 지정>

    public LoveGameManager lovegameManager;

    [SerializeField] private bool isDynamicScene = false;
    //[SerializeField] private string HappyEndingSceneName;
    //[SerializeField] private string SadEndingSceneName;//오버엔딩(점수가 낮을경우) 보여지는 엔딩

    public Maintain_GameDatamanager maintain_gameDatamanager;
    public GameObject MarryMeSuggestUI;

    [SerializeField] private int mother_planetIndex;
    // Start is called before the first frame update
    void Start()
    {
        dialogueCnt = 0;
        playercameracontroll = FindObjectOfType<Player3d_Planet>();
        maintain_gameDatamanager = FindObjectOfType<Maintain_GameDatamanager>();
        lovegameManager = FindObjectOfType<LoveGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNext) //활성화가 되었을 때만 대사가 진행되도록
        {
            if (dialogueCnt >= ViewThinkCloud_startIndex)
            {
                if (thinkCloud != null && isAction_TargetTutorial == true)
                {
                    thinkCloud.gameObject.SetActive(true);//명령형 말풍선을 보이게 시작할 대화index값
                }
            }
            if (dialogueCnt > dialogue.Length) // 수정된 조건
            {
                ONOFF(false); // 대사가 끝남
                return; // 더 이상 진행하지 않도록 반환
            }
        }
    }

    public void ShowDialogue() //유니티UI대화창 클릭시에 다음 대화 진행되도록
    {
        ONOFF(true); //대화가 시작됨
        //dialogueCnt = 0;
        NextDialogue(); //호출되자마자 대사가 진행될 수 있도록 
    }
    private void ONOFF(bool _flag)
    {
        sprite_Box.gameObject.SetActive(_flag);//대화창박스활성화
        sprite_CG.gameObject.SetActive(_flag);//대화창몬스터Sprite활성화
        txt_Dialogue.gameObject.SetActive(_flag);//대화텍스트활성화
        //nameBox.gameObject.SetActive(_flag);//화자이름박스활성화

        if (thinkCloud != null && isAction_TargetTutorial == true)
        {
            thinkCloud.gameObject.SetActive(false);//말풍선 기본값 항상 숨김
        }

        if (isCompleted)
        {
            //각 Dialogue 분기의 마지막에 명시적으로 false로 감추는 역할
            if (thinkCloud != null && isAction_TargetTutorial == true)
            {
                thinkCloud.gameObject.SetActive(_flag);//말풍선 대화 마지막에 명시적으로 감추기
            }
        }

        isNext = _flag;
    }

    private void NextDialogue()
    {
        if (dialogueCnt < dialogue.Length) // 대화 인덱스가 범위를 벗어나지 않도록 체크
        {
            //Debug.Log("[[Conversation]] [[NextDialogue]] dialogueIndex>>" + dialogueCnt);
            // 첫번째 대사와 첫번째 cg부터 계속 다음 cg로 진행되면서 화면에 보이게 된다.
            txt_Dialogue.text = dialogue[dialogueCnt].dialogue;//대화텍스트출력
            //txt_Name.text = dialogue[dialogueCnt].name;//화자이름출력
            if (dialogue[dialogueCnt].cg != null)
            {
                if (dialogue[dialogueCnt].isDynamicCg == false)
                {
                    isPlayerSpeak = true;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //Inspector Dialogue 입력 프롬프트(수동)에 따라서 그 대화일때의 화자의 CgSprite값(사람이 지정한) 설정
                    //Debug.Log("[[Conversation]] sprite_CG name>" + sprite_CG.transform.name);
                    sprite_CG.sprite = dialogue[dialogueCnt].cg;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //Debug.Log("[[Conversation]] SpriteCG>" + dialogue[dialogueCnt].cg);
                }
                else if (dialogue[dialogueCnt].isDynamicCg == true)
                {
                    //플레이어가 아닌 몬스터(슬라임,문어,타노스)등이 말하는 경우(몬스터의 표정이 동적으로 바뀌는 기획개발컨셉)
                    isPlayerSpeak = false;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //이와같은 경우에는 수동지정이 아닌 외부 클래스(스크립트)등에서 참조하여 접근 동적 Swap
                }
                sprite_CG.gameObject.SetActive(true); // 스프라이트가 있으면 활성화
            }
            else
            {
                if (dialogue[dialogueCnt].isDynamicCg == false)
                {
                    isPlayerSpeak = true;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //Inspector Dialogue 입력 프롬프트(수동)에 따라서 그 대화일때
                    //화자 CgSprite값이 없다면 해당 대화에서 이미지 사용안하겠다(수동)
                    sprite_CG.gameObject.SetActive(false); // 스프라이트가 없으면 비활성화             
                }
                else if (dialogue[dialogueCnt].isDynamicCg == true)
                {
                    //플레이어가 아닌 몬스터(슬라임,문어,타노스)등이 말하는 경우(몬스터의 표정이 동적으로 바뀌는 기획개발컨셉)
                    isPlayerSpeak = false;
                    img_Speaker.sprite = dialogue[dialogueCnt].speakerShape;
                    //NowSpeakername = dialogue[dialogueCnt].name;
                    //해당 대화 순서에서 만약 DynamicCg로써 자동으로 외부에서 SpriteCg에 접근하여 이미지Swap하는
                    //케이스라면 이미지가 활성화 되어있어야만 한다.
                    sprite_CG.gameObject.SetActive(true);
                }
            }

            //대화창 클릭시점(명시적 기본값 말풍선은 항상 숨기고)->이후 dialogueCnt >= ViewThinkCloud_startIndex 조건 Update백그라운드 말풍선활성화
            if (thinkCloud != null && isAction_TargetTutorial == true)
            {
                thinkCloud.gameObject.SetActive(false);
            }

            dialogueCnt++; // 다음 대사와 cg가 나오도록
            playercameracontroll.IsMoved = false;//대화도중엔 플레이어 카메라조작 X
        }
        else if (dialogueCnt >= dialogue.Length)
        {
            ONOFF(false); // 대사가 끝났다면 UI를 비활성화
            playercameracontroll.IsMoved = true;//대화가 끝났으면 카메라조작 O
            isCompleted = true;

            isPlayerSpeak = false;
            NowSpeakername = "";

            //대화 끝 이후 특수활성화하여 연결시킬 오브젝트가 있다면
            if (targetactive_ClickEvent != null)
            {
                targetactive_ClickEvent.CanClick = true;
            }
            //다이아로그->다이아로그 케이스
            if (isCompleted_NextDialog)
            {
                if (Target_Dialogue != null)
                {
                    Target_Dialogue.SetActive(true);
                    //Debug.Log("[[Conversation]] 다음 대화 다이아로그로 넘어감 activeConversationIndex증가>>");
                    lovegameManager.activeConversationIndex++;
                }
            }
            //다이아로그->튜토리얼 진행시작 케이스
            if (isAction_TargetTutorial)
            {
                if (TargetTutorial != null)
                {
                    //타노스랑,슬라임 미니게임 활성화
                    //Debug.Log("[[Conversation]] 다음 액션 튜토리얼로 넘어감 activeConversationIndex증가>>");
                    TargetTutorial.SetNextTutorial();
                }
                if (OXQuizPanel != null)
                {
                    //문어 미니게임(OX) 활성화
                    OXQuizPanel.SetActive(true);
                }
            }

            //마지막 대화 다이아로그(Conversation)에서 결과에 따라서 새드,해피 앤딩 여부 분기 이동>>
            if (isLastDialog)
            {
                if (isDynamicScene)
                {
                    if (playercameracontroll.LoveScore >= 10)
                    {
                        //Debug.Log("[[Conversation]]몬스터기쁨 결말 기쁨 결말 씬으로 이동 예정!!!");//DEBUG
                        //SceneManager.LoadScene(HappyEndingSceneName);
                        MarryMeSuggestUI.SetActive(true);
                        maintain_gameDatamanager.visit_planetList[mother_planetIndex] = true;
                    }

                    if(playercameracontroll.HeartCount_ <= 0)
                    {
                        GameOverPanel.SetActive(true);
                        maintain_gameDatamanager.visit_planetList[mother_planetIndex] = true;
                    }
                }
            }
        }
    }

    public void LoveAccept_Action()
    {
        if (isLastDialog)
        {
            //Debug.Log("결혼 승낙 해피앤딩 씬 이동>>");
            //SceneManager.LoadScene(HappyEndingSceneName);
            goodEndingPanel.SetActive(true);
            MarryMeSuggestUI.SetActive(false);
            playercameracontroll.HealthSlider.gameObject.SetActive(false);

        }
    }
    public void LoveRefuse_Action()
    {
        if (isLastDialog)
        {
            //Debug.Log("결혼 거절 행성 밖 씬 이동,거절 횟수 +1 갱신");
            maintain_gameDatamanager.SetRefuseCount(1);
            SceneManager.LoadScene("RoadViewBase_SpaceTour");
        }
    }
}
