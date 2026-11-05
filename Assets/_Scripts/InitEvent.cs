using UnityEngine;

public class InitEvent : MonoBehaviour
{
    public GameObject InitialEventObjects;
    public GameObject FinalEventObjects;

    void Start()
    {
        InitialEventObjects.SetActive(true);
        FinalEventObjects.SetActive(false);
    }
}
