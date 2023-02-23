using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject textBox;
    public GameObject[] customButtons;
    public GameObject optionPanel;

    static Story story;
    TextMeshProUGUI nameText;
    TextMeshProUGUI dialogueText;
    static Choice choiceSelected;

    public KeyCode startKey;
    string name = "Test Player";

    // Start is called before the first frame update
    public void Setup(TextAsset inkFile, string name)
    {
        story = new Story(inkFile.text);
        this.name = name;
        nameText = textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        choiceSelected = null;
    }

    // Update is called once per frame
    public void Call(NPCInteractable n)
    {
        if (story.canContinue)
        {
            nameText.text = name;
            DisplayNextDialogue();
            if (story.currentChoices.Count != 0)
            {
                StartCoroutine(ShowChoices());
            }
        }
        else
        {
            FinishDialogue(n);
        }
    }

    void DisplayNextDialogue()
    {
        string currentSentence = story.Continue();
        dialogueText.text = currentSentence;
        //StopAllCoroutines();
        //StartCoroutine(AdvanceDialogue(currentSentence));
    }

    private void FinishDialogue(NPCInteractable n) {
        Debug.Log("End of Dialogue : " + name);
        textBox.SetActive(false);
        if (n.inkFileCount != n.inkFileSize)
        {
            n.inkFileCount += 1;
        }
        //move later to advance from decision
        /*
        else
        {
            optionPanel.SetActive(false);
            for (int i = 0; i < optionPanel.transform.childCount; i++)
            {
                Destroy(optionPanel.transform.GetChild(i).gameObject);
            }
        }
        */
        for(int i = 0; i < story.currentChoices.Count; i++)
        {
            int pos = 100 + (i * -100);
            customButtons[i].transform.position = new Vector3(0, pos, 0);
        }
    }

    IEnumerator AdvanceDialogue(string sentence) {
        dialogueText.text = sentence;
        yield return null;
    }

    IEnumerator ShowChoices()
    {
        Debug.Log("There are choices need to be made here!");
        List<Choice> _choices = story.currentChoices;

        for (int i = 0; i < _choices.Count; i++)
        {
            float pos = i * 100.0f;
            customButtons[i].transform.position += Vector3.down * pos;
            GameObject temp = Instantiate(customButtons[i], optionPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            //temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide(); });
            temp.GetComponent<Button>().onClick.AddListener(testOnClick);
        }

        optionPanel.SetActive(true);

        yield return new WaitUntil(() => { return choiceSelected != null; });

        AdvanceFromDecision();
    }

    public void testOnClick()
    {
        Debug.Log("clicked");
    }

    public static void SetDecision(object element)
    {
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null; // Forgot to reset the choiceSelected. Otherwise, it would select an option without player intervention.
        DisplayNextDialogue();
    }

}
