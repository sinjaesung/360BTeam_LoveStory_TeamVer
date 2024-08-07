using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public GameObject dialoguePanel;
    public string npcName;
    public List<string> dialogueLines = new List<string>();
    Button continueButton;
    TextMeshProUGUI dialogueText, nameText;
    int dialogueIndex;

    [SerializeField] CameraMoveTest cameraMove;

    // public PortalNPC portalnpc;
    //Use this for initialization
    private void Awake()
    {
        cameraMove = FindObjectOfType<CameraMoveTest>();

        // if (FindObjectOfType<PlayerDialogRefer>() != null)
        //{
        Debug.Log("DialogueSystem dialoguePanel:" + dialoguePanel.name);
            continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
            dialogueText = dialoguePanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<TextMeshProUGUI>();
            Debug.Log("dialogueSystem:" + dialoguePanel);
        //}
        //최초생성된 Instance가 저장되어있을것이고, 이후에 또 이게 실행되면
        //Instance가 추가생성될텐데 추가 생성을 막기위해 사용
        Debug.Log("Instance생성:" + Instance);

        Instance = this;

        StartCoroutine(StartSetup());
    }

    private IEnumerator StartSetup()
    {
        yield return new WaitForSeconds(0.1f);
        //if (FindObjectOfType<PlayerDialogRefer>() != null)
        //{
            Debug.Log("DialogueSystem dialoguePanel:" + dialoguePanel.name);
            continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
            dialogueText = dialoguePanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<TextMeshProUGUI>();
            Debug.Log("StartSetup ContinueButton이벤트 위임:" + dialoguePanel.name + "," + continueButton.name + "," + dialogueText.name + "," + continueButton.name);
            continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
            Debug.Log("dialogueSystem:" + dialoguePanel);
            dialoguePanel.SetActive(false);
       // }

        yield return null;
    }
    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>();
        foreach (string line in lines)
        {
            dialogueLines.Add(line);
            Debug.Log("AddNewDialogue:" + line);

        }
        this.npcName = npcName;

        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }
    }
    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void ContinueDialogue()
    {
        Debug.Log("DioalogSystem ContinueDialogue" + dialogueIndex + "/" + dialogueLines.Count);
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
                cameraMove.CanControl = true;
            }
        }

    }
}