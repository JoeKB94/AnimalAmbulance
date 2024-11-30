using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    // Variable for movement speed of the first aid kit. 
    private float speed = 25.0f;

    // Sets Z-boundairy.
    private float zDestroyBound = 50.0f;

    // Update is called once per frame
    void Update()
    {
        MoveFirstAid(); // makes sure that the Aidkit moves once it's instantiated.
    }

    // Methode to move the AidKit.
    void MoveFirstAid()
    {
        // Move action.
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Destroys objects when they exceed given Z-bound.
        if (transform.position.z > zDestroyBound)
        {
            Destroy(gameObject);
        }
    }

    // Checks trigger on collider and then destroys both objects if tag matches. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("PowerUp") || other.CompareTag("HealthUp") || other.CompareTag("Animal"))
        {
            Destroy(gameObject);
        }
    }
}

