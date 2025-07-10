using GleyTrafficSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static UnityEngine.PostProcessing.AntialiasingModel;

namespace RR_Coronet
{
    internal class PrettyLog

    {        
        public static void Log(string Method, string Message)
        {
            String ModLogName = " [Reliant Robin's Coronet Mod] ";
            string MethodString = "[IN : " + Method + "] : ";
            Debug.Log(ModLogName + MethodString + Message);
        }

        public static void Warn(string Method, string Message)
        {
            String ModLogName = " [WARNING] [Reliant Robin's Coronet Mod] ";
            string MethodString = "[IN : " + Method + "] : ";
            Debug.LogWarning(ModLogName + MethodString + Message);
        }

        public static void Error(string Method, string Message)
        {
            String ModLogName = " [ERROR] [Reliant Robin's Coronet Mod] ";
            string MethodString = "[IN : " + Method + "] : ";
            Debug.LogError(ModLogName + MethodString + Message);
        }

        public static void DebugLog(string Method, string Message)
        {
   
            if (ModMain.DebugMode.Checked == true)
            {
                String ModLogName = " [DEBUG] [Reliant Robin's Coronet Mod] ";
                string MethodString = "[IN : " + Method + "] : ";
                Debug.Log(ModLogName + MethodString + Message);
            }
           
        }
    }
}
