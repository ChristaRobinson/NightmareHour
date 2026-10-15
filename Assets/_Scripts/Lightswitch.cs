using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public GameObject[] lightSources;
    private List<Light> lights;

    public void Start()
    {
        foreach (GameObject source in lightSources)
        {
            if(source.GetComponent<Light>() != null)
                lights.Add(source.GetComponent<Light>());
            else
                lights.Add(source.GetComponentInChildren<Light>());
        }
            
    }

    public void Lightswitch_Toggle()
    {
        foreach (Light light in lights)
        {
            switch (light.enabled)
            {
                case true: light.enabled = false; break;
                case false: light.enabled = true; break;
            }
        }
    }
}
