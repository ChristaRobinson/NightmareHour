using UnityEngine;

public class CrowbarContact : MonoBehaviour
{
    public GameObject breakObjects;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Crowbar"))
        {
            Instantiate(breakObjects, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Crowbar"))
        {
            Instantiate(breakObjects, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
