using UnityEngine;

public class Gravity : MonoBehaviour
{
    private CharacterController characterController;
    private float verticalVelocity = 0f;
    public float gravity = -9.81f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            verticalVelocity = -0.5f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 gravityMove = new Vector3(0, verticalVelocity, 0);
        characterController.Move(gravityMove * Time.deltaTime);
    }
}
