using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using NWH.VehiclePhysics2;
using Rewired;
using RR_Coronet;
using RR_Coronet.CustomMonoScripts;
using UnityEngine.Build.Pipeline;

// StartCar


    [HarmonyPatch(typeof(MainCarProperties), nameof(MainCarProperties.EngineStop2))]
    public static class OilGaugePatch
    {
        [HarmonyPostfix]
        static void PostFix(MainCarProperties __instance) 
        {   
           if (__instance.Cluster) 
           {
              if (__instance.Cluster.gameObject.TryGetComponent<OilGaugeFix>(out OilGaugeFix OGF)) 
              {
                try 
                {
                    OGF.OilGuage.Value = 0;
                    OGF.Casted = false;
                    OGF.enabled = false;
                }
                catch (Exception ex) 
                {
                    PrettyLog.Error(nameof(MainCarProperties.EngineStop2), "MCP error, This may break the car, Exception : " + ex);                
                }
              }
           }     
        }
    }

    [HarmonyPatch(typeof(MainCarProperties), nameof(MainCarProperties.StartCar))]
    public static class OilGaugePatch2
    {
        [HarmonyPostfix]
        static void PostFix(MainCarProperties __instance)
        {
        if (__instance.Cluster)
        {
            if (__instance.Cluster.gameObject.TryGetComponent<OilGaugeFix>(out OilGaugeFix OGF))
            {
                try
                {
                    OGF.Casted = true;
                    OGF.enabled = true;
                }
                catch (Exception ex)
                {
                    PrettyLog.Error(nameof(MainCarProperties.StartCar), "MCP error, This may break the car, Exception : " + ex);
                }
            }
        }
        }
    }