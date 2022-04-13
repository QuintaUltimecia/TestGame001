using UnityEngine;
using System.Collections.Generic;

namespace TestGame.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private int _count;
        [SerializeField] private Core _core;

        private const int _distanceBetweenEnemy = 3;

        [SerializeField] private List<GameObject> _enemyPool = new List<GameObject>();

        private void OnEnable()
        {
            _core = FindObjectOfType<Core>();
        }

        private void Start()
        {
            for (int i = 0, x = _distanceBetweenEnemy; i < _count; i++)
            {
                GameObject enemy = Instantiate(_enemy, transform);
                enemy.transform.localPosition = new Vector3(-x, 0, 0);
                _enemyPool.Add(enemy);
                x -= _distanceBetweenEnemy;
            }
        }

        public void EnemyAgression(Transform player)
        {
            for (int i = 0; i < _enemyPool.Count; i++)
            {
                _enemyPool[i].GetComponent<Enemy>().MoveStart(player);
            }
        }

        public void RemoveEnemy(GameObject enemy)
        {
            _enemyPool.Remove(enemy);
            print(EnemyCount);
            if (EnemyCount == 0) 
            {
                _core.Iteration++;
                _core.GamePlay(_core.Iteration);
            };
        }

        public int EnemyCount { get => _enemyPool.Count; }
    }
}
