using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float speed = 20f; // Speed of the enemy
    public float range = 5f; // Range within which the enemy will follow the player

    void Update()
    {
        BossScale();

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= range)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }


    public void BossScale()
    {
        if (transform.position.x > PlayerController.instance.transform.position.x)
        { 
            transform.localScale = new Vector3(-4, 4, 4);
        }
        else
        {
            transform.localScale = new Vector3(4, 4, 4);   
        }

    }
        

}