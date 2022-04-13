using UnityEngine;

namespace TestGame.Scripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _damage;

        private Vector3 _target;

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, _moveSpeed * Time.deltaTime);
        }

        public Vector3 Target { get => _target; set => _target = value; }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.GetDamage(_damage);
            }
            gameObject.SetActive(false);
        }
    }
}
