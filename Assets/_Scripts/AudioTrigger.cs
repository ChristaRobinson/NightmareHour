using UnityEngine;

public class AudioTrigger : MonoBehaviour
{

    public AudioSource EnterTriggerAudio;
    public AudioSource ExitTriggerAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterTriggerAudio.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExitTriggerAudio.Play();
        }
    }
}
