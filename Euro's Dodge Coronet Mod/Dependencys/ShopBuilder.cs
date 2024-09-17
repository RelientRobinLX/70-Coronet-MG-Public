using FluffyUnderware.DevTools.Extensions;
using RVP;
using SimplePartLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RR_Coronet.Dependencys;
using UnityEngine;

namespace RR_Coronet
{
    internal class ShopBuilder
    {
        public static void BuildBuyStand(GameObject Plate) 
        {
            Material[] USPlateStandRendererMAT = new Material[8];

            GameObject PlateStand = GameObject.Find("UnloadablesMain/shop/Shop/LicensePlate");

            GameObject PlateCatalogPlane = GameObject.Find("UnloadablesMain/shop/Shop/Plane");

            GameObject USPlateStand = PlateStand.DuplicateGameObject(PlateStand.transform.parent.transform);

            GameObject USPlateCatalogPlane = PlateCatalogPlane.DuplicateGameObject(USPlateStand.transform);

            USPlateStand.name = "USPlate";

            USPlateStand.GetComponent<MeshFilter>().mesh = Plate.GetComponent<MeshFilter>().mesh;

            MeshRenderer USPlateStandRenderer = USPlateStand.GetComponent<MeshRenderer>();

            MeshRenderer PlateStandRenderer = PlateStand.GetComponent<MeshRenderer>();

            MeshRenderer USPlateRenderer = Plate.GetComponent<MeshRenderer>();
            USPlateStandRenderer.materials = USPlateRenderer.materials;

            USPlateStandRendererMAT = PlateStandRenderer.materials;
            USPlateStandRendererMAT[0] = USPlateRenderer.materials[0];
            USPlateStandRendererMAT[1] = USPlateRenderer.materials[1];

            USPlateStandRenderer.materials = USPlateStandRendererMAT;

            USPlateStand.transform.eulerAngles = new Vector3(16.2295f, -82.5F, 0.6f); 

            USPlateStand.transform.localPosition = new Vector3(8.2536f, 1.198f, -10.768f);

            USPlateCatalogPlane.name = "CatalogBackground";

            CustomPLates UsPlateComp = USPlateStand.GetComponent<CustomPLates>();

            UsPlateComp.Item = Plate;
        }

        public static void AddMoparWheelsToShop() 
        {
            GameObject Shop = GameObject.Find("UnloadablesMain/shop/Shop/SHOPITEMS/");

            Part Wheels = Autoloader.LoadedPrefabs["RRLX_70_Steel1_15"];

            GameObject MoparWheelsShopItem = GameObject.Instantiate(Wheels.Prefab, Shop.transform);

            GameObject.Destroy(MoparWheelsShopItem.GetComponent<CarProperties>());
            GameObject.Destroy(MoparWheelsShopItem.GetComponent<Partinfo>());
            GameObject.Destroy(MoparWheelsShopItem.GetComponent<MeshCollider>());

            MoparWheelsShopItem.name = "Rim M R15";
            MoparWheelsShopItem.layer = LayerMask.NameToLayer("Items");
            MoparWheelsShopItem.tag = "Item";

            MoparWheelsShopItem.AddComponent<BoxCollider>();

            SaleItem MWSIS = MoparWheelsShopItem.AddComponent<SaleItem>();

            MWSIS.Item = Wheels.Prefab;
            MWSIS.Price = Wheels.PartInfo.price;
            MWSIS.SpawnSpot = GameObject.Find("UnloadablesMain/shop/Shop/ItemSpawn");

            MoparWheelsShopItem.transform.localPosition = new Vector3(9.234f, 0.6f, -13.742f);
        }

    }
}
