using UnityEngine;

namespace TestGame.Scripts
{
    public abstract class Character : MonoBehaviour, IGetDamage
    {
        [SerializeField] private int _health;

        public virtual void GetDamage(int damage)
        {
            Health -= damage;
            print(gameObject.name + $" get {damage} damage, health: {Health}");
        }

        public int Health { get => _health; private set => _health = Mathf.Max(0, value); }
    }
}
