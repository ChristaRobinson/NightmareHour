using UnityEngine;
using UnityEngine.Events;

public class AreaDetector : MonoBehaviour
{
    //Assign an area name to signal out later
    public string areaName;
    //public static UnityEvent OnAreaEntered;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the " + this.gameObject.name + " area");
            //OnAreaEntered.Invoke();
        }
    }
}
