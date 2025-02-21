using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string interactionResponse;

    private PlayerController _interactor = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_interactor != null && Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interactor = collision.gameObject.GetComponent<PlayerController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactor.gameObject == collision.gameObject)
        {
            _interactor = null;
        }
    }

    public void Interact()
    {
        Debug.Log(gameObject.name + ": \"" + interactionResponse + "\"");
    }
}
