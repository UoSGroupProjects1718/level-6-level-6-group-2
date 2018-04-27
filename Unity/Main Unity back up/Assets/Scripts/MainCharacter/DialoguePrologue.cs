using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePrologue : MonoBehaviour {

    public Dialogue dialogue;

    void Start()
    {
        DialogueTrigger();

    }

    public void DialogueTrigger()
    {

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }
}
