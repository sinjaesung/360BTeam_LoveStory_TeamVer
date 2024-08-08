using UnityEngine;

public class TutorialDestroyTagObjects : TutorialBase
{
    //타노스 미니게임
    [SerializeField]
    private Player3d_Planet playerController;
    [SerializeField]
    private GameObject[] objectList;
    [SerializeField]
    private string tagName;

    public override void Enter()
    {
        Debug.Log("[[타노스미니게임]] TutorialDestroyTagObjects Enter>>");
        // 플레이어의 이동, 공격이 가능하도록 설정
        playerController.IsMoved = true;
        playerController.IsAttacked = true;

        // 파괴해야할 오브젝트들을 활성화
        for (int i = 0; i < objectList.Length; ++i)
        {
            objectList[i].SetActive(true);
        }
    }

    public override void Execute(TutorialController controller)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
        Debug.Log("[[타노스미니게임]] TutorialDestroyTagObjects Excute");

        if (objects.Length == 0)
        {
            Debug.Log("[[타노스미니게임]] TutorialDestroyTagObjects Excute 해당 튜토리얼 적군 모두 섬멸[[End]]");
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("[[타노스미니게임]] TutorialDestroyTagObjects Exit>>");
        playerController.IsMoved = false;
        playerController.IsAttacked = false;
    }
}