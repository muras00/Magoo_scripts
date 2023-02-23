using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject connect;

    public PlayerInteract player;

    public KeyCode startKey;
    public bool start = false;
    public bool read = false;

    private void Update()
    {
        if (connect.activeInHierarchy == true && start == false)
        {
            //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            start = true;
        }

        if (Input.GetKeyDown(startKey) && start == true)
        {
            //start = FindObjectOfType<DialogueManager>().DisplayNextSentence();
            if (start == false) {
                read = true;
                connect.SetActive(false);
            }
        }
    }
}

public enum Bekanntheit { Unknown, Known, Friendly } 
