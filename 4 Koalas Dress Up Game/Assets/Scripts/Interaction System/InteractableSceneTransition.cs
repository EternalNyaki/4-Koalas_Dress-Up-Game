using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSceneTransition : InteractableObject
{
    public string sceneName;

    public override void Interact()
    {
        base.Interact();

        PlayerManager.Instance.SetScene(sceneName);
    }
}
