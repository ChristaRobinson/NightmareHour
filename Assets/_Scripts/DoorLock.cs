using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace _Scripts
{
    public class DoorLock : MonoBehaviour
    {
        private HingeJoint _doorHinge;
        private JointLimits _lockedLimits;
        private JointLimits _unlockedLimits;
        public GameObject requiredKey;
        private Rigidbody _requiredKeyRigidbody;
        private Collider _requiredKeyCollider;
        //private Collider _keyHoleCollider;
        private AudioSource _audioSource;
        private XRSocketInteractor[] _socketInteractors;

        public bool isLocked = true;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _socketInteractors = GetComponentsInChildren<XRSocketInteractor>();
            _doorHinge = GetComponentInParent<HingeJoint>();
            _requiredKeyRigidbody = requiredKey.GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
            _lockedLimits = _doorHinge.limits;
            _lockedLimits.max = 0; _unlockedLimits.max = 138;
            _lockedLimits.min = 0; _unlockedLimits.min = -138;
            _doorHinge.limits = _lockedLimits;
        }

        private void UnlockDoor()
        {
            if (!isLocked) return;
            isLocked = false;
            _audioSource.Play();
            this.enabled = false;
            _requiredKeyRigidbody.detectCollisions = false;
            _requiredKeyRigidbody.gameObject.SetActive(false);
            foreach (XRSocketInteractor socketInteractor in _socketInteractors)
                socketInteractor.enabled = false;
            
            _doorHinge.limits = _unlockedLimits;
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!(_socketInteractors[0].hasSelection || _socketInteractors[1].hasSelection))
                return;

            UnlockDoor();
        }
    }
}
