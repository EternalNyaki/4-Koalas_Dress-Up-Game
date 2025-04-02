using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text resultsText;

    void Start()
    {
        scoreText.text = "Score: " + PlayerManager.Instance.discoScore + " / " + PlayerManager.Instance.discoMaxScore;
        if ((float)PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore < 0.5f)
        {
            resultsText.text = "Better luck next time...";
        }
        else if ((float)PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore < 0.75f)
        {
            resultsText.text = "Success!";
        }
        else if ((float)PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore < 0.9f)
        {
            resultsText.text = "Groovy!!";
        }
        else
        {
            resultsText.text = "Radical!!!";
        }

        TelemetryLogManager.Instance.LogEvent(this, TelemetryLogManager.EventType.MicrogameComplete);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayerManager.Instance.SetScene("Overworld City");
        }
    }
}
