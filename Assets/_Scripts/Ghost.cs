using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour
{
    private Player player;

    private int _ghostAppearanceCount = 0;
    [SerializeField] private Transform[] ghostPoints;

    private float _viewTolerance = 8.0f;

    private float _distanceScalar = 1.0f;

    private float _maxDistance = 20.0f;

    private float _minDistance = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistanceScalar();
        ViewTolerance();
    }

    private void ViewTolerance()
    {
        if (player.IsViewingGhost())
        {
            Debug.Log("Character spots ghost. View Tolerance:" +  _viewTolerance);
            switch (player.GetGhostVisibility())
            {
                case Player.GhostVisibility.Foveal:
                    _viewTolerance -= Time.deltaTime * 3.0f * _distanceScalar;
                    break;
                case Player.GhostVisibility.Parafoveal:
                    _viewTolerance -= Time.deltaTime * 2.0f * _distanceScalar;
                    break;
                case Player.GhostVisibility.Peripheral:
                    _viewTolerance -= Time.deltaTime * 1.0f * _distanceScalar;
                    break;
                case Player.GhostVisibility.None:
                    break;
            }
        }
            

        //Ghost should disappear.
        if (_viewTolerance <= 0.0f)
        {
            transform.position = ghostPoints[++_ghostAppearanceCount % ghostPoints.Length].position;
            _viewTolerance = 6.0f;
        }
    }

    private void UpdateDistanceScalar()
    {
        if (player.IsViewingGhost())
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            float scalarStrength = Mathf.InverseLerp(_minDistance, _maxDistance, distance);
            _distanceScalar = Mathf.Lerp(3.0f, 0.5f, scalarStrength);
        }
        
    }
}
