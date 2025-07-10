using Den.Tools;
using JetBrains.Annotations;
using SimplePartLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Pseudo;

namespace RR_Coronet
{
    public class InteriorModShop
    {
        public static GameObject ModShop;

        private static Material ModShopMaterial;

        public static IList<GameObject> InteriorPartsList = new List<GameObject>();

        public static IList<GameObject> InternalPartList = new List<GameObject>(); 

        private static Transform ExistingInteriorShopSelection;

        private static int IndexInternal = 0;

        private static Material DefaultMat;

        private static bool HasRan = false;

        private static TimedActions TAS;

        private static OutThrower OTS;

        private static GameObject ShopSpawnSpot;

        private static Transform ColorPalletes;

        // Allows the user to add a part to the mod shop.

        public static void MakeShop() 
        {
            Stopwatch InteriorShopStopWatch = Stopwatch.StartNew();
            PrettyLog.DebugLog(nameof(MakeShop), "1");

            GameObject OldObject = GameObject.Find("Unloadables/Porrt/Hangar_v2/Object084");

            ExistingInteriorShopSelection = GameObject.Find("Unloadables/Porrt/Hangar_v2_6/showpp/Shop/SHOPITEMS/").transform;


            if (HasRan == false) 
            {
                TAS = ModShop.AddComponent<TimedActions>();

                OTS = ModShop.transform.Find("outThrower").gameObject.AddComponent<OutThrower>();

                DefaultMat = GameObject.Find("MaterialParent").GetComponent<CarMaterials>().Interior1;

                ShopSpawnSpot = ModShop.transform.Find("SalePos").gameObject;

                OTS.KickOutPosition = ModShop.transform.Find("outplace").transform;


                TAS.CloseObject = ModShop.transform.GetChild(0).gameObject;
                TAS.CloseTime = 18;
                TAS.OpenObject = ModShop.transform.GetChild(1).gameObject;
                TAS.OpenTime = 8;
                TAS.OutThroower = OTS.gameObject;

                ModShopMaterial = OldObject.GetComponent<MeshRenderer>().material;

                ModShop.GetComponent<MeshRenderer>().material = ModShopMaterial;

                ModShop.transform.GetChild(0).GetComponent<MeshRenderer>().material = ModShopMaterial;
                ModShop.transform.GetChild(1).GetComponent<MeshRenderer>().material = ModShopMaterial;

                ColorPalletes = ModShop.transform.Find("ShopStuff/SHOPITEMS").transform;

                for (int i = 0; i < ColorPalletes.childCount; i++)
                {
                    GameObject Part = ColorPalletes.GetChild(i).gameObject;

                    if (Part.name.Contains("09") || Part.name.Contains("70"))
                    {

                        SaleItem PS = Part.AddComponent<SaleItem>();

                        PrettyLog.DebugLog(nameof(MakeShop), Part.name);

                        if (Part.transform.childCount > 0)
                        {
                            if (Part.transform.GetChild(0).name == "PARTS_MANAGER_PART")
                            {
                                GameObject PartPref = PartManager.gameParts.Find(x => x.name == Part.name);
                                PS.Item = PartPref;
                                PS.Price = PartPref.GetComponent<Partinfo>().price;
                                PS.interior = 1;

                                // Once we have found the part, We can change the name to its display version
                                PS.gameObject.name = PS.Item.GetComponent<CarProperties>().PartName;

                                Part.GetComponent<MeshRenderer>().material = DefaultMat;
                            }
                            else 
                            {
                                try
                                {
                                    GameObject PartPref = Autoloader.LoadedPrefabs["RRLX_" + Part.name].Prefab;
                                    PS.Item = PartPref;
                                    PS.Price = PartPref.GetComponent<Partinfo>().price;
                                    PS.interior = 1;

                                    // Once we have found the part, We can change the name to its display version
                                    PS.gameObject.name = PS.Item.GetComponent<CarProperties>().PartName;

                                    Part.GetComponent<MeshRenderer>().material = DefaultMat;
                                }
                                catch // Lets just make sure if a referance returns null, That we can catch it.
                                {
                                    PrettyLog.Error(nameof(MakeShop), "Couldn't Find : " + Part.name + " In list.");
                                }
                            }
                        }
                        else 
                        {
                            try
                            {
                                GameObject PartPref = Autoloader.LoadedPrefabs["RRLX_" + Part.name].Prefab;
                                PS.Item = PartPref;
                                PS.Price = PartPref.GetComponent<Partinfo>().price;
                                PS.interior = 1;

                                // Once we have found the part, We can change the name to its display version
                                PS.gameObject.name = PS.Item.GetComponent<CarProperties>().PartName;

                                Part.GetComponent<MeshRenderer>().material = DefaultMat;
                            }
                            catch // Lets just make sure if a referance returns null, That we can catch it.
                            {
                                PrettyLog.Error(nameof(MakeShop), "Couldn't Find : " + Part.name + " In list.");
                            }
                        }

                        PrettyLog.DebugLog(nameof(MakeShop), "4");


                        PS.SpawnSpot = ShopSpawnSpot;
                        PS.TimedActions = TAS;

                        Part.layer = LayerMask.NameToLayer("Items");
                        Part.tag = "Item";

                        PrettyLog.DebugLog(nameof(MakeShop), "5");

                        InternalPartList.Insert(IndexInternal, Part);

                        IndexInternal += 1;

                    }
                    HasRan = true;
                }

                PrettyLog.DebugLog(nameof(MakeShop), "3");

                PrettyLog.DebugLog(nameof(MakeShop), "6");

                GameObject P1 = ColorPalletes.GetChild(0).gameObject;
                GameObject P2 = ColorPalletes.GetChild(1).gameObject;
                GameObject P3 = ColorPalletes.GetChild(2).gameObject;
                GameObject P4 = ColorPalletes.GetChild(3).gameObject;
                GameObject P5 = ColorPalletes.GetChild(4).gameObject;
                GameObject P6 = ColorPalletes.GetChild(5).gameObject;
                GameObject P7 = ColorPalletes.GetChild(6).gameObject;
                GameObject P8 = ColorPalletes.GetChild(7).gameObject;

                SetINteriorColors P1S = P1.AddComponent<SetINteriorColors>();
                SetINteriorColors P2S = P2.AddComponent<SetINteriorColors>();
                SetINteriorColors P3S = P3.AddComponent<SetINteriorColors>();
                SetINteriorColors P4S = P4.AddComponent<SetINteriorColors>();
                SetINteriorColors P5S = P5.AddComponent<SetINteriorColors>();
                SetINteriorColors P6S = P6.AddComponent<SetINteriorColors>();
                SetINteriorColors P7S = P7.AddComponent<SetINteriorColors>();
                SetINteriorColors P8S = P8.AddComponent<SetINteriorColors>();

                PrettyLog.DebugLog(nameof(MakeShop), "7");


                SetupIN(P1S, 0);
                SetupIN(P2S, 1);
                SetupIN(P3S, 2);
                SetupIN(P4S, 3);
                SetupIN(P5S, 4);
                SetupIN(P6S, 5);
                SetupIN(P7S, 6);
                SetupIN(P8S, 7);

            }


            PrettyLog.DebugLog(nameof(MakeShop), "8");


            // Replace old static mesh

            GameObject NewShop = GameObject.Instantiate(ModShop);

            NewShop.transform.parent = OldObject.transform.parent;
            NewShop.transform.position = OldObject.transform.position;
            NewShop.transform.rotation = OldObject.transform.rotation;
            NewShop.transform.localScale = new Vector3(1,1,1);

            GameObject.Destroy(OldObject.transform.parent.GetChild(0).gameObject);
            GameObject.Destroy(OldObject);

            InteriorShopStopWatch.Stop();
                
            PrettyLog.Log(nameof(MakeShop), "Took : " + InteriorShopStopWatch.ElapsedMilliseconds.ToString() + " ms to load.");


        }


        private static void SetupIN(SetINteriorColors SIC, int OI) 
        {
            SIC.Saleitems = InternalPartList.ToArray<GameObject>();
            SIC.interior = ExistingInteriorShopSelection.GetChild(OI).GetComponent<SetINteriorColors>().interior;
            SIC.gameObject.layer = LayerMask.NameToLayer("Items");
            SIC.gameObject.tag = "Item";
            SIC.gameObject.GetComponent<MeshRenderer>().materials = ExistingInteriorShopSelection.GetChild(OI).GetComponent<MeshRenderer>().materials;
        }


    }
}
