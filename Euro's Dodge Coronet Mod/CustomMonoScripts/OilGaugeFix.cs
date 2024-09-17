using NWH.VehiclePhysics2;
using NWH.VehiclePhysics2.Powertrain;
using NWH.VehiclePhysics2.VehicleGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RR_Coronet.CustomMonoScripts
{
    public class OilGaugeFix : MonoBehaviour
    {
        public AnalogGauge OilGuage;
        public VehicleController vc;
        public bool Casted = false;

        bool HasComps = false;

        public void OnEnable()
        {
            OilGuage = this.gameObject.transform.Find("OILgauge").gameObject.GetComponent<AnalogGauge>();

            vc = this.gameObject.GetComponentInParent<MainCarProperties>().exp;

            PrettyLog.DebugLog("LOCATION", this.gameObject.transform.name);

            if (OilGuage) 
            {
                HasComps = true;
            }
            if (!Casted) 
            {
                this.enabled = false;
            }
        }

        public void Update()
        {
            if (HasComps) 
            {
                OilGuage.Value = vc.powertrain.engine.RPM;
            }
        }

    }
}
