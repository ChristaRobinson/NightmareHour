using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public Light[] lightSources;

    

    public void Lightswitch_Toggle()
    {
        foreach (Light light in lightSources)
        {
            switch (light.enabled)
            {
                case true: light.enabled = false;
                    GetComponentInParent<ToggleMaterialAnimation>().PlayToggleAnimation(false);
                    break;
                case false: light.enabled = true;
                    GetComponentInParent<ToggleMaterialAnimation>().PlayToggleAnimation(true);
                    break;
            }
        }
    }
}
