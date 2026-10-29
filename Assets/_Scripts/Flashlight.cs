using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class Flashlight : MonoBehaviour
    {
        private const int InnerLight = 1;
        private const int Bulb = 3;

        private AudioSource _audioSource;
        private Player _player;
        Light _lightComponent;
        private MeshRenderer _lensMeshRenderer;
        private bool _isTurnedOn;
        private bool _flickerEventEnabled;
        private float _flickerDuration;
     

        [SerializeField] private Material emissiveMaterial;
        [SerializeField] private Material diffuseMaterial;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _lightComponent = transform.Find("Torchlight").GetComponent<Light>();
            _lensMeshRenderer = transform.Find("flashlight").GetComponent<MeshRenderer>();
            _isTurnedOn = _lightComponent.enabled;

        }

        public void ToggleFlashlight()
        {
            _audioSource.Play();
            _lensMeshRenderer.materials[InnerLight] = _isTurnedOn ? emissiveMaterial : diffuseMaterial;
            _lensMeshRenderer.materials[Bulb] = _isTurnedOn ? emissiveMaterial : diffuseMaterial;
            _lightComponent.enabled = false;
            _isTurnedOn = _lightComponent.enabled;
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

        public bool IsIlluminatingGhost()
        {
            if (!transform.parent)
                return false;
            if (_player.ghostProximity.Length < 1)
                return false;
        
            Vector3 ghostDirection = (_player.ghostProximity[0].transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, ghostDirection) < _lightComponent.innerSpotAngle)
            {
                if (!Physics.Raycast(transform.position, ghostDirection, _lightComponent.intensity,
                        LayerMask.GetMask("Default")))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
