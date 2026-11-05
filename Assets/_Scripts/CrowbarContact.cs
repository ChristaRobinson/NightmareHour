using UnityEngine;

public class CrowbarContact : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crowbar"))
        {
            Destroy(gameObject);
        }
    }
}
