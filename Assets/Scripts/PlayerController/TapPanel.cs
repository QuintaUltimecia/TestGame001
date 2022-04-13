using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace TestGame.Scripts
{
    public class TapPanel : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent _onMove;

        public void OnPointerClick(PointerEventData eventData) => _onMove?.Invoke();
    }
}
