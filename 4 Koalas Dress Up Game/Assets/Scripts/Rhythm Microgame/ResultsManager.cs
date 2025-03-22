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
        if (PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore < 0.5f)
        {
            resultsText.text = "Better luck next time...";
        }
        else if (PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore < 0.75f)
        {
            resultsText.text = "Success!";
        }
        else if (PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore < 0.9f)
        {
            resultsText.text = "Groovy!!";
        }
        else
        {
            resultsText.text = "Radical!!!";
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            PlayerManager.Instance.SetScene("Overworld City");
        }
    }
}
