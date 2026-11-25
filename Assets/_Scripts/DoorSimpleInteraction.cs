using UnityEngine;

namespace _Scripts
{
    public class DoorSimpleInteraction : MonoBehaviour
    {
        private HingeJoint _hingeJoint;

        private bool _isOpen = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _hingeJoint = GetComponent<HingeJoint>();
        }
        
        public void ToggleDoor()
        {
            JointMotor motor = _hingeJoint.motor;
            JointSpring spring = _hingeJoint.spring;
            motor.force = 20; // Adjust as needed
            motor.targetVelocity = _isOpen ? -30 : 30; // Direction
            
            spring.targetPosition = _isOpen ? 0 : -138.32f;
            
            _hingeJoint.motor = motor;
            _hingeJoint.spring = spring;
            _hingeJoint.useMotor = true;
        
            _isOpen = !_isOpen;
        }
    }
}
