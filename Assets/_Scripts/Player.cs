using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Player : MonoBehaviour
{
    private Camera _camera;
    private float _visualFOV = 50f;
    private float _viewDistance = 15f;
    private float _peripheryFOV = 40f;
    private float _parafovealFOV = 25f;
    private float _fovealFOV = 10f;
    private float _viewAngle;

    public Collider[] ghostProximity;
    
    public enum GhostVisibility { Foveal, Parafoveal, Peripheral, None }

    private GhostVisibility _ghostVisibility = GhostVisibility.None;
    
    public LayerMask ghostLayerMask;
    public LayerMask environmentLayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        //StartCoroutine(RefreshGhostVisibility(0.5f));
    }
    
    // Update is called once per frame
    void Update()
    {
        //if(GetGhostVisibility() == GhostVisibility.Peripheral)
            //Debug.Log("Looking at ghost.");
        
    }

    /*IEnumerator RefreshGhostVisibility(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            SpotGhost();
        }
    }

    void SpotGhost()
    {
        ghostProximity = Physics.OverlapSphere(_camera.transform.position, _viewDistance, ghostLayerMask);
        if (ghostProximity.Length == 0) return;
        
        Vector3 ghostDirection = (ghostProximity[0].transform.position - _camera.transform.position).normalized;
        
        if (Vector3.Angle(_camera.transform.forward, ghostDirection) < _visualFOV)
        {
            float ghostDistance = (ghostProximity[0].transform.position - _camera.transform.position).magnitude;
            if (!Physics.Raycast(_camera.transform.position, ghostDirection, ghostDistance, environmentLayerMask))
            {
                if (Vector3.Angle(_camera.transform.forward, ghostDirection) < _peripheryFOV)
                    _ghostVisibility = GhostVisibility.Peripheral;
                if (Vector3.Angle(_camera.transform.forward, ghostDirection) < _parafovealFOV)
                    _ghostVisibility = GhostVisibility.Parafoveal;
                if (Vector3.Angle(_camera.transform.forward, ghostDirection) < _fovealFOV)
                    _ghostVisibility = GhostVisibility.Foveal;
            }
        }
        else
            _ghostVisibility = GhostVisibility.None;
    }*/
    
    public bool CanSeeGhost(Transform ghost)
    {
        Vector3 viewportPos =
            _camera.WorldToViewportPoint(ghost.position);

        bool onScreen =
            viewportPos.z > 0 &&
            viewportPos.x > 0 &&
            viewportPos.x < 1 &&
            viewportPos.y > 0 &&
            viewportPos.y < 1;

        if (!onScreen)
            return false;

        Vector3 direction =
            ghost.position - _camera.transform.position;

        float distance = direction.magnitude;

        if (Physics.Raycast(_camera.transform.position, direction.normalized, distance, LayerMask.GetMask("Default")))
        {
            return false;
        }

        return true;
    }

    public bool IsViewingGhost() { return _ghostVisibility != GhostVisibility.None; }
    public GhostVisibility GetGhostVisibility() { return _ghostVisibility; }
}
