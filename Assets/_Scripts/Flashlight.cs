using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private const int Lens = 2;
    
    private Light _lightComponent;
    private MeshRenderer _lensMeshRenderer;
    private bool _isTurnedOn = false;
    private bool _flickerEventEnabled;
    private float _flickerDuration;
     

    [SerializeField] private Material emissiveMaterial;
    [SerializeField] private Material diffuseMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lightComponent = GetComponentInChildren(typeof(Light)) as Light;
        _lensMeshRenderer = transform.GetChild(Lens).gameObject.GetComponent<MeshRenderer>();
        
    }

    public void ToggleFlashlight()
    {
        _lensMeshRenderer.material = _isTurnedOn ? emissiveMaterial : diffuseMaterial;
        _lightComponent.enabled = _isTurnedOn ? true : false;
    }

    public void StartFlicker(float duration)
    {
        _flickerEventEnabled = true;
        _flickerDuration = duration;
        StartCoroutine(FlashlightFlicker());
    }

    private IEnumerator FlashlightFlicker()
    {
        while (_flickerDuration > 0 && _flickerEventEnabled)
        {
            _flickerDuration -= Time.deltaTime;
            
            /*WIP - Flicker Logic Here */

            yield return null;
        }

        if (_flickerDuration < 0)
            EndFlicker();

    }

    private void EndFlicker()
    {
        StopCoroutine(FlashlightFlicker());
        _flickerEventEnabled = false;
        _flickerDuration = 0;
    }
}
