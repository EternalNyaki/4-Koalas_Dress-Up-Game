using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pause Mode enum
//Represents the paused/unpaused state of the scene
public enum PauseMode
{
    Unpaused,
    Dialogue
}

//Pause Mode Manager
//Class that manages pausing and unpausing objects in the scene
public class PauseModeManager : Singleton<PauseModeManager>
{
    //The current paused/unpaused state
    public PauseMode pauseMode;

    //Canvas holding the dialogue box
    public Canvas dialogueCanvas;
    //Reference to the script controlling the player's movement
    public RPGMovement playerMovement;

    protected override void Initialize()
    {
        base.Initialize();

        SetPauseMode(pauseMode);
    }

    /// <summary>
    /// Set Pause Mode method
    /// Sets the paused/unpaused state of the scene and updates objects accordingly
    /// </summary>
    /// <param name="mode"> The paused/unpaused state to change to </param>
    public void SetPauseMode(PauseMode mode)
    {
        switch (mode)
        {
            case PauseMode.Unpaused:
                dialogueCanvas.gameObject.SetActive(false);

                playerMovement.enabled = true;
                break;

            case PauseMode.Dialogue:
                dialogueCanvas.gameObject.SetActive(true);

                playerMovement.enabled = false;
                break;
        }

        pauseMode = mode;
    }
}
