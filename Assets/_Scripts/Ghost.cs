using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class Ghost : MonoBehaviour
    {
        private Player _player;
        private Flashlight _flashlight;

        private int _ghostAppearanceCount = 0;
        [SerializeField] private Transform[] ghostPoints;

        private float _viewTolerance = 8.0f;

        private float _distanceScalar = 1.0f;

        private float _maxDistance = 20.0f;

        private float _minDistance = 5.0f;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _flashlight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
            StartCoroutine(RefreshViewTolerance(0.25f));
        }

        // Update is called once per frame
        void Update()
        {
            UpdateDistanceScalar();
        }
    
        IEnumerator RefreshViewTolerance(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                ViewTolerance();
            }
        }

        private void ViewTolerance()
        {
            if (_player.IsViewingGhost())
            {
                Debug.Log("Character spots ghost. View Tolerance:" +  _viewTolerance);
                switch (_player.GetGhostVisibility())
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

            if (_viewTolerance > 2.0f && _flashlight.IsIlluminatingGhost())
                _viewTolerance = 2.0f;
            else if (_viewTolerance <= 2.0f)
                _viewTolerance = 0.0f;

            //Ghost should disappear.
            if (_viewTolerance <= 0.0f)
            {
                if(ghostPoints.Length > 0)
                    transform.position = ghostPoints[++_ghostAppearanceCount % ghostPoints.Length].position;
                
                _viewTolerance = 6.0f;
            }
        }

        private void UpdateDistanceScalar()
        {
            if (_player.IsViewingGhost())
            {
                float distance = Vector3.Distance(transform.position, _player.transform.position);
                float scalarStrength = Mathf.InverseLerp(_minDistance, _maxDistance, distance);
                _distanceScalar = Mathf.Lerp(3.0f, 0.5f, scalarStrength);
            }
        
        }
    }
}
