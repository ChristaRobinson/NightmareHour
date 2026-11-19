using UnityEngine;

public class Footsteps : MonoBehaviour
{
    //Footstep library
    private AudioClip[] _footsteps;

    private AudioSource _audioSource;

    private float _footstepDistance;
    private readonly float _footstepThreshold = 0.4f;

    private Vector3 _lastStepPosition;

    private int _lastAudioClip = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _footsteps = Resources.LoadAll<AudioClip>("Footsteps");
        _audioSource = GetComponent<AudioSource>();
        _lastStepPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 movement = transform.position - _lastStepPosition;
        movement.y = 0f;

        _footstepDistance += movement.magnitude;

        if (_footstepDistance >= _footstepThreshold)
        {
            _footstepDistance -= _footstepThreshold;
            PlayFootStep();
        }
    }

    private void PlayFootStep()
    {
        int footstepIndex = Random.Range(0, _footsteps.Length);
        if (footstepIndex == _lastAudioClip)
            footstepIndex += 1 % _footsteps.Length;
        _audioSource.PlayOneShot(_footsteps[footstepIndex]);
        _lastAudioClip = footstepIndex;
    }
}
