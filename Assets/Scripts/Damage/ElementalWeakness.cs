using System;

namespace Damage
{
    public class ElementalWeakness : DamageModBehavior
    {
        public Element weakness;
        public float multiplier = 2;
        
        protected override int Modifier(int damage, Element element)
        {
            if (element == weakness)
            {
                return (int) Math.Round(damage * multiplier);
            }
            return damage;
        }
    }
}