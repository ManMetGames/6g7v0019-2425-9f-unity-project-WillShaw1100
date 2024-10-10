using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraScript : MonoBehaviour
{
    public GameObject player;
    public LayerMask groundLayer;
   // private float heightAboveGround = 3.0f;
   // private float distanceFromPlayer = 5.0f;
    //private float smooth = 2.0f;

    private Vector3 offset = new Vector3(-4, 1, 0);


    // Start is called before the first frame update
    void Start()
    {

    }

    
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            transform.position = player.transform.position + offset;


        }

    }
}