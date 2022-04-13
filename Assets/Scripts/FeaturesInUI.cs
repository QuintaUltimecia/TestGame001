using UnityEngine;
using UnityEngine.UI;

namespace TestGame.Scripts
{
    public class FeaturesInUI : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private Slider _healthSlider;

        private void Start()
        {
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = _character.Health;
            _healthSlider.value = _character.Health;
        }

        public void GetFeaturesInUI()
        {
            _healthSlider.value = _character.Health;
        }
    }
}
