using Microsoft.Cci;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.UI;

namespace RR_Coronet
{
    internal class CarBuilder
    {


        private static string EmptyException = "1213123190831297";

        private static GameObject ExhaustSmokeReplacement;

        private static Dictionary<string, string> CarNames = new Dictionary<string, string>();


        public static void CarBuilderInit()
        {
            if (DateTime.Today.Month == 4 || DateTime.Today.Month == 12)
            {
                if (DateTime.Today.Day == 1)
                {
                    CarNames.Add("70 Superbee", "Cornbread");
                    CarNames.Add("70 Coronet RT", "Cornbread");
                    CarNames.Add("70 Coronet 500", "Cornbread");

                    CarNames.Add("RRLX_69_Superbee", "Cornbread");
                    CarNames.Add("RRLX_69_Coronet_RT", "Cornbread");
                    CarNames.Add("RRLX_69_Coronet_500", "Cornbread");

                    CarNames.Add("RRLX_70_Coronet_Sedan", "Cornbread S");
                    CarNames.Add("RRLX_70_Coronet_Wagon", "Cornbread W");

                    CarNames.Add("RRLX_69_Coronet_Sedan", "Cornbread S");
                    CarNames.Add("RRLX_69_Coronet_Wagon", "Cornbread W");
                }
                return;
            }

            CarNames.Add("70 Superbee", "70 Coronet");
            CarNames.Add("70 Coronet RT", "70 Coronet");
            CarNames.Add("RRLX_70_Coronet_500", "70 Coronet");

            CarNames.Add("RRLX_69_Superbee", "69 Coronet");
            CarNames.Add("RRLX_69_Coronet_RT", "69 Coronet");
            CarNames.Add("RRLX_69_Coronet_500", "69 Coronet");

            CarNames.Add("RRLX_70_Coronet_Sedan", "70 Coronet S");
            CarNames.Add("RRLX_70_Coronet_Wagon", "70 Coronet W");

            CarNames.Add("RRLX_69_Coronet_Sedan", "69 Coronet S");
            CarNames.Add("RRLX_69_Coronet_Wagon", "69 Coronet W");
        }

        // Ran during the Mod constructor init, Runs for each car loaded. Sorts both the postbuild and template build setup, While also setting exceptions for multiple car support. 
        public static void LoadCarPreset(Car CarClass) 
        {
            // Set the car exceptions based on the gameobject name of the car's built prefab.
            SetExceptions(CarClass);

            // Run a switch statement using the same name to determine which methods should be set for postbuild (faster than using one single method, As the game only has to check for the car type once Rather than checking each time postbuild or template build is ran.)
            switch (CarClass.BuiltCarPrefab.GetComponent<CarGenerator>().CarName) 
            {
                case "70 Superbee":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetSuperbeeTemplate70);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetSuperbeePostBuild70);
                    break;
                case "70 Coronet RT":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetRTtemplate70);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetRTPostBuild70);
                    break;
                case "RRLX_70_Coronet_500":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.Coronet500template70);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.Coronet500PostBuild70);
                    break;
                case "RRLX_70_Coronet_Sedan":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetSedantemplate70);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetSedanPostBuild70);
                    break;
                case "RRLX_70_Coronet_Wagon":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetWagontemplate70);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetWagonPostBuild70);
                    break;
                case "RRLX_69_Superbee":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetSuperbeeTemplate69);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetSuperbeePostBuild69);
                    break;
                case "RRLX_69_Coronet_RT":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetRTtemplate69);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetRTPostBuild69);
                    break;
                case "RRLX_69_Coronet_500":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.Coronet500template69);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.Coronet500PostBuild69);
                    break;
                case "RRLX_69_Coronet_Sedan":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetSedantemplate69);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetSedanPostBuild69);
                    break;
                case "RRLX_69_Coronet_Wagon":
                    CarClass.SetCarTemplateFunction(CarBuilderFunctions.CoronetWagontemplate69);
                    CarClass.SetPostBuildFunction(CarBuilderFunctions.CoronetWagonPostBuild69);
                    break;
            }
        }

        private static void SetExceptions(Car c) 
        {

            switch(c.BuiltCarPrefab.GetComponent<CarGenerator>().CarName) 
            {
                // 70 Superbee 2 dr
                case "70 Superbee":
                    c.AddException("CylinderBlock", "CylinderBlockV8");
                    c.AddException("GearBox06", "GearBox07");
                    c.AddException("Rim", "RRLX_70_Steel1_15", true);
                    c.AddException("tire15", "tire15HF");
                    c.AddException("Wiper06", "WiperChromed06");
                    c.AddException("WiperBlade06", "WiperBlade07");
                    c.AddException("SeatFR06", "SeatFR09");
                    c.AddException("SeatFL06", "SeatFL09");
                    c.AddException("RRLX_70_Hood", "RRLX_70C_Hood_Pitch_G9");
                    c.AddException("RRLX_70C_RearPanel", "RRLX_70C_Superbee_Rear_Panel1_G10");
                    c.AddException("LicensePlate", "US_Plate1_70");
                    c.AddException("SteeringWheel06", "RRLX_70RT_SteeringWheel_G9");
                    c.AddException("RRLX_70C_WindowMR", "RRLX_70C_WindowMR1_G9", true); // Needed to prevent part lookup error, for some reason..
                    c.AddException("RRLX_70RT_VentRL", EmptyException);
                    c.AddException("RRLX_70RT_VentRR", EmptyException);
                    c.AddException("RRLX_70C_Trunk_Lip", "RRLX_70C_Trunk_Lip_Super_G10");
                    c.AddException("RRLX_70C_FenderFillerRR", "RRLX_70C_FenderFillerRR_G9");
                    c.AddException("RRLX_70C_FenderFillerRL", "RRLX_70C_FenderFillerRL_G9");
                    c.AddException("RRLX_70_ValanceR", "RRLX_70_ValanceR_G9");
                    c.AddException("RRLX_70_SteeringColumn", "RRLX_70_Steering_Column2_G9");
                    c.AddException("RRLX_70C_FenderL", "RRLX_70_FenderL1_G9");
                    c.AddException("RRLX_70C_FenderR", "RRLX_70_FenderR1_G9");
                    c.AddException("RRLX_70C_FenderRL", "RRLX_70C_FenderRL_G10");
                    c.AddException("RRLX_70C_FenderRR", "RRLX_70C_FenderRR_G10");
                    c.AddException("ExhaustR07", "RRLX_70_ExhaustR1", true);
                    c.AddException("ExhaustL07", "RRLX_70_ExhaustL1", true);
                    c.AddException("RRLX_70C_C_StripeL", "RRLX_70C_C_BStripeL_G10");
                    c.AddException("RRLX_70C_C_StripeR", "RRLX_70C_C_BStripeR_G10");
                    c.AddException("RRLX_70_GrillFR", "RRLX_70_GrillFR_G9");
                    c.AddException("RRLX_70_GrillFL", "RRLX_70_GrillFL_G9");
                    c.AddException("RRLX_70_BumperR", "RRLX_70_BumperR_G9");
                    c.AddException("RRLX_70_Valance_F", "RRLX_70_Valance_F_G9");
                    c.AddException("RRLX_70_HeadlightBracketL", "RRLX_70_HeadlightBracketL1_G9");
                    c.AddException("RRLX_70_HeadlightBracketR", "RRLX_70_HeadlightBracketR1_G9");
                    c.AddException("RRLX_70_Trunk", "RRLX_70_Trunk_G9");
                    c.AddException("RRLX_70_MirrorL", "RRLX_70_SportMirrorL_1_G9");
                    c.AddException("RRLX_70_MirrorR", "RRLX_70_SportMirrorR_1_G9");
                    c.AddException("RRLX_70C_DoorL", "RRLX_70C_DoorL2_G10");
                    c.AddException("RRLX_70C_DoorR", "RRLX_70C_DoorR2_G10");
                    break;

                // 70 Coronet RT 2 dr
                case "70 Coronet RT":
                    c.AddException("CylinderBlock", "CylinderBlockV8");
                    c.AddException("GearBox06", "GearBox07");
                    c.AddException("Rim", "Rim15St3", true); //Rim15St3
                    c.AddException("tire15", "tire15HF");
                    c.AddException("Wiper06", "WiperChromed06");
                    c.AddException("WiperBlade06", "WiperBlade07");
                    c.AddException("SeatFR06", "SeatFR09");
                    c.AddException("SeatFL06", "SeatFL09");
                    c.AddException("IntakeManifold07", "IntakeManifold607");
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
                    c.AddException("RRLX_70C_FenderL", "RRLX_70_FenderL1_G9");
                    c.AddException("RRLX_70C_FenderR", "RRLX_70_FenderR1_G9");
                    c.AddException("70_Hood", "RRLX_70C_Hood_G9");
                    c.AddException("RRLX_70C_FenderRL", "RRLX_70C_FenderRL_G10");
                    c.AddException("RRLX_70C_FenderRR", "RRLX_70C_FenderRR_G10");
                    c.AddException("ExhaustR07", "RRLX_70_ExhaustR1", true);
                    c.AddException("ExhaustL07", "RRLX_70_ExhaustL1", true);
                    c.AddException("RRLX_70C_C_StripeL", EmptyException);
                    c.AddException("RRLX_70C_C_StripeR", EmptyException);
                    c.AddException("RRLX_70_GrillFR", "RRLX_70_GrillFR_G9");
                    c.AddException("RRLX_70_GrillFL", "RRLX_70_GrillFL_G9");
                    c.AddException("RRLX_70_BumperR", "RRLX_70_BumperR_G9");
                    c.AddException("RRLX_70_Valance_F", "RRLX_70_Valance_F_G9");
                    c.AddException("RRLX_70_HeadlightBracketL", "RRLX_70_HeadlightBracketL1_G9");
                    c.AddException("RRLX_70_HeadlightBracketR", "RRLX_70_HeadlightBracketR1_G9");
                    c.AddException("RRLX_70_Trunk", "RRLX_70_Trunk_G9");
                    c.AddException("RRLX_70_MirrorL", "RRLX_70_SportMirrorL_1_G9");
                    c.AddException("RRLX_70_MirrorR", "RRLX_70_SportMirrorR_1_G9");
                    c.AddException("RRLX_70C_DoorL", "RRLX_70C_DoorL2_G10");
                    c.AddException("RRLX_70C_DoorR", "RRLX_70C_DoorR2_G10");
                    break;
                case "RRLX_70_Coronet_500":
                    c.AddException("CylinderBlock", "CylinderBlockV8");
                    c.AddException("GearBox06", "GearBox07");
                    c.AddException("Rim", "RRLX_70_Steel1_15", true);
                    c.AddException("tire15", "tire15HF");
                    c.AddException("Wiper06", "WiperChromed06");
                    c.AddException("WiperBlade06", "WiperBlade07");
                    c.AddException("SeatFR06", "SeatFR09");
                    c.AddException("SeatFL06", "SeatFL09");
                    c.AddException("RRLX_70_Hood", "RRLX_70C_Hood_Stock_G9");
                    c.AddException("LicensePlate", "US_Plate1_70");
                    c.AddException("SteeringWheel06", "RRLX_70RT_SteeringWheel_G9");
                    c.AddException("RRLX_70C_WindowMR", "RRLX_70C_WindowMR1_G9", true); // Needed to prevent part lookup error, for some reason..
                    c.AddException("RRLX_70RT_VentRL", EmptyException);
                    c.AddException("RRLX_70RT_VentRR", EmptyException);
                    c.AddException("RRLX_70C_FenderFillerRR", "RRLX_70C_FenderFillerRR_RT_G9");
                    c.AddException("RRLX_70C_FenderFillerRL", "RRLX_70C_FenderFillerRL_RT_G9");
                    c.AddException("RRLX_70_ValanceR", "RRLX_70_ValanceR_RT_G9");
                    c.AddException("RRLX_70C_RearPanel", "RRLX_70RT_Rear_Panel_G10");
                    c.AddException("RRLX_70C_Trunk_Lip", "RRLX_70C_TrunkLipRT_G9");
                    c.AddException("RRLX_70_SteeringColumn", "RRLX_70_Steering_Column2_G9");
                    c.AddException("RRLX_70C_FenderL", "RRLX_70_FenderL1_G9");
                    c.AddException("RRLX_70C_FenderR", "RRLX_70_FenderR1_G9");
                    c.AddException("RRLX_70C_FenderRL", "RRLX_70C_FenderRL_G10");
                    c.AddException("RRLX_70C_FenderRR", "RRLX_70C_FenderRR_G10");
                    c.AddException("RRLX_70C_Roof", "RRLX_70C_RoofC1_G10");
                    c.AddException("RRLX_70C_Hood_Scoop_L", EmptyException);
                    c.AddException("RRLX_70C_Hood_Scoop_R", EmptyException);
                    c.AddException("ExhaustR07", "RRLX_70_ExhaustR1", true);
                    c.AddException("ExhaustL07", "RRLX_70_ExhaustL1", true);
                    c.AddException("SteelMHubCap", "RRLX_70_HubCap2");
                    c.AddException("RRLX_70_GrillFR", "RRLX_70_GrillFR_G9");
                    c.AddException("RRLX_70_GrillFL", "RRLX_70_GrillFL_G9");
                    c.AddException("RRLX_70C_C_StripeL", EmptyException);
                    c.AddException("RRLX_70C_C_StripeR", EmptyException);
                    c.AddException("RRLX_70_BumperR", "RRLX_70_BumperR_G9");
                    c.AddException("RRLX_70_Valance_F", "RRLX_70_Valance_F_G9");
                    c.AddException("RRLX_70_HeadlightBracketL", "RRLX_70_HeadlightBracketL1_G9");
                    c.AddException("RRLX_70_HeadlightBracketR", "RRLX_70_HeadlightBracketR1_G9");
                    c.AddException("RRLX_70_MirrorL", "RRLX_70_SportMirrorL_1_G9");
                    c.AddException("RRLX_70_MirrorR", "RRLX_70_SportMirrorR_1_G9");
                    c.AddException("RRLX_70C_DoorL", "RRLX_70C_DoorL2_G10");
                    c.AddException("RRLX_70C_DoorR", "RRLX_70C_DoorR2_G10");
                    break;
                case "RRLX_69_Superbee":
                    c.AddException("CylinderBlock", "CylinderBlockV8");
                    c.AddException("GearBox06", "GearBox07");
                    c.AddException("Rim", "RRLX_70_Steel1_15", true);
                    c.AddException("tire15", "tire15HF");
                    c.AddException("Wiper06", "WiperChromed06");
                    c.AddException("WiperBlade06", "WiperBlade07");
                    c.AddException("SeatFR06", "SeatFR09");
                    c.AddException("SeatFL06", "SeatFL09");
                    c.AddException("RRLX_70_Hood", "RRLX_69_Hood_Superbee_G10");
                    c.AddException("RRLX_70C_RearPanel", "RRLX_69_Superbee_Rear_Panel");
                    c.AddException("LicensePlate", "US_Plate1_70");
                    c.AddException("SteeringWheel06", "RRLX_70RT_SteeringWheel_G9");
                    c.AddException("RRLX_70C_WindowMR", "RRLX_70C_WindowMR1_G9", true); // Needed to prevent part lookup error, for some reason..
                    c.AddException("RRLX_70RT_VentRL", EmptyException);
                    c.AddException("RRLX_70RT_VentRR", EmptyException);
                    c.AddException("RRLX_70C_FenderFillerRR", "RRLX_69_FenderFillerRR_G10");
                    c.AddException("RRLX_70C_FenderFillerRL", "RRLX_69_FenderFillerRL_G10");
                    c.AddException("RRLX_70_ValanceR", "RRLX_69_ValanceR_G9");
                    c.AddException("RRLX_70_SteeringColumn", "RRLX_70_Steering_Column2_G9");
                    c.AddException("RRLX_70C_FenderL", "RRLX_69_FenderL1_G9");
                    c.AddException("RRLX_70C_FenderR", "RRLX_69_FenderR1_G9");
                    c.AddException("RRLX_70C_FenderRL", "RRLX_69C_FenderRL_G1");
                    c.AddException("RRLX_70C_FenderRR", "RRLX_69C_FenderRR_G1");
                    c.AddException("RRLX_70C_Trunk_Lip", "RRLX_69_TrunkLip1_G10");
                    c.AddException("RRLX_70_FenderTFL", "RRLX_69_FenderTFL1");
                    c.AddException("RRLX_70_FenderTFR", "RRLX_69_FenderTFR1");
                    c.AddException("ExhaustR07", "RRLX_70_ExhaustR1", true);
                    c.AddException("ExhaustL07", "RRLX_70_ExhaustL1", true);
                    c.AddException("RRLX_69C_Stripe_Trunk", "RRLX_69C_BStripe_Trunk_G10");
                    c.AddException("RRLX_69C_StripeL", "RRLX_69C_BStripeL_G10");
                    c.AddException("RRLX_69C_StripeR", "RRLX_69C_BStripeR_G10");
                    c.AddException("RRLX_70_BumperR", "RRLX_69_BumperR_G10");
                    c.AddException("RRLX_70_Valance_F", "RRLX_69_BumperBrace_G10");
                    c.AddException("RRLX_70_HeadlightBracketL", "RRLX_69_HeadlightBracketL1_G9");
                    c.AddException("RRLX_70_HeadlightBracketR", "RRLX_69_HeadlightBracketR1_G9");
                    c.AddException("RRLX_70_Trunk", "RRLX_69_Trunk_G10");
                    c.AddException("RRLX_70_FrontBumperSupport", "RRLX_69_FrontBumperSupport_G10");
                    c.AddException("RRLX_70_MirrorL", "RRLX_69_MirrorL_1_10");
                    c.AddException("RRLX_70_MirrorR", "RRLX_69_MirrorR_1_10");
                    c.AddException("RRLX_70C_DoorL", "RRLX_70C_DoorL2_G10");
                    c.AddException("RRLX_70C_DoorR", "RRLX_70C_DoorR2_G10");
                    break;
            }

            //Universal Exceptions (all cars)
            c.AddException("RRLX_70_FenderMatL", EmptyException);
            c.AddException("RRLX_70_FenderMatR", EmptyException);
            c.AddException("TrailerHook06", EmptyException);
            c.AddException("RRLX_70_Spoiler", EmptyException);
            c.AddException("RoofRack06", EmptyException);
            c.AddException("TaxiSign", EmptyException);
            c.AddException("SpeedoDigital", EmptyException);
            c.AddException("HeadlightCase06", EmptyException);
            c.AddException("RRLX_Lightbar", EmptyException);
            c.AddException("RRLX_SpotlightL", EmptyException);
            c.AddException("RRLX_SpotlightR", EmptyException);


        }

        public static void SetExhaustParticleSystem(GameObject go) 
        {
            ExhaustSmokeReplacement = go;
        }

        private static void MoveWheelControllers(GameObject CarObject)
        {
            GameObject WheelControllerFR = CarObject.transform.Find("FrontSusp/Crossmemmber07/WheelContParentFR").gameObject;

            if (WheelControllerFR)
            {
                WheelControllerFR.transform.localPosition = new Vector3(0.655f, WheelControllerFR.transform.localPosition.y, WheelControllerFR.transform.localPosition.z); //0.685f
                PrettyLog.DebugLog("MoveWheelControllers", "FR Wheel Positions moved to : " + WheelControllerFR.transform.localPosition.ToString());
            }

            GameObject WheelControllerFL = CarObject.transform.Find("FrontSusp/Crossmemmber07/WheelContParentFL").gameObject;

            if (WheelControllerFL)
            {
                WheelControllerFL.transform.localPosition = new Vector3(-0.655f, WheelControllerFL.transform.localPosition.y, WheelControllerFL.transform.localPosition.z);
                PrettyLog.DebugLog("MoveWheelControllers", "FL Wheel Positions moved to : " + WheelControllerFL.transform.localPosition.ToString());
            }

            GameObject WheelControllerRR = CarObject.transform.Find("RearSusp/WheelControllerRR").gameObject;

            if (WheelControllerRR)
            {
                WheelControllerRR.transform.localPosition = new Vector3(WheelControllerRR.transform.localPosition.x, WheelControllerRR.transform.localPosition.y, 0.7145f);
                PrettyLog.DebugLog("MoveWheelControllers", "RR Wheel Positions moved to : " + WheelControllerRR.transform.localPosition.ToString());
            }

            GameObject WheelControllerRL = CarObject.transform.Find("RearSusp/WheelControllerRL").gameObject;

            if (WheelControllerRL)
            {
                WheelControllerRL.transform.localPosition = new Vector3(WheelControllerRL.transform.localPosition.x, WheelControllerRL.transform.localPosition.y, -0.7145f);
                PrettyLog.DebugLog("MoveWheelControllers", "RL Wheel Positions moved to : " + WheelControllerRL.transform.localPosition.ToString());
            } // 0.97

            GameObject Axle07 = CarObject.transform.Find("RearSusp/RearAxle07/").gameObject;

            MyBoneSCR AxleBone = Axle07.GetComponent<MyBoneSCR>();

            if (Axle07)
            {
                Axle07.transform.localScale = new Vector3(Axle07.transform.localScale.x, Axle07.transform.localScale.y, 0.97f);
                Axle07.GetComponent<MyBoneSCR>();
                PrettyLog.DebugLog("MoveWheelControllers", "Resized Axle07 to : " + Axle07.transform.localScale.ToString());
            }

            if (AxleBone) 
            {
                AxleBone._initZScale = 0.97f;
            }

            PrettyLog.DebugLog("MoveWheelControllers", "Finished.");

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


        private class CarBuilderFunctions 
        {
            // 70 2 doors
            public static void CoronetSuperbeePostBuild70(GameObject Car) 
            {
                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Postbuild Running for : " + Car.name);

                Stopwatch PostBuild = Stopwatch.StartNew();

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Getting MCP.");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Setting MCP Name.");

                MCP.CarName = CarNames[Car.name];

                // Changes Sit height
                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Changing Seat Height.");
                MCP.InCarHeight = 0.78f;

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing Fluids.");

                Fixes.ReflectionFixes.FixFluids(Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing American Classic.");

                Fixes.ReflectionFixes.AmericanClassicFix(Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing SuspensionShit.");

                Fixes.ReflectionFixes.SuspensionShit(Car);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(0).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotLshock").transform;

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(1).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotRshock").transform;


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing Regs.");

                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorR/RRLX_70C_DoorR/RRLX_70C_WindowLiftFR/RRLX_70C_WindowLiftFR/WindowRollerR").gameObject, "FR");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorL/RRLX_70C_DoorL/RRLX_70C_WindowLiftFL/RRLX_70C_WindowLiftFL/WindowRollerL").gameObject, "FL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRL/RRLX_70C_InnerPanelRL/RRLX_70C_WindowLiftRL/RRLX_70C_WindowLiftRL/WindowRollerL").gameObject, "RL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRR/RRLX_70C_InnerPanelRR/RRLX_70C_WindowLiftRR/RRLX_70C_WindowLiftRR/WindowRollerR").gameObject, "RR");


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Base Relfections Fixes Complete.");

                PrettyLog.DebugLog(nameof(CoronetSuperbeePostBuild70), "Adding Passer");

                //AddHarmonyPasser(Car.name, Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Moving Wheel Controllers.");

                MoveWheelControllers(Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Moved Wheel Controllers.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing Exhaust System.");

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke").GetComponent<ParticleSystem>();
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke2").GetComponent<ParticleSystem>();

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().vc = MCP.exp;
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().vc = MCP.exp;

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixed Exhaust System.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing HingePivots.");

                Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door/HingePivot").gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing Referances For Start Option Parts.");

                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GB5");
                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GBA");

                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GSB");
                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GSD");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixed Referances For Start Option Parts.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing Referance for Cluster Mileage");

                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye").GetComponent<CarProperties>().MileageText = Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Canvas/Text").GetComponent<Text>(); // Fixes NULL mileage error on new cars.

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixed Referance for Cluster Mileage");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Removing hub cap");

                GameObject.Destroy(Car.transform.Find("RRLX_70C_Floor_Trunk/RRLX_70C_Floor_Trunk/RRLX_70_SpareWheelStand/RRLX_70_SpareWheelStand/Rim/Rim/SteelMHubCap/SteelMHubCap").gameObject);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Removed hub cap");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixing ClusterR and L.");


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Fixed ClusterR and L.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Enabling MCP Start Options.");

                MCP.Opt1 = true;
                MCP.Opt2 = true;
                MCP.Opt3 = true;
                MCP.Opt4 = true;
                MCP.Opt5 = true;
                MCP.Opt6 = true;
                MCP.Opt7 = true;
                MCP.Opt8 = true;
                MCP.Opt9 = true;

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Enabled MCP Start Options.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Initalizing Fix for Child Damage.");

                Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildDMG(MCP);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Ran FixChildDMG.");


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Finished PostBuild Loading.");

                PostBuild.Stop();

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Took : " + PostBuild.ElapsedMilliseconds.ToString() + " ms to load for " + MCP.CarName);
            }
            public static void CoronetSuperbeeTemplate70(GameObject Car)
            {
                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "TemplateBuild Running for : " + Car.name);

                Stopwatch TemplateWatch = Stopwatch.StartNew();

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Getting MCP...");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "MCP found.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Setting MCP Name.");

                MCP.CarName = CarNames[Car.name];

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Modifying InCarHeight...");

                MCP.InCarHeight = 0.78f;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "InCarHeight Modified");


                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Getting Suspension Gameobjects for Chad referance fixes...");

                // Replaces referances that have gone null after being ripped from chad.

                GameObject CrossTrans = Car.transform.Find("FrontSusp").transform.GetChild(0).gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Crossmember Found.");

                GameObject WiresMain06 = Car.transform.Find("WiresMain06").gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "WireMain06 Found.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Getting Suspension Gameobjects for Chad referance fixes...");

                CrossTrans.GetComponent<transparents>().DEPENDANTS[0].dependant = Car.transform.Find("RRLX_70_InnerPanelR").gameObject;
                CrossTrans.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Suspension Gameobjects for Chad referance fixes found!");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Replacing WiresMain06 Transparent Dependent...");

                WiresMain06.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "WiresMain06 Transparent Dependent Replaced");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Running internal BrakeLine Pivot fix...");

                // Fix the brake lines agian on template.
                Fixes.ReflectionFixes.ReflectionFoundationFixes.BrakePivotFix(Car.transform.Find("MainBrakeLine").transform);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Ran internal BrakeLine Pivot fix.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Fixing Axle07 Referances...");


                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Fixed Axle07 Referance 1...");

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Fixed Axle07 Referance 2.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Modifying Exhaust Emitters...");

                FixParticleSystems(Car);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Modified Exhaust Emitters.");


                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Modifying Wheelbase...");

                // Fix Wheelbase
                MoveWheelControllers(Car);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Modified Wheelbase.");

                //Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildPrefabRefs(Car);

                TemplateWatch.Stop();

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate70), "Took : " + TemplateWatch.ElapsedMilliseconds.ToString() + " ms to load");
            }
            public static void CoronetRTPostBuild70(GameObject Car)
            {
                PrettyLog.Log(nameof(CoronetSuperbeePostBuild70), "Postbuild Running for : " + Car.name);

                Stopwatch PostBuild = Stopwatch.StartNew();

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Getting MCP.");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Setting MCP Name.");

                MCP.CarName = CarNames[Car.name];

                // Changes Sit height
                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Changing Seat Height.");
                MCP.InCarHeight = 0.78f;

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing Fluids.");

                Fixes.ReflectionFixes.FixFluids(Car);

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing American Classic.");

                Fixes.ReflectionFixes.AmericanClassicFix(Car);

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing SuspensionShit.");

                Fixes.ReflectionFixes.SuspensionShit(Car);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(0).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotLshock").transform;

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(1).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotRshock").transform;


                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing Regs.");

                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorR/RRLX_70C_DoorR/RRLX_70C_WindowLiftFR/RRLX_70C_WindowLiftFR/WindowRollerR").gameObject, "FR");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorL/RRLX_70C_DoorL/RRLX_70C_WindowLiftFL/RRLX_70C_WindowLiftFL/WindowRollerL").gameObject, "FL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRL/RRLX_70C_InnerPanelRL/RRLX_70C_WindowLiftRL/RRLX_70C_WindowLiftRL/WindowRollerL").gameObject, "RL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRR/RRLX_70C_InnerPanelRR/RRLX_70C_WindowLiftRR/RRLX_70C_WindowLiftRR/WindowRollerR").gameObject, "RR");


                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Base Relfections Fixes Complete.");

                PrettyLog.DebugLog(nameof(CoronetRTPostBuild70), "Adding Passer");

                //AddHarmonyPasser(Car.name, Car);

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Moving Wheel Controllers.");

                MoveWheelControllers(Car);

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Moved Wheel Controllers.");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing Exhaust System.");

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke").GetComponent<ParticleSystem>();
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke2").GetComponent<ParticleSystem>();

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().vc = MCP.exp;
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().vc = MCP.exp;

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixed Exhaust System.");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing HingePivots.");

                Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door/HingePivot").gameObject;

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing Referances For Start Option Parts.");

                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GB5");
                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GBA");

                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GSB");
                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GSD");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixed Referances For Start Option Parts.");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing Referance for Cluster Mileage");

                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye").GetComponent<CarProperties>().MileageText = Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Canvas/Text").GetComponent<Text>(); // Fixes NULL mileage error on new cars.

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixed Referance for Cluster Mileage");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Removing hub cap");

                GameObject.Destroy(Car.transform.Find("RRLX_70C_Floor_Trunk/RRLX_70C_Floor_Trunk/RRLX_70_SpareWheelStand/RRLX_70_SpareWheelStand/Rim/Rim/SteelMHubCap/SteelMHubCap").gameObject);

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Removed hub cap");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixing ClusterR and L.");


                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Fixed ClusterR and L.");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Enabling MCP Start Options.");

                MCP.Opt1 = true;
                MCP.Opt2 = true;
                MCP.Opt3 = true;
                MCP.Opt4 = true;
                MCP.Opt5 = true;
                MCP.Opt6 = true;
                MCP.Opt7 = true;
                MCP.Opt8 = true;
                MCP.Opt9 = true;

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Enabled MCP Start Options.");

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Initalizing Fix for Child Damage.");

                Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildDMG(MCP);

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Ran FixChildDMG.");


                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Finished PostBuild Loading.");

                PostBuild.Stop();

                PrettyLog.Log(nameof(CoronetRTPostBuild70), "Took : " + PostBuild.ElapsedMilliseconds.ToString() + " ms to load for " + MCP.CarName);
            }
            public static void CoronetRTtemplate70(GameObject Car)
            {
                PrettyLog.Log(nameof(CoronetRTtemplate70), "TemplateBuild Running for : " + Car.name);

                Stopwatch TemplateWatch = Stopwatch.StartNew();

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Getting MCP...");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Setting MCP Name.");

                MCP.CarName = CarNames[Car.name];

                PrettyLog.Log(nameof(CoronetRTtemplate70), "MCP found.");

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Modifying InCarHeight...");

                MCP.InCarHeight = 0.78f;

                PrettyLog.Log(nameof(CoronetRTtemplate70), "InCarHeight Modified");


                PrettyLog.Log(nameof(CoronetRTtemplate70), "Getting Suspension Gameobjects for Chad referance fixes...");

                // Replaces referances that have gone null after being ripped from chad.

                GameObject CrossTrans = Car.transform.Find("FrontSusp").transform.GetChild(0).gameObject;

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Crossmember Found.");

                GameObject WiresMain06 = Car.transform.Find("WiresMain06").gameObject;

                PrettyLog.Log(nameof(CoronetRTtemplate70), "WireMain06 Found.");

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Getting Suspension Gameobjects for Chad referance fixes...");

                CrossTrans.GetComponent<transparents>().DEPENDANTS[0].dependant = Car.transform.Find("RRLX_70_InnerPanelR").gameObject;
                CrossTrans.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Suspension Gameobjects for Chad referance fixes found!");

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Replacing WiresMain06 Transparent Dependent...");

                WiresMain06.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                PrettyLog.Log(nameof(CoronetRTtemplate70), "WiresMain06 Transparent Dependent Replaced");

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Running internal BrakeLine Pivot fix...");

                // Fix the brake lines agian on template.
                Fixes.ReflectionFixes.ReflectionFoundationFixes.BrakePivotFix(Car.transform.Find("MainBrakeLine").transform);

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Ran internal BrakeLine Pivot fix.");

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Fixing Axle07 Referances...");


                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Fixed Axle07 Referance 1...");

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Fixed Axle07 Referance 2.");

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Modifying Exhaust Emitters...");

                FixParticleSystems(Car);

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Modified Exhaust Emitters.");


                PrettyLog.Log(nameof(CoronetRTtemplate70), "Modifying Wheelbase...");

                // Fix Wheelbase
                MoveWheelControllers(Car);

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Modified Wheelbase.");

                //Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildPrefabRefs(Car);

                TemplateWatch.Stop();

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Took : " + TemplateWatch.ElapsedMilliseconds.ToString() + " ms to load");
            }
            public static void Coronet500PostBuild70(GameObject Car)
            {
                PrettyLog.Log(nameof(Coronet500PostBuild70), "Postbuild Running for : " + Car.name);

                Stopwatch PostBuild = Stopwatch.StartNew();

                PrettyLog.Log(nameof(PostBuild), "Getting MCP.");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                PrettyLog.Log(nameof(CoronetRTtemplate70), "Setting MCP Name.");

                MCP.CarName = CarNames[Car.name];

                // Changes Sit height
                PrettyLog.Log(nameof(PostBuild), "Changing Seat Height.");
                MCP.InCarHeight = 0.78f;

                PrettyLog.Log(nameof(PostBuild), "Fixing Fluids.");

                Fixes.ReflectionFixes.FixFluids(Car);

                PrettyLog.Log(nameof(PostBuild), "Fixing American Classic.");

                Fixes.ReflectionFixes.AmericanClassicFix(Car);

                PrettyLog.Log(nameof(PostBuild), "Fixing SuspensionShit.");
                Fixes.ReflectionFixes.SuspensionShit(Car);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(0).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotLshock").transform;

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(1).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotRshock").transform;


                PrettyLog.Log(nameof(PostBuild), "Fixing Regs.");

                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorR/RRLX_70C_DoorR/RRLX_70C_WindowLiftFR/RRLX_70C_WindowLiftFR/WindowRollerR").gameObject, "FR");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorL/RRLX_70C_DoorL/RRLX_70C_WindowLiftFL/RRLX_70C_WindowLiftFL/WindowRollerL").gameObject, "FL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRL/RRLX_70C_InnerPanelRL/RRLX_70C_WindowLiftRL/RRLX_70C_WindowLiftRL/WindowRollerL").gameObject, "RL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRR/RRLX_70C_InnerPanelRR/RRLX_70C_WindowLiftRR/RRLX_70C_WindowLiftRR/WindowRollerR").gameObject, "RR");


                PrettyLog.Log(nameof(PostBuild), "Base Relfections Fixes Complete.");

                PrettyLog.DebugLog(nameof(PostBuild), "Adding Passer");

                //AddHarmonyPasser(Car.name, Car);

                PrettyLog.Log(nameof(PostBuild), "Moving Wheel Controllers.");

                MoveWheelControllers(Car);

                PrettyLog.Log(nameof(PostBuild), "Moved Wheel Controllers.");

                PrettyLog.Log(nameof(PostBuild), "Fixing Exhaust System.");

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke").GetComponent<ParticleSystem>();
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke2").GetComponent<ParticleSystem>();

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().vc = MCP.exp;
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().vc = MCP.exp;

                PrettyLog.Log(nameof(PostBuild), "Fixed Exhaust System.");

                PrettyLog.Log(nameof(PostBuild), "Fixing HingePivots.");


                Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlF/RRLX_70_CowlF/RRLX_70_Hood/RRLX_70_Hood/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk/HingePivot").gameObject;

                Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door").GetComponent<Partinfo>().HingePivot = Car.transform.Find("RRLX_70_ValanceR/RRLX_70_ValanceR/RRLX_70_BumperR/RRLX_70_BumperR/RRLX_70_Fuel_Door/RRLX_70_Fuel_Door/HingePivot").gameObject;

                PrettyLog.Log(nameof(PostBuild), "Fixing Referances For Start Option Parts.");

                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GB5");
                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GBA");

                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GSB");
                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GSD");

                GameObject i6Go = StartOptionManager.GetExistingStartOption("I6M");
                MainCarProperties EngineProps = Car.transform.Find("EngineTranny/CylinderBlock/CylinderBlock").GetComponent<MainCarProperties>();

                //Init vars
                EngineProps.Opt1 = new GameObject();
                EngineProps.Opt2 = new GameObject();
                EngineProps.Opt3 = new GameObject();
                EngineProps.Opt4 = new GameObject();
                EngineProps.Opt5 = new GameObject();
                EngineProps.Opt6 = new GameObject();

                // set
                EngineProps.Opt1 = i6Go;
                EngineProps.Opt2 = i6Go;
                EngineProps.Opt3 = i6Go;
                EngineProps.Opt4 = i6Go;
                EngineProps.Opt5 = i6Go;
                EngineProps.Opt6 = i6Go;

                Car.transform.Find("RRLX_70_CowlR/RRLX_70_CowlR/RRLX_70_Trunk/RRLX_70_Trunk").GetComponent<CarProperties>().StartOption1 = null; // Removes the spoiler from spawning on the coronet 500.

                PrettyLog.Log(nameof(PostBuild), "Fixed Referances For Start Option Parts.");

                PrettyLog.Log(nameof(PostBuild), "Fixing Referance for Cluster Mileage");

                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye").GetComponent<CarProperties>().MileageText = Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Canvas/Text").GetComponent<Text>(); // Fixes NULL mileage error on new cars.

                PrettyLog.Log(nameof(PostBuild), "Fixed Referance for Cluster Mileage");

                PrettyLog.Log(nameof(PostBuild), "Removing hub cap");

                GameObject.Destroy(Car.transform.Find("RRLX_70C_Floor_Trunk/RRLX_70C_Floor_Trunk/RRLX_70_SpareWheelStand/RRLX_70_SpareWheelStand/Rim/Rim/SteelMHubCap/SteelMHubCap").gameObject);

                PrettyLog.Log(nameof(PostBuild), "Removed hub cap");

                PrettyLog.Log(nameof(PostBuild), "Fixing ClusterR and L.");

                /*
                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Left").gameObject.SetActive(false);
                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Right").gameObject.SetActive(false);

                MCP.Cluster.ClusterL = Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Left").gameObject;
                MCP.Cluster.ClusterR = Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Right").gameObject;
                */

                PrettyLog.Log(nameof(PostBuild), "Fixed ClusterR and L.");

                PrettyLog.Log(nameof(PostBuild), "Enabling MCP Start Options.");

                MCP.Opt1 = true;
                MCP.Opt2 = true;
                MCP.Opt3 = true;
                MCP.Opt4 = true;
                MCP.Opt5 = true;
                MCP.Opt6 = true;
                MCP.Opt7 = true;
                MCP.Opt8 = true;
                MCP.Opt9 = true;

                PrettyLog.Log(nameof(PostBuild), "Enabled MCP Start Options.");

                PrettyLog.Log(nameof(PostBuild), "Initalizing Fix for Child Damage.");

                Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildDMG(MCP);

                PrettyLog.Log(nameof(PostBuild), "Ran FixChildDMG.");


                PrettyLog.Log(nameof(PostBuild), "Finished PostBuild Loading.");

                PostBuild.Stop();

                PrettyLog.Log(nameof(PostBuild), "Took : " + PostBuild.ElapsedMilliseconds.ToString() + " ms to load for " + MCP.CarName);
            }
            public static void Coronet500template70(GameObject Car)
            {
                
            }

            // 69 2 doors
            public static void CoronetSuperbeePostBuild69(GameObject Car)
            {
                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Postbuild Running for : " + Car.name);

                Stopwatch PostBuild = Stopwatch.StartNew();

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Getting MCP.");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Setting MCP Name.");

                MCP.CarName = CarNames[Car.name];

                // Changes Sit height
                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Changing Seat Height.");
                MCP.InCarHeight = 0.78f;

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing Fluids.");

                Fixes.ReflectionFixes.FixFluids(Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing American Classic.");

                Fixes.ReflectionFixes.AmericanClassicFix(Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing SuspensionShit.");
                Fixes.ReflectionFixes.SuspensionShit(Car);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(0).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotLshock").transform;

                Car.transform.Find("RRLX_70_RearShockSupport/RRLX_70_RearShockSupport").GetChild(1).GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp/RearAxle07/pivotRshock").transform;


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing Regs.");

                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorR/RRLX_70C_DoorR/RRLX_70C_WindowLiftFR/RRLX_70C_WindowLiftFR/WindowRollerR").gameObject, "FR");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70C_DoorL/RRLX_70C_DoorL/RRLX_70C_WindowLiftFL/RRLX_70C_WindowLiftFL/WindowRollerL").gameObject, "FL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRL/RRLX_70C_InnerPanelRL/RRLX_70C_WindowLiftRL/RRLX_70C_WindowLiftRL/WindowRollerL").gameObject, "RL");
                Fixes.ReflectionFixes.FixWindowRegs(Car.transform.Find("RRLX_70C_InnerPanelRR/RRLX_70C_InnerPanelRR/RRLX_70C_WindowLiftRR/RRLX_70C_WindowLiftRR/WindowRollerR").gameObject, "RR");


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Base Relfections Fixes Complete.");

                PrettyLog.DebugLog(nameof(CoronetSuperbeePostBuild69), "Adding Passer");

                //AddHarmonyPasser(Car.name, Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Moving Wheel Controllers.");

                MoveWheelControllers(Car);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Moved Wheel Controllers.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing Exhaust System.");

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke").GetComponent<ParticleSystem>();
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().particleSystems[0] = Car.transform.Find("ExhaustSmoke2").GetComponent<ParticleSystem>();

                Car.transform.Find("ExhaustSmoke").GetComponent<SMOKE>().vc = MCP.exp;
                Car.transform.Find("ExhaustSmoke2").GetComponent<SMOKE>().vc = MCP.exp;

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixed Exhaust System.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing Referances For Start Option Parts.");

                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GB5");
                Car.transform.Find("EngineTranny/GearBox06/GearBox06").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GBA");

                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption2 = StartOptionManager.GetExistingStartOption("GSB");
                Car.transform.Find("RRLX_70C_Floor/RRLX_70C_Floor/GearStick07/GearStick07").GetComponent<CarProperties>().StartOption3 = StartOptionManager.GetExistingStartOption("GSD");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixed Referances For Start Option Parts.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing Referance for Cluster Mileage");

                Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye").GetComponent<CarProperties>().MileageText = Car.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/RRLX_70_Dashboard/RRLX_70_Dashboard/RRLX_70_Cluster_Rallye/RRLX_70_Cluster_Rallye/Canvas/Text").GetComponent<Text>(); // Fixes NULL mileage error on new cars.

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixed Referance for Cluster Mileage");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Removing hub cap");

                GameObject.Destroy(Car.transform.Find("RRLX_70C_Floor_Trunk/RRLX_70C_Floor_Trunk/RRLX_70_SpareWheelStand/RRLX_70_SpareWheelStand/Rim/Rim/SteelMHubCap/SteelMHubCap").gameObject);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Removed hub cap");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixing ClusterR and L.");


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Fixed ClusterR and L.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Enabling MCP Start Options.");

                MCP.Opt1 = true;
                MCP.Opt2 = true;
                MCP.Opt3 = true;
                MCP.Opt4 = true;
                MCP.Opt5 = true;
                MCP.Opt6 = true;
                MCP.Opt7 = true;
                MCP.Opt8 = true;
                MCP.Opt9 = true;

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Enabled MCP Start Options.");

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Initalizing Fix for Child Damage.");

                Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildDMG(MCP);

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Ran FixChildDMG.");


                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Finished PostBuild Loading.");

                PostBuild.Stop();

                PrettyLog.Log(nameof(CoronetSuperbeePostBuild69), "Took : " + PostBuild.ElapsedMilliseconds.ToString() + " ms to load for " + MCP.CarName);
            }
            public static void CoronetSuperbeeTemplate69(GameObject Car)
            {
                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "TemplateBuild Running for : " + Car.name);

                Stopwatch TemplateWatch = Stopwatch.StartNew();

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Getting MCP...");

                MainCarProperties MCP = Car.GetComponent<MainCarProperties>();

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Setting MCP Name.");

                MCP.CarName = CarNames[Car.name];

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "MCP found.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Modifying InCarHeight...");

                MCP.InCarHeight = 0.78f;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "InCarHeight Modified");


                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Getting Suspension Gameobjects for Chad referance fixes...");

                // Replaces referances that have gone null after being ripped from chad.

                GameObject CrossTrans = Car.transform.Find("FrontSusp").transform.GetChild(0).gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Crossmember Found.");

                GameObject WiresMain06 = Car.transform.Find("WiresMain06").gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "WireMain06 Found.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Getting Suspension Gameobjects for Chad referance fixes...");

                CrossTrans.GetComponent<transparents>().DEPENDANTS[0].dependant = Car.transform.Find("RRLX_70_InnerPanelR").gameObject;
                CrossTrans.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Suspension Gameobjects for Chad referance fixes found!");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Replacing WiresMain06 Transparent Dependent...");

                WiresMain06.GetComponent<transparents>().DEPENDANTS[1].dependant = Car.transform.Find("RRLX_70_InnerPanelL").gameObject;

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "WiresMain06 Transparent Dependent Replaced");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Running internal BrakeLine Pivot fix...");

                // Fix the brake lines agian on template.
                Fixes.ReflectionFixes.ReflectionFoundationFixes.BrakePivotFix(Car.transform.Find("MainBrakeLine").transform);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Ran internal BrakeLine Pivot fix.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Fixing Axle07 Referances...");


                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransform = Car.transform.Find("RearSusp").GetChild(2);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Fixed Axle07 Referance 1...");

                Car.transform.Find("RearSusp/RearAxle07").GetComponent<MyBoneSCR>().targetTransformB = Car.transform.Find("RearSusp").GetChild(1);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Fixed Axle07 Referance 2.");

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Modifying Exhaust Emitters...");

                FixParticleSystems(Car);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Modified Exhaust Emitters.");


                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Modifying Wheelbase...");

                // Fix Wheelbase
                MoveWheelControllers(Car);

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Modified Wheelbase.");

                //Fixes.ReflectionFixes.ReflectionFoundationFixes.FixChildPrefabRefs(Car);

                TemplateWatch.Stop();

                PrettyLog.Log(nameof(CoronetSuperbeeTemplate69), "Took : " + TemplateWatch.ElapsedMilliseconds.ToString() + " ms to load");
            }
            public static void CoronetRTPostBuild69(GameObject CarObject)
            {

            }
            public static void CoronetRTtemplate69(GameObject CarObject)
            {

            }
            public static void Coronet500PostBuild69(GameObject CarObject)
            {

            }
            public static void Coronet500template69(GameObject CarObject)
            {

            }

            // 4 Doors

            public static void CoronetSedanPostBuild70(GameObject CarObject)
            {

            }
            public static void CoronetSedantemplate70(GameObject CarObject)
            {

            }
            public static void CoronetWagonPostBuild70(GameObject CarObject)
            {

            }
            public static void CoronetWagontemplate70(GameObject CarObject)
            {

            }
            public static void CoronetSedanPostBuild69(GameObject CarObject)
            {

            }
            public static void CoronetSedantemplate69(GameObject CarObject)
            {

            }
            public static void CoronetWagonPostBuild69(GameObject CarObject)
            {

            }
            public static void CoronetWagontemplate69(GameObject CarObject)
            {

            }

        }
    }
}
