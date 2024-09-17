using Den.Tools;
using FluffyUnderware.DevTools.Extensions;
using SimplePartLoader;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using PaintIn3D;

namespace RR_Coronet
{
    internal class MaterialManager
    {
        static string MethodString = "Material Manager";
        static Material[] newMaterials = new Material[3];
        static Material[] ChromeMat = new Material[1];
        public static Material[] GlassMaterial = new Material[2];
        static Material[] RuinedGlassMaterial = new Material[1];
        static Material[] MetalMaterial = new Material[1];
        static Material[] RustMaterial = new Material[1];
        static Material[] RustyMaterial = new Material[1];
        static Material[] PlasticMaterial = new Material[1];
        static Material[] PlatsicMaterialWithBackfaceShader = new Material[2];
        static Material[] DashWood = new Material[1];
        static Material[] RoughPlastic = new Material[1];
        static Material[] CarLight = new Material[1];
        static Material[] SteelMaterial = new Material[1];
        static Material[] RT_WheelMaterial = new Material[3];
        static Material[] CarpetMaterial = new Material[1];
        static Material[] ClusterTex = new Material[1];
        static Material[] LabelTex = new Material[1];
        static Material[] redmaterial = new Material[1];
        static Material[] ColumnMaterial = new Material[1];
        static Material[] Lamps = new Material[1];
        static Material[] LampPlastic = new Material[1];
        static Material[] PlateMaterial = new Material[1];
        static Material[] RT_LightMaterial = new Material[5];
        static Material[] SP_LightMaterial = new Material[6];
        static Material[] Rubber = new Material[1];

        static Shader BackfaceShader = Shader.Find("Azerilo/Double Sided Standard");

        static public int MaterialsLoaded = 0;


        static Material[] BoltMaterial = new Material[1];
        public static void ApplyMaterial(Part part) // Debaiting dumping for just setting the material shaders to standard.
        {

            if (part.Prefab.gameObject.TryGetComponent<Grid>(out Grid GridrComp))
            {
                PrettyLog.DebugLog(MethodString, "Scanning : " + part.CarProps.PrefabName);

                switch (GridrComp.cellSize.x) 
                {
                    case 1:
                        MeshFilter PartMesh = part.Prefab.GetComponent<MeshFilter>();
                        
                        part.Renderer.materials = new Material[3];
                        
                        PaintingSystem.SetupPart(part, PaintingSystem.Types.FullPaintingSupport);
                        
                        Mesh OutputMesh =  new Mesh();

                        P3dSeamFixer.Generate(PartMesh.mesh, OutputMesh, P3dCoord.First, 0, 0.005f);

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

                        PrettyLog.DebugLog(nameof(MaterialManager.ApplyMaterial), "Seams fixed on part : " + part.CarProps.PrefabName);

                        GridrComp.cellGap = new Vector3(0, 1, 0);

                        break;

                    case 2:

                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + " Detected Metal ID, Applying Metal Material...");
                        part.Renderer.materials = MetalMaterial;
                        part.CarProps.OldMaterial = RustMaterial[0];    
                        part.CarProps.JunkSpawnChance = 4;
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 3:

                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + " Detected Chrome ID, Applying Chrome Material..");
                        part.CarProps.Washable = false;
                        part.Renderer.materials = ChromeMat;
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 4:
                        MeshFilter PartMesh2 = part.Prefab.GetComponent<MeshFilter>();
                        Mesh OutputMesh2 = new Mesh();
                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Glass ID, Applying Glass Material...");
                        part.Renderer.materials = GlassMaterial;
                        part.CarProps.RuinedMaterial = RuinedGlassMaterial[0];
                        part.CarProps.OldMaterial = RuinedGlassMaterial[0];
                        part.CarProps.Washable = true;
                        part.Prefab.AddComponent<OriginalMesh>().Mesh = PartMesh2.mesh;
                        P3dSeamFixer.Generate(PartMesh2.mesh, OutputMesh2, P3dCoord.First, 0, 0.005f);
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 5:

                        if (part.HasProperty("ForceBackfaceShader"))
                        {
                            PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Forced Backface Plastic ID, Applying Forced Backface Plastic Material...");
                            part.Renderer.materials = PlasticMaterial;
                            part.Renderer.material.shader = BackfaceShader;
                            GridrComp.cellGap = new Vector3(0, 1, 0);
                        }
                        else
                        {
                            PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Plastic ID, Applying Plastic Material...");
                            part.Renderer.materials = PlasticMaterial;
                            GridrComp.cellGap = new Vector3(0, 1, 0);
                        }
                        break;

                    case 6:

                        if (part.HasProperty("UseOldMaterial"))
                        {
                            part.CarProps.OldMaterial = RustyMaterial[0];
                        }
                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Steel ID, Applying Steel Material...");

                        part.Renderer.materials = SteelMaterial;
                        //part.CarProps.Washable = false;
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 7:
                        if (part.HasProperty("UseOldMaterial"))
                        {
                            part.CarProps.OldMaterial = RuinedGlassMaterial[0];
                            part.CarProps.RuinedMaterial = RuinedGlassMaterial[0];
                        }
                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Glass ID, Applying Glass Material...");
                        part.Prefab.GetComponent<MeshRenderer>().materials = GlassMaterial;
                        part.CarProps.Washable = true;
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 8:

                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 9:

                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Skip ID, Will not apply to parent.");
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 10:

                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Light ID, Applying Light Material...");
                        part.Renderer.materials = CarLight;
                        part.Renderer.materials[0].shader = Shader.Find("Standard");
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 11:

                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Rough Plastic ID, Applying Rough Plastic Material..");
                        part.Renderer.materials = RoughPlastic;
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 12:

                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Carpet ID, Applying Carpet Material..");
                        part.Renderer.materials = CarpetMaterial;
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;

                    case 99:

                        PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Custom Material ID, Applying Custom Material..");
                        ApplyCustomMaterial(part.Prefab.transform.gameObject, GridrComp);
                        GridrComp.cellGap = new Vector3(0, 1, 0);
                        break;
                }

                MaterialsLoaded += 1;

                // Make a second loop to loop through children trim
                if (part.Prefab.gameObject.transform.childCount > 0)
                {
                    foreach (Grid ChildGrid in part.Prefab.gameObject.GetComponentsInChildren<Grid>())
                    {

                        if (ChildGrid.cellGap.y == 0)
                        {
                            GameObject ChildGameObject = ChildGrid.gameObject;
                            Renderer CGORenderer = ChildGameObject.GetComponent<Renderer>();
                            CarProperties CGOCarProp = ChildGameObject.GetComponent<CarProperties>();

                            switch (ChildGrid.cellSize.x)
                            {
                                case 2:

                                    PrettyLog.DebugLog(MethodString, ChildGameObject.name + " Detected Child Object With Metal ID, Applying Metal Material...");
                                    CGORenderer.materials = MetalMaterial;
                                    if (CGOCarProp.CantRust == false)
                                    {
                                        CGOCarProp.OldMaterial = RustMaterial[0];
                                        PrettyLog.DebugLog(MethodString, CGOCarProp.OldMaterial.name);
                                    }
                                    ChildGrid.cellGap = new Vector3(0, 1, 0);

                                    break;

                                case 3:

                                    PrettyLog.DebugLog(MethodString, ChildGameObject.name + " Detected Child Object With Chrome ID, Applying Chrome Material..");
                                    CGORenderer.materials = ChromeMat;
                                    ChildGrid.cellGap = new Vector3(0, 1, 0);

                                    break;

                                case 5:

                                    PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Child Object With Plastic ID, Applying Plastic Material...");
                                    CGORenderer.materials = PlasticMaterial;
                                    ChildGrid.cellGap = new Vector3(0, 1, 0);

                                    break;

                                case 6:

                                    PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Child Object With Steel ID, Applying Steel Material...");
                                    CGORenderer.materials = SteelMaterial;
                                    ChildGrid.cellGap = new Vector3(0, 1, 0);

                                    break;

                                case 11:

                                    PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Child Object With Rough Plastic ID, Applying Rough Plastic Material...");
                                    CGORenderer.materials = RoughPlastic;
                                    ChildGrid.cellGap = new Vector3(0, 1, 0);

                                    break;

                                case 99:

                                    PrettyLog.DebugLog(MethodString, part.CarProps.PartName + "  Detected Custom Material ID, Applying Custom Material..");
                                    ApplyCustomMaterial(ChildGameObject, GridrComp); 
                                    GridrComp.cellGap = new Vector3(0, 1, 0);

                                    break;
                            }

                            MaterialsLoaded += 1;

                        }
                    }
                }

                if (part.HasProperty("UniversalBackface"))
                {
                    for (int i = 0; i < part.Renderer.materials.Length; i++)
                    {
                        part.Renderer.materials[i].shader = BackfaceShader;
                    }
                }
            }

        }
        public static void SetupMaterialsOnLoad() // Okay literially the only clean method in the entire fucking mod.
        {
            // Loading materials from cached objects.

            newMaterials[0] = PaintingSystem.GetPaintRustMaterial(); // Paint and rust

            newMaterials[1] = PaintingSystem.GetDirtMaterial(); // Dirt

            newMaterials[2] = PaintingSystem.GetBodymatMaterial(); // Base

            RustMaterial[0] = cachedResources.Load<GameObject>("Firewall07").GetComponent<CarProperties>().OldMaterial;

            ChromeMat[0] = PaintingSystem.GetChromeMaterial();

            PlasticMaterial[0] = cachedResources.Load<GameObject>("WiperMotor07").GetComponent<MeshRenderer>().materials[0];

            MetalMaterial[0] = cachedResources.Load<GameObject>("Firewall07").GetComponent<MeshRenderer>().materials[0];

            RT_WheelMaterial = cachedResources.Load<GameObject>("SteeringWheel07").GetComponent<MeshRenderer>().materials;

            GlassMaterial[0] = cachedResources.Load<GameObject>("Window1").GetComponent<MeshRenderer>().materials[2];

            RuinedGlassMaterial[0] = cachedResources.Load<GameObject>("Windshield07").GetComponent<CarProperties>().RuinedMaterial;

            GlassMaterial[1] = PaintingSystem.GetDirtMaterial();

            SteelMaterial[0] = cachedResources.Load<GameObject>("SteeringColumn07").GetComponent<MeshRenderer>().materials[0];

            RustyMaterial[0] = cachedResources.Load<GameObject>("SteeringColumn07").GetComponent<CarProperties>().OldMaterial;

            CarLight = cachedResources.Load<GameObject>("Headlight06").GetComponent<MeshRenderer>().materials;

            Lamps = cachedResources.Load<GameObject>("TaillightL07").GetComponent<MeshRenderer>().materials;

            LampPlastic[0] = cachedResources.Load<GameObject>("TaillightL07").GetComponent<MeshRenderer>().materials[0];

            RoughPlastic[0] = cachedResources.Load<GameObject>("Dashboard07").GetComponent<MeshRenderer>().materials[0];

            CarpetMaterial[0] = cachedResources.Load<GameObject>("Carpet12").GetComponent<MeshRenderer>().materials[0];

            //Creating Plastic Material With backface shader

            PlatsicMaterialWithBackfaceShader[0] = PlasticMaterial[0];

            PlatsicMaterialWithBackfaceShader[1] = PaintingSystem.GetBodymatMaterial();

            RT_LightMaterial[0] = ChromeMat[0];
            RT_LightMaterial[1] = ColumnMaterial[0];
            RT_LightMaterial[2] = Lamps[0];
            RT_LightMaterial[3] = Lamps[1];
            RT_LightMaterial[4] = Lamps[1];

            SP_LightMaterial[0] = ChromeMat[0];
            SP_LightMaterial[1] = ColumnMaterial[0];
            SP_LightMaterial[2] = Lamps[1];
            SP_LightMaterial[3] = Lamps[0];
            SP_LightMaterial[4] = Lamps[1];
            SP_LightMaterial[5] = Lamps[1];

        }

        public static void SetupExternalMaterials(AssetBundle Bundle, String Item) // I have no words for how much i hate doing this method, its unnessery.
        {
            Material[] Temp = new Material[1];

            Temp[0] = (Material)Bundle.LoadAsset(Item);

            Temp[0].shader = Shader.Find("Standard");

            if (Temp[0] != null) 
            {

                switch (Item)
                {
                    case "CoronetWood":
                        DashWood[0] = Temp[0];
                        DashWood[0].shader = BackfaceShader;     
                        break;
                    case "ClusterTexture":
                        ClusterTex[0] = Temp[0];
                        break;
                    case "ClusterLabels":
                        LabelTex[0] = Temp[0];
                        break;
                    case "RedMaterial":
                        redmaterial = Temp;
                        break;
                    case "ColumnPaint":
                        ColumnMaterial = Temp; break;
                    case "PlateMaterial":
                        PlateMaterial = Temp;
                        break;
                    case "Rubber":
                        Rubber[0] = Temp[0];
                        Rubber[0].shader = BackfaceShader;
                        break;
                }
            }
        }
        public static void ApplyCustomMaterial(GameObject p, Grid Item) // SO BADLY Handled, Should replace at some point.
        {
            PrettyLog.DebugLog("Custom Materials", "Scanning : " + p.name + " !");

            Renderer r = p.GetComponent<Renderer>();

            switch (Item.cellGap.x) 
            {
                case 1:
                    r.materials = DashWood;
                    break;
                case 2:
                    r.materials = RT_WheelMaterial;
                    break;
                case 3:
                    r.materials[0] = DashWood[0];
                    r.materials[0].shader = BackfaceShader;
                    r.materials[1] = ClusterTex[0];
                    r.materials[1].shader = BackfaceShader;

                    break;
                case 4:
                    r.materials[0] = LabelTex[0];
                    ;
                    break;
                case 5:
                    r.materials = SP_LightMaterial;
                    break;
                case 6:
                    r.materials[0] = CarLight[0];
                    r.materials[1] = CarLight[0];
                    break;
                case 7:
                    r.materials[0] = redmaterial[0];
                    break;
                case 8:
                    r.materials[0] = ColumnMaterial[0];
                    break;
                case 9:
                    r.materials = CarLight;
                    break;
                case 10:
                    r.materials = Lamps;
                    break;
                case 11:
                    r.materials[0] = PlateMaterial[0];
                    break;
                case 12:
                    r.materials = GlassMaterial;
                    break;
                case 13:
                    r.materials[1] = ColumnMaterial[0];
                    r.materials[0] = ChromeMat[0];
                    break;
                case 14:
                    r.materials = RT_LightMaterial;
                    break;
                case 15:
                    r.materials[0] = ChromeMat[0];
                    r.materials[1] = redmaterial[0];
                    r.materials[2] = SteelMaterial[0];
                    r.materials[3] = ColumnMaterial[0];
                    break;
                case 16:
                    r.materials[0] = ChromeMat[0];
                    r.materials[1] = Rubber[0];
                    r.materials[1].shader = Shader.Find("Standard");
                    break;
                case 17:
                    r.materials[0] = ChromeMat[0];
                    r.materials[1] = DashWood[0];
                    r.materials[2] = ChromeMat[0];
                    break;
            }

            PrettyLog.DebugLog("Custom Materials", "Applied : " + p.name + " !");
        }


        public static void FixTheFUCKINGBoltMats(Part part) // Should be made more efficent, Replace the material instead.
        {
            foreach (GameObject bolt in part.Prefab.transform) 
            {
                if (bolt.name.Contains("Bolt")) 
                {
                    bolt.GetComponent<MeshRenderer>().material.shader = Shader.Find("Standard");

                }
            }
        }

        private static void SetupPainting(Part part, PaintingSystem.Types Support)  // Slow. Replace with SetupPart later, Thank you for your service.
        {
            part.Renderer.materials = newMaterials;
            CarProperties PartProps = part.CarProps;
            PartProps.Washable = true;
            PartProps.Paintable = true;
            PartProps.Fairable = true;
            PartProps.MeshRepairable = true;

            PaintingSystem.SetMaterialsForObject(part, -1, 0, 1);
            part.EnablePartPainting(Support);
            PrettyLog.DebugLog(MethodString, part.CarProps.name + " Has Been Setup For Painting and Repairing!");
        }
    }
}
