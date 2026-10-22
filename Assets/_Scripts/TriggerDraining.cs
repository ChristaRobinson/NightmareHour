using System.Runtime.Serialization;
using UnityEngine;

public class TriggerDraining : MonoBehaviour
{
    public DrainWater drainWater;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (drainWater != null)
        {
            drainWater.StartDraining();
        }
    }
}
