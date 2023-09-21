
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation
{
    public static class SideChoice
    {
        public static int ChooseWhomToHit(int layer)
        {
            int resultLayer = 0;

            if (LayerMask.LayerToName(layer).Equals("Enemy"))
                resultLayer = 1 << LayerMask.NameToLayer("Ally");
            else if (LayerMask.LayerToName(layer).Equals("Ally"))
                resultLayer = 1 << LayerMask.NameToLayer("Enemy");

            return resultLayer;
        }
    }
}
