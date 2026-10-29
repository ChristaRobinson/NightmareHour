using UnityEngine;

public class BookGrab : MonoBehaviour
{
    public GameObject openBook;
    public GameObject closedBook;

    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateBookState();
    }

    /* Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerActivation();
        }
    }
    */

    public void TriggerActivation()
    {
        ToggleBook();
    }

    private void ToggleBook()
    {
        isOpen = !isOpen;
        UpdateBookState();
    }

    private void UpdateBookState()
    {
        openBook.SetActive(isOpen);
        closedBook.SetActive(!isOpen);
    }
}
