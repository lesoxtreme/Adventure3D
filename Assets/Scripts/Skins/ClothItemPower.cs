using UnityEngine;

namespace Cloth
{
    public class ClothItemPower : ClothItemBase
    {
        public float damageMultiplier = .5f;
        public override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamageMultiplier(damageMultiplier, duration);
        }
    }
}
