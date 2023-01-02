using BepInEx;
using UnityEngine;

namespace RarityBundle
{
    [BepInDependency("root.rarity.lib")]
    [BepInPlugin(ModId, ModName, Version)]

    [BepInProcess("Rounds.exe")]
    internal class RarityBundle : BaseUnityPlugin
    {
        private const string ModId = "com.CrazyCoders.Rounds.RarityBundle";
        private const string ModName = "RarityBundle";
        public const string Version = "0.0.0"; // What version are we on (major.minor.patch)?

        public static RarityBundle instance { get; private set; }

        void Awake()
        {
            RarityLib.Utils.RarityUtils.AddRarity("Trinket", 3, new Color(0.38f, 0.38f, 0.38f), new Color(0.0978f, 0.1088f, 0.1321f));
            // Common 1
            RarityLib.Utils.RarityUtils.AddRarity("Scarce", 0.7f, new Color32(0, 172, 98, 255), new Color32(0, 95, 60, 255));
            // Uncommon 0.4
            RarityLib.Utils.RarityUtils.AddRarity("Exotic", 0.25f, new Color32(10, 50, 255, 255), new Color32(5, 25, 150, 255));
            // Rare 0.1
            RarityLib.Utils.RarityUtils.AddRarity("Epic", 0.0625f, new Color32(225, 0, 50, 255), new Color32(125, 0, 20, 255));
            // Legendary 0.025
            RarityLib.Utils.RarityUtils.AddRarity("Mythical", 0.00625f, new Color32(0, 255, 70, 255), new Color32(0, 125, 35, 255));
            RarityLib.Utils.RarityUtils.AddRarity("Divine", 0.0015625f, new Color32(255, 255, 180, 255), new Color32(125, 125, 100, 255));
        }
    }
}
