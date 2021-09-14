using AppodealAds.Unity.Api;
using UseEvents;
using UnityEngine;

namespace UseAd
{
    public class AdScript : MonoBehaviour
    {
        private const string AppKey = "d6518eddf62f6e0e727aa24f1677caf8f8a6ef3d377c138b";
        private void Start()
        {
            int adTypes = Appodeal.NON_SKIPPABLE_VIDEO;
            Appodeal.initialize(AppKey, adTypes);
        }
        private void OnEnable()
        {
            OnReloadButtonClick.OnAction += ShowNonScipableVideo;
        }
        private void OnDisable()
        {
            OnReloadButtonClick.OnAction -= ShowNonScipableVideo;
        }
        public void ShowNonScipableVideo()
        {
            if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO) && !Appodeal.isPrecache(Appodeal.NON_SKIPPABLE_VIDEO))
                Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
        }

    }
}