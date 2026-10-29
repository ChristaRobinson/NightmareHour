using _Scripts;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorKnob : MonoBehaviour
{
    private Transform[] _doorKnobs = new Transform[2]
    private HingeJoint _doorHinge;
    private Rigidbody _doorRigidbody;
    private XRGrabInteractable _grabInteractable;
    private AudioSource _doorAudio;
    private DoorLock _doorLock;
    private bool _isOpen;
    private bool _isInteracting;

    private Transform grabbingHand;
    

    public float _knobCurrentTurnAngle;

    private float _knobUnlockAngle;
    private float _doorClosedBias;

    public float doorAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _doorKnobs[0] = transform.parent.GetChild(2);
        _doorKnobs[1] = transform.parent.GetChild(3);
        _doorHinge = transform.parent.GetComponentInChildren<HingeJoint>();
        _doorRigidbody = transform.parent.GetComponentInChildren<Rigidbody>();
        _doorLock = transform.parent.GetComponentInChildren<DoorLock>();
        _doorAudio = transform.GetComponent<AudioSource>();
        
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectEntered.AddListener(OnGrab);
        _grabInteractable.selectExited.AddListener(OnRelease);

        _knobCurrentTurnAngle = Mathf.Abs(transform.localEulerAngles.z);
        _knobUnlockAngle = 25.0f;
        _doorClosedBias = 2.0f;
        _isOpen = false;
        _isInteracting = false;
    }

    void Update()
    {
        switch (_isInteracting)
        {
            case true:
                _knobCurrentTurnAngle = Mathf.Abs(transform.localEulerAngles.z);
                break;
            case false:
                transform.localEulerAngles = new Vector3(0, 0, 0);
                _knobCurrentTurnAngle = 0.0f;
                if (doorAngle < _doorClosedBias)
                    doorAngle = 0;
                break;
        }
        
        doorAngle = Mathf.Abs(_doorHinge.angle);
        _isOpen = doorAngle > 0 ? true : false;
        LatchBolt_Unlock();
        LatchBolt_Lock();

    }

    void FixedUpdate()
    {
        if (grabbingHand is null)
            return;

        Vector3 doorToHand =
            grabbingHand.position - _doorHinge.transform.position;

        Vector3 forceDirection =
            Vector3.ProjectOnPlane(
                doorToHand,
                _doorHinge.transform.up);

        _doorRigidbody.AddForceAtPosition(
            forceDirection * 50f,
            grabbingHand.position,
            ForceMode.Force);
    }
    private void LatchBolt_Unlock()
    {
        if (_isInteracting && (_knobCurrentTurnAngle > _knobUnlockAngle && _knobCurrentTurnAngle < 330))
        {
            _doorRigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
    }

    private void LatchBolt_Lock()
    {
        if(doorAngle == 0 && _knobCurrentTurnAngle < _knobUnlockAngle)
        {
            _doorRigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;
            _isOpen = false;
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        grabbingHand = args.interactorObject.transform;
        if(_doorLock != null)
            if (_doorLock.isLocked && _knobCurrentTurnAngle != 0)
            {
                _doorAudio.Play();
             return;
            }
        _isInteracting = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        grabbingHand = null;
        _isInteracting = false;
        if(_doorAudio)
            _doorAudio.Stop();
    }
}
