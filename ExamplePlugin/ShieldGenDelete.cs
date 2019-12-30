using BepInEx;
using RoR2;
using UnityEngine;
using System;
using System.Globalization;
using BepInEx.Configuration;
using ItemCatalog = On.RoR2.ItemCatalog;


namespace ExamplePlugin
{

    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Pickleses.ShieldGenDelete", "ShieldGenDelete", "1.0")]



    public class ShieldGenDelete : BaseUnityPlugin
    {

        public void Awake()
        { 
            ItemCatalog.RegisterItem += OnRegisterItem;
        }

        private void OnRegisterItem(ItemCatalog.orig_RegisterItem orig, ItemIndex itemIndex, ItemDef itemDef)
        {
            if (itemDef == null || itemDef.tier == ItemTier.NoTier)
            {
                orig.Invoke(itemIndex, itemDef);
                return;
            }

            ItemTier currTier = itemDef.tier;
            int defTier;

            switch (currTier)
            {
                case ItemTier.Tier1:
                    {
                        defTier = 1;
                        break;
                    }

                case ItemTier.Tier2:
                    {
                        defTier = 2;
                        break;
                    }

                case ItemTier.Tier3:
                    {
                        defTier = 3;
                        break;
                    }

                case ItemTier.Lunar:
                    {
                        defTier = 4;
                        break;
                    }

                case ItemTier.Boss:
                    {
                        defTier = 5;
                        break;
                    }

                default:
                    {
                        defTier = 0;
                        break;
                    }
            }



            string itemName = itemDef.name;
            
            if(itemName == null)
                itemName = itemIndex.ToString();

            string upper = itemName.ToUpper(CultureInfo.InvariantCulture);
            itemName = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "ITEM_{0}_NAME", (object) upper);



            if(itemName == "ITEM_PERSONALSHIELD_NAME")
            {

                itemDef.tier = ItemTier.NoTier;
                Logger.LogError("Fuck Personal Shield Gen");
            }
           
            orig.Invoke(itemIndex, itemDef);

        }


        public void Update()
        {

        }
           
    }
}