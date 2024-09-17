using FluffyUnderware.DevTools.Extensions;
using JetBrains.Annotations;
using NWH.VehiclePhysics;
using NWH.VehiclePhysics2.VehicleGUI;
using PaintIn3D;
using RR_Coronet.CustomMonoScripts;
using RVP;
using SimplePartLoader;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace RR_Coronet
{
    public class Fixes
    {
        public class PartFixes
        {
            public static void BuildIgitionKey(GameObject p) // will be changed soon for the normal key. As for some fucking reason the game sets the rotation at the most off angles possible.
            {
                PrettyLog.Log("BuildIgitionKey", "3");

                p.GetComponent<CarProperties>().IgnitionKey = true;

                PrettyLog.Log("BuildIgitionKey", "4");

                //p.transform.eulerAngles = new Vector3(25f, 0f, 0f);

                p.AddComponent<IgnitionKey>();

                PrettyLog.Log("BuildIgitionKey", "5");

                p.layer = LayerMask.NameToLayer("OpenableParts");

                PrettyLog.Log("BuildIgitionKey", "6");
            }

            public static void FixGasCap(Part part)
            {
                transparents CapT = part.Prefab.gameObject.transform.GetComponentInChildren<transparents>();

                GameObject Cap = CapT.gameObject.transform.GetComponentInChildren<DISABLER>().gameObject;

                GameObject FuelContainer = part.Prefab.gameObject.transform.GetComponentInChildren<FLUID>().gameObject;

                PrettyLog.Log("FixGasCap", "3");

                PickupCup PC = Cap.AddComponent<PickupCup>();

                PrettyLog.Log("FixGasCap", "4");

                PC.canHold = true;

                PrettyLog.Log("FixGasCap", "5");

                PC.Fluid = FuelContainer;

                PrettyLog.Log("FixGasCap", "6");

                Cap.layer = LayerMask.NameToLayer("Windows");

            }
            public static void FixIndecators(Part p, int Pos)
            {
                PrettyLog.Log("FixIndecators", "START");

                CarLight CL = p.Prefab.AddComponent<CarLight>();
                CL.MaterialIndex = 0;
                CL.LightRend = p.GetComponent<Renderer>();
                if (Pos == 1)
                {
                    CL.RightLight = true;

                }
                if (Pos == 2)
                {
                    CL.LeftLight = true;


                }
                PrettyLog.Log("FixIndecators", "END");
            }


            public static void AddHazzards(Part p)
            {
                // Wiper switch setup

                Switch NewSwitchH = p.Prefab.transform.Find("hazards").gameObject.AddComponent<Switch>();

                NewSwitchH.HazardLightSwitch = true;
                NewSwitchH.SwitchName = "Hazard lights";

                NewSwitchH.gameObject.layer = LayerMask.NameToLayer("OpenableParts");
            }
            public static void FixCluster(Part p) // REALLY messy.
            {
                //-
                // / CLUSTER LIGHTS
                //-

                // Create CarLight on cluster parent

                PrettyLog.DebugLog("G", "CL");


                CarLight CL = p.Prefab.AddComponent<CarLight>();

                CL.DontNeedBulb = true;
                CL.emissionColor = new Color(2f, 2f, 2f, 1f);
                CL.MaterialIndex = 1;
                CL.LightRend = p.Prefab.GetComponent<MeshRenderer>();
                CL.RunningLight = true;

                p.SetStandardShader();

                //-
                // / GAUGES
                //-

                // RPM Gauge
                // Prevent NWH from doing a stupid reset, seriously Why the in the fuck does it do this..

                PrettyLog.DebugLog("G", "RPM");


                p.Prefab.transform.Find("RPMgauge/Needle").transform.eulerAngles = new Vector3(344.904968f, 180f, 4.55000257f);

                //Create Gauges comps and reconstruct data.

                AnalogGauge RPMNeedleG = p.Prefab.transform.Find("RPMgauge").gameObject.AddComponent<AnalogGauge>();


                RPMNeedleG.startAngle = 10f;
                RPMNeedleG.endAngle = 220f;
                RPMNeedleG.maxValue = 8000f;

                PrettyLog.DebugLog("G", "OIL");

                //OIL Gauge
                // Prevent NWH from doing a stupid reset, seriously Why the in the fuck does it do this..

                p.Prefab.transform.Find("OILgauge/Needle").transform.eulerAngles = new Vector3(0f, 0f, 1.50051558f);

                //Create Gauges comps and reconstruct data.

                AnalogGauge OilNeedleG = p.Prefab.transform.Find("OILgauge").gameObject.AddComponent<AnalogGauge>();


                OilNeedleG.startAngle = 0f;
                OilNeedleG.endAngle = -52.96f;
                OilNeedleG.maxValue = 2500f; // Lower the value by more than 1/2 to increase the 'pressure'
                OilNeedleG.needleSmoothing = 0.9f; // Add a shit ton of smooth.

                OilGaugeFix OGF = p.Prefab.AddComponent<OilGaugeFix>();
                OGF.enabled = false;

                PrettyLog.DebugLog("G", "Speed");

                // Speed Gauge
                // Prevent NWH from doing a stupid reset, seriously Why the in the fuck does it do this..

                p.Prefab.transform.Find("Speedo/Needle").transform.eulerAngles = new Vector3(344.904999f, 180f, 346.299988f);

                AnalogGauge SpeedNeedleG = p.Prefab.transform.Find("Speedo").gameObject.AddComponent<AnalogGauge>();

                SpeedNeedleG.startAngle = -15f;
                SpeedNeedleG.endAngle = 259.200012f;
                SpeedNeedleG.maxValue = 150f;
                SpeedNeedleG.needleSmoothing = 0.082f;

                PrettyLog.DebugLog("G", "Fuel");

                // Fuel Gauge
                // Prevent NWH from doing a stupid reset, seriously Why the in the fuck does it do this..

                p.Prefab.transform.Find("Fuelgauge/Needle").transform.eulerAngles = new Vector3(2.56046246e-06f, -2.24833416e-11f, 1.50051558f);

                AnalogGauge FuelNeedleG = p.Prefab.transform.Find("Fuelgauge").gameObject.AddComponent<AnalogGauge>();

                FuelNeedleG.startAngle = 1.50051558f;
                FuelNeedleG.endAngle = -52.96f;
                FuelNeedleG.maxValue = 80f;

                PrettyLog.DebugLog("G", "Temp");

                // Temp Gauge
                // Prevent NWH from doing a stupid reset, seriously Why the in the fuck does it do this..

                p.Prefab.transform.Find("Tempgauge/Needle").transform.eulerAngles = new Vector3(0f, 0f, 1.50051558f);

                AnalogGauge TempNeedleG = p.Prefab.transform.Find("Tempgauge").gameObject.AddComponent<AnalogGauge>();

                TempNeedleG.startAngle = 1.50051558f;
                TempNeedleG.endAngle = -52.96f;
                TempNeedleG.maxValue = 80f;

                PrettyLog.DebugLog("G", "Alt");

                // Alt Gauge
                // Prevent NWH from doing a stupid reset, seriously Why the in the fuck does it do this..

                p.Prefab.transform.Find("Batgauge/Needle").transform.eulerAngles = new Vector3(0f, 0f, 1.50051558f);

                AnalogGauge AltNeedleG = p.Prefab.transform.Find("Batgauge").gameObject.AddComponent<AnalogGauge>();

                AltNeedleG.startAngle = 1.50051558f;
                AltNeedleG.endAngle = -52.96f;
                AltNeedleG.maxValue = 5f;


                PrettyLog.DebugLog("G", "SW");

                //-
                // / SWITCHES
                //-

                // Light switch setup

                Switch NewSwitchL = p.Prefab.transform.Find("LightButton").gameObject.AddComponent<Switch>();

                NewSwitchL.LightSwitch = true;
                NewSwitchL.SwitchName = "Lights";

                NewSwitchL.gameObject.layer = LayerMask.NameToLayer("OpenableParts");

                // Wiper switch setup

                Switch NewSwitchW = p.Prefab.transform.Find("WiperButton").gameObject.AddComponent<Switch>();

                NewSwitchW.WiperSwitch = true;
                NewSwitchW.SwitchName = "Wipers";

                NewSwitchW.gameObject.layer = LayerMask.NameToLayer("OpenableParts");

                // p.Prefab.gameObject.layer = LayerMask.NameToLayer("Windows");

            }
            public static void SetDetachablePart(String Part, DetachablePart DP)
            {

                switch (Part)
                {

                    case "Other":
                        PrettyLog.Log("RVPD", "11");
                        DP.angularDrag = 0.05f;
                        DP.breakForce = 25f;
                        DP.looseForce = -1f;
                        DP.mass = 0.1f;
                        DP.TargetbreakForce = 25f;
                        PrettyLog.Log("RVPD", "2");
                        break;

                    case "Bodypanel":
                        PrettyLog.Log("RVPD", "11");
                        DP.angularDrag = 0.05f;
                        DP.breakForce = 60f;
                        DP.looseForce = -1f;
                        DP.mass = 0.1f;
                        DP.TargetbreakForce = 60f;
                        PrettyLog.Log("RVPD", "2");
                        break;

                    case "Openable":
                        PrettyLog.Log("RVPD", "11");
                        DP.angularDrag = 0.05f;
                        DP.breakForce = 80f;
                        DP.looseForce = -1f;
                        DP.mass = 0.1f;
                        DP.TargetbreakForce = 80f;
                        PrettyLog.Log("RVPD", "2");
                        break;


                }

            }

            public static void MakeWindowRegs(int type, Part part) 
            { 

                switch (type) 
                {
                    case 1:

                        GameObject WindowRollerFL = part.Prefab.transform.Find("WindowRollerL").gameObject;
                        WindowLift WRLL = WindowRollerFL.AddComponent<WindowLift>();

                        WRLL.Window = part.Prefab.transform.Find("RRLX_70C_WindowML").gameObject;
                        WRLL.WindowOpen = part.Prefab.transform.Find("WindowOpen").gameObject;
                        WRLL.WindowClosed = part.Prefab.transform.Find("WindowClosed").gameObject;

                        WRLL.rotationspeed = 500f;

                        WRLL.HighestPosition = 0.535f;
                        WRLL.LowestPosition = -0.4816917f;

                        WindowRollerFL.layer = LayerMask.NameToLayer("OpenableParts");

                        break;

                    case 2:
                        GameObject WindowRollerFR = part.Prefab.transform.Find("WindowRollerR").gameObject;
                        WindowLift WRLR = WindowRollerFR.AddComponent<WindowLift>();

                        WRLR.Window = part.Prefab.transform.Find("RRLX_70C_WindowMR").gameObject;
                        WRLR.WindowOpen = part.Prefab.transform.Find("WindowOpen").gameObject;
                        WRLR.WindowClosed = part.Prefab.transform.Find("WindowClosed").gameObject;

                        WRLR.rotationspeed = 500f;

                        WRLR.HighestPosition = 0.535f;
                        WRLR.LowestPosition = -0.4816917f;

                        WindowRollerFR.layer = LayerMask.NameToLayer("OpenableParts");

                        break;

                    case 3:
                        GameObject WindowRollerRL = part.Prefab.transform.Find("WindowRollerL").gameObject;
                        WindowLift WRLL2 = WindowRollerRL.AddComponent<WindowLift>();

                        WRLL2.Window = part.Prefab.transform.Find("RRLX_70C_WindowRL").gameObject;
                        WRLL2.WindowOpen = part.Prefab.transform.Find("WindowOpen").gameObject;
                        WRLL2.WindowClosed = part.Prefab.transform.Find("WindowClosed").gameObject;

                        WRLL2.rotationspeed = 500f;

                        WRLL2.HighestPosition = 0.535f;
                        WRLL2.LowestPosition = -0.4816917f;

                        WindowRollerRL.layer = LayerMask.NameToLayer("OpenableParts");

                        break;

                    case 4:
                        GameObject WindowRollerRR = part.Prefab.transform.Find("WindowRollerR").gameObject;
                        WindowLift WRLL3 = WindowRollerRR.AddComponent<WindowLift>();

                        WRLL3.Window = part.Prefab.transform.Find("RRLX_70C_WindowRR").gameObject;
                        WRLL3.WindowOpen = part.Prefab.transform.Find("WindowOpen").gameObject;
                        WRLL3.WindowClosed = part.Prefab.transform.Find("WindowClosed").gameObject;

                        WRLL3.rotationspeed = 500f;

                        WRLL3.HighestPosition = 0.535f;
                        WRLL3.LowestPosition = -0.4816917f;

                        WindowRollerRR.layer = LayerMask.NameToLayer("OpenableParts");

                        break;
                }
            
            }
            internal class PartFoundationFixes
            {

            }


        }

        public class ReflectionFixes 
        {

            public static void TemplateFixes(GameObject objective) 
            { 
            
            


            
            }

            public static void FixFluids(GameObject objective) 
            {

                // All fluid fixes

                PrettyLog.Log("ReflectionFixes", "0");


                Transform engine = objective.transform.Find("EngineTranny/CylinderBlock/CylinderBlock");
                if (engine)
                {
                    //Dipstick Fix

                    PrettyLog.DebugLog("ReflectionFixes", "1");


                    engine.Find("Dipstick/Dipstick").gameObject.GetComponent<PickupCup>().DipstickOil = engine.Find("Dipstick/Dipstick/DipstickLevel").gameObject;

                    //OilFluidContainerHead Fix
                    PrettyLog.DebugLog("ReflectionFixes", "2");

                    engine.Find("CylinderHeadR07/CylinderHeadR07/CylinderHeadCoverR07/CylinderHeadCoverR07/OilReservuarCUP/OilReservuarCUP").GetComponent<PickupCup>().Fluid = engine.Find("CylinderHeadR07/CylinderHeadR07/CylinderHeadCoverR07/CylinderHeadCoverR07/OilFluidContainerHead").gameObject;

                    PrettyLog.Log("ReflectionFixes", "3");
                    
                    //OilReservuarSCREW Fix

                    engine.Find("OilPan07/OilPan07/OilReservuarSCREW/OilReservuarSCREW").GetComponent<PickupCup>().Fluid = engine.Find("OilPan07/OilPan07/OilFluidContainer").gameObject;

                    PrettyLog.DebugLog("ReflectionFixes", "4");
                    // OilPain Refill

                    FLUID OilpainFluid = engine.Find("OilPan07/OilPan07/OilFluidContainer").GetComponent<FLUID>();

                    OilpainFluid.Container = engine.Find("OilPan07/OilPan07/OilFluidContainer").GetComponent<FLUID>();
                    OilpainFluid.ContainerSize = 8;
                    OilpainFluid.Condition = 1;
                    OilpainFluid.FluidSize = 6;
                    OilpainFluid.MinFluidSize = 1.5f;

                    OilpainFluid.transform.parent.GetComponent<CarProperties>().FluidSize = 2f;
                    OilpainFluid.transform.parent.GetComponent<CarProperties>().FluidCondition = 1f;

                }

                Transform radiator = objective.transform.Find("RRLX_70_UpperRadiatorSupport/RRLX_70_UpperRadiatorSupport/RRLX_70_LowerRadiatorSupport/RRLX_70_LowerRadiatorSupport/Radiator07/Radiator07");
                if (radiator)
                {
                    PrettyLog.DebugLog("ReflectionFixes RAD", "1");
                    //CommonFixes.FixPart(radiator.gameObject, FixType.Radiator);
                    FLUID CoolantFluid = radiator.Find("CoolantFluidContainer").GetComponent<FLUID>();
                    PrettyLog.DebugLog("ReflectionFixes RAD", "2");
                    // Container fix
                    radiator.Find("CoolantFluidReservuarCUP/CoolantFluidReservuarCUP").gameObject.GetComponent<PickupCup>().Fluid = radiator.Find("CoolantFluidContainer").gameObject;

                    PrettyLog.DebugLog("ReflectionFixes RAD", "3");
                    // Visual fix
                    CoolantFluid.VisualFluid = radiator.Find("VisualFLuid").gameObject;
                    PrettyLog.Log("ReflectionFixes RAD", "4");
                    CoolantFluid.Container = radiator.Find("CoolantFluidContainer").GetComponent<FLUID>();
                    CoolantFluid.FluidSize = 6f;
                    CoolantFluid.Condition = 1f;
                    CoolantFluid.transform.parent.GetComponent<CarProperties>().FluidSize = 6f;
                    CoolantFluid.transform.parent.GetComponent<CarProperties>().FluidCondition = 1f;


                }

                Transform gasTank = objective.transform.Find("RRLX_70C_Floor_Trunk/RRLX_70C_Floor_Trunk/RRLX_70_Gas_Tank/RRLX_70_Gas_Tank");
                if (gasTank)
                {
                    CommonFixes.FixPart(gasTank.gameObject, FixType.FuelTank);

                    FLUID FuelTankComponent = gasTank.Find("FuelContainer").GetComponent<FLUID>();

                    FuelTankComponent.Condition = 1;
                    FuelTankComponent.FluidSize = 25f;
                    FuelTankComponent.transform.parent.GetComponent<CarProperties>().FluidSize = 25f;
                    FuelTankComponent.transform.parent.GetComponent<CarProperties>().FluidCondition = 1f;
                }

                Transform brakeFluidContainer = objective.transform.Find("RRLX_70_Firewall/RRLX_70_Firewall/BrakeMasterCylinder07/BrakeMasterCylinder07");
                if (brakeFluidContainer)
                {
                    CommonFixes.FixPart(brakeFluidContainer.gameObject, FixType.BrakeCylinder);
                    FLUID BrakeFluidComponent = brakeFluidContainer.Find("BrakeFluidContainer").GetComponent<FLUID>();

                    BrakeFluidComponent.FluidSize = BrakeFluidComponent.ContainerSize;
                    BrakeFluidComponent.Condition = 1f;
                    BrakeFluidComponent.transform.parent.GetComponent<CarProperties>().FluidSize = BrakeFluidComponent.ContainerSize - 0.01f;
                    BrakeFluidComponent.transform.parent.GetComponent<CarProperties>().FluidCondition = 1f;

                    //Temp utils Brakeline Fix

                    Transform BrakeLinePivotParent = objective.transform.Find("MainBrakeLine");

                    if (BrakeLinePivotParent) 
                    {
                        ReflectionFoundationFixes.BrakePivotFix(BrakeLinePivotParent);

                    }

                    Transform Dust = objective.transform.Find("DUST");
                    if (Dust) 
                    {
                        ReflectionFoundationFixes.DustFix(Dust);
                    }

                  //  ReflectionFixes.ReflectionFoundationFixes.FixSmoke(objective.transform);

                }

                PrettyLog.Log("ReflectionFixes", "Finished!");

            }

            public static void SuspensionShit(GameObject objective) // Rename.
            {
                Transform Crossmemeber = objective.transform.Find("FrontSusp/Crossmemmber07/Crossmemmber07");
                if (Crossmemeber) 
                {
                    // Front Control arms fix
                    Crossmemeber.Find("LowerControlArmFL07").gameObject.GetComponent<MyBoneSCR>().targetTransform = Crossmemeber.Find("HubCarrierFL07/pivotllow").transform;
                    Crossmemeber.Find("LowerControlArmFR07").gameObject.GetComponent<MyBoneSCR>().targetTransform = Crossmemeber.Find("HubCarrierFR07/pivotlow1").transform;

                }
                Transform RearAxle = objective.transform.Find("RearSusp/RearAxle07");
                if (RearAxle) 
                {
                    // Rear Axle Fix.
                    RearAxle.gameObject.GetComponent<MyBoneSCR>().targetTransformB = objective.transform.Find("RearSusp/NonROtVIsualANDrAxlePivot").transform;
                    // Handbrake Fix
                    RearAxle.Find("RearAxle07/HandbbrakeCable07").gameObject.GetComponent<MyBoneSCR>().LocalStrtetchTarget = RearAxle.Find("RearAxle07/DummyPivHbrak").transform;

                }
                /*Transform EngineTranny = objective.transform.Find("EngineTranny");
                if (EngineTranny) 
                { 
                    // Driveshaft fix.
                    EngineTranny.Find("DriveShaft07").gameObject.GetComponent<MyBoneSCR>().StrechToName = 

                }*/

            }
            public static void AmericanClassicFix(GameObject objective)
            {
                Transform AC = objective.transform.Find("V8_american_classic");
                if (AC)
                {
                    AC.GetComponent<Camera_NWH2_RES>().interior = AC.Find("V8_american_classic_interior").gameObject;
                    AC.GetComponent<Camera_NWH2_RES>().exterior = AC.Find("V8_american_classic").gameObject;
                }

            }

            public static void FixInnerFenderSuspension(GameObject objective, string Position) 
            {
                switch(Position) 
                {
                    case "R":

                        GameObject RightShock = objective.transform.Find("ShockAbsorberF07").gameObject;

                        if (RightShock) 
                        {
                            MyBoneSCR ShockGameObjectNewBoneObject = RightShock.AddComponent<MyBoneSCR>();
                            ShockGameObjectNewBoneObject._initDistance = 0.3852f;
                            ShockGameObjectNewBoneObject._initZScale = 1.0f;
                            ShockGameObjectNewBoneObject.KeepOnZ = false;
                            ShockGameObjectNewBoneObject.stretchToTarget = true;
                            ShockGameObjectNewBoneObject.StrechToName = "PivotspringR";
                            ShockGameObjectNewBoneObject.thisTransform = RightShock.transform;
                        }

                        GameObject RightControlArm = objective.transform.Find("UpperControlArmF07").gameObject;

                        if (RightControlArm)
                        {
                            MyBoneSCR UpperGameObjectNewBoneObject = RightControlArm.AddComponent<MyBoneSCR>();
                            UpperGameObjectNewBoneObject._initDistance = 0.2987f;
                            UpperGameObjectNewBoneObject._initZScale = 1.0f;
                            UpperGameObjectNewBoneObject.KeepOnZ = true;
                            UpperGameObjectNewBoneObject.stretchToTarget = true;
                            UpperGameObjectNewBoneObject.StrechToName = "pivotup1";
                            UpperGameObjectNewBoneObject.thisTransform = RightControlArm.transform;
                        }


                        break;

                    case "L":

                        GameObject LeftShock = objective.transform.Find("ShockAbsorberF07").gameObject;

                        if (LeftShock)
                        {
                            MyBoneSCR ShockGameObjectNewBoneObject = LeftShock.AddComponent<MyBoneSCR>();
                            ShockGameObjectNewBoneObject._initDistance = 0.3802f;
                            ShockGameObjectNewBoneObject._initZScale = 1.0f;
                            ShockGameObjectNewBoneObject.KeepOnZ = false;
                            ShockGameObjectNewBoneObject.stretchToTarget = true;
                            ShockGameObjectNewBoneObject.StrechToName = "PivotspringL";
                            ShockGameObjectNewBoneObject.thisTransform = LeftShock.transform;
                        }

                        GameObject LeftControlArm = objective.transform.Find("UpperControlArmF07").gameObject;

                        if (LeftControlArm)
                        {
                            MyBoneSCR UpperGameObjectNewBoneObject = LeftControlArm.AddComponent<MyBoneSCR>();
                            UpperGameObjectNewBoneObject._initDistance = 0.2991f;
                            UpperGameObjectNewBoneObject._initZScale = 1.0f;
                            UpperGameObjectNewBoneObject.KeepOnZ = true;
                            UpperGameObjectNewBoneObject.stretchToTarget = true;
                            UpperGameObjectNewBoneObject.StrechToName = "pivotup";
                            UpperGameObjectNewBoneObject.thisTransform = LeftControlArm.gameObject.transform;
                        }

                        break;

                }
            }
            public static void FixRearSuspension(GameObject objective, string Position)
            {
                switch (Position) 
                {
                    case "R":

                        GameObject RightLeafSpring = objective.transform.Find("Leafspring07").gameObject;

                        if (RightLeafSpring)
                        {
                            MyBoneSCR UpperGameObjectNewBoneObject = RightLeafSpring.AddComponent<MyBoneSCR>();
                            UpperGameObjectNewBoneObject._initDistance = 0.1254f;
                            UpperGameObjectNewBoneObject._initZScale = 1.0f;
                            UpperGameObjectNewBoneObject.KeepOnZ = false;
                            UpperGameObjectNewBoneObject.DontRotate = true;
                            UpperGameObjectNewBoneObject.stretchToTarget = true;
                            UpperGameObjectNewBoneObject.RotateAsTarget = true;
                            UpperGameObjectNewBoneObject.StrechToName = "pivotRSpring";
                            UpperGameObjectNewBoneObject.thisTransform = RightLeafSpring.transform;
                        }
                        break;

                    case "L":

                        GameObject LeftLeafSpring = objective.transform.Find("Leafspring07").gameObject;

                        if (LeftLeafSpring)
                        {
                            MyBoneSCR UpperGameObjectNewBoneObject = LeftLeafSpring.AddComponent<MyBoneSCR>();
                            UpperGameObjectNewBoneObject._initDistance = 0.126f;
                            UpperGameObjectNewBoneObject._initZScale = 1.0f;
                            UpperGameObjectNewBoneObject.KeepOnZ = false;
                            UpperGameObjectNewBoneObject.DontRotate = true;
                            UpperGameObjectNewBoneObject.stretchToTarget = true;
                            UpperGameObjectNewBoneObject.RotateAsTarget = true;
                            UpperGameObjectNewBoneObject.StrechToName = "pivotLSpring";
                            UpperGameObjectNewBoneObject.thisTransform = LeftLeafSpring.transform;
                        }
                        break;
                }

            }

            public static void FixWindowRegs(GameObject part, string Pos) 
            {

                switch (Pos) 
                {
                    case "FR":
                        PrettyLog.DebugLog(nameof(FixWindowRegs), "1");

                        WindowLift WRLR = part.GetComponent<WindowLift>(); // WHY IN THE FUCK DOES WRLR NEED TO BE ADDED WHEN IT SHOULD ALREADY FUCKING EXIST! FUCK THESE WINDOWS.

                        GameObject PartParent = part.transform.parent.gameObject;

                        WRLR.Window = PartParent.transform.Find("RRLX_70C_WindowMR").gameObject;
                        WRLR.WindowOpen = PartParent.transform.Find("WindowOpen").gameObject;
                        WRLR.WindowClosed = PartParent.transform.Find("WindowClosed").gameObject;

                        WRLR.rotationspeed = 500f;

                        WRLR.HighestPosition = 0.535f;
                        WRLR.LowestPosition = -0.4816917f;


                        part.layer = LayerMask.NameToLayer("OpenableParts");

                        break;

                    case "FL":

                        WindowLift WRLL = part.GetComponent<WindowLift>();

                        GameObject PartParent2 = part.transform.parent.gameObject;

                        WRLL.Window = PartParent2.transform.Find("RRLX_70C_WindowML").gameObject;
                        WRLL.WindowOpen = PartParent2.transform.Find("WindowOpen").gameObject;
                        WRLL.WindowClosed = PartParent2.transform.Find("WindowClosed").gameObject;

                        WRLL.rotationspeed = 500f;

                        WRLL.HighestPosition = 0.535f;
                        WRLL.LowestPosition = -0.4816917f;

                        part.layer = LayerMask.NameToLayer("OpenableParts");

                        break;

                    case "RL":

                        WindowLift WRLL2 = part.GetComponent<WindowLift>();

                        GameObject PartParent3 = part.transform.parent.gameObject;

                        WRLL2.Window = PartParent3.transform.Find("RRLX_70C_WindowRL").gameObject;
                        WRLL2.WindowOpen = PartParent3.transform.Find("WindowOpen").gameObject;
                        WRLL2.WindowClosed = PartParent3.transform.Find("WindowClosed").gameObject;

                        WRLL2.rotationspeed = 500f;

                        WRLL2.HighestPosition = 0.535f;
                        WRLL2.LowestPosition = -0.4816917f;

                        part.layer = LayerMask.NameToLayer("OpenableParts");

                        break;

                    case "RR":

                        WindowLift WRLL3 = part.GetComponent<WindowLift>();

                        GameObject PartParent4 = part.transform.parent.gameObject;

                        WRLL3.Window = PartParent4.transform.Find("RRLX_70C_WindowRR").gameObject;
                        WRLL3.WindowOpen = PartParent4.transform.Find("WindowOpen").gameObject;
                        WRLL3.WindowClosed = PartParent4.transform.Find("WindowClosed").gameObject;

                        WRLL3.rotationspeed = 500f;

                        WRLL3.HighestPosition = 0.535f;
                        WRLL3.LowestPosition = -0.4816917f;

                        part.layer = LayerMask.NameToLayer("OpenableParts");

                        break;
                }


            }

            public static void FixRearShocks(GameObject objective) // Yeah should prob fix that XD
            {
                GameObject ShockL = objective.transform.GetChild(0).gameObject;
                GameObject ShockR = objective.transform.GetChild(1).gameObject;

                MyBoneSCR ShockBoneL = ShockL.AddComponent<MyBoneSCR>();

                ShockBoneL._initDistance = 0.3657f;
                ShockBoneL._initZScale = 1f;
                ShockBoneL.StrechToName = "pivotLshock";
                ShockBoneL.stretchToTarget = true;
                ShockBoneL.thisTransform = ShockBoneL.transform;
                ShockBoneL.LocalStrtetchTarget = ShockL.transform.GetChild(0);
                ShockBoneL.FaceOpposite = true;


                MyBoneSCR ShockBoneR = ShockR.AddComponent<MyBoneSCR>();

                ShockBoneR._initDistance = 0.3657f;
                ShockBoneR._initZScale = 1f;
                ShockBoneR.StrechToName = "pivotRshock";
                ShockBoneR.stretchToTarget = true;
                ShockBoneR.thisTransform = ShockR.transform;
                ShockBoneR.LocalStrtetchTarget = ShockR.transform.GetChild(0);
                ShockBoneR.FaceOpposite = true;

            }

            internal class ReflectionFoundationFixes
            {
                public static void DustFix(Transform Dust) // Maybe no longer required.
                {
                    Dust.position = new Vector3 (142.4358f, 62.08f, 551.7893f);
                    Dust.localPosition = new Vector3(-0.0001f, 0f, 0f);
                }
                public static void BrakePivotFix(Transform BrakeLinePivotParent) // Waiting for utils update to fix this, then Remove.
                {
                    //RR
                    BrakeLinePivotParent.Find("RRbrakePivot").position = new Vector3(141.863f, 62.4733f, 557.3492f);
                    BrakeLinePivotParent.Find("RRbrakePivot").localPosition = new Vector3(0.9254f, -0.3114f, -2.7555f);
                    BrakeLinePivotParent.Find("RRbrakePivot").rotation = new Quaternion(51.2252f, 357.6768f, 266.8861f, 0);


                    //RL
                    BrakeLinePivotParent.Find("RLbrakePivot").position = new Vector3(138.1359f, 62.478f, 557.946f);
                    BrakeLinePivotParent.Find("RLbrakePivot").localPosition = new Vector3(-0.0853f, -0.307f, -2.7557f);
                    BrakeLinePivotParent.Find("RLbrakePivot").rotation = new Quaternion(308.7748f, 357.6768f, 92.8439f, 0);


                    //FR
                    BrakeLinePivotParent.Find("FRbrakePivot").position = new Vector3(139.9418f, 62.4781f, 560.3953f);
                    BrakeLinePivotParent.Find("FRbrakePivot").localPosition = new Vector3(0.9246f, -0.3065f, 0.115f);
                    BrakeLinePivotParent.Find("FRbrakePivot").rotation = new Quaternion(308.7748f, 2.3232f, 267.1561f, 0);


                    //FL
                    BrakeLinePivotParent.Find("FLbrakePivot").position = new Vector3(138.974f, 62.4784f, 560.6915f);
                    BrakeLinePivotParent.Find("FLbrakePivot").localPosition = new Vector3(-0.0875f, -0.3065f, 0.115f);
                    BrakeLinePivotParent.Find("FLbrakePivot").rotation = new Quaternion(308.7748f, 357.6768f, 92.8439f, 0);

                }
                public static void FixChildDMG(MainCarProperties CarObject) 
                {
                    CarProperties[] AllCP = CarObject.gameObject.GetComponentsInChildren<CarProperties>();

                    foreach (CarProperties CP in AllCP) 
                    {
                        if (CP.ChildDamag != null & CP.SinglePart == true) 
                        {
                            PrettyLog.Log(nameof(FixChildDMG), CP.ChildDamag.name);
                            try 
                            {
                                CP.ChildDamag = CP.transform.Find(CP.ChildDamag.name).GetComponent<CarProperties>();
                            }
                            catch (Exception ex) 
                            {
                                PrettyLog.Error(nameof(FixChildDMG), "Tried setting : " + CP.ChildDamag.name + "as a child dmg, But failed. Execption : " + ex);
                            }
                        }
                    }
                }
            }
        }
    }
}

