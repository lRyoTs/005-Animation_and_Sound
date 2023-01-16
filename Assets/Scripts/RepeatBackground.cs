using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatWidth; //Width of an unit of the background

    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position; //Store initial position
        repeatWidth = GetComponent<BoxCollider>().size.x / 2; //Store accurate half part
    }

    // Update is called once per frame
    void Update()
    {
        //Restart background
        if (transform.position.x < startPos.x -repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
