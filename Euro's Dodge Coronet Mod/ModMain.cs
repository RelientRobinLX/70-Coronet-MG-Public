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
using FluffyUnderware.DevTools.Extensions;
using System.Linq;
using TMPro;
using RR_Coronet.CustomMonoScripts;
using System.Reflection;
using Den.Tools;
using PaintIn3D;
using System.ComponentModel;
using System.Collections.Generic;

namespace RR_Coronet
{


    public class ModMain : Mod
    {
        // Looking for docs? https://fedearre.github.io/my-garage-modding-docs/
        public override string ID => "Robin.Coronet";
        public override string Name => "Robin's Coronet Mod";
        public override string Author => "1998 Reliant Robin";
        public override string Version => "1.4";
        public override byte[] Icon => Properties.Resources.RCMicon;


        // Hey YOU! Have a good day. Okay?

        //Utils UI stuff

        internal static ModInstance ThisMod;

        internal static Checkbox DebugMode;

        internal static Checkbox SaveMode;

        internal static Checkbox InteriorShopEnabled;

        internal static Checkbox EnablePatchingCheckBox;

        internal static Checkbox EnableURPMode;

        internal static bool Debug = true;

        internal static bool EnablePatching = true;

        private static bool SaveDis = false;

        private static bool EnableInteriorStore = true;

        private static bool URP_Mode = false;

        internal static string Rbuild = "RELEASE";

        internal static string Tbuild = "EMERGENCY STF BUILD";

        internal static string build = Tbuild;

        Part US_Plate_Coro;
        
        Car CoronetSuperbee;
        Car CoronetRT500;
        Car Coronet500;

        Car CoronetSedan;
        Car CoronetWagon;

        Car SixtyNineCoronetSuperbee;
        Car SixtyNineCoronetRT;
        Car SixtyNineCoronet500;

        Car SixtyNineCoronetSedan;
        Car SixtyNineCoronetWagon;

        AssetBundle CoronetBundle;
        AssetBundle StartOptionBundle;
        AssetBundle MaterialsBundle;
        


        public string ModLogName = "[Reliant Robin's Coronet Mod] ";

        public ModMain()
        {

            PrettyLog.Log(nameof(ModMain), "Relient Robin's Coronet Mod is now loading! Lets pray it loads lmao.");

            PrettyLog.Log(nameof(ModMain), "(Version : " + Version + " Build Type : " + Tbuild + ")");

            Stopwatch ModMainStopWatch = Stopwatch.StartNew();

            ThisMod = ModUtils.RegisterMod(this);

            SetURPmode();

            // Builds UI options

            BuildOptions();

            if (EnablePatchingCheckBox.Checked) 
            {
                DoPatching();
            }


            if (DebugMode.Checked) 
            {
                ThisMod.EnableDebug(SaveDis);
            }
  
            CoronetBundle = AssetBundle.LoadFromMemory(Properties.Resources.coronet);

            StartOptionBundle = AssetBundle.LoadFromMemory(Properties.Resources.coronet_startoption);


            if (!URP_Mode)
            {
                MaterialsBundle = AssetBundle.LoadFromMemory(Properties.Resources.coronet_materials);

                ThisMod.Settings.PaintResolution = PaintingSystem.PartPaintResolution.High;

                ThisMod.Settings.UseBackfaceShader = true;
            }
            else 
            {
                MaterialsBundle = AssetBundle.LoadFromMemory(Properties.Resources.coronet_materials_urp);

            }

            ModUtils.RegisterCarCategory("70 Coronet");
            ModUtils.RegisterCarCategory("69 Coronet");

            PrettyLog.DebugLog(nameof(ModMain), "Car Catagorys Registered!");

            PrettyLog.DebugLog(nameof(ModMain), "Sending Autoloader Bundle!");

            Autoloader.Autoload(ThisMod, CoronetBundle, "rrlx");

            US_Plate_Coro = ThisMod.Load(CoronetBundle, "US_Plate1_CoronetDependent");

            US_Plate_Coro.UseHandAttachment();

            InteriorModShop.ModShop = CoronetBundle.LoadAsset<GameObject>("CoronetInteriorShopPREFAB");

            CarBuilder.SetExhaustParticleSystem(CoronetBundle.LoadAsset<GameObject>("ExhaustSmoke"));

            // Start Options

            // Set Bundle
            StartOptionManager.SetStartOptionBundle(StartOptionBundle);

            // Load the vinyl roof start option.
            StartOptionManager.LoadOption("VinylRoofStartOption", "RRLX_70C_Roof2B_G9");
            // Load the Chrome late front bumper option.
            StartOptionManager.LoadOption("FrontSupportLateChromeStartOption", "RRLX_70_FrontBumperSupport_G10");
            // Load the Spoiler Trunk option.
            StartOptionManager.LoadOption("TrunkSpoilerStartOption", "RRLX_70_Trunk_G9");

            // Set the vinyl roof start option.
            Autoloader.LoadedPrefabs["RRLX_70C_Roof1_G9"].AddStartOption(StartOptionManager.GetStartOption("VinylRoofStartOption"));
            // Set the chrome late front support start option.
            Autoloader.LoadedPrefabs["RRLX_70_FrontBumperSupport_G10"].AddStartOption(StartOptionManager.GetStartOption("FrontSupportLateChromeStartOption"));
            // Set the Spoiler Trunk start option.
            Autoloader.LoadedPrefabs["RRLX_70_Trunk_G9"].AddStartOption(StartOptionManager.GetStartOption("TrunkSpoilerStartOption"));

            Fixes.PartFixes.FixWheels(Autoloader.LoadedPrefabs["RRLX_70_Steel1_15"]);
            Fixes.PartFixes.FixWheels(Autoloader.LoadedPrefabs["RRLX_70_Steel1W_15"]);

            //Init NMM.
            MaterialHandler.ImportCustomMaterial("RustyChrome", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("ClusterTexture", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("ClusterLabels", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("CoronetWood", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("RedMaterial", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("ColumnPaint", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("RedLightBarMat", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("Rubber", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("Siren", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("Shadow", MaterialsBundle);

            // Vinyl Stripe Textures

            MaterialHandler.ImportCustomMaterial("C-Stripe-Black-L", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("C-Stripe-Black-R", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("C-Stripe-White-L", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("C-Stripe-White-R", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("Striaght-Stripe-Black-L", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("Striaght-Stripe-Black-R", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("Striaght-Stripe-White-L", MaterialsBundle);

            MaterialHandler.ImportCustomMaterial("Striaght-Stripe-White-R", MaterialsBundle);

            // Load the stinkin cars!

            CoronetSuperbee = ThisMod.LoadCar(CoronetBundle, "Coronet_empty", "Coronet_empty2", "Coronet_Transparents");

            CoronetRT500 = ThisMod.LoadCar(CoronetBundle, "CoronetRT_Empty", "CoronetRT_Empty2", "Coronet_Transparents");

            //Coronet500 = ThisMod.LoadCar(CoronetBundle, "CoronetConvert_Empty", "CoronetConvert_Empty2", "Coronet_Transparents");

            SixtyNineCoronetSuperbee = ThisMod.LoadCar(CoronetBundle, "Coronet69_empty", "Coronet69_empty2", "Coronet_Transparents");

            // Init CarBuilder.
            CarBuilder.CarBuilderInit();


            // Load our profile
            CarBuilder.LoadCarPreset(CoronetSuperbee);
            CarBuilder.LoadCarPreset(CoronetRT500);
            CarBuilder.LoadCarPreset(SixtyNineCoronetSuperbee);

            //NewCarBuilder.LocalLoadCar(CoronetSuperbee);
            //NewCarBuilder.LocalLoadCar(CoronetRT500);
            //NewCarBuilder.LocalLoadCar(Coronet500);

            SPL.FirstLoad += new SPL.FirstLoadDelegate(this.OnFirstLoad);

            SPL.LoadFinish += new SPL.LoadFinishDelegate(this.OnLoadFinish);

            // !!!!!!! DISABLE BEFORE RELASE !!!!!!!
            //ThisMod.GenerateThumbnails();

            CoronetBundle.Unload(false);

            ModMainStopWatch.Stop();

            PrettyLog.Log(nameof(ModMain), "Mod Main Took :" + ModMainStopWatch.ElapsedMilliseconds.ToString() + " ms to load.");

        }
    
        public void OnFirstLoad() 
        {
            PrettyLog.Log(nameof(OnFirstLoad), "Firstload Start");

            Stopwatch FirstLoadSW = Stopwatch.StartNew();

            PrettyLog.Log(nameof(OnFirstLoad), "Init Material Handler");

            MaterialHandler.InitF();

            PrettyLog.Log(nameof(MaterialHandler), "Loading StartOptions");

            StartOptionManager.LoadExistingOption("CylinderBlockI6all", "I6M");

            StartOptionManager.LoadExistingOption("GearBox507", "GB5");
            StartOptionManager.LoadExistingOption("GearBox07A", "GBA");

            StartOptionManager.LoadExistingOption("GearStick07GripBright", "GSB");
            StartOptionManager.LoadExistingOption("GearStick07GripDark", "GSD");

            PrettyLog.Log(nameof(MaterialHandler), "StartOptions Loaded");

            Stopwatch MaterialsStopWatch = Stopwatch.StartNew();

            PrettyLog.Log(nameof(MaterialHandler), "Starting Material Loop.");

            foreach (Part part in ThisMod.Parts)
            {
                PrettyLog.Log(nameof(OnFirstLoad), part.CarProps.PrefabName);

                Grid grid = part.GetComponent<Grid>();
                Grid[] grids = part.Prefab.GetComponentsInChildren<Grid>();

                if (grid) 
                {
                    MaterialHandler.UseLegacySorting(part, grid);

                    if (grids.Count() > 0)
                    {

                        grids.Remove(grid);

                        foreach (Grid g in grids)
                        {
                            if (g.gameObject.name != grid.gameObject.name)
                            {
                                PrettyLog.Log("CHILD PART", g.gameObject.name);
                                MaterialHandler.UseLegacyChildSorting(g.gameObject.GetComponent<MeshRenderer>(), g);
                            }
                        }
                    }
                }

                if (URP_Mode == true) 
                {
                   // FixOddMaterialGlitch();
                }

                MaterialsStopWatch.Stop();

                PrettyLog.Log(nameof(MaterialHandler), "Material Loop Finished in " + MaterialsStopWatch.ElapsedMilliseconds + "ms");



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
                    
                    PrettyLog.Warn(nameof(OnFirstLoad), part.CarProps.PrefabName + " Has DMGRemovablepart set to true! This will cause issues with crash damage without its required component!");

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

               MaterialHandler.FixBoltMatShading(part);
            }
             

            PrettyLog.DebugLog(nameof(OnFirstLoad), "Loop Complete.");

            // Add the required components to the window reg parts.

            Fixes.PartFixes.MakeWindowRegs(1, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftFL_G9"]);

            Fixes.PartFixes.MakeWindowRegs(2, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftFR_G9"]);

            Fixes.PartFixes.MakeWindowRegs(3, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftRL_G9"]);

            Fixes.PartFixes.MakeWindowRegs(4, Autoloader.LoadedPrefabs["RRLX_70C_WindowLiftRR_G9"]);

            // Fix the gas tank caps.

            Fixes.PartFixes.FixGasCap(Autoloader.LoadedPrefabs["RRLX_70_Gas_Tank_G9"]);

            Fixes.PartFixes.FixGasCap(Autoloader.LoadedPrefabs["RRLX_70_Gas_Tank2_G9"]);

            // Build the ignition keys for both steeering columns (Support legacy)

            Fixes.PartFixes.BuildIgitionKey(Autoloader.LoadedPrefabs["RRLX_70_Column_IG1_G9"].Prefab.transform.Find("GameObject").GetChild(0).gameObject);

            Fixes.PartFixes.BuildIgitionKey(Autoloader.LoadedPrefabs["RRLX_70_Steering_Column2_G9"].Prefab.transform.Find("GameObject").GetChild(0).gameObject);

            // Add the hazzards to the column
            
            Fixes.PartFixes.AddHazzards(Autoloader.LoadedPrefabs["RRLX_70_Steering_Column2_G9"]);

            // Add all the NHW stuff to the cluster.

            Fixes.PartFixes.FixCluster(Autoloader.LoadedPrefabs["RRLX_70_Cluster_Rallye1_G9"]);

            // Fix weld issue for lower rad mount
            Autoloader.LoadedPrefabs["RRLX_70_LowerRadiatorSupport1_G9"].Prefab.transform.GetChild(2).GetComponent<WeldCut>().WeldsToParent = true;
            Autoloader.LoadedPrefabs["RRLX_70_LowerRadiatorSupport1_G9"].Prefab.transform.GetChild(3).GetComponent<WeldCut>().WeldsToParent = true;

            // Add HideMesh Script
            Autoloader.LoadedPrefabs["RRLX_69C_HoodA12_G9"].Prefab.transform.GetChild(0).gameObject.AddComponent<HideMesh>();
            Autoloader.LoadedPrefabs["RRLX_70C_Hood_CutHP_G9"].Prefab.transform.GetChild(0).gameObject.AddComponent<HideMesh>();
            Autoloader.LoadedPrefabs["RRLX_70C_Hood_FlatHP_G9"].Prefab.transform.GetChild(0).gameObject.AddComponent<HideMesh>();
            Autoloader.LoadedPrefabs["RRLX_70C_Hood_PitchHP_G9"].Prefab.transform.GetChild(0).gameObject.AddComponent<HideMesh>();

            CatalogHandler.Setup();

            FirstLoadSW.Stop();

            PrettyLog.Log(nameof(OnFirstLoad), "Took : " + FirstLoadSW.ElapsedMilliseconds.ToString() + " ms to load.");


        }


        private void BuildOptions() 
        {

            ThisMod.AddSpacerToUI();

            DebugMode = ThisMod.AddCheckboxToUI("RR_CORONET_DEBUG_VALUE", "Enable Debug Mode", Debug);
            ThisMod.AddLabelToUI("[Description] Allows coronet mod to run more debug messages in the Player.Log file");

            ThisMod.AddSpacerToUI();

            SaveMode = ThisMod.AddCheckboxToUI("RR_CORONET_SAVE_DIS_VALUE", "Enable Save Disasembler [Dont touch unless you know what you're doing!]", SaveDis);
            ThisMod.AddLabelToUI("[Description] A debug option for developers, DO NOT use this randomly, Or your game will load extremely slow.");

            ThisMod.AddSpacerToUI();

            InteriorShopEnabled = ThisMod.AddCheckboxToUI("RR_CORONET_ENABLE_INTERIOR_STORE", "Enable Custom Interior Store [increases loading times]", EnableInteriorStore);
            ThisMod.AddLabelToUI("[Description] Allows you to control if you want the custom interior store that is featured in coronet mod, Turning this option off will mean you wont have anywhere to find interior parts..");

            ThisMod.AddSpacerToUI();

            EnablePatchingCheckBox = ThisMod.AddCheckboxToUI("RR_CORONET_ENABLE_PATCHING", "Enable Harmony Patching", EnablePatching);
            ThisMod.AddLabelToUI("[Description] Turn off if you are having issues that you think maybe related to coronet mod.");

            ThisMod.AddLabelToUI("Version : " + Version + " BETA");
            ThisMod.AddLabelToUI("Build Type : " + build);

            ThisMod.AddSpacerToUI();

            ThisMod.AddLabelToUI("Author : " + Author + " (Discord : reliant_robin_lx)");

        }

        public static void DoPatching() 
        {
            var harmony = new Harmony("com.Robin.Coronet");

            harmony.PatchAll();
        }

        public override void OnLoad()
        {
            ShopBuilder.AddMoparWheelsToShop();
            ShopBuilder.BuildBuyStand(US_Plate_Coro.Prefab.gameObject);

            /*
            GameObject CataloguePrefabCats = GameObject.Find("SceneManager/PartsCatalogue/Car");
            TMP_Dropdown CarList = CataloguePrefabCats.GetComponent<TMP_Dropdown>();

            foreach (TMP_Dropdown.OptionData OD in  CarList.options) 
            {
                if (CarRenames.ContainsKey(OD.text)) 
                {
                    OD.text = CarRenames[OD.text];
                }
            }
            */
        }

        public override void OnNewMapLoad()
        {
            if (InteriorShopEnabled.Checked == true) 
            {
                ModUtils.ExecuteNextFrame(delegate
                {
                    InteriorModShop.MakeShop();
                });
            }
        }

        private static void SetURPmode() 
        {
            PrettyLog.Log(nameof(SetURPmode), "Running Shader Checks");

            Shader GlassShader = Shader.Find("Shader Graphs/CarGlass");
            Shader PaintShader = Shader.Find("Shader Graphs/CarPaintNoMetalic");

            PrettyLog.Log(nameof(SetURPmode), "Ran Shader Checks");


            if (GlassShader || PaintShader)
            {
                URP_Mode = true;
                PrettyLog.Log(nameof(SetURPmode), "URP Build Detected, Enabling URP Mode.");

            }
            else 
            {
                URP_Mode = false;
                PrettyLog.Log(nameof(SetURPmode), "Old Build Detected, Disabling URP Mode.");
            }

        }

        public static bool GetURPModeState() 
        {
            return URP_Mode;
        }

        private void FixOddMaterialGlitch() 
        {
            foreach (Part p in ThisMod.Parts) 
            {
                foreach (Material m in p.Renderer.materials) 
                {
                    if (m.shader.name == "Shader Graphs/CarGlass" || m.shader.name == "Shader Graphs/CarPaintNoMetalic")
                    {

                    }
                    else 
                    {
                        if (m.shader.name != "Universal Render Pipeline/Lit") 
                        {
                            m.shader = Shader.Find("Universal Render Pipeline/Lit");
                        }
                    }
                }
                foreach (MeshRenderer MR in p.Prefab.GetComponentsInChildren<MeshRenderer>()) 
                {
                    if (MR != p.Renderer) 
                    {
                        foreach (Material material in MR.materials) 
                        {
                            if (material.shader.name != "Universal Render Pipeline/Lit")
                            {
                                material.shader = Shader.Find("Universal Render Pipeline/Lit");
                            }
                        }
                    }
                }
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
        public void OnLoadFinish()
        {
        }
    }
}
