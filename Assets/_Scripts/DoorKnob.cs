using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorKnob : MonoBehaviour
{
    private HingeJoint _doorHinge;

    private Rigidbody _doorRigidbody;
    private XRGrabInteractable _grabInteractable;
    private bool _isOpen;
    private bool _isInteracting;

    public float _knobCurrentTurnAngle;

    private float _knobUnlockAngle;
    private float _doorClosedBias;

    public float doorAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _doorHinge = transform.parent.GetComponentInChildren<HingeJoint>();
        _doorRigidbody = transform.parent.GetComponentInChildren<Rigidbody>();
        
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
                break;
        }
        
        _isOpen = doorAngle > 0 ? true : false;
        doorAngle = (doorAngle > _doorClosedBias) ? doorAngle : 0;
        LatchBolt_Unlock();
        LatchBolt_Lock();
        
        doorAngle = Mathf.Abs(_doorHinge.angle);
            
        
        
        

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
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        _isInteracting = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        _isInteracting = false;
    }
}
