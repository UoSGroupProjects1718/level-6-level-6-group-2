using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {

    private Queue<string> npcText;

	// Use this for initialization
	void Start ()
    {
        npcText = new Queue<string>(); 
	}
	
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting This Conversation" + dialogue.npc);

        npcText.Clear();

        foreach (string story in dialogue.npcText)
        {
            npcText.Enqueue(story);
        }
        DisplayNextStory();
    }

    public void DisplayNextStory()
    {
        if(npcText.Count == 0)
        {
            EndDialogue();
            return;
        }

        string story = npcText.Dequeue();
        Debug.Log(story);
    
    }

    void EndDialogue()
    {
        Debug.Log("End Of Dialogue");
    }

}
