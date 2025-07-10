using PaintIn3D;
using SimplePartLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static SimplePartLoader.PaintingSystem;
using static RR_Coronet.Dependencys.GamePaintingCompat;
using Den.Tools;

namespace RR_Coronet.Dependencys
{
    public class MaterialHandler
    {
        private static Dictionary<string, Material[]> Materials = new Dictionary<string, Material[]>();

        private static Dictionary<string, MeshRenderer> CachedRenderers = new Dictionary<string, MeshRenderer>();

        private static Dictionary<string, CarProperties> CachedCarprops = new Dictionary<string, CarProperties>();

        private static Dictionary<string, Shader> CachedShaders = new Dictionary<string, Shader>();

        private static Vector3 MarkComplete = new Vector3(0, 1, 0);

        //////////////////////////////////////////////////////////////////////////////////////
        /// | Loading functions |
        /// <summary>
        /// A Series of functions for multiple private dictionarys in this class.
        /// </summary>
        /// <Loading></Loading>
        //////////////////////////////////////////////////////////////////////////////////////

        public static void InitF()
        {
            Stopwatch InitFStopWatch = Stopwatch.StartNew();
            PrettyLog.Log(nameof(MaterialHandler), "1");
            // Cache common shaders

            if (!ModMain.GetURPModeState())
            {
                CachedShaders.Add("Stan", Shader.Find("Standard"));
                CachedShaders.Add("Backface", Shader.Find("Azerilo/Double Sided Standard"));
                PrettyLog.Log(nameof(MaterialHandler), "2");
/*
                Materials.Add("Chrome", new Material[2] { PaintingSystem.GetChromeMaterial(), PaintingSystem.GetDirtMaterial() });

                Materials.Add("ChromeC", new Material[1] { PaintingSystem.GetChromeMaterial()});

                PrettyLog.Log(nameof(MaterialHandler), "3");
  */
            }
            else 
            {
                /*
                CachedShaders.Add("Stan", Shader.Find("Universal Render Pipeline/Lit"));

                Materials.Add("Chrome", new Material[1] {GamePainting.GetChromeMaterial()});

                Materials.Add("ChromeC", new Material[1] { GamePainting.GetChromeMaterial() });
                */
            }

            Materials.Add("Chrome", new Material[2] { PaintingSystem.GetChromeMaterial(), PaintingSystem.GetDirtMaterial() });

            Materials.Add("ChromeC", new Material[1] { PaintingSystem.GetChromeMaterial() });

            PrettyLog.Log(nameof(MaterialHandler), "3");

            // Add materials.
            PrettyLog.Log(nameof(MaterialHandler), "4");

            Materials.Add("Steel", new Material[1] { ImportExistingMaterial("SteeringColumn07")});

            Materials.Add("SteelURP", new Material[1] {Materials["Steel"][0]});

            Materials.Add("SteelC", new Material[1] {ImportExistingMaterial("SteeringColumn07")});

            Materials.Add("Glass", new Material[2] {ImportExistingMaterial("Window1", 2), PaintingSystem.GetDirtMaterial()});

            Materials.Add("MetalDark", new Material[1] {ImportExistingMaterial("Firewall07")});

            Materials.Add("Plastic", new Material[1] {ImportExistingMaterial("WiperMotor07")});

            Materials.Add("RustyExhaust", new Material[1] { ImportExistingRuinedMaterial("ExhaustL07")});

            Materials.Add("ExhaustMaterial", new Material[2] {ImportExistingMaterial("ExhaustL07"), Materials["ChromeC"][0]});

            Materials.Add("RoughPlastic", new Material[1] {ImportExistingMaterial("Dashboard07")});

            Materials.Add("Carpet", new Material[1] {ImportExistingMaterial("Carpet12")});

            Materials.Add("SurfaceRust", new Material[1] {ImportExistingOldMaterial("SteeringColumn07")});

            Materials.Add("Rust", new Material[1] {ImportExistingOldMaterial("Firewall07")});

            Materials.Add("RuinedGlass", new Material[1] {ImportExistingOldMaterial("Windshield07")});

            Materials.Add("RtWheel", ImportExistingMaterials("SteeringWheel07"));

            Materials.Add("Lamps", ImportExistingMaterials("TaillightL07"));

            Materials.Add("CarLight", ImportExistingMaterials("Headlight06"));

            Materials.Add("SuperbeeLamps", new Material[6] {Materials["ChromeC"][0], Materials["ColumnPaint"][0], Materials["Lamps"][1], Materials["Lamps"][0], Materials["Lamps"][1], Materials["Lamps"][1]});

            Materials.Add("RTLamps", new Material[5] {Materials["ChromeC"][0], Materials["ColumnPaint"][0], Materials["Lamps"][0], Materials["Lamps"][1], Materials["Lamps"][1]});

            Materials.Add("ChromeColumnPaint", new Material[2] {Materials["ChromeC"][0], Materials["ColumnPaint"][0]});

            Materials.Add("ChromeRubber", new Material[2] {Materials["ChromeC"][0], Materials["Rubber"][0]});

            Materials.Add("RubberChrome", new Material[2] { Materials["Rubber"][0], Materials["ChromeC"][0] });

            Materials.Add("ChromeRubberMetal", new Material[3] { Materials["ChromeC"][0], Materials["Rubber"][0], Materials["MetalDark"][0] });

            Materials.Add("ColumnPaint_Backface", Materials["ColumnPaint"]);

            Materials.Add("SteelC_Backface", Materials["SteelC"]);

            Materials.Add("ChromeC_Backface", Materials["ChromeC"]);

            Materials.Add("Rubber_Backface", Materials["Rubber"]);

            Materials.Add("CoronetWoodClusterTexture", new Material[2] { Materials["CoronetWood"][0], Materials["ClusterTexture"][0] });

            Materials.Add("CoronetWoodColumnPaint", new Material[2] { Materials["CoronetWood"][0], Materials["ColumnPaint"][0] });

            Materials.Add("Plastic_Backface", Materials["Plastic"]);

            Materials.Add("69SuperbeeLamps", new Material[6] {Materials["Lamps"][1], Materials["SteelC"][0], Materials["ChromeC"][0], Materials["Lamps"][1], Materials["Lamps"][0], Materials["Lamps"][1]});

            Materials.Add("69SuperbeeLamps2", new Material[6] {Materials["SteelC"][0], Materials["Lamps"][1], Materials["ChromeC"][0], Materials["Lamps"][1], Materials["Lamps"][0], Materials["Lamps"][1]});

            Materials.Add("69BlinkersF", new Material[3] {Materials["ChromeC"][0], Materials["CarLight"][0], Materials["Lamps"][0]});

            Materials.Add("PlasticBody", new Material[2] {Materials["Plastic_Backface"][0], PaintingSystem.GetDirtMaterial()});

            Materials.Add("Lightbar", new Material[6] {Materials["SteelC"][0], Materials["Siren"][0], Materials["ChromeC"][0], Materials["RedLightBarMat"][0], Materials["Lamps"][0], Materials["Lamps"][1]});

            Materials.Add("Spotlight", new Material[4] {Materials["ChromeC"][0], Materials["Lamps"][0], Materials["Glass"][0], Materials["Rubber"][0]});

            Materials.Add("70SuperbeeTrim", new Material[4] { Materials["ChromeC"][0], Materials["Steel"][0], Materials["Steel"][0], Materials["ColumnPaint"][0] });
            
            Materials.Add("70Coronet500Trim", new Material[2] { Materials["ChromeC"][0], Materials["ColumnPaint"][0]});

            Materials.Add("69SuperbeeTrim", new Material[3] { Materials["ChromeC"][0], Materials["ColumnPaint"][0], Materials["ChromeC"][0] });

            Materials.Add("ChromeDColumntPaint", new Material[2] { Materials["Chrome"][0], Materials["ColumnPaint"][0]});

            Materials.Add("ChromeShadow", new Material[2] { Materials["ChromeC"][0], Materials["Shadow"][0] });

            Materials.Add("WoodtrimDoorCards", new Material[3] { Materials["ChromeC"][0], Materials["CoronetWood"][0], Materials["ChromeC"][0] });



            if (ModMain.GetURPModeState() == false)
            {
                // Pre-set Shaders

                Materials["CoronetWood"][0].shader = CachedShaders["Backface"];
                Materials["ClusterTexture"][0].shader = CachedShaders["Backface"];
                Materials["Rubber"][0].shader = CachedShaders["Stan"];
                Materials["ColumnPaint_Backface"][0].shader = CachedShaders["Backface"];
                Materials["SteelC_Backface"][0].shader = CachedShaders["Backface"];
                Materials["ChromeC_Backface"][0].shader = CachedShaders["Backface"];
                Materials["Rubber_Backface"][0].shader = CachedShaders["Backface"];
                Materials["Plastic_Backface"][0].shader = CachedShaders["Backface"];
            }
            else 
            {
            }

            InitFStopWatch.Stop();

            PrettyLog.Log(nameof(MaterialHandler), "Complete InitF in " + InitFStopWatch.ElapsedMilliseconds + "ms.");

        }


        //////////////////////////////////////////////////////////////////////////////////////
        /// | Imports |
        /// <summary>
        /// A Series of Imports for multiple private dictionarys in this class.
        /// </summary>
        /// <Imports></Imports>
        //////////////////////////////////////////////////////////////////////////////////////

        public static Material ImportExistingMaterial(string ExistingPartName, int index = 0)
        {
            MeshRenderer ExistingObject = null;
            Material ExistingMaterial = PaintingSystem.GetBodymatMaterial();

            try
            {
                if (CachedRenderers.ContainsKey(ExistingPartName))
                {
                    ExistingObject = CachedRenderers[ExistingPartName];
                    ExistingMaterial = ExistingObject.materials[index];
                }
                else 
                {
                    ExistingObject = cachedResources.Load<GameObject>(ExistingPartName).GetComponent<MeshRenderer>();
                    ExistingMaterial = ExistingObject.materials[index];
                    CachedRenderers.Add(ExistingPartName, ExistingObject);
                }
            }
            catch (Exception ex)
            {
                PrettyLog.Warn(nameof(ImportExistingMaterial), "Failed to find '" + ExistingPartName + "' in parts manager. Failed to load material. Exception : " + ex);
            }

            return ExistingMaterial;

        }

        public static Material[] ImportExistingMaterials(string ExistingPartName)
        {
            MeshRenderer ExistingObject = null;
            Material[] ExistingMaterial = new Material[1]{ PaintingSystem.GetBodymatMaterial()};

            try
            {
                if (CachedRenderers.ContainsKey(ExistingPartName))
                {
                    ExistingObject = CachedRenderers[ExistingPartName];
                    ExistingMaterial = ExistingObject.materials;
                }
                else
                {
                    ExistingObject = cachedResources.Load<GameObject>(ExistingPartName).GetComponent<MeshRenderer>();
                    ExistingMaterial = ExistingObject.materials;
                    CachedRenderers.Add(ExistingPartName, ExistingObject);
                }
            }
            catch (Exception ex)
            {
                PrettyLog.Warn(nameof(ImportExistingMaterials), "Failed to find '" + ExistingPartName + "' in parts manager. Failed to load material. Exception : " + ex);
            }

            return ExistingMaterial;

        }
        public static Material ImportExistingOldMaterial(string ExistingPartName)
        {
            CarProperties ExistingObject = null;
            Material ExistingMaterial = null;

            try
            {
                if (CachedCarprops.ContainsKey(ExistingPartName))
                {
                    ExistingObject = CachedCarprops[ExistingPartName];
                    ExistingMaterial = ExistingObject.OldMaterial;
                }
                else
                {
                    ExistingObject = cachedResources.Load<GameObject>(ExistingPartName).GetComponent<CarProperties>();
                    CachedCarprops.Add(ExistingPartName, ExistingObject);
                    ExistingMaterial = ExistingObject.OldMaterial;
                }
            }
            catch(Exception ex) 
            {
                PrettyLog.Warn(nameof(ImportExistingOldMaterial), "Failed to find '" + ExistingPartName + "' in parts manager. Failed to load material. Exception : " + ex);
            }

            return ExistingMaterial;

        }

        public static Material ImportExistingRuinedMaterial(string ExistingPartName)
        {
            CarProperties ExistingObject = null;
            Material ExistingMaterial = null;

            try
            {
                if (CachedCarprops.ContainsKey(ExistingPartName))
                {
                    ExistingObject = CachedCarprops[ExistingPartName];
                    ExistingMaterial = ExistingObject.RuinedMaterial;
                }
                else
                {
                    ExistingObject = cachedResources.Load<GameObject>(ExistingPartName).GetComponent<CarProperties>();
                    CachedCarprops.Add(ExistingPartName, ExistingObject);
                    ExistingMaterial = ExistingObject.RuinedMaterial;
                }
            }
            catch (Exception ex)
            {
                PrettyLog.Warn(nameof(ImportExistingRuinedMaterial), "Failed to find '" + ExistingPartName + "' in parts manager. Failed to load material. Exception : " + ex);
            }

            return ExistingMaterial;
        }

        public static void ImportCustomMaterial(string MaterialName, AssetBundle Bundle) 
        {
            Material CustomMaterial = null;

            try
            {
                PrettyLog.Log(nameof(ImportCustomMaterial), "Loading Custom Material : " +  MaterialName);
                CustomMaterial = Bundle.LoadAsset<Material>(MaterialName);
                Materials.Add(MaterialName, new Material[1] {CustomMaterial});
                PrettyLog.Log(nameof(ImportCustomMaterial), "Loaded Custom Material : " + MaterialName);

            }
            catch (Exception ex) 
            {
                PrettyLog.Warn(nameof(ImportCustomMaterial), "Failed to find '" + MaterialName + "' in Bundle. Failed to load material. Exception : " + ex);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////
        /// | General Methods |
        /// <summary>
        /// A Series of General Methods for multiple private dictionarys in this class.
        /// </summary>
        /// <General /Methods></General Methods>
        //////////////////////////////////////////////////////////////////////////////////////

        public static void UseLegacySorting(Part part, Grid gridComp)
        {
            switch (gridComp.cellSize.x)
            {
                case 1: // Part Paint

                    SetPaintMaterial(part);

                    break;

                case 2: // Rust Metal

                    part.Renderer.materials = Materials["MetalDark"];
                    part.CarProps.OldMaterial = Materials["Rust"][0];
                    part.CarProps.JunkSpawnChance = 4;
                    break;

                case 3: // Chrome

                    SetChromeDirtMaterial(part);

                    break;

                case 5: // Plastic

                    part.Renderer.materials = Materials["Plastic"];

                    break;


                case 6: // Steel

                    SetSteelDirtMaterial(part);

                    break;

                case 7: // Glass

                    SetGlassMaterial(part);
                    part.CarProps.Washable = false;
                    break;

                case 9: // Skip ID.
                    break;

                case 11: // Rough Plastic

                    part.Renderer.materials = Materials["RoughPlastic"];

                    break;

                case 12: // Carpet 

                    part.Renderer.materials = Materials["Carpet"];

                    break;

                case 13: // Rubber 

                    part.Renderer.materials = Materials["Rubber"];

                    break;

                case 14: // Exhaust 

                    part.Renderer.materials = Materials["ExhaustMaterial"];

                    part.CarProps.WornMaterial = Materials["RustyExhaust"][0];
                    part.CarProps.RuinedMaterial = Materials["RustyExhaust"][0];
                    part.CarProps.OldMaterial = Materials["RustyExhaust"][0];

                    break;

                case 15: // Plastic with backface and dirt 

                    part.Renderer.materials = Materials["PlasticBody"];

                    break;
                case 16:
                    part.Renderer.materials = Materials["ChromeDColumntPaint"];
                    break;

                case 17:
                    part.Renderer.materials = Materials["Shadow"];
                    break;
                case 99: // Custom Materials.

                    SetCustomMaterial(gridComp, part.Renderer);

                    break;

                case 111: // Adds Paint that doesn't count (Faster than using autoloader to check)

                    SetPaintMaterial(part);
                    part.RemovePaintCounters();

                    break;
            }

            gridComp.cellGap = MarkComplete;
        }

        public static void UseLegacyChildSorting(MeshRenderer MR, Grid gridComp)
        {
            if (gridComp.cellGap.y == 0) 
            {
                switch (gridComp.cellSize.x)
                {
                    case 1:

                        break;

                    case 2:

                        MR.materials = Materials["MetalDark"];
                        break;

                    case 3:

                        MR.materials = Materials["ChromeC"];
                        break;

                    case 5:

                        MR.materials = Materials["Plastic"];
                        break;

                    case 6:

                        MR.materials = Materials["SteelC"];
                        break;

                    case 11:

                        MR.materials = Materials["RoughPlastic"];
                        break;

                    case 99:

                        SetCustomMaterial(gridComp, MR);
                        break;
                }

                gridComp.cellGap = MarkComplete;

            }
        }


        //////////////////////////////////////////////////////////////////////////////////////
        /// | Private Methods |
        /// <summary>
        /// A Series of Private Methods for multiple private dictionarys in this class.
        /// </summary>
        /// <Private /Methods></Private Methods>
        //////////////////////////////////////////////////////////////////////////////////////

        private static void SetCustomMaterial(Grid grid, Renderer r) 
        {
            switch(grid.cellGap.x) 
            {
                case 1:
                    r.materials = Materials["CoronetWoodColumnPaint"];
                    break;
                case 2:
                    r.materials = Materials["RtWheel"];
                    break;
                case 3:
                    r.materials = Materials["CoronetWoodClusterTexture"];
                    break;
                case 4:
                    r.materials = Materials["ClusterLabels"];
                    break;
                case 5:
                    r.materials = Materials["SuperbeeLamps"];
                    break;
                case 6:
                    r.materials[0] = Materials["CarLight"][0];
                    r.materials[1] = Materials["CarLight"][0];
                    break;
                case 7:
                    r.materials[0] = Materials["RedMaterial"][0];
                    break;
                case 8:
                    r.materials[0] = Materials["ColumnPaint"][0];
                    break;
                case 9:
                    r.materials = Materials["CarLight"];
                    break;
                case 10:
                    r.materials = Materials["Lamps"];
                    break;
                case 13:
                    r.materials = Materials["ChromeRubber"];
                    break;
                case 14:
                    r.materials = Materials["RTLamps"]; ;
                    break;
                case 15:
                    r.materials = Materials["70SuperbeeTrim"];
                    break;
                case 16:
                    r.materials = new Material[2];
                    r.materials[0] = Materials["ChromeC"][0];
                    r.materials[1] = Materials["Rubber_Backface"][0];
                    break;
                case 17:
                    r.materials = Materials["WoodtrimDoorCards"];
                    break;
                case 18:
                    r.materials = Materials["ChromeRubber"];
                    break;
                case 19:
                    r.materials = Materials["69SuperbeeLamps"];
                    break;
                case 20:
                    r.materials = Materials["69SuperbeeTrim"];

                    break;
                case 21:
                    r.materials = Materials["69BlinkersF"];
                    break;
                case 22:
                    r.materials = Materials["69SuperbeeLamps2"];
                    break;
                case 23:
                    r.materials = Materials["Lightbar"];
                    break;
                case 24:
                    r.materials = Materials["Spotlight"];
                    break;
                case 25:
                    r.materials = Materials["C-Stripe-Black-L"];
                    break;
                case 26:
                    r.materials = Materials["C-Stripe-Black-R"];
                    break;
                case 27:
                    r.materials = Materials["C-Stripe-White-L"];
                    break;
                case 28:
                    r.materials = Materials["C-Stripe-White-R"];
                    break;
                case 29:
                    r.materials = Materials["Striaght-Stripe-Black-L"];
                    break;
                case 30:
                    r.materials = Materials["Striaght-Stripe-Black-R"];
                    break;
                case 31:
                    r.materials = Materials["Striaght-Stripe-White-L"];
                    break;
                case 32:
                    r.materials = Materials["Striaght-Stripe-White-R"];
                    break;
                case 33:
                    r.materials = Materials["ChromeRubberMetal"];
                    break;
                case 34:
                    r.materials = Materials["70Coronet500Trim"];
                    break;
                case 35:
                    r.materials = Materials["ChromeShadow"];
                    break;

            }
            
        }

        private static Mesh ApplySeamFixer(Mesh PartMesh, Mesh OutputMesh, P3dCoord p3dCoordType = P3dCoord.First, int threshold = 0, float border = 0.005f)
        {
            P3dSeamFixer.Generate(PartMesh, OutputMesh, p3dCoordType, threshold, border);

            return OutputMesh;
        }

        private static void MakeOriginalMesh(Part part, Mesh PartMesh)
        {
            part.Prefab.AddComponent<OriginalMesh>().Mesh = PartMesh;
        }

        public static void FixBoltMatShading(Part p)
        {
            if (ModMain.GetURPModeState()) 
            {
                return;
            }
            HexNut[] HexNuts = p.Prefab.GetComponents<HexNut>();
            BoltNut[] BoltNuts = p.Prefab.GetComponents<BoltNut>();
            FlatNut[] FlatNuts = p.Prefab.GetComponents<FlatNut>();

            if (HexNuts.Length == 0 & BoltNuts.Length == 0 & FlatNuts.Length == 0)
            {
                return;
            }

            foreach (HexNut HN in HexNuts)
            {
                HN.GetComponent<MeshRenderer>().material.shader = CachedShaders["Stan"];
            }

            foreach (BoltNut BN in BoltNuts)
            {
                BN.GetComponent<MeshRenderer>().material.shader = CachedShaders["Stan"];
            }

            foreach (FlatNut FN in FlatNuts)
            {
                FN.GetComponent<MeshRenderer>().material.shader = CachedShaders["Stan"];
            }
        }


        // Material Conversion layer for URP

        private static void SetPaintMaterial(Part part)
        {
            if (!ModMain.GetURPModeState()) 
            {
                MeshFilter PartMesh = part.Prefab.GetComponent<MeshFilter>();

                Mesh OutputMesh = new Mesh();

                part.Renderer.materials = new Material[3];

                PaintingSystem.SetupPart(part, PaintingSystem.Types.FullPaintingSupport);

                ApplySeamFixer(PartMesh.mesh, OutputMesh);

                foreach (P3dChangeCounter p3 in part.Prefab.GetComponents<P3dChangeCounter>())
                {
                    p3.MaskMesh = PartMesh.mesh;
                }

                part.Prefab.AddComponent<OriginalMesh>().Mesh = PartMesh.mesh;

                PartMesh.mesh = OutputMesh;

                part.CarProps.Washable = true;
                part.CarProps.Paintable = true;
                part.CarProps.Fairable = true;
                part.CarProps.MeshRepairable = true;
            }
            else 
            {
                MeshFilter PartMesh = part.Prefab.GetComponent<MeshFilter>();

                Mesh OutputMesh = new Mesh();

                part.Renderer.materials = new Material[1];

                GamePaintingCompat.SetupPart(part, new GamePaintingCompat.PartPaintSetup(){MetallicRustDust = true, ColorMap = true, ClearCoat = true});

                ApplySeamFixer(PartMesh.mesh, OutputMesh);

                foreach (P3dChangeCounter p3 in part.Prefab.GetComponents<P3dChangeCounter>())
                {
                    p3.MaskMesh = PartMesh.mesh;
                }

                part.Prefab.AddComponent<OriginalMesh>().Mesh = PartMesh.mesh;

                PartMesh.mesh = OutputMesh;

                part.CarProps.Washable = true;
                part.CarProps.Paintable = true;
                part.CarProps.Fairable = true;
                part.CarProps.MeshRepairable = true;
            }

        }

        private static void SetGlassMaterial(Part part) // Allows us to use controls in a switch.
        {
            if (!ModMain.GetURPModeState())
            {
                part.Renderer.materials = new Material[2];

                MeshFilter PartMesh2 = part.Prefab.GetComponent<MeshFilter>();
                Mesh OutputMesh2 = new Mesh();

                part.Renderer.materials = Materials["Glass"];
                part.CarProps.Washable = true;

                part.Prefab.AddComponent<OriginalMesh>().Mesh = PartMesh2.mesh;
                ApplySeamFixer(PartMesh2.mesh, OutputMesh2);
            }
            else
            {

                MeshFilter PartMesh2 = part.Prefab.GetComponent<MeshFilter>();
                Mesh OutputMesh2 = new Mesh();
                part.CarProps.Washable = true;

                GamePaintingCompat.SetupGlassPart(part, true); // param 2 == Use Part Counter

                ApplySeamFixer(PartMesh2.mesh, OutputMesh2);

            }
        }

        private static void SetChromeDirtMaterial(Part part)
        {
            if (!ModMain.GetURPModeState())
            {
                part.Renderer.materials = new Material[2];

                part.Renderer.materials = Materials["Chrome"];

                part.CarProps.RuinedMaterial = new Material(CachedShaders["Stan"]);
                part.CarProps.WornMaterial = new Material(CachedShaders["Stan"]);
                part.CarProps.OldMaterial = new Material(CachedShaders["Stan"]);

                part.CarProps.RuinedMaterial = Materials["RustyChrome"][0];
                part.CarProps.WornMaterial = Materials["RustyChrome"][0];

                part.CarProps.Washable = true;
                part.CarProps.Fairable = false;
            }
            else
            {
                part.Renderer.materials = new Material[1];

                GamePaintingCompat.SetupChromePart(part);

                part.CarProps.RuinedMaterial = Materials["RustyChrome"][0];
                part.CarProps.WornMaterial = Materials["RustyChrome"][0];

                part.CarProps.Washable = true;
                part.CarProps.Fairable = false;
            }
        }

        private static void SetSteelDirtMaterial(Part part)
        {
            if (!ModMain.GetURPModeState())
            {
                part.Renderer.materials = Materials["Steel"];

                part.CarProps.RuinedMaterial = new Material(CachedShaders["Stan"]);
                part.CarProps.WornMaterial = new Material(CachedShaders["Stan"]);
                part.CarProps.OldMaterial = new Material(CachedShaders["Stan"]);

                part.CarProps.RuinedMaterial = Materials["SurfaceRust"][0];
                part.CarProps.WornMaterial = Materials["SurfaceRust"][0];
                part.CarProps.OldMaterial = Materials["SurfaceRust"][0];
            }
            else
            {
                part.Renderer.materials = Materials["SteelURP"];


                part.CarProps.RuinedMaterial = Materials["SurfaceRust"][0];
                part.CarProps.WornMaterial = Materials["SurfaceRust"][0];
                part.CarProps.OldMaterial = Materials["SurfaceRust"][0];
            }
        }


    }
}
