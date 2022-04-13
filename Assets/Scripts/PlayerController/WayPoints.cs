using UnityEngine;
using System.Collections.Generic;

namespace TestGame.Scripts
{
    public class WayPoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> _wayPoints = new List<Transform>();

        private Transform _transform;

        private void OnEnable()
        {
            _transform = transform;

            for (int i = 0; i < _transform.childCount; i++)
                _wayPoints.Add(_transform.GetChild(i));
        }

        public Transform GetWayPoint(int number) 
        {
            return _wayPoints[number];
        }
    }
}
