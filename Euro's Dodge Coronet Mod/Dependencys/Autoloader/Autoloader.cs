using FluffyUnderware.DevTools.Extensions;
using RR_Coronet.Dependencys.Autoloader;
using RVP;
using SimplePartLoader;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

namespace RR_Coronet
{
    internal class Autoloader
    {
        public static Dictionary<string, Part> LoadedPrefabs = new Dictionary<string, Part>();

        public static void Autoload(ModInstance ThisMod, AssetBundle ab, string prefix)
        {
            Stopwatch AutoloadMethodStopWatch = Stopwatch.StartNew();

            foreach (string assetString in ab.GetAllAssetNames())
            {
                if (assetString.Contains(prefix))
                {
                    PrettyLog.DebugLog(nameof(Autoloader), "Loading Part path : " + assetString);

                    Part LocalLoadedPart = null;

                    try
                    {
                        LocalLoadedPart = ThisMod.Load(ab, assetString);

                        LoadedPrefabs.Add(LocalLoadedPart.CarProps.PrefabName, LocalLoadedPart);

                        PrettyLog.DebugLog(nameof(Autoloader), "Loaded Part : " + LocalLoadedPart.CarProps.PrefabName + " | Using path : " + assetString);

                    }
                    catch (Exception ex)
                    {
                        PrettyLog.Error(nameof(Autoloader), "Failed to load Part path : " + assetString + " | Exception : " + ex.ToString());
                        continue;
                    }

                    CheckOpenableStatus(LocalLoadedPart);

                    if (LocalLoadedPart.Properties.Count > 0)
                    {
                        CheckPartPropsStatus(LocalLoadedPart);
                    }

                    LocalLoadedPart.PartInfo.price -= 15f;

                }
            }

            AutoloadMethodStopWatch.Stop();

            PrettyLog.Log(nameof(Autoloader), "Loaded " + LoadedPrefabs.Count + " Parts from " + ab.name + " Assetbundle in : " + AutoloadMethodStopWatch.ElapsedMilliseconds + " ms");
        }


        private static void CheckOpenableStatus(Part LocalLoadedPart) 
        {
            if (LocalLoadedPart.Properties.Contains("DisableOpenable") == true)
            {
                return;
            }

            if (LocalLoadedPart.PartInfo.Openable == true)
            {
                if (LocalLoadedPart.PartInfo.HoodHalf)
                {
                    LocalLoadedPart.MakeOpenable(OpeningType.HOOD_HALF);
                    return;
                }
                if (LocalLoadedPart.PartInfo.Trunk)
                {
                    LocalLoadedPart.MakeOpenable(OpeningType.TRUNK);
                    return;
                }
                if (LocalLoadedPart.PartInfo.Ldoor)
                {
                    LocalLoadedPart.MakeOpenable(OpeningType.DOOR_LEFT);
                    return;
                }
                if (LocalLoadedPart.PartInfo.Rdoor)
                {
                    LocalLoadedPart.MakeOpenable(OpeningType.DOOR_RIGHT);
                    return;
                }
            }
        }

        private static void CheckPartPropsStatus(Part LocalLoadedPart) 
        {
            foreach (string Property in LocalLoadedPart.Properties) 
            {
                switch (Property) 
                {
                    case "Prytool":
                        LocalLoadedPart.UsePrytoolAttachment();
                        break;
                    case "Hand":
                        LocalLoadedPart.UseHandAttachment();
                        break;
                    case "Rim":
                        LocalLoadedPart.Prefab.tag = "Wheel1";
                        LocalLoadedPart.Prefab.transform.Find("TireValve").gameObject.layer = LayerMask.NameToLayer("FlatBolts");
                        LocalLoadedPart.CarProps.RealWheel = true;
                        LocalLoadedPart.Prefab.AddComponent<ANYdamage>().Rim = true;
                        break;
                    case "CarLight":
                        LocalLoadedPart.SetStandardShader();
                        break;
                }
            }
        }


        public static void Load(AssetBundle Bundle, String Name, bool isPart = false) 
        {

            try
            {
                if (isPart)
                {
                    Part p = ModMain.ThisMod.Load(Bundle, "Name");
                    LoadedPrefabs.Add(p.CarProps.PrefabName, p);
                }
                else
                {
                    Bundle.LoadAsset(Name);
                }

            }
            catch(Exception ex) 
            {
                PrettyLog.Error(nameof(Load), ex.ToString());
            }

        }

    }
}
