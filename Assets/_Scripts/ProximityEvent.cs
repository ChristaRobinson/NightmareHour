using System;
using UnityEngine;

namespace _Scripts
{
    public class ProximityEvent : MonoBehaviour
    {
        //TaskManager taskTracker;
        [Header("Trigger Settings")] 
        private bool hasTriggered = false;

        public bool isOneShot = true;
        
        
        /*These gameobjects have animations created that will be set to play from activation of this script.*/
        [SerializeField]
        private GameObject[] actors;

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
