using UnityEngine;
using UnityEngine.Events;

public class TriggerScript : MonoBehaviour
{
    private bool TriggeredOnce;
    public UnityEvent onEnterTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (TriggeredOnce == false)
        {
            TriggeredOnce = true;
            onEnterTrigger.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
