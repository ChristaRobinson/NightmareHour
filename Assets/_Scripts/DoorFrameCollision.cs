using UnityEngine;

public class DoorFrameCollision : MonoBehaviour
{
    private Collider _doorCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _doorCollider = GetComponent<Collider>();
        Collider[] doorFrames = Physics.OverlapSphere(transform.position, 1.5f, LayerMask.GetMask("DoorFrames"));
        foreach(Collider collider in doorFrames)
        {
            Physics.IgnoreCollision(collider, _doorCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
