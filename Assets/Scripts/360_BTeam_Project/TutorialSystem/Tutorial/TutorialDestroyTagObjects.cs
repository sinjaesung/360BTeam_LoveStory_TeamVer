using UnityEngine;

public class TutorialDestroyTagObjects : TutorialBase
{
    //Ÿ�뽺 �̴ϰ���
    [SerializeField]
    private Player3d_Planet playerController;
    [SerializeField]
    private GameObject[] objectList;
    [SerializeField]
    private string tagName;

    public override void Enter()
    {
        Debug.Log("[[Ÿ�뽺�̴ϰ���]] TutorialDestroyTagObjects Enter>>");
        // �÷��̾��� �̵�, ������ �����ϵ��� ����
        playerController.IsMoved = true;
        playerController.IsAttacked = true;

        // �ı��ؾ��� ������Ʈ���� Ȱ��ȭ
        for (int i = 0; i < objectList.Length; ++i)
        {
            objectList[i].SetActive(true);
        }
    }

    public override void Execute(TutorialController controller)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
        Debug.Log("[[Ÿ�뽺�̴ϰ���]] TutorialDestroyTagObjects Excute");

        if (objects.Length == 0)
        {
            Debug.Log("[[Ÿ�뽺�̴ϰ���]] TutorialDestroyTagObjects Excute �ش� Ʃ�丮�� ���� ��� ����[[End]]");
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("[[Ÿ�뽺�̴ϰ���]] TutorialDestroyTagObjects Exit>>");
        playerController.IsMoved = false;
        playerController.IsAttacked = false;
    }
}