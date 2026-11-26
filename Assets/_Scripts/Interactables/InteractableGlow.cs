using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class InteractableGlow : MonoBehaviour
{
    public Material glowMaterial;
    private MeshRenderer meshRenderer;
    private XRBaseInteractable interactable;
    private Material[] originalMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        interactable = GetComponent<XRBaseInteractable>();

        if (meshRenderer != null)
        {
            originalMaterial = meshRenderer.sharedMaterials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
