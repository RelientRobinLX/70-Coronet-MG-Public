using SimplePartLoader;
using System;

namespace RR_Coronet.Dependencys
{

    // Compatability layer.

    internal class GamePaintingCompat
    {
        internal static void SetupChromePart(Part part)
        {
            if (ModMain.GetURPModeState())
            {
                SimplePartLoader.GamePainting.SetupChromePart(part);
            }
            else 
            {
                return;
            }
        }

        internal static void SetupGlassPart(Part part, bool v)
        {
            if (ModMain.GetURPModeState())
            {
                SimplePartLoader.GamePainting.SetupGlassPart(part, v);
            }
            else
            {
                return;
            }
        }

        internal static void SetupPart(Part part, PartPaintSetup partPaintSetup)
        {
            if (ModMain.GetURPModeState())
            {
                SimplePartLoader.GamePainting.SetupPart(part);
            }
            else
            {
                return;
            }
        }

        internal class PartPaintSetup
        {
            public PartPaintSetup()
            {
            }

            public bool MetallicRustDust { get; set; }
            public bool ColorMap { get; set; }
            public bool ClearCoat { get; set; }
        }
    }
}