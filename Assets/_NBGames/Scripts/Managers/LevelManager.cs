using UnityEngine;

namespace _NBGames.Scripts.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private GameObject _xrRig;

        [SerializeField] private GameObject[] _jumpPoints;
        private int _currentJumpPoint, _oldJumpPoint;

        private void OnEnable()
        {
            EventManager.onAllCurrentEnemiesKilled += GoToNextJumpPoint;
        }

        private void OnDisable()
        {
            EventManager.onAllCurrentEnemiesKilled -= GoToNextJumpPoint;
        }

        // Start is called before the first frame update
        void Start()
        {
            _xrRig = GameObject.FindWithTag("Player");
            if (_xrRig == null)
            {
                Debug.LogError("JumpPointController was unable to find XR Rig!");
            }
            else
            {
                GoToStartPoint();
            }
        }

        private void GoToStartPoint()
        {
            var jumpPosition = _jumpPoints[_currentJumpPoint].transform.position;
            jumpPosition.y = _xrRig.transform.position.y;

            _xrRig.transform.position = jumpPosition;
        }

        private void GoToNextJumpPoint()
        {
            if ((_currentJumpPoint + 1) < _jumpPoints.Length)
            {
                _oldJumpPoint = _currentJumpPoint;
                _currentJumpPoint++;

                _jumpPoints[_currentJumpPoint].SetActive(true);
                var jumpPosition = _jumpPoints[_currentJumpPoint].transform.position;
                jumpPosition.y = _xrRig.transform.position.y;

                _xrRig.transform.position = jumpPosition;
                _xrRig.transform.rotation = _jumpPoints[_currentJumpPoint].transform.rotation;
                
                _jumpPoints[_oldJumpPoint].SetActive(false);
            }
            else
            {
                LevelEnd();
            }
        }

        private void LevelEnd()
        {
            Debug.Log("Level completed!");
        }
    }
}
