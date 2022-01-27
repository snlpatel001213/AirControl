using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Communicator;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string tag = gameObject.tag;
    }

    // on collision update
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag== "Airplane"){
            Debug.Log("Collided with "+ tag);
            StaticOutputSchema.IfCollision=true;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== "Airplane"){
            Debug.Log("Collided with "+ tag);
            StaticOutputSchema.IfCollision=true;
        }

    }
}
