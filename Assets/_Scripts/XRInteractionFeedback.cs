using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Changes an object's material color during XR hover and selection.
/// </summary>
[RequireComponent(typeof(XRBaseInteractable))]
public sealed class XRInteractionFeedback : MonoBehaviour
{
    [SerializeField]
    private Renderer targetRenderer;

    [SerializeField]
    private Color normalColor = Color.white;

    [SerializeField]
    private Color hoverColor = Color.yellow;

    [SerializeField]
    private Color selectedColor = Color.green;

    private XRBaseInteractable interactable;
    private Material instanceMaterial;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();

        if (targetRenderer == null)
        {
            targetRenderer = GetComponentInChildren<Renderer>();
        }

        if (targetRenderer == null)
        {
            Debug.LogWarning(
                $"{nameof(XRInteractionFeedback)} on {name} " +
                "could not find a Renderer.",
                this);

            return;
        }

        instanceMaterial = targetRenderer.material;
        instanceMaterial.color = normalColor;
    }

    private void OnEnable()
    {
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
        interactable.selectEntered.RemoveListener(OnSelectEntered);
        interactable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs eventArgs)
    {
        SetColor(hoverColor);
    }

    private void OnHoverExited(HoverExitEventArgs eventArgs)
    {
        if (!interactable.isSelected)
        {
            SetColor(normalColor);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs eventArgs)
    {
        SetColor(selectedColor);
    }

    private void OnSelectExited(SelectExitEventArgs eventArgs)
    {
        SetColor(
            interactable.isHovered
                ? hoverColor
                : normalColor);
    }

    private void SetColor(Color color)
    {
        if (instanceMaterial != null)
        {
            instanceMaterial.color = color;
        }
    }
}
