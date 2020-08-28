using UnityEngine;
using System.Collections;

public class LooseCollider : MonoBehaviour
{
    private GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collider)
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.LoadLevel("Loose Screen");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
