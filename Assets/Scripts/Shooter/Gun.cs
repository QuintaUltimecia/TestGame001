using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

namespace TestGame.Scripts
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _startBulletPosition;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private int _clipWithBullets;
        [SerializeField] private LayerMask _shootLayer;
        [SerializeField] private UnityEvent _onShoot;

        private Camera _camera;
        private int _bulletCount;

        [SerializeField] private List<GameObject> _bulletsPool = new List<GameObject>();

        private void OnEnable()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _bulletCount = _clipWithBullets - 1;

            for (int i = 0; i < _clipWithBullets; i++)
            {
                GameObject bullet = Instantiate(_bullet, transform);
                bullet.transform.position = _startBulletPosition.position;
                _bulletsPool.Add(bullet);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) RaycastRealization();
        }

        private void RaycastRealization()
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit, 100f, _shootLayer))
            {
                _onShoot?.Invoke();
                _bulletsPool[_bulletCount].GetComponent<Bullet>().Target = hit.point;
                _bulletsPool[_bulletCount].SetActive(true);
                _bulletCount--;
                if (_bulletCount == 0) Reload();
            }
        }

        private void Reload()
        {
            for (int i = 0; i < _bulletsPool.Count; i++)
            {
                _bulletsPool[i].transform.position = _startBulletPosition.position;
            }
            _bulletCount = _clipWithBullets - 1;
        }
    }
}
