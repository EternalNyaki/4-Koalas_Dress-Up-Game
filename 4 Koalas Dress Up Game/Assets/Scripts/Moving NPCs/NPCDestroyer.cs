using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDestroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        MovingNPC npc = collision.GetComponent<MovingNPC>();

        if (npc != null)
        {
            Destroy(collision.gameObject);
        }
    }
}
