using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.SceneView;
using UnityEngine.SceneManagement;

public class MainRoadMap : MonoBehaviour
{
    [SerializeField] private FadeInOut fadeinout;

    [SerializeField] private int moveSceneIndex = 0;
    void Start()
    {
        fadeinout = FindObjectOfType<FadeInOut>();
    }

    public void SceneMove()
    {
        Debug.Log("씬이동>>" + moveSceneIndex);
        //SceneManager.LoadScene(moveSceneIndex);
        StartCoroutine(SceneLoad());
    }
    IEnumerator SceneLoad()
    {
        float waitTime = fadeinout.GetFadeTime();
        fadeinout.StartFadeOut();
        yield return new WaitForSeconds(waitTime);
        fadeinout.StartFadeIn();
        yield return new WaitForSeconds(waitTime);
        Debug.Log("fadeinout효과가 모두 끝난 waitTime후에 씬 전환" + waitTime);
        SceneManager.LoadScene(moveSceneIndex);
    }
}
