using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RR_Coronet.CustomMonoScripts
{

    [HarmonyPatch(typeof(MainCarProperties), nameof(MainCarProperties.CreatingJunkyard))]
    public static class RarityControllerPatcher
    {
        [HarmonyPostfix]
        private static void PostFix(MainCarProperties __instance)
        {

            Transform CarObject = __instance.transform;

            PrettyLog.Log("Rarity Patch", "ALIVE!");

            CoronetHarmonyParamters CHP = CarObject.GetComponent<CoronetHarmonyParamters>();

            if (CHP) 
            {
                StartRoll();
            }

            void StartRoll() 
            {

                float RandomNumber = UnityEngine.Random.Range(CHP.RarityRate.x, CHP.RarityRate.y);

                PrettyLog.Log("Rarity Patch", "Starting roll!");


                if (RandomNumber > CHP.RarityRate.z)
                {
                    PrettyLog.Log("Rarity Patch", "Rolled In Favour, Destorying!");

                    GameObject.Destroy(CarObject.gameObject);
                }

            }

        }

    }
 
}
