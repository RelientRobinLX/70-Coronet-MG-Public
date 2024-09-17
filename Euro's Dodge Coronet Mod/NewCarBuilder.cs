using ArctiumStudios.SplineTools;
using Den.Tools;
using EVP;
using FluffyUnderware.Curvy.Examples;
using FluffyUnderware.DevTools.Extensions;
using Gaia;
using NWH.VehiclePhysics2.Effects;
using NWH.VehiclePhysics2.VehicleGUI;
using RR_Coronet.CustomMonoScripts;
using RVP;
using SimplePartLoader;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace RR_Coronet
{
    internal class NewCarBuilder
    {
        // Public

        public static GameObject ExhaustSmokeReplacement;

        public static CarList CarParentList;

        public static GameObject ChadBuilt;

        public static GameObject BartBuilt;


        public static string EmptyException = "1213123190831297";

        public static GameObject Gbox5ALL = new GameObject();
        public static GameObject GearBox07AAll = new GameObject();
        public static GameObject CylinderBlockI6all = new GameObject();
        public static GameObject rallyeWheels = new GameObject();


        public static void LocalLoadCar(Car CarObject)
        {
            LoadExceptionProfile(CarObject.BuiltCarPrefab.GetComponent<CarGenerator>().CarName, CarObject);

            CarObject.SetCarTemplateFunction(CarBuilderFunctions.TemplateBuild);
            CarObject.SetPostBuildFunction(CarBuilderFunctions.PostBuild);

        }


        /// <summary>
        /// Private
        /// </summary>

        private static void MoveWheelControllers(GameObject CarObject) 
        {
            GameObject WheelControllerFR = CarObject.transform.Find("FrontSusp/Crossmemmber07/WheelContParentFR").gameObject;

            if (WheelControllerFR) 
            {
                WheelControllerFR.transform.localPosition = new Vector3(0.685f, WheelControllerFR.transform.localPosition.y, WheelControllerFR.transform.localPosition.z);
                PrettyLog.DebugLog("MoveWheelControllers", "FR Wheel Positions moved to : " + WheelControllerFR.transform.localPosition.ToString());
            }

            GameObject WheelControllerFL = CarObject.transform.Find("FrontSusp/Crossmemmber07/WheelContParentFL").gameObject;

            if (WheelControllerFL)
            {
                WheelControllerFL.transform.localPosition = new Vector3(-0.685f, WheelControllerFL.transform.localPosition.y, WheelControllerFL.transform.localPosition.z);
                PrettyLog.DebugLog("MoveWheelControllers", "FL Wheel Positions moved to : " + WheelControllerFL.transform.localPosition.ToString());
            }
            
            GameObject WheelControllerRR = CarObject.transform.Find("RearSusp/WheelControllerRR").gameObject;

            if (WheelControllerRR)
            {
                WheelControllerRR.transform.localPosition = new Vector3(WheelControllerRR.transform.localPosition.x, WheelControllerRR.transform.localPosition.y, 0.7345f);
                PrettyLog.DebugLog("MoveWheelControllers", "RR Wheel Positions moved to : " + WheelControllerRR.transform.localPosition.ToString());
            }

            GameObject WheelControllerRL = CarObject.transform.Find("RearSusp/WheelControllerRL").gameObject;

            if (WheelControllerRL)
            {
                WheelControllerRL.transform.localPosition = new Vector3(WheelControllerRL.transform.localPosition.x, WheelControllerRL.transform.localPosition.y, -0.7345f);
                PrettyLog.DebugLog("MoveWheelControllers", "RL Wheel Positions moved to : " + WheelControllerRL.transform.localPosition.ToString());
            }
        }


        private static void LoadExceptionProfile(string CarName, Car c) //Sedan crashes, nice.
        {
            if (CarName == "70 Superbee")
            {
                c.AddException("CylinderBlock", "CylinderBlockV8");
                c.AddException("GearBox06", "GearBox07");
                c.AddException("Rim", "RRLX_70_Steel1_15", true);
                c.AddException("tire15", "tire15HF");
                c.AddException("Wiper06", "WiperChromed06");
                c.AddException("WiperBlade06", "WiperBlade07");
                c.AddException("SeatFR06", "SeatFR09");
                c.AddException("SeatFL06", "SeatFL09");
                c.AddException("RRLX_70_Hood", "RRLX_70C_Hood_Pitch_G9");
                c.AddException("RRLX_70C_RearSuperbeePanel", "RRLX_70C_Superbee_Rear_Panel1_G10");
                c.AddException("LicensePlate", "US_Plate1_70");
                c.AddException("SteeringWheel06", "RRLX_70RT_SteeringWheel_G9");
                c.AddException("RRLX_70C_WindowMR", "RRLX_70C_WindowMR1_G9", true); // Needed to prevent part lookup error, for some reason..
                c.AddException("RRLX_70RT_VentRL", EmptyException);
                c.AddException("RRLX_70RT_VentRR", EmptyException);
                c.AddException("RRLX_70C_FenderFillerRR", "RRLX_70C_FenderFillerRR_G9");
                c.AddException("RRLX_70C_FenderFillerRL", "RRLX_70C_FenderFillerRL_G9");
                c.AddException("RRLX_70_ValanceR", "RRLX_70_ValanceR_G9");
                c.AddException("RRLX_70_SteeringColumn", "RRLX_70_Steering_Column2_G9");




                ///RRLX_70_Steering_Column2_G9
                //c.AutomaticFitToCar = "Superbee";

            }
            if (CarName == "70 Coronet RT")
            {
                c.AddException("CylinderBlock", "CylinderBlockV8");
                c.AddException("GearBox06", "GearBox07");
                c.AddException("Rim", "RRLX_70_Steel1_15", true); //Rim15St3
                c.AddException("tire15", "tire15HF");
                c.AddException("IntakeManifold07", "IntakeManifold607");
                c.AddException("Wiper06", "WiperChromed06");
                c.AddException("WiperBlade06", "WiperBlade07");
                c.AddException("SeatFR06", "SeatFR09");
                c.AddException("SeatFL06", "SeatFL09");
                c.AddException("RRLX_70C_FenderFillerRR", "RRLX_70C_FenderFillerRR_RT_G9");
                c.AddException("RRLX_70C_FenderFillerRL", "RRLX_70C_FenderFillerRL_RT_G9");
                c.AddException("RRLX_70_ValanceR", "RRLX_70_ValanceR_RT_G9");
                c.AddException("RRLX_70C_RearPanel", "RRLX_70RT_Rear_Panel_G10");
                c.AddException("RRLX_70C_Trunk_Lip", "RRLX_70C_TrunkLipRT_G9");
                c.AddException("LicensePlate", "US_Plate1_70");
                c.AddException("RRLX_70_Hood", "RRLX_70C_Hood_Stock_G9");
                c.AddException("SteeringWheel06", "RRLX_70RT_SteeringWheel_G9");
                c.AddException("RRLX_70_BumperFR", "RRLX_70_BumperFR2_G9");
                c.AddException("RRLX_70_BumperFL", "RRLX_70_BumperFL2_G9");
                c.AddException("RRLX_70C_WindowMR", "RRLX_70C_WindowMR1_G9", true); // Needed to prevent part lookup error, for some reason..
                c.AddException("RRLX_70C_DoorCardFR", "RRLX_70C_DoorCardFR_RT_G10");
                c.AddException("RRLX_70C_DoorCardFL", "RRLX_70C_DoorCardFL_RT_G10");
                c.AddException("RRLX_70C_DoorCardRR", "RRLX_70C_DoorCardRR_RT_G10");
                c.AddException("RRLX_70C_DoorCardRL", "RRLX_70C_DoorCardRL_RT_G10");
                c.AddException("RRLX_70_SteeringColumn", "RRLX_70_Steering_Column2_G9");

                //c.AutomaticFitToCar = "Coronet RT";


            }

            if (CarName == "70 Coronet Sedan")
            {
                c.AddException("CylinderBlock", "CylinderBlockV8");
                c.AddException("GearBox06", "GearBox07");
                c.AddException("Rim", "RRLX_70_Steel1_15");
                c.AddException("tire15", "tire15HF");
                c.AddException("Wiper06", "WiperChromed06");
                c.AddException("WiperBlade06", "WiperBlade07");
                c.AddException("SeatFR06", "SeatFR09");
                c.AddException("SeatFL06", "SeatFL09");
                c.AddException("LicensePlate", "US_Plate1_70");
                c.AddException("70_Hood", "70C_Hood_Stock");
                //c.AutomaticFitToCar = "Coronet Sedan";
            }

            if (CarName == "70 Coronet Wagon")
            {
                c.AddException("CylinderBlock", "CylinderBlockI6");
                c.AddException("GearBox06", "GearBox07");
                c.AddException("Rim", "Rim15St3");
                c.AddException("tire15", "tire15HF");
                c.AddException("IntakeManifold07", "IntakeManifold607");
                c.AddException("Wiper06", "WiperChromed06");
                c.AddException("WiperBlade06", "WiperBlade07");
                c.AddException("SeatFR06", "SeatFR09");
                c.AddException("SeatFL06", "SeatFL09");
                c.AddException("70_ValanceR", "70_ValanceR_RT");
                c.AddException("LicensePlate", "US_Plate1_70");
                c.AddException("70_Hood", "70C_Hood_Stock");
                //c.AutomaticFitToCar = "Coronet Wagon";
            }

            //Universal Exceptions (all cars)
            c.AddException("RRLX_70_FenderMatL", EmptyException);
            c.AddException("RRLX_70_FenderMatR", EmptyException);
            c.AddException("TrailerHook06", EmptyException);


        }

        private static void FixParticleSystems(GameObject CarObject) // Good Enough for now.
        {
            
            GameObject Chad = (GameObject)cachedResources.Load("Chad");

            ParticleSystem ChadPS = Chad.transform.Find("ExhaustSmoke").GetComponent<ParticleSystem>();

            Transform ExhuastSmoke1 = CarObject.transform.Find("ExhaustSmoke");
            Transform ExhuastSmoke2 = CarObject.transform.Find("ExhaustSmoke2");

            GameObject NewExahust1 = GameObject.Instantiate(ExhaustSmokeReplacement, CarObject.transform);

            GameObject NewExahust2 = GameObject.Instantiate(ExhaustSmokeReplacement, CarObject.transform);

            ParticleSystem NewExahust1ParticleSystem = NewExahust1.GetComponent<ParticleSystem>();
            ParticleSystem NewExahust2ParticleSystem = NewExahust2.GetComponent<ParticleSystem>();

            NewExahust1.name = "ExhaustSmoke";
            NewExahust1.transform.parent = CarObject.transform;
            NewExahust1.transform.position = ExhuastSmoke1.position;
            NewExahust1.transform.localPosition = ExhuastSmoke1.localPosition;
            NewExahust1.transform.eulerAngles = ExhuastSmoke1.eulerAngles;
            NewExahust1.GetComponent<ParticleSystemRenderer>().material = ExhuastSmoke1.GetComponent<ParticleSystemRenderer>().material;
            List<ParticleSystem> S1list = new List<ParticleSystem>();
            List<ParticleSystem> S2list = new List<ParticleSystem>();

            var main = NewExahust1ParticleSystem.main.startSize;
            main.mode = ParticleSystemCurveMode.TwoConstants;
            main.constant = 0.4f;
            main.constantMin = 0.1f;
            main.constantMax = 0.4f;

            var main2 = NewExahust2ParticleSystem.main.startSize;
            main2.mode = ParticleSystemCurveMode.TwoConstants;
            main2.constant = 0.4f;
            main2.constantMin = 0.1f;
            main2.constantMax = 0.4f;


            SMOKE S1OLD = ExhuastSmoke1.GetComponent<SMOKE>();
            SMOKE S1 = NewExahust1.AddComponent<SMOKE>();   
            S1list.Add(NewExahust1ParticleSystem);
            S1.particleSystems.Capacity = 1;
            S1.particleSystems = S1list;
            S1.lifetimeDistance = 0.4f;
            S1.maxSizeMultiplier = 2.5f;
            S1.maxSpeedMultiplier = 1.4f;
            S1.BlackColor = S1OLD.BlackColor;
            S1.BlueColor = S1OLD.BlueColor;
            S1.normalColor = S1OLD.normalColor;
            S1.sootColor = S1OLD.sootColor;
            S1.WhiteColor = S1OLD.WhiteColor;

            NewExahust2.name = "ExhaustSmoke2";
            NewExahust2.transform.parent = CarObject.transform;
            NewExahust2.transform.position = ExhuastSmoke2.position;
            NewExahust2.transform.localPosition = ExhuastSmoke2.localPosition;
            NewExahust2.transform.eulerAngles = ExhuastSmoke2.eulerAngles;
            NewExahust2.GetComponent<ParticleSystemRenderer>().material = ExhuastSmoke2.GetComponent<ParticleSystemRenderer>().material;

            SMOKE S2OLD = ExhuastSmoke1.GetComponent<SMOKE>();
            SMOKE S2 = NewExahust2.AddComponent<SMOKE>();
            S2list.Add(NewExahust2.GetComponent<ParticleSystem>());
            S2.particleSystems = S2list;
            S2.lifetimeDistance = 0.4f;
            S2.maxSizeMultiplier = 2.5f;
            S2.maxSpeedMultiplier = 1.4f;
            S2.BlackColor = S2OLD.BlackColor;
            S2.BlueColor = S2OLD.BlueColor;
            S2.normalColor = S2OLD.normalColor;
            S2.sootColor = S2OLD.sootColor;
            S2.WhiteColor = S2OLD.WhiteColor;

            S1.enabled = true;
            S2.enabled = true;
            S1.maxSizeMultiplier = 1f;
            S2.maxSizeMultiplier = 1f;

            GameObject.Destroy(ExhuastSmoke2.gameObject);

            GameObject.Destroy(ExhuastSmoke1.gameObject);
            
        }

        private static void FixParticleSystemsTEST(GameObject CarObject) // Remove later : Version that does not use a assetbundle.
        {
            GameObject Chad = (GameObject)cachedResources.Load("Chad");

            ParticleSystem ChadPS = Chad.transform.Find("ExhaustSmoke").GetComponent<ParticleSystem>();

            Transform ExhuastSmoke1 = CarObject.transform.Find("ExhaustSmoke");
            Transform ExhuastSmoke2 = CarObject.transform.Find("ExhaustSmoke2");


            GameObject.Destroy(ExhuastSmoke2.gameObject);

            GameObject.Destroy(ExhuastSmoke1.gameObject);

            GameObject NewExahust1 = Chad.transform.Find("ExhaustSmoke").gameObject.DuplicateGameObject(CarObject.transform, false);

            GameObject NewExahust2 = Chad.transform.Find("ExhaustSmoke2").gameObject.DuplicateGameObject(CarObject.transform, false);

            NewExahust1.name = "ExhaustSmoke";
            NewExahust2.name = "ExhaustSmoke2";

            SMOKE S1 = NewExahust1.GetComponent<SMOKE>();
            SMOKE S2 = NewExahust2.GetComponent<SMOKE>();

            S1.particleSystems = new List<ParticleSystem>();
            S2.particleSystems = new List<ParticleSystem>();

            S1.particleSystems.Add(NewExahust1.GetComponent<ParticleSystem>());
            S2.particleSystems.Add(NewExahust2.GetComponent<ParticleSystem>());

        }

        private static void AddHarmonyPasser(String CarName, GameObject CarBuilt) 
        {

            MainCarProperties MCP = CarBuilt.GetComponent<MainCarProperties>();

            switch (CarName)
            {
                case "70 Coronet RT":

                    MCP.CarName = "Coronet RT";


                    CoronetHarmonyParamters CHP1 = CarBuilt.AddComponent<CoronetHarmonyParamters>();
                    CHP1.RarityRate = new Vector3(0, 5, 3);
                    break;

                case "70 Superbee":

                    MCP.CarName = "Superbee";

                    CoronetHarmonyParamters CHP2 = CarBuilt.AddComponent<CoronetHarmonyParamters>();
                    CHP2.RarityRate = new Vector3(0, 5, 4);

                    break;

                case "70 Coronet Sedan":

                    MCP.CarName = "Coronet Sedan";

                    CoronetHarmonyParamters CHP3 = CarBuilt.AddComponent<CoronetHarmonyParamters>();
                    CHP3.RarityRate = new Vector3(0, 10, 10);
                    break;

                case "70 Coronet Wagon":

                    MCP.CarName = "Coronet Wagon";

                    CoronetHarmonyParamters CHP4 = CarBuilt.AddComponent<CoronetHarmonyParamters>();
                    CHP4.RarityRate = new Vector3(0, 10, 10);
                    break;

            }
        }


        public static void CoronetStartOptionsAssembler(GameObject ParentStartOption, GameObject Option) // ALSO broken.
        {
            ParentStartOption = Option;
            PrettyLog.DebugLog(nameof(CoronetStartOptionsAssembler), "Added: " + Option.name + "To:" + ParentStartOption.transform.parent.name  + ".");
        }

        /// <summary>
        /// Build Functions
        /// </summary>

        public class CarBuilderFunctions
        {

            public static void TemplateBuild(GameObject Car)
            {
                Stopwatch TemplateWatch = Stopwatch.StartNew();

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                MCP.InCarHeight = 0.78f;

                if (Car.name == "70 Coronet RT")
                {
                    MCP.CarName = "Coronet RT";
                }
                if (Car.name == "70 Superbee")
                {
                    MCP.CarName = "Superbee";
                }

                // Replaces referances that have gone null after being ripped from chad.

                GameObject CrossTrans = Car.transform.Find("FrontSusp").transform.GetChild(0).gameObject;

                GameObject WiresMain06 = Car.transform.Find("WiresMain06").gameObject;


                CrossTrans.GetComponent<transparents>().DEPENDANTS[0].dependant = Car.transform.Find("RRLX_70_InnerPanelR").gameObject;
                CrossTrans.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                WiresMain06.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                // Fix the brake lines agian on template.
                Fixes.ReflectionFixes.ReflectionFoundationFixes.BrakePivotFix(Car.transform.Find("MainBrakeLine").transform);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                /*
                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(0).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotLshock").transform;

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(1).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotRshock").transform;
                */

                FixParticleSystems(Car);

                // Fix Wheelbase
                MoveWheelControllers(Car);

                TemplateWatch.Stop();

                PrettyLog.Log(nameof(TemplateBuild), "Took : " + TemplateWatch.ElapsedMilliseconds.ToString() + " ms to load");

            }

            public static void PostBuild(GameObject Car)
            {
                Stopwatch PostBuild = Stopwatch.StartNew();

                PrettyLog.DebugLog(nameof(PostBuild), "Getting CarParent");

                CarParentList = GameObject.Find("CarsParent").GetComponent<CarList>();

                PrettyLog.DebugLog(nameof(PostBuild), "Found CarParent, Getting Chad.");

               // int BartI = CarParentList.Cars.Find(x => x.name == "Bart");

                ChadBuilt = CarParentList.Cars[0];

               // BartBuilt = CarParentList.Cars[BartI];

                PrettyLog.DebugLog(nameof(PostBuild), "Chad Found.");

                PrettyLog.DebugLog(nameof(PostBuild), "Getting MCP.");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                // Changes Sit height
                PrettyLog.DebugLog(nameof(PostBuild), "Changing Seat Height.");
                MCP.InCarHeight = 0.78f;

                PrettyLog.DebugLog(nameof(PostBuild), "Fixing Fluids.");

                Fixes.ReflectionFixes.FixFluids(Car);

                PrettyLog.DebugLog(nameof(PostBuild), "Fixing American Classic.");

                Fixes.ReflectionFixes.AmericanClassicFix(Car);

                PrettyLog.DebugLog(nameof(PostBuild), "Fixing SuspensionShit.");
                Fixes.ReflectionFixes.SuspensionShit(Car);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(0).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotLshock").transform;

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(1).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotRshock").transform;


                PrettyLog.DebugLog(nameof(PostBuild), "Fixing Regs.");

                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorR/RRLX_70C_DoorR/RRLX_70C_WindowLiftFR/RRLX_70C_WindowLiftFR/WindowRollerR").gameObject, "FR");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorL/RRLX_70C_DoorL/RRLX_70C_WindowLiftFL/RRLX_70C_WindowLiftFL/WindowRollerL").gameObject, "FL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRL/RRLX_70C_InnerPanelRL/RRLX_70C_WindowLiftRL/RRLX_70C_WindowLiftRL/WindowRollerL").gameObject, "RL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRR/RRLX_70C_InnerPanelRR/RRLX_70C_WindowLiftRR/RRLX_70C_WindowLiftRR/WindowRollerR").gameObject, "RR");

                PrettyLog.DebugLog(nameof(PostBuild), "Base Relfections Fixes Complete.");

                PrettyLog.DebugLog(nameof(PostBuild), "Adding Passer");

                AddHarmonyPasser(Car.name, Car);

                PrettyLog.DebugLog(nameof(PostBuild), "Moving Wheel Controllers.");
                MoveWheelControllers(Car);
                PrettyLog.DebugLog(nameof(PostBuild), "Moved Wheel Controllers.");

                PrettyLog.DebugLog(nameof(PostBuild), "Fixing Exhaust System.");

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke").GetComponent<ParticleSystem>();
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke2").GetComponent<ParticleSystem>();

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().vc = MCP.exp;
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().vc = MCP.exp;

                PrettyLog.DebugLog(nameof(PostBuild), "Fixed Exhaust System.");

                PrettyLog.DebugLog(nameof(PostBuild), "Fixing HingePivots.");


                Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door/HingePivot").gameObject;

                PrettyLog.DebugLog(nameof(PostBuild), "Fixed HingePivots.");

                // Start Options

                PrettyLog.DebugLog(nameof(PostBuild), "Fixing Referances For Start Option Parts.");

                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption2 = ChadBuilt.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption2;
                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption3 = ChadBuilt.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption3;

                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption2 = ChadBuilt.transform.Find("floor07/floor07/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption4;
                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption3 = ChadBuilt.transform.Find("floor07/floor07/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption5;
                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption6 = ChadBuilt.transform.Find("floor07/floor07/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption6;

                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorL/RRLX_70C_DoorL").GetComponent<Partinfo>().EnabledTouchable = false;
                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorR/RRLX_70C_DoorR").GetComponent<Partinfo>().EnabledTouchable = false;


                Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood").GetComponent<Partinfo>().EnabledTouchable = false;

                Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk").GetComponent<Partinfo>().EnabledTouchable = false;

                Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk").GetComponent<Partinfo>().EnabledTouchable = false;

                Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door").GetComponent<Partinfo>().EnabledTouchable = false;

                /*
                rallyeWheels = BartBuilt.transform.Find("FrontSusp/Crossmemmber07/Crossmemmber07/HubCarrierFL07/HubCarrierFL07/WheelFL/HubF07/HubF07/Rim/Rim").GetComponent<CarProperties>().PREFAB;

                //GameObject.Instantiate(cachedResources.Load<GameObject>("tire15"), rallyeWheels.transform.Find("tire15"));

                Car.transform.Find("FrontSusp/Crossmemmber07/Crossmemmber07/HubCarrierFL07/HubCarrierFL07/WheelFL/HubF07/HubF07/Rim/Rim").GetComponent<CarProperties>().StartOption2 = rallyeWheels;
                Car.transform.Find("FrontSusp/Crossmemmber07/Crossmemmber07/HubCarrierFR07/HubCarrierFR07/WheelFR/HubF07/HubF07/Rim/Rim").GetComponent<CarProperties>().StartOption2 = rallyeWheels;
                Car.transform.Find("RearSusp/RearAxle07/RearAxle07/WheelRL/HubR07/HubR07/Rim/Rim").GetComponent<CarProperties>().StartOption2 = rallyeWheels;
                Car.transform.Find("RearSusp/RearAxle07/RearAxle07/WheelRR/HubR07/HubR07/Rim/Rim").GetComponent<CarProperties>().StartOption2 = rallyeWheels;
                */

                //Car.transform.Find("RRLX_70C_Roof/RRLX_70C_Roof").GetComponent<CarProperties>().StartOption6 = Autoloader.LoadedPrefabs["RRLX_70C_Roof2_G9"].Prefab;

                PrettyLog.DebugLog(nameof(PostBuild), "Fixed Referances For Start Option Parts.");
                
                PrettyLog.DebugLog(nameof(PostBuild), "Fixing Referance for Cluster Mileage");

                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye").GetComponent<CarProperties>().MileageText = Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Canvas/Text").GetComponent<Text>(); // Fixes NULL mileage error on new cars.
                
                PrettyLog.DebugLog(nameof(PostBuild), "Fixed Referance for Cluster Mileage");

                PrettyLog.DebugLog(nameof(PostBuild), "Removing hub cap");

                GameObject.Destroy(Car.transform.Find("RRLX_70C_Floor_Trunk/RRLX_70C_Floor_Trunk/RRLX_70_SpareWheelStand/RRLX_70_SpareWheelStand/Rim/Rim/SteelMHubCap/SteelMHubCap").gameObject);

                PrettyLog.DebugLog(nameof(PostBuild), "Removed hub cap");


                PrettyLog.DebugLog(nameof(PostBuild), "Enabling MCP Start Options.");

                MCP.Opt1 = true;
                MCP.Opt2 = true;
                MCP.Opt3 = true;
                MCP.Opt4 = true;
                MCP.Opt5 = true;
                MCP.Opt6 = true;
                MCP.Opt7 = true;
                MCP.Opt8 = true;
                MCP.Opt9 = true;

                PrettyLog.DebugLog(nameof(PostBuild), "Enabled MCP Start Options.");

                PrettyLog.DebugLog(nameof(PostBuild), "Initalizing Fix for Child Damage.");

                Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildDMG(MCP);

                PrettyLog.DebugLog(nameof(PostBuild), "Ran FixChildDMG.");


                PrettyLog.Log(nameof(PostBuild), "Finished PostBuild Loading.");

                PostBuild.Stop();

                PrettyLog.Log(nameof(PostBuild), "Took : " + PostBuild.ElapsedMilliseconds.ToString() + " ms to load for " + MCP.CarName);

            }


        }


    }


}
