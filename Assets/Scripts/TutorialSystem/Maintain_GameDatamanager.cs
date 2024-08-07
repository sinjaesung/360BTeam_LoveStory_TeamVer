using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Maintain_GameDatamanager : MonoBehaviour
{
    public static Maintain_GameDatamanager instance = null;
    public GameObject LoveRefuseDataObject;
    private bool isMoved = false;
    private void Awake()
    {
        Debug.Log("[[Maintain_GameDatamanager]]Awake 첫 Origin씬에서 유일 존재하고있는 개체여야하고, 그 씬에만 배치했다고 가정시에" +
            "그 씬으로 다시 돌아와야만 이게 다시 실행되고, Origin씬 이후로 삭제되지 않고 계속 존속");
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    //게임 내에서 씬 이동시 유지하고있는 관련 데이터(거절횟수)
    public int RefuseCount = 0;
    public bool[] visit_planetList;

    public void SetRefuseCount(int amount)
    {
        RefuseCount += amount;
    }
    public int GetRefuseCount()
    {
        return RefuseCount;
    }

    private void Update()
    {
        LoveRefuseDataObject.GetComponent<TMP_Text>().text = "사랑거절 횟수:" + RefuseCount;

        if(RefuseCount >= 3)
        {
            if (!isMoved)
            {
                Debug.Log("[[Maintain_GameDatamanager]] 3개 다 거절 한경우 관련씬으로 보낸다>>");
                SceneManager.LoadScene("LonelyEnding");//3개 모두 거절한 경우 보낸다.
            }

            isMoved = true;
        }
    }
}
