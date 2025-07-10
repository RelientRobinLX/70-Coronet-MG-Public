using Den.Tools;
using FluffyUnderware.Curvy.Shapes;
using HarmonyLib;
using SimplePartLoader.CarGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace RR_Coronet.Dependencys.Autoloader
{
    internal class CatalogHandler
    {
        private static List<string> bannedState = new List<string>();
        private static List<string> CatchList = new List<string>();
        private static Dictionary<string, GameObject> AddedParts = new Dictionary<string, GameObject>();

        private static Dictionary<string, Partinfo> EngineParts = new Dictionary<string, Partinfo>();
        private static Dictionary<string, Partinfo> SuspParts = new Dictionary<string, Partinfo>();
        private static Dictionary<string, Partinfo> BrakeParts = new Dictionary<string, Partinfo>();
        private static Dictionary<string, Partinfo> AccParts = new Dictionary<string, Partinfo>();


        public static void Setup() 
        {
            PrettyLog.Log(nameof(CatalogHandler), "Setup Start");

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
            bannedState.Add("TaillightL07");
            bannedState.Add("TaillightR07");
            bannedState.Add("SteeringWheel06");
            bannedState.Add("SteeringWheel07");
            bannedState.Add("Mirror07");
            bannedState.Add("MirrorL07");
            bannedState.Add("MirrorR07");
            bannedState.Add("ExhaustL07");
            bannedState.Add("ExhaustR07");

            PrettyLog.Log(nameof(CatalogHandler), "Banned list complete");

            CatchList.Add("SeatFR09");
            CatchList.Add("SeatFL09");
            CatchList.Add("Mirror06");
            CatchList.Add("Mirror06CHR");
            CatchList.Add("SunVisorR07");
            CatchList.Add("SunVisorL07");
            CatchList.Add("BrakePadsFront06"); 
            CatchList.Add("BrakePadsRear06");

            PrettyLog.Log(nameof(CatalogHandler), "Catch list complete");

            PrettyLog.Log(nameof(CatalogHandler), "init loop function");

            CatalogMassUpdate.AddCarChanges(CatalogHandler.LoopFunction, "70 Coronet");

            AddFitsToCarViaCachedResources("BrakePadsFront06");

            AddFitsToCarViaCachedResources("BrakePadsRear06");

            AddFitsToCarViaCachedResources("LightBulbsH4");

            AddFitsToCarViaCachedResources("LightBulbs");

            PrettyLog.Log(nameof(CatalogHandler), "printing final list.");

            PrintFinalParts();
        }
        public static bool LoopFunction(Partinfo pie) 
        {
            if (CatchList.Contains(pie.gameObject.name) && bannedState.Contains(pie.gameObject.name) == false) // Get any parts that are not limted to the chad.
            {
                AddedParts.Add(pie.gameObject.name, pie.gameObject);
                AddToFitsToCar(pie, "69 Coronet");
                return true;
            }
            if(pie.Rim || pie.Tire)
            {
                return false; // Removes tags for tires and rims.
            }
            if (pie.FitsToCar.Contains("Chad")) // Gimmie chad parts
            {
                if (pie.Suspension == true || pie.Engine == true || pie.Accessories == true || pie.Brakes == true) // Gimmie ONLY chad suspension, Engine, Accessories, and brakes.
                {
                    if (bannedState.Contains(pie.gameObject.name) || pie.FitsToCar.Contains("f100") || AddedParts.Keys.Contains(pie.gameObject.name) || pie.FitsToCar.Contains("70 Coronet")) // If you have a part that is in my list, Then lets return whatever the list has.
                    {
                        return false;
                    }
                    else // If you dont, You're prob not banned, or i forgot to add you, so you're allowed to exist
                    {
                        AddedParts.Add(pie.gameObject.name, pie.gameObject);
                        AddToFitsToCar(pie,"69 Coronet");
                        return true;
                    }
                }
                else // You have body panels, Me no want
                {
                    return false;
                }
            }
            else // You dont have relevent parts, i dont want you.
            {
                return false;
            }
        }
        public static void PrintFinalParts()
        {
            foreach (string key in AddedParts.Keys) 
            {
                PrettyLog.Log(nameof(CatalogHandler),"ADDED PARTS LIST CONTAINS : " + key);
            }
        }

        private static void CheckType(Partinfo PI) 
        {
            if(PI.Engine == true) 
            {
                EngineParts.Add(PI.gameObject.name, PI);
                return;
            }
            if (PI.Suspension == true)
            {
                SuspParts.Add(PI.gameObject.name, PI);
                return;
            }
            if (PI.Brakes == true)
            {
                BrakeParts.Add(PI.gameObject.name, PI);
                return;
            }
            if (PI.Accessories == true)
            {
                AccParts.Add(PI.gameObject.name, PI);
                return;
            }
        }

        private static void AddToFitsToCar(Partinfo pi, string StringToAdd) 
        {
            PrettyLog.Log(nameof(AddToFitsToCar), "Adding : " + StringToAdd +  " to :" + pi.gameObject.name + ", Getting Last Index.");
            int LastElement = pi.FitsToCar.Length;
            int NewElement = pi.FitsToCar.Length + 1;

            PrettyLog.Log(nameof(AddToFitsToCar), "Got last index : " + LastElement +", Attempting to resize array.");
            Array.Resize(ref pi.FitsToCar, pi.FitsToCar.Length + 1);
            PrettyLog.Log(nameof(AddToFitsToCar), "Sucessfully resized array to : " + pi.FitsToCar.Length + ", Attempting to insert new FitsToCarString");
            pi.FitsToCar[NewElement - 1] = StringToAdd;
            PrettyLog.Log(nameof(AddToFitsToCar), "Sucessfully Added " + StringToAdd + "to FitsToCar Array of " + pi.gameObject.name);
        }

        private static void AddFitsToCarViaCachedResources(string path)  //BrakePadsRear06
        {
            GameObject GoToChange = cachedResources.Load<GameObject>(path);

            if (GoToChange)
            {
                Partinfo PiToModify = GoToChange.GetComponent<Partinfo>();

                if (PiToModify == null) 
                {
                    PrettyLog.Warn(nameof(AddFitsToCarViaCachedResources), "Failed to find PartInfo.");
                    return;
                }

                int LastElement = PiToModify.FitsToCar.Length;
                int NewElement = PiToModify.FitsToCar.Length + 2;

                Array.Resize(ref PiToModify.FitsToCar, PiToModify.FitsToCar.Length + 2);

                PiToModify.FitsToCar[NewElement - 1] = "70 Coronet";
                PiToModify.FitsToCar[NewElement - 2] = "69 Coronet";

            }
        }

    }
}
