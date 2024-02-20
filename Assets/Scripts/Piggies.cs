using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggies : MonoBehaviour
{
    [SerializeField] private GameObject gameobject;
    void OnCollisionEnter2D(Collision2D other) {
        if ( other.relativeVelocity.magnitude > 1) {
            Destroy(gameobject);
        }
    }
}
