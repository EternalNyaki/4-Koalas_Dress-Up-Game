using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPassthrough : MonoBehaviour
{
    public void SetScene(string sceneName)
    {
        PlayerManager.Instance.SetScene(sceneName);
    }

    public void LogGameEnd()
    {
        TelemetryLogManager.Instance.GameEnd();
    }

    public void QuitGame()
    {
        PlayerManager.Instance.QuitGame();
    }

    public void LogGameStart()
    {
        TelemetryLogManager.Instance.GameStart();
    }
}
