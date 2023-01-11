using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("OnTriggerEnter2D()");
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("OnTriggerExit2D()");
    }
}
