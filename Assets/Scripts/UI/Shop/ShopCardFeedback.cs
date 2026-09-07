using System.Collections;
using UnityEngine;

namespace Game.UI.Shop
{
    public static class ShopCardFeedback
    {
        public static IEnumerator PressedAnimation(Transform target)
        {
            Vector3 original = target.localScale;
            Vector3 pressed = original * 0.94f;

            target.localScale = pressed;
            yield return new WaitForSeconds(0.07f);

            target.localScale = original;
        }
    }
}
