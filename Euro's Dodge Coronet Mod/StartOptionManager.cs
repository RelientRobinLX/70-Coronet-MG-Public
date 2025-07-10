using FluffyUnderware.DevTools.Extensions;
using SimplePartLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RR_Coronet
{
    internal class StartOptionManager
    {

        private static AssetBundle StartOptionBundle;
        private static Dictionary<string, StartOption> StartOptionParts = new Dictionary<string, StartOption>();
        private static Dictionary<string, GameObject> ExistingStartOptionParts = new Dictionary<string, GameObject>();

        public static void SetStartOptionBundle(AssetBundle bundle)
        {
            if (!StartOptionBundle)
            {
                StartOptionBundle = bundle;
            }
        }

        public static void LoadOption(string LoadString, string PartToCopy) 
        {
            StartOption PartToLoad = null;

            try
            {
                PartToLoad = ModMain.ThisMod.LoadStartOption(StartOptionBundle, LoadString, PartToCopy);
            }
            catch (Exception ex) 
            {
                PrettyLog.Error(nameof(StartOptionManager), "Failed to load : " + LoadString + " Exception : " + ex.ToString());
            }

            if(PartToLoad != null) 
            {
                StartOptionParts.Add(PartToLoad.Prefab.name, PartToLoad);

                foreach (Transform t in PartToLoad.Prefab.transform) 
                {
                    PartToLoad.AddException(t.name, t.GetChild(0).name);
                    GameObject.Destroy(t.gameObject);
                }
            }
        }
        public static void LoadExistingOption(string LoadName, string ListName) 
        {
            GameObject GOtoLoad = (GameObject)cachedResources.Load(LoadName);

            if (GOtoLoad.transform)
            {
                ExistingStartOptionParts.Add(ListName, GOtoLoad);
            }
            else 
            {
                PrettyLog.Warn(nameof(GetExistingStartOption), "Failed to fetch : " + LoadName + " From cachedResources.");
            }
        }

        public static GameObject GetExistingStartOption(string Name)
        {
            GameObject GOtoLoad = ExistingStartOptionParts[Name];

            if (GOtoLoad.transform)
            {
                return GOtoLoad;
            }
            else
            {
                PrettyLog.Warn(nameof(GetExistingStartOption) , "Failed to fetch : " + Name + " From ExistingStartOptions.");
                return new GameObject();
            }
        }
        public static StartOption GetStartOption(string key) 
        {
            StartOption SO;

            SO = StartOptionParts[key];

            return SO;
        }

    }
}
