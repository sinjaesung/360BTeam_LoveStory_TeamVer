using UnityEngine;

public abstract class TutorialBaseVer2 : MonoBehaviour
{
    // 해당 튜토리얼 과정을 시작할 때 1회 호출
    public abstract void Enter();

    // 해당 튜토리얼 과정을 진행하는 동안 매 프레임 호출
    public abstract void Execute(TutorialControllerVer2 controller);

    // 해당 튜토리얼 과정을 종료할 때 1회 호출
    public abstract void Exit();
}


