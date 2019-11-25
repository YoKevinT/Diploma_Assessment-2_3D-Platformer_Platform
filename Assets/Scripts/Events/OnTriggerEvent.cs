using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    // Reference Tag to detect collisions with
    public string hitTag;
    public UnityEvent onEnter, onStay, onExit;

    private void OnTriggerEnter(Collider other)
    {
        // If hitting this tag
        if (other.tag == hitTag || hitTag == "")
        {
            // Invoke (Run) the event!
            onEnter.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If hitting this tag
        if (other.tag == hitTag || hitTag == "")
        {
            // Invoke (Run) the event!
            onStay.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If hitting this tag
        if (other.tag == hitTag || hitTag == "")
        {
            // Invoke (Run) the event!
            onExit.Invoke();
        }
    }
}
