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
        private Light _lightComponent;
        private MeshRenderer _lensMeshRenderer;
        private bool _isTurnedOn = false;
        
        
        private bool _flickerEventEnabled;
        private float _flickerDuration;
        private Coroutine _flickerCoroutine;
     

        [SerializeField] private Material emissiveMaterial;
        [SerializeField] private Material diffuseMaterial;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _lightComponent = transform.Find("Torchlight").GetComponent<Light>();
            _lightComponent.enabled = false;
            _lensMeshRenderer = transform.Find("flashlight").GetComponent<MeshRenderer>();
            _isTurnedOn = _lightComponent.enabled;

        }

        public void ToggleFlashlight()
        {
            _audioSource.Play();

            Material[] flashlightMats = _lensMeshRenderer.materials;
            _lightComponent.enabled = _isTurnedOn ? false : true;
            _isTurnedOn = _lightComponent.enabled;
            
            flashlightMats[InnerLight] = _isTurnedOn ? emissiveMaterial : diffuseMaterial;
            flashlightMats[Bulb] = _isTurnedOn ? emissiveMaterial : diffuseMaterial;
            
            _lensMeshRenderer.materials = flashlightMats;
            
        }

        public void StartFlicker(float duration)
        {
            if (_flickerCoroutine != null)
                StopCoroutine(_flickerCoroutine);

            _flickerEventEnabled = true;
            _flickerDuration = duration;

            _flickerCoroutine = StartCoroutine(FlashlightFlicker());
        }

        private IEnumerator FlashlightFlicker()
        {
            bool originalState = _lightComponent.enabled;

            while (_flickerDuration > 0f && _flickerEventEnabled)
            {
                _flickerDuration -= Time.deltaTime;

                _lightComponent.enabled = Random.value > 0.5f;

                yield return new WaitForSeconds(
                    Random.Range(0.05f, 0.20f));
            }

            _lightComponent.enabled = originalState;

            EndFlicker();

        }

        private void EndFlicker()
        {
            if (_flickerCoroutine != null)
            {
                StopCoroutine(_flickerCoroutine);
                _flickerCoroutine = null;
            }

            _flickerEventEnabled = false;
            _flickerDuration = 0f;
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
