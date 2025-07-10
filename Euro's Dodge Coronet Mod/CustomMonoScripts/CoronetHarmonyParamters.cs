using Den.Tools;
using NWH.VehiclePhysics2;
using NWH.VehiclePhysics2.VehicleGUI;
using PaintIn3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RR_Coronet.CustomMonoScripts
{
    public class CoronetHarmonyParamters : MonoBehaviour
    {
        public VehicleController vc;

        public MainCarProperties MCP;

        public Vector3 RarityRate = new Vector3(0, 0, 0);

        public string CarInternalName;

        private string VIN;

        private CarList list;
        public void Start() 
        {
            //VIN = setVIN();

            list = GameObject.Find("SceneManager/CarsParent").GetComponent<CarList>();

            if (list)
            {
                base.gameObject.GetComponent<MainCarProperties>().PREFAB = list.Cars[list.Cars.Find(x => x.name == CarInternalName)].gameObject;
            }
            else 
            {
                PrettyLog.Error(nameof(this.name), "Failed to find CarList for MCP Prefab Asignment.");
            }
        }

        public string getVin() 
        {
            return VIN;
        }

        private void CheckColorForCstripe() 
        {
            double Deadzone = 0.5;

            if (MCP.Color.gamma.r <= Deadzone || MCP.Color.gamma.b <= Deadzone || MCP.Color.gamma.g <= Deadzone)
            {
            }


        }
        private string setVIN() 
        {
            string VinString = "";
            string Engine, Interior, BColor;

            switch(MCP.EngineBlock.PrefabName)
            {
                case "CylinderBlockV8":
                    Engine = "E8";
                    if (CarInternalName == "70 Coronet RT") 
                    {
                        Engine = "E86";
                    }
                    break;
                case "CylinderBlockI6":
                    Engine = "E6";
                    break;
            }
            switch (MCP.OriginalInterior) 
            {
                case 1:
                    Interior = "IBK";
                    break;
                case 2:
                    Interior = "IBL";
                    break;
            }


           return VinString;
        }

    }
}
