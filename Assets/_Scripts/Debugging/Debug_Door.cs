using UnityEngine;

public class Debug_Door : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O))
            rb.AddForce(transform.forward * 50);

        if (Input.GetKey(KeyCode.P))
            rb.AddForce(-transform.forward * 50);
    }
}
