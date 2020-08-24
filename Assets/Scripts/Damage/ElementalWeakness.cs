using System;

namespace Damage
{
    public class ElementalWeakness : DamageModBehavior
    {
        public Element weakness;
        public float multiplier = 2;
        
        protected override float Modifier(float damage, Element element)
        {
            if (element == weakness)
            {
                return (float) Math.Round(damage * multiplier);
            }
            return damage;
        }
    }
}