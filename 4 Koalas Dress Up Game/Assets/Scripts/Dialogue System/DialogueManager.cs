using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;

//Dialogue Manager
//Handles reading dialogue from an Ink story asset and writing it to the dialogue box
public class DialogueManager : Singleton<DialogueManager>
{
    //Ink story asset to read dialogue from
    public TextAsset inkAsset;
    //UI text to write dialogue to
    public TMP_Text mainText;

    //Audio clip to play when the player presses the interact button
    public AudioClip interactSound;

    //Story object loaded from Ink asset
    private Story story;

    //Currently selected dialogue choice
    private int _selectedChoice = 0;

    //Audio source component
    private AudioSource _audioSource;

    protected override void Initialize()
    {
        _audioSource = GetComponent<AudioSource>();

        base.Initialize();

        SetStory(inkAsset);
    }

    public void SetStory(TextAsset inkStory)
    {
        //Load story object
        story = new Story(inkStory.text);
        mainText.text = story.Continue();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseModeManager.Instance.pauseMode != PauseMode.Dialogue) { return; }

        if (story.canContinue)
        {
            //Play next line of dialogue on interaction
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                _audioSource.PlayOneShot(interactSound);
                mainText.text = story.Continue();
            }
        }
        else if (story.currentChoices.Count > 0)
        {
            //Navigate dialogue options
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _selectedChoice--;
                _selectedChoice = Mathf.Clamp(_selectedChoice, 0, story.currentChoices.Count - 1);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _selectedChoice++;
                _selectedChoice = Mathf.Clamp(_selectedChoice, 0, story.currentChoices.Count - 1);
            }

            //Write dialogue options to UI
            mainText.text = story.currentText + (story.currentText == "" ? "" : "\n \n");
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                if (i == _selectedChoice)
                {
                    mainText.text += "> ";
                }
                mainText.text += story.currentChoices[i].text + "\n";
            }

            //Select dialogue option on interaction
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                _audioSource.PlayOneShot(interactSound);
                story.ChooseChoiceIndex(_selectedChoice);
                mainText.text = story.Continue();
            }
        }
        else
        {
            //If there is no more dialogue, resume game on interaction
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                _audioSource.PlayOneShot(interactSound);
                story.ResetState();
                PauseModeManager.Instance.SetPauseMode(PauseMode.Unpaused);
            }
        }

        if (story.currentTags.Contains("Disco Outfit") && !PlayerManager.Instance.IsWearingDiscoOutfit())
        {
            if (story.canContinue)
            {
                mainText.text = story.Continue();
            }
            else
            {
                story.ResetState();
                PauseModeManager.Instance.SetPauseMode(PauseMode.Unpaused);
            }
        }
        if (story.currentTags.Contains("Casual Outfit") && !PlayerManager.Instance.IsWearingCasualOutfit())
        {
            if (story.canContinue)
            {
                mainText.text = story.Continue();
            }
            else
            {
                story.ResetState();
                PauseModeManager.Instance.SetPauseMode(PauseMode.Unpaused);
            }
        }
        if (story.currentTags.Contains("Start Rhythm Microgame") && Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            PlayerManager.Instance.SetScene("Rhythm Microgame");
        }
    }
}
