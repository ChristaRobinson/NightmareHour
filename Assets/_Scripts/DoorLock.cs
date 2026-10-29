using UnityEngine;

namespace _Scripts
{
    public class DoorLock : MonoBehaviour
    {
        public GameObject requiredKey;

        public bool isLocked = true;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == requiredKey)
            {
                isLocked = false;
                Debug.Log("Door unlocked");
            }
        }
    }
}
