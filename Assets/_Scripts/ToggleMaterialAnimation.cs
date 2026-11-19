using UnityEngine;

public class ToggleMaterialAnimation : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    private Material[] _materials;
    

    private bool _isTurnedOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _materials = Resources.LoadAll<Material>("CeilingFan");
    }

    // Update is called once per frame
    public void Toggle(bool isTurnedOn)
    {
        Material[] meshMaterials = meshRenderer.materials;
        
        meshMaterials[0] = isTurnedOn ? _materials[1] : _materials[0];

        meshRenderer.materials = meshMaterials;

    }
}
