using HarmonyLib;
using System;
using System.Linq;

namespace RarityBundle {
    [Serializable]
    [HarmonyPatch(typeof(ModdingUtils.Utils.Cards))]
    public class ModingUtilsCardsPatch {

        [HarmonyPatch(nameof(ModdingUtils.Utils.Cards.CardDoesNotConflictWithCards))]
        public static void CardDoesNotConflictWithCardsPatch(CardInfo card, CardInfo[] cards, ref bool __result) {
            if(__result && card.rarity == RarityBundle.Unique) {
                __result = !cards.Any(c => c.rarity == RarityBundle.Unique);
            }
        }
        [HarmonyPatch(nameof(ModdingUtils.Utils.Cards.AddCardToPlayer), typeof(Player), typeof(CardInfo) , typeof(bool) , typeof(string), typeof(float), typeof(float), typeof(bool))]
        public static bool AddCardToPlayerPatch(Player player, CardInfo card) {
            if(player == null || card == null) return true;
            return !(card.rarity == RarityBundle.Unique && player.data.currentCards.Any(c => c.rarity == RarityBundle.Unique));
        }

    }
}
