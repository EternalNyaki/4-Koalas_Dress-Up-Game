using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dialogue Object script
//Begins dialogue on interaction
public class DialogueObject : InteractableObject
{
    //Ink story asset to play dialogue from
    public TextAsset dialogue;

    public override void Interact()
    {
        DialogueManager.Instance.SetStory(dialogue);
        PauseModeManager.Instance.SetPauseMode(PauseMode.Dialogue);
    }
}
