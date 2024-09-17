using SimplePartLoader;
using SimplePartLoader.CarGen;
using System;
using UnityEngine;
using System.Diagnostics;
using RVP;
using RR_Coronet.Dependencys;
using HarmonyLib;
using RR_Coronet.Dependencys.Autoloader;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace RR_Coronet
{


    public class ModMain : Mod
    {
        // Looking for docs? https://fedearre.github.io/my-garage-modding-docs/
        public override string ID => "Robin.Coronet";
        public override string Name => "Robin's Coronet Mod (BETA)";
        public override string Author => "1998 Reliant Robin";
        public override string Version => "1.0.7";



        // Hey YOU! Have a good day. Okay?

        //Utils UI stuff

        internal static ModInstance ThisMod;

        internal static Checkbox DebugMode;

        ModDropdown PaintDrop;

        internal static bool Debug = false;

        string[] Paintlist = {"High", "Medium", "Low"};

        float REMOVE_LATER_PRICE_TOT = 0;

        Part US_Plate_Coro;
        
        Car CoronetSuperbee;
        Car CoronetRT500;
        Car CoronetSedan;
       // Car CoronetWagon;

        AssetBundle CoronetBundle;

        public string ModLogName = "[Reliant Robin's Coronet Mod] ";

        public ModMain()
        {

            Stopwatch ModMainStopWatch = Stopwatch.StartNew();

            ThisMod = ModUtils.RegisterMod(this);

            DoPatching();

            ThisMod.Settings.PaintResolution = PaintingSystem.PartPaintResolution.High;

            ThisMod.Settings.AutomaticFitsToCar = new string[1];

            ThisMod.Settings.UseBackfaceShader = true;

            // Builds UI options
            BuildOptions();

            if (Debug) 
            {
                ThisMod.EnableDebug(false);
            }

            ThisMod.Settings.AutomaticFitsToCar[0] = "70 Coronet";

            String MethodString = "ModMain";

            PrettyLog.Log("", "Relient Robin's Coronet Mod " + " (Version : " + Version + ") " + "is now loading! Lets pray it loads lmao.");

            CoronetBundle = AssetBundle.LoadFromMemory(Properties.Resources.coronet);

            ModUtils.RegisterCarCategory("70 Coronet");

            PrettyLog.DebugLog(MethodString, "Car Catagory Registered!");

            PrettyLog.DebugLog(MethodString, "Sending Autoloader Bundle!");

            Autoloader.Autoload(ThisMod, CoronetBundle, "70");

            US_Plate_Coro = ThisMod.Load(CoronetBundle, "US_Plate1_CoronetDependent");

            US_Plate_Coro.UseHandAttachment();

            InteriorModShop.ModShop = CoronetBundle.LoadAsset<GameObject>("CoronetInteriorShopPREFAB");

            NewCarBuilder.ExhaustSmokeReplacement = CoronetBundle.LoadAsset<GameObject>("ExhaustSmoke");

            //Load and set our custom materials to their respective referances

            MaterialManager.SetupExternalMaterials(CoronetBundle, "CoronetWood");

            MaterialManager.SetupExternalMaterials(CoronetBundle, "ClusterTexture");

            MaterialManager.SetupExternalMaterials(CoronetBundle, "ClusterLabels");

            MaterialManager.SetupExternalMaterials(CoronetBundle, "RedMaterial");

            MaterialManager.SetupExternalMaterials(CoronetBundle, "ColumnPaint");

            MaterialManager.SetupExternalMaterials(CoronetBundle, "Rubber");

            // Load the stinkin cars!

            CoronetSuperbee = ThisMod.LoadCar(CoronetBundle, "Coronet_empty", "Coronet_empty2", "Coronet_Transparents");

            CoronetRT500 = ThisMod.LoadCar(CoronetBundle, "CoronetRT_Empty", "CoronetRT_Empty2", "Coronet_Transparents");

            // Load our profile

            NewCarBuilder.LocalLoadCar(CoronetSuperbee);
            NewCarBuilder.LocalLoadCar(CoronetRT500);

           // NewCarBuilder.LocalLoadCar(CoronetSedan);

            SPL.FirstLoad += new SPL.FirstLoadDelegate(this.OnFirstLoad);

            SPL.LoadFinish += new SPL.LoadFinishDelegate(this.OnLoadFinish);

            CoronetBundle.Unload(false);

            ModMainStopWatch.Stop();

            PrettyLog.Log(nameof(ModMain), "Mod Main Took :" + ModMainStopWatch.ElapsedMilliseconds.ToString() + " ms to load.");

        }
  
        public void OnFirstLoad() {
            String MethodString = "OnFirstLoad";

            MaterialManager.SetupMaterialsOnLoad();

            Stopwatch FirstLoadSW = Stopwatch.StartNew();

            foreach (Part part in ThisMod.Parts)
            {
               
                MaterialManager.ApplyMaterial(part);

                part.CarProps.NormalMesh = part.GetComponent<MeshFilter>().mesh;

                // Switch for all uniform changes.

                switch (part.PartInfo.RenamedPrefab) 
                {
                    case "RRLX_70_InnerPanelL":
                        Fixes.ReflectionFixes.FixInnerFenderSuspension(part.Prefab.gameObject, "L");
                        break;
                    case "RRLX_70_InnerPanelR":
                        Fixes.ReflectionFixes.FixInnerFenderSuspension(part.Prefab.gameObject, "R");
                        break;
                    case "RRLX_70_FramerailL":
                        Fixes.ReflectionFixes.FixRearSuspension(part.Prefab.gameObject, "L");
                        break;
                    case "RRLX_70_FramerailR":
                        Fixes.ReflectionFixes.FixRearSuspension(part.Prefab.gameObject, "R");
                        break;
                    case "RRLX_70_RearShockSupport":
                        Fixes.ReflectionFixes.FixRearShocks(part.Prefab.gameObject);
                        break;
                }

                // Less uniform changes

                if (part.CarProps.DMGRemovablepart == true) 
                {
                    
                    PrettyLog.Warn(MethodString, part.CarProps.PrefabName + " Has DMGRemovablepart set to true! This will cause issues with crash damage without its required component!");

                    DetachablePart RVPD = part.Prefab.AddComponent<DetachablePart>();

                    if (part.Properties.Contains("RemovableBodyPanel")) 
                    {
                        if (RVPD)
                        {
                            PrettyLog.DebugLog("RVPD", "2");
                            Fixes.PartFixes.SetDetachablePart("Bodypanel", RVPD);
                        }
                    }

                    if (part.PartInfo.Openable)
                    {

                        if (RVPD)
                        {
                            PrettyLog.DebugLog("RVPD", "2");
                            Fixes.PartFixes.SetDetachablePart("Openable", RVPD);
                        }
                    }
                    else 
                    {

                        part.CarProps.DMGAnyDamag = true;
                        part.CarProps.DMGdisplacepart = true;

                        PrettyLog.DebugLog("RVPD", "1");

                        if (RVPD)
                        {
                            PrettyLog.DebugLog("RVPD", "2");
                            Fixes.PartFixes.SetDetachablePart("Other", RVPD);
                        }
                        else
                        {
                            PrettyLog.Error("RVP Setup", "Could not find Detatchable Part Comp");
                            part.CarProps.DMGRemovablepart = false;
                            part.CarProps.DMGAnyDamag = false;
                        }
                    }
                }
            }

            PrettyLog.DebugLog(MethodString, "Loop Complete.");

            Fixes.PartFixes.MakeWindowRegs(1, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftFL_G9"]);

            Fixes.PartFixes.MakeWindowRegs(2, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftFR_G9"]);

            Fixes.PartFixes.MakeWindowRegs(3, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftRL_G9"]);

            Fixes.PartFixes.MakeWindowRegs(4, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftRR_G9"]);

            Fixes.PartFixes.FixGasCap(Autoloader.LoadedPrefabs["RRLX_70_Gas_Tank_G9"]);

            Fixes.PartFixes.BuildIgitionKey(Autoloader.LoadedPrefabs["RRLX_70_Column_IG1_G9"].Prefab.transform.Find("GameObject").GetChild(0).gameObject);

            Fixes.PartFixes.BuildIgitionKey(Autoloader.LoadedPrefabs["RRLX_70_Steering_Column2_G9"].Prefab.transform.Find("GameObject").GetChild(0).gameObject);

            Fixes.PartFixes.AddHazzards(Autoloader.LoadedPrefabs["RRLX_70_Steering_Column2_G9"]);

            Fixes.PartFixes.FixCluster(Autoloader.LoadedPrefabs["RRLX_70_Cluster_Rallye1_G9"]);

            LessShityCatalogHandler.Setup();

            ModUtils.ExecuteNextFrame(ExcecuteOnNextFrameBoltMats);

            FirstLoadSW.Stop();

            PrettyLog.Log(nameof(OnFirstLoad), "Took : " + FirstLoadSW.ElapsedMilliseconds.ToString() + " ms to load.");


        }


        private void BuildOptions() 
        {

            PaintDrop = ThisMod.AddDropdownToUI("RR_CORONET_PAINT_VALUE", "Paint Qualilty", Paintlist, 1);

            ThisMod.AddSpacerToUI();

            DebugMode = ThisMod.AddCheckboxToUI("RR_CORONET_DEBUG_VALUE", "Enable Debug Mode", Debug);
        }

        public static void DoPatching() 
        {
            var harmony = new Harmony("com.Robin.Coronet");

            harmony.PatchAll();
        }

        public void OnLoadFinish()
        {
        }
        public override void OnLoad()
        {
            ShopBuilder.AddMoparWheelsToShop();
            ShopBuilder.BuildBuyStand(US_Plate_Coro.Prefab.gameObject);
        }

        public override void OnNewMapLoad()
        {
            ModUtils.ExecuteNextFrame(delegate
            {
                InteriorModShop.MakeShop();
            });
        }

        void ExcecuteOnNextFrameBoltMats() 
        {
            foreach (Part p in ThisMod.Parts) 
            {
                MaterialManager.FixTheFUCKINGBoltMats(p);
            }
        }

        public override void Continue()
        {
        }
        public override void OnSave()
        {
        }
        public override void Update()
        {
        }

    }
}
