using HarmonyLib;
using RarityLib.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rarity_Bundle {

    [Serializable]
    [HarmonyPatch(typeof(CardRarityColor), "Toggle")]
    public class UniqueRarityColor : MonoBehaviour {

        private static bool Prefix(CardRarityColor __instance) {
            CardInfo card = __instance.GetComponentInParent<CardInfo>();
            if (card == null || card.rarity != RarityBundle.RarityBundle.Unique || card.rarity == CardInfo.Rarity.Common) { //extra check incase rarity regestration failed some how.
                return true;
            }
            __instance.gameObject.AddComponent<UniqueRarityColor>();
            CardVisuals componentInParent = __instance.GetComponentInParent<CardVisuals>();
            componentInParent.toggleSelectionAction = (Action<bool>)Delegate.Remove(componentInParent.toggleSelectionAction, new Action<bool>(__instance.Toggle));

            Destroy( __instance);
            __instance.GetComponent<Image>().color = RarityUtils.GetRarityData(RarityBundle.RarityBundle.Unique).color;
            return false;
        }

        void Update() {
            Image image = GetComponent<Image>();
            CanvasRenderer renderer = GetComponent<CanvasRenderer>();
            if(image == null || renderer == null) return;
            Color32 color = RarityUtils.GetRarityData(RarityBundle.RarityBundle.Unique).color;
            Color32 colorOff = RarityUtils.GetRarityData(RarityBundle.RarityBundle.Unique).colorOff;
            if(renderer.GetColor() == Color.white) {
                image.color= Color.white;
                renderer.SetColor(colorOff);
            }
            if(renderer.GetColor() == colorOff) {
                image.CrossFadeColor(color, 0.75f, true, true);
            }else if(renderer.GetColor() == color) {
                image.CrossFadeColor(colorOff, 0.75f, true, true);
            }
            
        }

    }
}
