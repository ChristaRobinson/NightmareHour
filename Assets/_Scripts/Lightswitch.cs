using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public Light[] lightSources;
    private AudioSource _audioSource;

    [SerializeField]
    private ToggleMaterialAnimation materialToggler;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Lightswitch_Toggle()
    {
        _audioSource.Play();
        foreach (Light light in lightSources)
        {
            switch (light.enabled)
            {
                case true: light.enabled = false;
                    materialToggler.Toggle(false);
                    break;
                case false: light.enabled = true;
                    materialToggler.Toggle(true);
                    break;
            }
        }
    }
}
