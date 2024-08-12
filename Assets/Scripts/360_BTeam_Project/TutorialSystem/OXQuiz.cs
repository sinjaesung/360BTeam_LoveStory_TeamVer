using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quiz
{
    [TextArea]
    public string question = " ";
    [Header("정답이 O면체크")]
    public bool correctAnswer; // 정답이 O인지 X인지 (O이면 true, X이면 false)
}

public class OXQuiz : MonoBehaviour
{
    [SerializeField] Button buttonO;
    [SerializeField] Button buttonX;
    [SerializeField] TMP_Text questionText;
    [SerializeField] Quiz[] quiz;
    [SerializeField] public Player3d_Planet player;
    [SerializeField] GameObject GameOverCon;
    [SerializeField] GameObject NextCon;
    [SerializeField] Image cgImg;  // 퀴즈 결과에 따른 이미지를 표시하기 위한 Image 컴포넌트
    [SerializeField] Sprite Correct_Cg;
    [SerializeField] Sprite Wrong_Cg;
    private int quizCountIndex = 0;

    private void Start()
    {
        quizCountIndex = 0;
        DisplayCurrentQuiz();

        // 버튼 클릭 이벤트 등록
        buttonO.onClick.AddListener(() => OnAnswerSelected(true));
        buttonX.onClick.AddListener(() => OnAnswerSelected(false));
    }

    private void Update()
    {
        if (player.HeartCount_ <= 0)
        {
            GameOver();
        }
    }

    // 현재 퀴즈를 표시
    private void DisplayCurrentQuiz()
    {
        if (quizCountIndex < quiz.Length)
        {
            questionText.text = quiz[quizCountIndex].question;
        }
        else
        {
            // 퀴즈가 끝났을 때 처리
            this.gameObject.SetActive(false);
            NextCon.SetActive(true);
        }
    }

    // 사용자가 답을 선택했을 때 호출되는 메서드
    private void OnAnswerSelected(bool userAnswer)
    {
        if (quiz[quizCountIndex].correctAnswer == userAnswer)
        {
            CorrectAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    public void NextQuiz()
    {
        quizCountIndex++;
        if (quizCountIndex < quiz.Length)
        {
            DisplayCurrentQuiz();  // 다음 퀴즈를 표시
        }
        else
        {
            // 퀴즈가 모두 끝났을 때 처리
            this.gameObject.SetActive(false);
            NextCon.SetActive(true);
        }
    }

    public void CorrectAnswer()
    {
        cgImg.sprite = Correct_Cg;
        NextQuiz();
    }

    public void WrongAnswer()
    {
        cgImg.sprite = Wrong_Cg;
        player.HeartCount_ -= 1;
        NextQuiz();
    }

    public void GameOver()
    {
        this.gameObject.SetActive(false);
        GameOverCon.SetActive(true);
    }
}