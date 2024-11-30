using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatRoad : MonoBehaviour
{
    // Sets startPos variable for the road.
    private Vector3 startPos;
    private float RoadPosZ = 28.0f;

    // Sets variable for the point where the road should be set back to startPos.
    private float repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Sets exact startPos of the road.
        startPos = new Vector3(0, 0, RoadPosZ);
        // Sets exact Width where the road should be repeated.
        repeatWidth = GetComponent<BoxCollider>().size.z * 2;  
    }

    // Update is called once per frame
    void Update()
    {
        // Checks location of the road and sets it back to startPos once the repeatWidth is exceeded.  
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
