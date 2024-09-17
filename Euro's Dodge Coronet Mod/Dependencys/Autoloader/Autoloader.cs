using FluffyUnderware.DevTools.Extensions;
using RVP;
using SimplePartLoader;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace RR_Coronet
{
    internal class Autoloader
    {
        static int PartsLoaded;

        static int InteriorIndex = 0;

        public static string Identifer = "RRLX_";

        static String MethodString = "Autoloader";
        public static List<CarProperties> ChildDMG = new List<CarProperties>();
        public static Dictionary<string, Part> LoadedPrefabs = new Dictionary<string, Part>();

        public static void Autoload(ModInstance ModI, AssetBundle Bundle, String Prefix) 
        {
            if (Bundle == null)
            {
                PrettyLog.Error(MethodString, "Failed to find existing bundle, Please check resources!");
            }
            else 
            {
                PrettyLog.DebugLog(MethodString, "Bundle Recived! Loading..");

                Stopwatch AutoLoaderStopwatch = Stopwatch.StartNew();

                foreach (string asset in Bundle.GetAllAssetNames())
                {
                    if (asset.Contains(Prefix))
                    {
                        PrettyLog.DebugLog(MethodString, asset + " Is being processed!");

                        Part newpart = ModI.Load(Bundle, asset);

                        Partinfo PI1 = newpart.Prefab.GetComponent<Partinfo>();

                        LoadedPrefabs.Add(newpart.CarProps.PrefabName, newpart);

                        PrettyLog.DebugLog(MethodString, newpart.CarProps.name + " Has Been Auto Loaded!");

                        if (newpart.CarProps.gameObject.TryGetComponent<Partinfo>(out Partinfo PI))
                        {

                            if (newpart.CarProps.Openable == true)
                            {

                                if (PI.Rdoor)
                                {

                                    newpart.MakeOpenable(OpeningType.DOOR_RIGHT);

                                    newpart.PartInfo.HingePivot = newpart.CarProps.gameObject.transform.Find("HingePivot").gameObject;

                                }

                                if (PI.Ldoor)
                                {
                                    newpart.MakeOpenable(OpeningType.DOOR_LEFT);

                                    newpart.PartInfo.HingePivot = newpart.CarProps.gameObject.transform.Find("HingePivot").gameObject;
                                }

                                if (PI.HoodHalf)
                                {

                                    newpart.MakeOpenable(OpeningType.HOOD_HALF);

                                    PrettyLog.Log(nameof(Autoloader), newpart.CarProps.PartName);

                                    newpart.PartInfo.HingePivot = newpart.CarProps.gameObject.transform.Find("HingePivot").gameObject;
                                }

                                if (PI.Trunk)
                                {

                                    newpart.MakeOpenable(OpeningType.TRUNK);

                                    newpart.PartInfo.HingePivot = newpart.CarProps.gameObject.transform.Find("HingePivot").gameObject;

                                }

                            }

                        }

                        if (newpart.HasProperty("Prytool"))
                        {
                            newpart.UsePrytoolAttachment(); 
                        }

                        if (newpart.HasProperty("Hand"))
                        {
                            newpart.UseHandAttachment();
                        }

                        if (newpart.HasProperty("CarLight"))
                        {
                            newpart.SetStandardShader();
                        }

                        if (newpart.PartInfo.Window) 
                        {
                            if (newpart.PartInfo.RenamedPrefab == Identifer + "70C_Windshield" || newpart.PartInfo.RenamedPrefab == Identifer + "70C_Window_R")
                            {
                                PrettyLog.DebugLog(nameof(Autoloader), "Skipping 70C_Windshield and 70C_Window_R");
                            }
                            else 
                            {
                                ShatterPart SP =  newpart.CarProps.gameObject.AddComponent<RVP.ShatterPart>();
                                SP.tag = "Window";
                            
                            }
                        
                        }

                        if (newpart.CarProps.Interior) 
                        {
                            InteriorModShop.InteriorPartsList.Insert(InteriorIndex, newpart.Prefab.gameObject);
                            InteriorIndex += 1;
                        }

                        if (newpart.PartInfo.Rim) 
                        {
                            newpart.Prefab.tag = "Wheel1";
                            newpart.Prefab.transform.Find("TireValve").gameObject.layer = LayerMask.NameToLayer("FlatBolts");
                            newpart.Prefab.GetComponent<CarProperties>().RealWheel = true;
                            newpart.Prefab.AddComponent<ANYdamage>().Rim = true;
                        
                        }
                        newpart.PartInfo.price -= 15f;
                    }

                }                
                PartsLoaded = LoadedPrefabs.Count;

                AutoLoaderStopwatch.Stop();
                PrettyLog.Log(MethodString, "Autoloader Finished! Loaded " + PartsLoaded + " Parts with the prefix '" + Prefix + "'" + " In : " + AutoLoaderStopwatch.Elapsed.ToString());

            }
        }

        public static void ManualLoad(AssetBundle Bundle, String Name) 
        {
           
            Bundle.LoadAsset(Name);

        }

    }
}
