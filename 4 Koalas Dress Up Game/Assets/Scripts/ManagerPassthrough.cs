using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: This class is gonna need some better templating or smth, this is jank af
public class ManagerPassthrough : MonoBehaviour
{
    public void SetScene(string sceneName)
    {
        PlayerManager.Instance.SetScene(sceneName);
    }

    public void QuitGame()
    {
        PlayerManager.Instance.QuitGame();
    }
}
