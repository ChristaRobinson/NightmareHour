using UnityEngine;

public class CrowbarContact : MonoBehaviour
{
    public GameObject breakObjects;
    public float delayTime = 5f;
    private bool isBroken = false;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Crowbar"))
        {
            if (isBroken == false)
            {
                GameObject chunks = Instantiate(breakObjects, transform.position, transform.rotation);
                Destroy(gameObject);
                isBroken = true;
                Destroy(chunks, delayTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Crowbar"))
        {
            if (isBroken == false)
            {
                GameObject chunks = Instantiate(breakObjects, transform.position, transform.rotation);
                Destroy(gameObject);
                isBroken = true;
                Destroy(chunks, delayTime);
            }
        }
    }
}
