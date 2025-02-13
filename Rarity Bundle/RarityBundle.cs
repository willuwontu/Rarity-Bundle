using BepInEx;
using HarmonyLib;
using RarityLib.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib;
using UnityEngine;

namespace RarityBundle {
    [BepInDependency("root.rarity.lib")]
    [BepInPlugin(ModId, ModName, Version)]

    [BepInProcess("Rounds.exe")]
    internal class RarityBundle : BaseUnityPlugin
    {
        private const string ModId = "com.CrazyCoders.Rounds.RarityBundle";
        private const string ModName = "RarityBundle";
        public const string Version = "0.2.0"; // What version are we on (major.minor.patch)?

        public static RarityBundle instance { get; private set; }

        public static CardInfo.Rarity Trinket, Common, Scarce, Uncommon, Exotic, Rare, Epic, Legendary, Mythical, Divine, Unique;

        void Awake() {
            new Harmony(ModId).PatchAll();
            var plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            RarityLib.Utils.RarityUtils.AddRarity("Trinket", plugins.Exists(plugin => plugin.Info.Metadata.GUID == "Root.Rarity.Dard.Draw") ? 1.75f : 3f, new Color(0.38f, 0.38f, 0.38f), new Color(0.0978f, 0.1088f, 0.1321f));
            // Common 1
            RarityLib.Utils.RarityUtils.AddRarity("Scarce", 0.7f, new Color32(0, 172, 98, 255), new Color32(0, 95, 60, 255));
            // Uncommon 0.4
            RarityLib.Utils.RarityUtils.AddRarity("Exotic", 0.25f, new Color32(10, 50, 255, 255), new Color32(5, 25, 150, 255));
            // Rare 0.1
            RarityLib.Utils.RarityUtils.AddRarity("Epic", 0.0625f, new Color32(225, 0, 50, 255), new Color32(125, 0, 20, 255));
            // Legendary 0.025
            RarityLib.Utils.RarityUtils.AddRarity("Mythical", 0.00625f, new Color32(0, 255, 70, 255), new Color32(0, 125, 35, 255));
            RarityLib.Utils.RarityUtils.AddRarity("Divine", 0.0015625f, new Color32(255, 255, 180, 255), new Color32(125, 125, 100, 255));
            RarityLib.Utils.RarityUtils.AddRarity("Unique", 0.003f, new Color32(255, 255, 130, 255), new Color32(0, 255, 255, 255));

        }
        void Start() {
            Trinket = RarityUtils.GetRarity("Trinket");
            Common = CardInfo.Rarity.Common;
            Scarce = RarityUtils.GetRarity("Scarce");
            Uncommon = CardInfo.Rarity.Uncommon;
            Exotic = RarityUtils.GetRarity("Exotic");
            Rare = CardInfo.Rarity.Rare;
            Epic = RarityUtils.GetRarity("Epic");
            Legendary = RarityUtils.GetRarity("Legendary");
            Mythical = RarityUtils.GetRarity("Mythical");
            Divine = RarityUtils.GetRarity("Divine");
            Unique = RarityUtils.GetRarity("Unique");

            ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, cardinfo) => {
                if(cardinfo.rarity != Unique) return true;  //Make sure its the rarity we actually care about.
                if(player.data.currentCards.Any(c=> c.rarity == Unique)) return false; //Players can only have 1 Unique card each
                if(CardChoice.instance != null 
                    && ((List<GameObject>)CardChoice.instance.GetFieldValue("spawnedCards")).Any(g =>
                                    g != null && g.GetComponent<CardInfo>() != null && g.GetComponent<CardInfo>().rarity == Unique)) 
                                            return false; //Ideally this should stop more then one Unique card from showing up in a single hand.
                if(PlayerManager.instance.players.Any(p => p.data.currentCards.Any(c => c == cardinfo))) return false; //No two players can have the same Unique card.
                return true;
            });
        }
    }

    public static class Rarities
    {
        public static CardInfo.Rarity GetRarity(string name)
        {
            return RarityLib.Utils.RarityUtils.GetRarity(name);
        }
        public static CardInfo.Rarity Trinket => RarityLib.Utils.RarityUtils.GetRarity("Trinket");
        public static CardInfo.Rarity Common => CardInfo.Rarity.Common;
        public static CardInfo.Rarity Scarce => RarityLib.Utils.RarityUtils.GetRarity("Scarce");
        public static CardInfo.Rarity Uncommon => CardInfo.Rarity.Uncommon;
        public static CardInfo.Rarity Exotic => RarityLib.Utils.RarityUtils.GetRarity("Exotic");
        public static CardInfo.Rarity Rare => CardInfo.Rarity.Rare;
        public static CardInfo.Rarity Epic => RarityLib.Utils.RarityUtils.GetRarity("Epic");
        public static CardInfo.Rarity Legendary => RarityLib.Utils.RarityUtils.GetRarity("Legendary");
        public static CardInfo.Rarity Mythical => RarityLib.Utils.RarityUtils.GetRarity("Mythical");
        public static CardInfo.Rarity Divine => RarityLib.Utils.RarityUtils.GetRarity("Divine");
    }
}
