using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggies : MonoBehaviour
{
    [SerializeField] PiggiesSo _piggiesSo;
    void OnCollisionEnter2D(Collision2D other) {
        if ( other.relativeVelocity.magnitude > 1) {
            Destroy(gameObject);
        }
    }
}
