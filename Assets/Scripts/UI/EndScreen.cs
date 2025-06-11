using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public GameObject thanksCanvas; // Canvas con el PNG

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (thanksCanvas != null)
                thanksCanvas.SetActive(true);
        }
    }
}