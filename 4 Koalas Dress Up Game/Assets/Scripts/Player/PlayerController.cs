using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : RPGMovement
{

    public AudioClip exitSound;

    public float positionLogInterval = 1f;

    private AudioSource _audioSource;

    private float _positionLogTimer;

    protected override void Initialize()
    {
        base.Initialize();

        _audioSource = GetComponent<AudioSource>();

        _positionLogTimer = 0f;
    }

    protected override void GetInputs()
    {
        base.GetInputs();

        if (_positionLogTimer > positionLogInterval)
        {
            TelemetryLogManager.Instance.LogPlayerPosition(this, transform.position);

            _positionLogTimer -= positionLogInterval;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //HACK: This is a really dumb stupid way of doing this and it sucks (also repeated code)
            StartCoroutine(LoadDelay(0.5f, 1));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseModeManager.Instance.SetPauseMode(PauseMode.QuitMenu);
        }

        _positionLogTimer += Time.deltaTime;
    }

    private IEnumerator LoadDelay(float delayTime, int sceneID)
    {
        _audioSource.PlayOneShot(exitSound);
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneID);
    }

    protected override void OnStartMove()
    {
        base.OnStartMove();

        _audioSource.Play();
    }

    protected override void OnEndMove()
    {
        base.OnEndMove();

        _audioSource.Stop();
    }
}
