using Den.Tools;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering;

namespace RR_Coronet.Dependencys.Autoloader
{
    internal class LessShityCatalogHandler
    {
        public static List<string> bannedState = new List<string>();
        public static List<string> CatchList = new List<string>();
        public static void Setup() 
        {
            bannedState.Add("BatteryTray07");
            bannedState.Add("Cluster07");
            bannedState.Add("TrimFL07");
            bannedState.Add("TrimFR07");
            bannedState.Add("TrimRR07");
            bannedState.Add("TrimRL07");
            bannedState.Add("WindowLiftFRC07");
            bannedState.Add("WindowLiftFLC07");
            bannedState.Add("GasTank07");
            bannedState.Add("BumperF07");
            bannedState.Add("BumperR07");
            bannedState.Add("SteeringColumn07");
            bannedState.Add("TransmissionSupport07");
            bannedState.Add("HeadlightCase06");
            bannedState.Add("RoofRack06");
            bannedState.Add("TaillightL07");
            bannedState.Add("TaillightR07");
            bannedState.Add("SteeringWheel06");
            bannedState.Add("Mirror07");
            bannedState.Add("MirrorL07");
            bannedState.Add("MirrorR07");


            CatchList.Add("SeatFR09");
            CatchList.Add("SeatFL09");
            CatchList.Add("Mirror06");
            // CatchList.Add("SunVisorR07");
            // CatchList.Add("SunVisorL07");

            CatalogMassUpdate.AddCarChanges(LessShityCatalogHandler.LoopFunction, "70 Coronet");

        }
        public static bool LoopFunction(Partinfo pie) 
        {
            if (pie.FitsToCar.Contains("Chad")) // Gimmie chad parts
            {
                if (pie.Suspension == true || pie.Engine == true || pie.Accessories == true || pie.Brakes == true) // Gimmie ONLY chad suspension, Engine, Accessories, and brakes.
                {
                    if (bannedState.Contains(pie.gameObject.name) || pie.FitsToCar.Contains("f100")) // If you have a part that is in my list, Then lets return whatever the list has.
                    {
                        return false;
                    }
                    else // If you dont, You're prob not banned, or i forgot to add you, so you're allowed to exist
                    {
                        return true;
                    }
                }
                else // You have body panels, Me no want
                {
                    return false;
                }
            }
            if (CatchList.Contains(pie.gameObject.name)) // Get any parts that are not limted to the chad.
            {
                return true;
            }
            else // You dont have relevent parts, i dont want you.
            {
                return false;
            }
        }
    }
}
