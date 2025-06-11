using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform activeRoom;
    public float dampSpeed;

    public static CameraController instance;

    [Range(-130,130)]
    public float minModX, maxModX, minModY, maxModY;    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        activeRoom = player;
        transform.position = new Vector3(player.position.x, player.position.y, -1);
    }


    // Update is called once per frame
    void Update()
    {
        var minPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x - minModX;
        var maxPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.max.x + maxModX;
        var minPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y - minModY;
        var maxPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.max.y + maxModY;

        
        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(player.position.x, minPosX, maxPosX),
            Mathf.Clamp(player.position.y, minPosY, maxPosY),
            Mathf.Clamp(player.position.z, -10f, -10f)
            );

        //transform.position = new Vector3(clampedPos.x, clampedPos.y, clampedPos.z); 

        Vector3 smoothPos = Vector3.Lerp(transform.position, clampedPos, dampSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
