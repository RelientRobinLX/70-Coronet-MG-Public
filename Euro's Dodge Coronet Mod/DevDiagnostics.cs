using SimplePartLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR_Coronet
{
    internal class DevDiagnostics
    {
        public static void CheckWeldCounts(Part newpart) 
        {
            WeldCut[] welds = newpart.Prefab.GetComponentsInChildren<WeldCut>();

            if (welds.Count() > 0)
            {
                if (newpart.PartInfo.attachedwelds > 0)
                {
                    if(newpart.PartInfo.attachedwelds != welds.Count())
                    {
                        PrettyLog.Error(nameof(CheckWeldCounts), "PART DOES NOT CONTAIN CORRECT WELD COUNT : " + newpart.CarProps.PrefabName);
                    }
                    if (newpart.PartInfo.attachedwelds > welds.Count()) 
                    {
                        PrettyLog.Error(nameof(CheckWeldCounts), "PART HAS HIGHER FIXED COUNT THAN REAL COUNT! : " + newpart.CarProps.PrefabName);
                    }
                    else
                    {
                        PrettyLog.Log(nameof(CheckWeldCounts), "PART PASSED : " + newpart.CarProps.PrefabName);
                    }
                }
                else
                {
                    PrettyLog.Error(nameof(CheckWeldCounts), "PART HAS NO WELD COUNT : " + newpart.CarProps.PrefabName);
                }
            }
        }

        public static void CheckWelded(Part newpart)
        {
            WeldCut[] welds = newpart.Prefab.GetComponentsInChildren<WeldCut>();

            int weldsBroken = 0;

            if (welds.Count() > 0)
            {
               foreach (WeldCut weld in welds) 
               {
                    if (weld.welded == false)
                    {
                        weldsBroken += 1;
                        PrettyLog.Log(nameof(CheckWelded), "LIAR!");
                    }
               }

                if (weldsBroken > 0)
                {
                    PrettyLog.Error(nameof(CheckWelded), "WELDS IN PART :" + newpart.CarProps.PrefabName + " HAVE WELDED SET TO FALSE! " + weldsBroken);
                }
                else 
                {
                    PrettyLog.Log(nameof(CheckWelded), "WELDS IN PART :" + newpart.CarProps.PrefabName + " PASSED " + weldsBroken);

                }
            }
        }

        public static void CheckWeldedNames(Part newpart, string Objective)
        {
            WeldCut[] welds = newpart.Prefab.GetComponentsInChildren<WeldCut>();

            if (welds.Count() > 0)
            {
                foreach (WeldCut weld in welds)
                {
                    if (weld.otherobjectName == Objective) 
                    {
                        PrettyLog.Log(nameof(CheckWeldedNames), weld.transform.parent.name +  " HAS WELD RR");
                    }
                }
            }
        }
    }
}
