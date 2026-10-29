using System;
using UnityEngine;

namespace _Scripts
{
    public class ProximityEvent : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "Flashlight")
                other.GetComponent<Flashlight>().StartFlicker(5);
        }
    }
}
