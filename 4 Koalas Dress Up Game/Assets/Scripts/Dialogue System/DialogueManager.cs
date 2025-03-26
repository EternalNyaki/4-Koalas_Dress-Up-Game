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

    /// <summary>
    /// Reset the current Ink story and set the given Ink JSON asset as the current story
    /// </summary>
    /// <param name="inkStory"></param>
    public void SetStory(TextAsset inkStory)
    {
        story?.ResetState();

        //Load story object
        story = new Story(inkStory.text);
        mainText.text = story.Continue();

        Debug.Log(inkStory.name);
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
                LoadNextDialogue();
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
                story.ChooseChoiceIndex(_selectedChoice);
                LoadNextDialogue();
            }
        }
        else
        {
            //If there is no more dialogue, resume game on interaction
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                ResetStory();
            }
        }

        //Tags-based logic
        if (story.currentTags.Contains("disco") && !PlayerManager.Instance.IsWearingDiscoOutfit())
        {
            //#disco tag logic
            if (story.canContinue)
            {
                LoadNextDialogue(false);
            }
            else
            {
                ResetStory(false);
            }
        }
        else if (story.currentTags.Contains("casual") && !PlayerManager.Instance.IsWearingCasualOutfit())
        {
            //#casual tag logic
            if (story.canContinue)
            {
                LoadNextDialogue(false);
            }
            else
            {
                ResetStory(false);
            }
        }
        else if (story.currentTags.Contains("microgame") && Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            //#microgame tag logic
            TelemetryLogManager.Instance.LogEvent(this, TelemetryLogManager.EventType.MicrogameStart);

            PlayerManager.Instance.SetScene("Rhythm Microgame");
        }
    }

    public void LoadNextDialogue(bool playSound = true)
    {
        if (playSound) { _audioSource.PlayOneShot(interactSound); }

        mainText.text = story.Continue();

        Debug.Log("Dialogue continued");
    }

    public void ResetStory(bool playSound = true)
    {
        if (playSound) { _audioSource.PlayOneShot(interactSound); }

        story.ResetState();
        story = null;

        PauseModeManager.Instance.SetPauseMode(PauseMode.Unpaused);

        Debug.Log("Dialogue reset");
    }
}
