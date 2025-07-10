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

            MoparWheelsShopItem.name = "Steel M R15";
            MoparWheelsShopItem.layer = LayerMask.NameToLayer("Items");
            MoparWheelsShopItem.tag = "Item";

            MoparWheelsShopItem.AddComponent<BoxCollider>();

            SaleItem MWSIS = MoparWheelsShopItem.AddComponent<SaleItem>();

            MWSIS.Item = Wheels.Prefab;
            MWSIS.Price = Wheels.PartInfo.price;
            MWSIS.SpawnSpot = GameObject.Find("UnloadablesMain/shop/Shop/ItemSpawn");

            MoparWheelsShopItem.transform.localPosition = new Vector3(9.234f, 0.6f, -14.342f);


            Part WheelsWide = Autoloader.LoadedPrefabs["RRLX_70_Steel1W_15"];

            GameObject MoparWheelsWideShopItem = GameObject.Instantiate(WheelsWide.Prefab, Shop.transform);

            GameObject.Destroy(MoparWheelsWideShopItem.GetComponent<CarProperties>());
            GameObject.Destroy(MoparWheelsWideShopItem.GetComponent<Partinfo>());
            GameObject.Destroy(MoparWheelsWideShopItem.GetComponent<MeshCollider>());

            MoparWheelsWideShopItem.name = "Steel M Wide R15";
            MoparWheelsWideShopItem.layer = LayerMask.NameToLayer("Items");
            MoparWheelsWideShopItem.tag = "Item";

            MoparWheelsWideShopItem.AddComponent<BoxCollider>();

            SaleItem MWSIS2 = MoparWheelsWideShopItem.AddComponent<SaleItem>();

            MWSIS2.Item = WheelsWide.Prefab;
            MWSIS2.Price = WheelsWide.PartInfo.price;
            MWSIS2.SpawnSpot = GameObject.Find("UnloadablesMain/shop/Shop/ItemSpawn");

            MoparWheelsWideShopItem.transform.localPosition = new Vector3(9.18f, 0.6f, -13.742f);


            Part WheelCaps = Autoloader.LoadedPrefabs["RRLX_70_HubCap1"];

            GameObject WheelCapsShopItem = GameObject.Instantiate(WheelCaps.Prefab, Shop.transform);

            GameObject.Destroy(WheelCapsShopItem.GetComponent<CarProperties>());
            GameObject.Destroy(WheelCapsShopItem.GetComponent<Partinfo>());
            GameObject.Destroy(WheelCapsShopItem.GetComponent<MeshCollider>());

            WheelCapsShopItem.name = "Coronet Basic Hubcaps";
            WheelCapsShopItem.layer = LayerMask.NameToLayer("Items");
            WheelCapsShopItem.tag = "Item";

            WheelCapsShopItem.AddComponent<BoxCollider>();

            SaleItem MWSIS3 = WheelCapsShopItem.AddComponent<SaleItem>();

            MWSIS3.Item = WheelCaps.Prefab;
            MWSIS3.Price = WheelCaps.PartInfo.price;
            MWSIS3.SpawnSpot = GameObject.Find("UnloadablesMain/shop/Shop/ItemSpawn");

            WheelCapsShopItem.transform.localPosition = new Vector3(8.5539f, 1.609f, -14.815f);
            WheelCapsShopItem.transform.localRotation.Set(0f, 90f, 0f, 0f);

            Part WheelCaps2 = Autoloader.LoadedPrefabs["RRLX_70_HubCap2"];

            GameObject WheelCaps2ShopItem = GameObject.Instantiate(WheelCaps2.Prefab, Shop.transform);

            GameObject.Destroy(WheelCaps2ShopItem.GetComponent<CarProperties>());
            GameObject.Destroy(WheelCaps2ShopItem.GetComponent<Partinfo>());
            GameObject.Destroy(WheelCaps2ShopItem.GetComponent<MeshCollider>());

            WheelCaps2ShopItem.name = "Coronet 500 Hubcaps";
            WheelCaps2ShopItem.layer = LayerMask.NameToLayer("Items");
            WheelCaps2ShopItem.tag = "Item";

            WheelCaps2ShopItem.AddComponent<BoxCollider>();

            SaleItem MWSIS4 = WheelCaps2ShopItem.AddComponent<SaleItem>();

            MWSIS4.Item = WheelCaps2.Prefab;
            MWSIS4.Price = WheelCaps2.PartInfo.price;
            MWSIS4.SpawnSpot = GameObject.Find("UnloadablesMain/shop/Shop/ItemSpawn");

            WheelCaps2ShopItem.transform.localPosition = new Vector3(8.9539f, 1.609f, -14.815f);
            WheelCaps2ShopItem.transform.localRotation.Set(0f, 90f, 0f, 0f);
        }

    }
}
