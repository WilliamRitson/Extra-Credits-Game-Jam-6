using UnityEngine;

namespace Damage
{
    [RequireComponent(typeof(Damageable))]
    public abstract class DamageModBehavior : MonoBehaviour
    {
        private Damageable toModify;
        private void Start()
        {
            toModify = GetComponent<Damageable>();
            toModify.Modifiers.Add(Modifier);
        }

        private void OnDestroy()
        {
            toModify.Modifiers.Remove(Modifier);
        }

        protected abstract float Modifier(float damage, Element element);
    }
}