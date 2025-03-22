using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interactable Object component base class
//Component that lets an object recognize interaction from the player
public abstract class InteractableObject : MonoBehaviour
{
    //Interaction initiated sound
    public AudioClip interactSound;

    //Audio component for interaction sound
    private AudioSource _audioSource;

    //Reference to a player that could interact with this object
    private PlayerController _player = null;

    void Start()
    {
        Initialize();
    }

    //Method to override for any initialization code
    //Called in Start()
    protected virtual void Initialize()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null && Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the triggering collider is a player, get a reference to it
        _player = collision.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Clear the player reference
        if (_player.gameObject == collision.gameObject)
        {
            _player = null;
        }
    }

    //Interact Method
    //Runs when the player interacts with this object
    //Can be overriden for alternative functionality
    public virtual void Interact()
    {
        _audioSource.PlayOneShot(interactSound);
    }
}
