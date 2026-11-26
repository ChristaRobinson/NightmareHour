using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts
{
    public class DoorKnob : MonoBehaviour
    {
        private HingeJoint _doorHinge;
        private Rigidbody _doorRigidbody;
        private XRGrabInteractable _grabInteractable;
        private AudioSource _doorAudio;
        private DoorLock _doorLock;
        private bool _isInteracting;
        private bool _hasLock;
        private Transform _grabbingHand;
        private float _initialHandRotation;
        private Quaternion _initialDoorRotation;
    

        private float _knobCurrentTurnAngle;

        private float _knobUnlockAngle;
        private float _doorClosedBias;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            _doorHinge = transform.parent.GetComponent<HingeJoint>();
            _doorRigidbody = transform.parent.GetComponentInChildren<Rigidbody>();
            _doorLock = transform.parent.GetComponentInChildren<DoorLock>();
            _doorAudio = transform.GetComponent<AudioSource>();
            _hasLock = _doorLock != null;
            _grabInteractable = GetComponentInParent<XRGrabInteractable>();
            _grabInteractable.selectEntered.AddListener(OnSelectEntered);
            _grabInteractable.selectExited.AddListener(OnSelectExited);
            _initialDoorRotation = transform.parent.transform.rotation;

            _knobCurrentTurnAngle = Mathf.Abs(transform.localEulerAngles.z);
            _knobUnlockAngle = 35.0f;
            _doorClosedBias = 2.0f;
            
            _isInteracting = false;
        }

        private void Update()
        {
            switch (_isInteracting)
            {
                case true:
                    _knobCurrentTurnAngle = Mathf.DeltaAngle(_grabbingHand.transform.localEulerAngles.z,_initialHandRotation);
                    LatchBolt_Unlock();
                    break;
                case false:
                    _knobCurrentTurnAngle = 0.0f;
                    LatchBolt_Lock();
                    break;
            }
        }
        private void LatchBolt_Unlock()
        {
            if (_isInteracting && (Mathf.Abs(_knobCurrentTurnAngle) > Mathf.Abs(_knobUnlockAngle)))
            {
                _doorRigidbody.constraints = RigidbodyConstraints.None;
            }
        }

        private void LatchBolt_Lock()
        {
            if (Mathf.Abs(_doorHinge.angle) < _doorClosedBias)
            {
                _doorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                transform.parent.transform.rotation = _initialDoorRotation;
            }
        }

        private void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (_hasLock && _doorLock.isActiveAndEnabled)
            {
                _doorAudio.Play();
                return;
            }
            
            _grabbingHand = args.interactorObject.transform.parent.transform;
            _initialHandRotation = _grabbingHand.transform.localEulerAngles.z;
            
            _isInteracting = true;
        }

        private void OnSelectExited(SelectExitEventArgs args)
        {
            _initialHandRotation = 0;
            _grabbingHand = null;
            _isInteracting = false;
            if(_doorAudio)
                _doorAudio.Stop();
        }
    }
}
