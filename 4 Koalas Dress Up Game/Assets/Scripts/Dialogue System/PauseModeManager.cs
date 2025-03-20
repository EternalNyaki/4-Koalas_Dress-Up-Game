using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pause Mode enum
//Represents the paused/unpaused state of the scene
public enum PauseMode
{
    Unpaused,
    Dialogue,
    QuitMenu
}

//Pause Mode Manager
//Class that manages pausing and unpausing objects in the scene
public class PauseModeManager : Singleton<PauseModeManager>
{
    //The current paused/unpaused state
    public PauseMode pauseMode;

    //Canvas holding the dialogue box
    public GameObject textBox;
    //Canvas holding the quit menu
    public GameObject quitMenu;
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
                quitMenu.SetActive(false);
                textBox.SetActive(false);

                playerMovement.enabled = true;

                foreach (var v in FindObjectsOfType<InteractableObject>())
                {
                    v.enabled = true;
                }
                break;

            case PauseMode.Dialogue:
                quitMenu.SetActive(false);
                textBox.SetActive(true);

                playerMovement.enabled = false;

                foreach (var v in FindObjectsOfType<InteractableObject>())
                {
                    v.enabled = false;
                }
                break;

            case PauseMode.QuitMenu:
                quitMenu.SetActive(true);
                textBox.SetActive(false);

                playerMovement.enabled = false;

                foreach (var v in FindObjectsOfType<InteractableObject>())
                {
                    v.enabled = false;
                }
                break;
        }

        pauseMode = mode;
    }

    public void SetPauseMode(int pauseMode)
    {
        SetPauseMode((PauseMode)pauseMode);
    }
}
