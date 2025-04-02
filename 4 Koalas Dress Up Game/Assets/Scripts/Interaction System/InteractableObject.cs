using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interactable Object component base class
//Component that lets an object recognize interaction from the player
public abstract class InteractableObject : MonoBehaviour
{
    //Interaction initiated sound
    public AudioClip interactSound;

    //Sprite used in indicate that the player can interact with this object
    public SpriteRenderer interactionIndicator;

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
        if (_player != null && Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"I am {transform.parent.gameObject.name}, collision is {collision.gameObject.name}");
        //If the triggering collider is a player, get a reference to it
        _player = collision.gameObject.GetComponent<PlayerController>();

        if (_player != null && interactionIndicator != null)
        {
            interactionIndicator.color = new(255f, 255f, 255f, 255f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Clear the player reference
        if (_player?.gameObject == collision.gameObject)
        {
            if (_player != null && interactionIndicator != null)
            {
                interactionIndicator.color = new(255f, 255f, 255f, 175f);
            }

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
