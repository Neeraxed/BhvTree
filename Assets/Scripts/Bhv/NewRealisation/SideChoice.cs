
using UnityEngine;

namespace Assets.Scripts.Bhv.NewRealisation
{
    public static class SideChoice
    {
        public static int ChooseSide(int layer)
        {
            int resultLayer = 0;

            if (LayerMask.LayerToName(layer).Equals("Enemy"))
                resultLayer = 1 << LayerMask.NameToLayer("AllyUnit");
            else if (LayerMask.LayerToName(layer).Equals("AllyUnit"))
                resultLayer = 1 << LayerMask.NameToLayer("Enemy");

            return resultLayer;
        }
    }
}
