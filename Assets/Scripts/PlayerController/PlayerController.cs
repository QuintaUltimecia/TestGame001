using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace TestGame.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private UnityEvent _onMove;
        [SerializeField] private UnityEvent _offMove;

        private Transform _transform;

        private bool _atTheWayPoint = true;

        private void OnEnable() => _transform = transform;

        private IEnumerator Move(Transform wayPoint)
        {
            _onMove?.Invoke();
            _atTheWayPoint = false;
            StartCoroutine(Rotation(wayPoint.position));
            while (Vector3.Distance(_transform.position, wayPoint.position) != 0)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wayPoint.position, _moveSpeed * Time.deltaTime);
                yield return null;
            }
            StartCoroutine(Rotation(Vector3.zero));
            _atTheWayPoint = true;
            _offMove?.Invoke();
        }

        public IEnumerator Rotation(Vector3 position)
        {
            while (_transform.rotation != Quaternion.LookRotation(position))
            {
                _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(position), _rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public void MoveStart(Transform wayPoint) { if (wayPoint != null) StartCoroutine(Move(wayPoint)); }
        public bool AtTheWayPoint { get => _atTheWayPoint; }
    }
}
