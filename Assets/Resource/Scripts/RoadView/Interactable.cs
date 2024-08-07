using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Interactable object TriggerEnter objectsss" + other.transform.tag);

        if (other.transform.tag == "Player")
        {
            Interact();
        }
    }
   
    public virtual void Interact()
    {
        Debug.Log("Interacting with base class.");
    }
}