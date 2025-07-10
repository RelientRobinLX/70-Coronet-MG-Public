using HarmonyLib;
using RR_Coronet.CustomMonoScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR_Coronet.HarmonyPatching
{
    [HarmonyPatch(typeof(CarProperties), nameof(CarProperties.Attach))]
    public static class HideMeshPatch
    {
        [HarmonyPostfix]
        static void PostFix(CarProperties __instance)
        {
            PrettyLog.Log(__instance.gameObject.name, "Called 1");

            if (__instance.Attached == true) 
            {
                HideMesh[] hideMeshes = __instance.gameObject.GetComponentsInChildren<HideMesh>();

                PrettyLog.Log(__instance.gameObject.name, "Called 2");


                if (hideMeshes.Length > 0)
                {
                    foreach (HideMesh hideMesh in hideMeshes)
                    {
                        PrettyLog.Log(__instance.gameObject.name, "Called 3");
                        hideMesh.Toggle(true);
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }

    [HarmonyPatch(typeof(CarProperties), nameof(CarProperties.Remove))]
    public static class HideMeshPatch2
    {
        [HarmonyPostfix]
        static void PostFix(CarProperties __instance)
        {
            PrettyLog.Log(__instance.gameObject.name, "Called R 1");

            HideMesh[] hideMeshes = __instance.gameObject.GetComponentsInChildren<HideMesh>();

            PrettyLog.Log(__instance.gameObject.name, "Called R 2");

            if (hideMeshes.Length > 0)
            {
                foreach (HideMesh hideMesh in hideMeshes)
                {
                    PrettyLog.Log(__instance.gameObject.name, "Called R 3");
                    hideMesh.Toggle(false);
                }
            }
        }
    }
}
