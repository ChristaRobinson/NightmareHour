using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private string EventName;

    [SerializeField] private float lookAwayFOV;
    [SerializeField] private float DelayTime;

    public GameObject InitialEventObjects;
    public GameObject FinalEventObjects;

    private Vector3 directionToObject;
    private bool playerLookAway = false;
    private bool eventTriggered = false;
    //prevent the event from triggering immediately, and being completely visible to the user
    private float LookAwayTimer = 0;
    

    void Start()
    {
        InitialEventObjects.SetActive(true);
        FinalEventObjects.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(eventTriggered)
        {
            return;
        }
        directionToObject = (transform.position - playerCamera.transform.position);

        if (Vector3.Angle(playerCamera.transform.forward, directionToObject) > lookAwayFOV)
        {
            playerLookAway = true;

            LookAwayTimer += Time.deltaTime;
            if(LookAwayTimer > DelayTime)
            {
                TriggerEvent();
            }
            //Debug.Log("Player looked away!");
        }
        else
        {
            playerLookAway = false;
            LookAwayTimer = 0;
            //Debug.Log("Player looked back!");
        }
    }

    void TriggerEvent()
    {
        eventTriggered = true;
        InitialEventObjects.SetActive(false);
        FinalEventObjects.SetActive(true);
        Debug.Log("Triggered " + EventName + "!");
    }
}
