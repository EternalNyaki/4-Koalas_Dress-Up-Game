using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : RPGMovement
{
    public AudioClip exitSound;

    private AudioSource _audioSource;

    protected override void Initialize()
    {
        base.Initialize();

        _audioSource = GetComponent<AudioSource>();
    }

    protected override void GetInputs()
    {
        base.GetInputs();

        //HACK: Temporary method to switch scenes for milestone #1, will change later
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //HACK: This is a really dumb stupid way of doing this and it sucks (also repeated code)
            StartCoroutine(LoadDelay(0.5f, 1));
        }
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
