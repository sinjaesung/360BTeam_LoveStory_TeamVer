using UnityEngine;

public class TutorialFadeEffectVer2 : TutorialBaseVer2
{
    [SerializeField]
    private FadeEffect fadeEffect;
    [SerializeField]
    private bool isFadeIn = false;
    private bool isCompleted = false;

    public override void Enter()
    {
        Debug.Log("TutorialFadeEffectVer2 Enter>>");
        if (isFadeIn == true)
        {
            fadeEffect.FadeIn(OnAfterFadeEffect);
        }
        else
        {
            fadeEffect.FadeOut(OnAfterFadeEffect);
        }
    }

    private void OnAfterFadeEffect()
    {
        isCompleted = true;
    }

    public override void Execute(TutorialControllerVer2 controller)
    {
        if (isCompleted == true)
        {
            controller.SetNextTutorial();
        }
    }

    public override void Exit()
    {
        Debug.Log("TutorialFadeEffectVer2 Exit>>");
    }
}
