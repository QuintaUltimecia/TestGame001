using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace TestGame.Scripts
{
    public class Enemy : Character
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _distanceToPlayer;

        private Transform _transform;
        private EnemySpawner _enemySpawner;
        private Rigidbody _rigidbody;
        private Animator _animator;

        [SerializeField] private UnityEvent _onDeath;
        [SerializeField] UnityEvent _onDamage;

        private void OnEnable()
        {
            _transform = transform;
            _enemySpawner = transform.parent.GetComponent<EnemySpawner>();
            _rigidbody = GetComponent<Rigidbody>();
            _animator = transform.GetChild(0).GetComponent<Animator>();

            _onDeath?.AddListener(Death);
        }

        private IEnumerator Move(Transform player)
        {
            while (Vector3.Distance(_transform.position, player.position) > _distanceToPlayer)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, player.position, _moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public override void GetDamage(int damage)
        {
            base.GetDamage(damage);
            _onDamage?.Invoke();
            if (Health == 0) _onDeath?.Invoke();
        }

        public void Death()
        {
            _enemySpawner.RemoveEnemy(gameObject);
            _rigidbody.isKinematic = false;
            _animator.enabled = false;
            transform.rotation = Quaternion.Euler(-15, 180, 0);
            gameObject.layer = 0;
        }

        public void MoveStart(Transform player) { if (player != null) StartCoroutine(Move(player)); }
    }
}
