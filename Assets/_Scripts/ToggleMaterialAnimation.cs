using UnityEngine;

public class ToggleMaterialAnimation : MonoBehaviour
{
    private Animation animation;
    public enum Type { CeilingFan }

    public Type type;

    private bool _isTurnedOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    public void PlayToggleAnimation(bool isActive)
    {
        _isTurnedOn = isActive;
        Toggle();

    }

    private void Toggle()
    {
        switch (type)
        {
            case Type.CeilingFan:
                switch (_isTurnedOn)
                {
                    case true: animation.Play("CeilingFan_Off");
                        break;
                    case false: animation.Play("CeilingFan_On");
                        break;
                }
                break;
            default:
                break;
                
        }
        
    }
}
