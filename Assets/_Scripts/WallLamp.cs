using UnityEngine;

public class WallLamp : MonoBehaviour
{
    private AudioSource wallLamp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wallLamp = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        wallLamp.Play();
    }
}
