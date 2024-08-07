using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LoveGameManager : MonoBehaviour
{
    private Player3d_Planet player;

    [SerializeField] private Image Character_Conversation_stateImage; //Ver1,2공통 사용하는,캐릭터범용 대화UI Sprite이미지
    [SerializeField] private Image LoveMonsterState;//Ver1에서만 사용하는 우측상단UI 몬스터이미지
    [SerializeField] private SpriteRenderer Sprite2DCharacterImage;//Ver1에서 맵상의 2D Sprite(Movement)

    [SerializeField] private Sprite[] images;

    [SerializeField] public Converstation activeConversation;//활성화 대화
    [SerializeField] public int activeConversationIndex = 0;//활성화대화인댁스
    [SerializeField] public bool isVer2Env = false;
    [SerializeField] private Converstation[] conversations;
    [SerializeField] private GameObject gameoverCon;
    [SerializeField] private GameObject group1;
    [SerializeField] private GameObject group2;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player3d_Planet>();
        //conversations = FindObjectsOfType<Converstation>();

        if (isVer2Env)
        {
            Array.Sort(conversations, (obj1, obj2) => obj1.ConversationIndex.CompareTo(obj2.ConversationIndex));
            for (int e = 0; e < conversations.Length; e++)
            {
                Debug.Log(e + "| [[LoveGameManager]] 초기화 Conversations개체들 Index순서대로 정렬 indexValue:" + conversations[e].ConversationIndex);
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isVer2Env)
        {
            activeConversation = conversations[activeConversationIndex];
        }

        if (player.LoveScore < -10)
        {
           // Debug.Log("[[LoveGameManager]]몬스터화남");
            if (Sprite2DCharacterImage != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                Sprite2DCharacterImage.sprite = images[0];
            }
            if (LoveMonsterState != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                LoveMonsterState.sprite = images[0];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//재현Ver 관련 행성씬 로직
                {
                    //다른 캐릭터화자와 공유하는 캐릭터이미지
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //플레이어가 아닌 캐릭터(몬스터등)이 말하는경우에만 동적으로 대화ui 화자 이미지 Swap
                        Character_Conversation_stateImage.sprite = images[0];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] 현재 Conversation name및 해당 개체에서 현재 말하고 있는 화자" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] 화자가 플레이어인 경우로 동적 할당 접근을 하지 않는 개체의 경우이다");
                    }
                }
                else//재성Ver 관련 행성씬 로직
                {
                    Character_Conversation_stateImage.sprite = images[0];
                }
            }
        }
        else if(player.LoveScore >=-10 && player.LoveScore< 0)
        {
            //Debug.Log("[[LoveGameManager]]몬스터놀람");
            if (Sprite2DCharacterImage != null)
            { 
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                Sprite2DCharacterImage.sprite = images[1];
            }
            if (LoveMonsterState != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                LoveMonsterState.sprite = images[1];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//재현Ver 관련 행성씬 로직
                {
                    //다른 캐릭터화자와 공유하는 캐릭터이미지
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //플레이어가 말하고 있는 경우에는 플레이어가 설정한 Sprite이미지를
                        //몬스터 현재 Swap이미지로 바꾸지 않는다.
                        Character_Conversation_stateImage.sprite = images[1];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] 현재 Conversation name및 해당 개체에서 현재 말하고 있는 화자" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] 화자가 플레이어인 경우로 동적 할당 접근을 하지 않는 개체의 경우이다");
                    }
                }
                else//재성Ver 관련 행성씬 로직
                {
                    Character_Conversation_stateImage.sprite = images[1];
                }
            }
        }
        else if(player.LoveScore >=0 && player.LoveScore < 10)
        {
           // Debug.Log("[[LoveGameManager]]몬스터평상시");
            if (Sprite2DCharacterImage != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                Sprite2DCharacterImage.sprite = images[2];
            }
            if (LoveMonsterState != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                LoveMonsterState.sprite = images[2];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//재현Ver 관련 행성씬 로직
                {
                    //다른 캐릭터화자와 공유하는 캐릭터이미지
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //플레이어가 말하고 있는 경우에는 플레이어가 설정한 Sprite이미지를
                        //몬스터 현재 Swap이미지로 바꾸지 않는다.
                        Character_Conversation_stateImage.sprite = images[2];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] 현재 Conversation name및 해당 개체에서 현재 말하고 있는 화자" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] 화자가 플레이어인 경우로 동적 할당 접근을 하지 않는 개체의 경우이다");
                    }
                }
                else//재성Ver 관련 행성씬 로직
                {
                    Character_Conversation_stateImage.sprite = images[2];
                }
            }
        }
        else if (player.LoveScore >= 10 && player.LoveScore < 26)
        {
           // Debug.Log("[[LoveGameManager]]몬스터지긋이");
            if (Sprite2DCharacterImage != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                Sprite2DCharacterImage.sprite = images[3];
            }
            if (LoveMonsterState != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                LoveMonsterState.sprite = images[3];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//재현Ver 관련 행성씬 로직
                {
                    //다른 캐릭터화자와 공유하는 캐릭터이미지
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //플레이어가 말하고 있는 경우에는 플레이어가 설정한 Sprite이미지를
                        //몬스터 현재 Swap이미지로 바꾸지 않는다.
                        Character_Conversation_stateImage.sprite = images[3];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] 현재 Conversation name및 해당 개체에서 현재 말하고 있는 화자" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] 화자가 플레이어인 경우로 동적 할당 접근을 하지 않는 개체의 경우이다");
                    }
                }
                else//재성Ver 관련 행성씬 로직
                {
                    Character_Conversation_stateImage.sprite = images[3];
                }
            }
        }
        else if (player.LoveScore >= 26)
        {
            //Debug.Log("[[LoveGameManager]]몬스터기쁨");
            if (Sprite2DCharacterImage != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                Sprite2DCharacterImage.sprite = images[4];
            }
            if (LoveMonsterState != null)
            {
                //독립적 몬스터 이미지(다른캐릭터화자와 공유하지 않음)
                LoveMonsterState.sprite = images[4];
            }
            if (Character_Conversation_stateImage != null)
            {
                if (isVer2Env)//재현Ver 관련 행성씬 로직
                {
                    //다른 캐릭터화자와 공유하는 캐릭터이미지
                    if (!activeConversation.isPlayerSpeak)
                    {
                        //플레이어가 말하고 있는 경우에는 플레이어가 설정한 Sprite이미지를
                        //몬스터 현재 Swap이미지로 바꾸지 않는다.
                        Character_Conversation_stateImage.sprite = images[4];
                    }
                    else if (activeConversation.isPlayerSpeak)
                    {
                        Debug.Log("[[LoveGameManager]] 현재 Conversation name및 해당 개체에서 현재 말하고 있는 화자" + activeConversation.transform.name + ">SpeakerName:" + activeConversation.NowSpeakername);
                        Debug.Log("[[LoveGameManager]] 화자가 플레이어인 경우로 동적 할당 접근을 하지 않는 개체의 경우이다");
                    }
                }
                else//재성Ver 관련 행성씬 로직
                {
                    Character_Conversation_stateImage.sprite = images[4];
                }
            }
        }

      
        if (player.LoveScore >= 10)
        {
            //Debug.Log("몬스터기쁨 결말 기쁨 결말 씬으로 이동 예정!!!");//DEBUG
        }
        else if(player.LoveScore < -8)
        {
            //Debug.Log("몬스터화남 결말 화남 결말 씬으로 이동 예정!!!");//DEBUG
        }           
    }

    public void GameOver()
    {
        Debug.Log("게임오버>>");      
        gameoverCon.SetActive(true);
        group1.SetActive(false);
        group2.SetActive(false);        
    }
    public void ReloadNowScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GotoStartScene()
    {
        SceneManager.LoadScene("RoadViewBase_SpaceTour");
    }

    public void BacktoMoveScene()
    {
        SceneManager.LoadScene("RoadViewBase_SpaceTour");
    }
}
