using SimplePartLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RR_Coronet
{
    internal class ExpandedFind
    {
        public static GameObject FindPartPrefab(ModInstance ThisMod, string PrefabName)
        {
            GameObject ReturnObject = null;

            Part p = ThisMod.Parts.Find(x => x.CarProps.PrefabName == PrefabName);
            
            ReturnObject = p.Prefab.gameObject;

            return ReturnObject;
        }

        public static Part FindPart(ModInstance ThisMod, string PrefabName)
        {
            Part ReturnObject = null;

            Part p = ThisMod.Parts.Find(x => x.CarProps.PrefabName == PrefabName);

            ReturnObject = p;

            return ReturnObject;
        }
    }
}
