using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    //Default outfit
    public ClothingItem defaultHat;
    public ClothingItem defaultShirt;
    public ClothingItem defaultPants;
    public ClothingItem defaultShoes;

    //Current outfit
    public ClothingItem hat { get; private set; }
    public ClothingItem shirt { get; private set; }
    public ClothingItem pants { get; private set; }
    public ClothingItem shoes { get; private set; }

    public int discoScore { get; private set; }
    public int discoMaxScore { get; private set; }

    //Outfit Changed event
    public event EventHandler<ClothingType> OutfitChanged;

    public AudioClip exitSound;

    private Vector2 _savedPosition = new(0f, 0f);

    private AudioSource _audioSource;

#if UNITY_EDITOR
    //OnValidate is called whenever the script is loaded or a value changes in the Inspector (Editor-only)
    private void OnValidate()
    {
        //Validate clothing types for each clothing slot
        //TODO: Update warning messages for clarity
        if (defaultHat?.type != ClothingType.Hat)
        {
            Debug.LogError("Hat must be a hat");
            defaultHat = null;
        }
        if (defaultShirt?.type != ClothingType.Shirt)
        {
            Debug.LogError("Shirt must be a shirt");
            defaultShirt = null;
        }
        if (defaultPants?.type != ClothingType.Pants)
        {
            Debug.LogError("Pants must be pants");
            defaultPants = null;
        }
        if (defaultShoes?.type != ClothingType.Shoes)
        {
            Debug.LogError("Shoes must be shoes");
            defaultShoes = null;
        }
    }
#endif

    protected override void Initialize()
    {
        base.Initialize();

        DontDestroyOnLoad(gameObject);

        //Get components
        _audioSource = GetComponent<AudioSource>();

        //Initialize outfit
        hat = defaultHat;
        shirt = defaultShirt;
        pants = defaultPants;
        shoes = defaultShoes;

        discoScore = 0;
    }

    //Set Clothing method
    //Sets the clothing item for the slot of the type of the given item
    public void SetClothing(ClothingItem item)
    {
        switch (item.type)
        {
            case ClothingType.Hat:
                hat = item;
                break;

            case ClothingType.Shirt:
                shirt = item;
                break;

            case ClothingType.Pants:
                pants = item;
                break;

            case ClothingType.Shoes:
                shoes = item;
                break;
        }

        OnOutfitChanged(item.type);
    }

    public bool IsWearingDiscoOutfit()
    {
        return hat.name == "Afro" &&
               shirt.name == "DiscoShirt" &&
               pants.name == "DiscoPants" &&
               shoes.name == "DiscoShoes";
    }

    public bool IsWearingCasualOutfit()
    {
        return hat.name == "TestHat" &&
               shirt.name == "TestShirt" &&
               pants.name == "TestPants" &&
               shoes.name == "TestShoes";
    }

    public void SetDiscoScore(int score, int maxScore)
    {
        discoScore = score;
        discoMaxScore = maxScore;
    }

    //Method to invoke Outfit Changed event
    protected virtual void OnOutfitChanged(ClothingType e)
    {
        OutfitChanged?.Invoke(this, e);
    }

    public void SetScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "Overworld City")
        {
            _savedPosition = FindObjectOfType<PlayerController>().transform.position;
        }
        //HACK: This is a really dumb stupid way of doing this and it sucks
        StartCoroutine(LoadDelay(0.5f, sceneName));
    }

    private IEnumerator LoadDelay(float delayTime, string sceneName)
    {
        _audioSource.PlayOneShot(exitSound);
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);

        if (sceneName == "Overworld City")
        {
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

            FindObjectOfType<PlayerController>().transform.position = _savedPosition;
            FindObjectOfType<FollowCamera>().transform.position = (Vector3)_savedPosition + new Vector3(0f, 0f, -10f);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
