using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public GameObject InitialEventObjects;
    public GameObject FinalEventObjects;
    
    void Start()
    {
        InitialEventObjects.SetActive(true);
        FinalEventObjects.SetActive(false);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        InitialEventObjects.SetActive(true);
        FinalEventObjects.SetActive(false);
    }
    */
    private void OnTriggerExit(Collider other)
    {
        InitialEventObjects.SetActive(false);
        FinalEventObjects.SetActive(true);
    }
}
