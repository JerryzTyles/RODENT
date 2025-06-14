using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDestroyer : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, .4f);

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }

        }
    }
}
