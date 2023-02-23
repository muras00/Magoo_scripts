using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject dialoguePanel;
    public KeyCode startKey;

    NPCInteractable n;

    private void Update()
    {
        if (Input.GetKeyDown(startKey) && !dialoguePanel.activeInHierarchy) {
            float interactRange = 20f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray) {
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    dialoguePanel.SetActive(true);
                    n = GetInteractableObject();
                    Debug.Log("Array size : " + n.inkFileCount);
                    FindObjectOfType<DialogueManager>().Setup(n.inkFileArray[n.inkFileCount], n.name);
                    FindObjectOfType<DialogueManager>().Call(n);
                    break;
                }
            }
        }
        else if(Input.GetKeyDown(startKey) && dialoguePanel.activeInHierarchy)
        {
            FindObjectOfType<DialogueManager>().Call(n);
        }
    }

    public NPCInteractable GetInteractableObject() {
        List<NPCInteractable> npcInteractableList = new List<NPCInteractable>();
        float interactRange = 40f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                npcInteractableList.Add(npcInteractable);
            }
        }

        NPCInteractable closestNPCInteractable = null;
        foreach (NPCInteractable npcInteractable in npcInteractableList)
        {
            if (closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable.transform.position) <
                        Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    closestNPCInteractable = npcInteractable;
                }
            }
        }
        return closestNPCInteractable;
    }
}
