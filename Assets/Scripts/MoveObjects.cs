using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    // Sets speed for movement of the objects (= public to allow speeds change per different object).
    public float speed = 5.0f;

    // Sets Z-bound.
    private float zDestroyBound = -26.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Action to move objects in given direction * speed and time.
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Destroys objects when they exceed given Z-bound.
        if(transform.position.z < zDestroyBound)
        {
            Destroy(gameObject);
        }
    }
}
