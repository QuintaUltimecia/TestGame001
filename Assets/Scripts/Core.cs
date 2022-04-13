using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace TestGame.Scripts
{
    public class Core : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private WayPoints _wayPoints;
        [SerializeField] private Transform _enemies;
        [SerializeField] private Gun _playerGun;
        [SerializeField] private UnityEvent _onGameOver;
        private EnemySpawner[] _enemySpawner;
        private int _maxIteration;

        private void OnEnable()
        {
            _enemySpawner = new EnemySpawner[_enemies.childCount];
            for (int i = 0; i < _enemySpawner.Length; i++)
            {
                _enemySpawner[i] = _enemies.GetChild(i).GetComponent<EnemySpawner>();
            }

            _maxIteration = _enemySpawner.Length;
        }

        private void Start()
        {
            _playerGun.enabled = false;
        }

        private void GameOver()
        {
            _onGameOver?.Invoke();
        }

        private IEnumerator Combat(int iteration)
        {
            while (_player.AtTheWayPoint is false)
            {
                yield return null;
            }

            _enemySpawner[iteration].EnemyAgression(_player.transform);
            _playerGun.enabled = true;
        }

        public void GamePlay(int iteration)
        {
            if (iteration == _maxIteration)
            {
                GameOver();
            }
            else 
            {
                _player.MoveStart(_wayPoints.GetWayPoint(iteration));
                _playerGun.enabled = false;
                StartCoroutine(Combat(iteration));
            }
        }

        public int Iteration { get; set; }
    }
}

