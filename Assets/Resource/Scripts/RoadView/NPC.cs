using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public string[] dialogue;
    public string npcname;

    [SerializeField] CameraMoveTest cameraMove;
    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        playerCamera = FindObjectOfType<Camera>();
        cameraMove = playerCamera.GetComponent<CameraMoveTest>();
    }
    public override void Interact()
    {
        DialogueSystem.Instance.AddNewDialogue(dialogue, npcname);
        Debug.Log("Interacting with NPC.");

        cameraMove.CanControl = false;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Interactable object TriggerExit objectsss" + other.transform.tag);

        DialogueSystem.Instance.CloseDialogue();
        cameraMove.CanControl = true;
    }

}