using AppodealAds.Unity.Api;
using UseEvents;
using UnityEngine;

namespace UseAd
{
    public class AdScript : MonoBehaviour
    {
        [SerializeField] private int _deathToAd;

        private int _deadCount; 
        private const string APPKEY = "d6518eddf62f6e0e727aa24f1677caf8f8a6ef3d377c138b";
        private void Start()
        {
            _deadCount = PlayerPrefs.GetInt("deadCount");
            int adTypes = Appodeal.NON_SKIPPABLE_VIDEO | Appodeal.INTERSTITIAL;
            Appodeal.initialize(APPKEY, adTypes, false);
        }
        private void OnEnable()
        {
            OnReloadButtonClick.OnAction += ShowAd;
            OnDie.OnAction += DeadCountUp;
        }
        private void OnDisable()
        {
            OnReloadButtonClick.OnAction -= ShowAd;
            OnDie.OnAction -= DeadCountUp;
        }

        private void ShowAd()
        {
            if(PlayerPrefs.GetInt("deadCount") >= _deathToAd)
            {
                if (!TryShowConcretAd(Appodeal.INTERSTITIAL))
                    TryShowConcretAd(Appodeal.NON_SKIPPABLE_VIDEO);
                Debug.Log(PlayerPrefs.GetInt("deadCount"));
            }
        }

        private bool TryShowConcretAd(int typeAd)
        {
            if (Appodeal.canShow(typeAd) && !Appodeal.isPrecache(typeAd))
            {
                Appodeal.show(typeAd);
                _deadCount = 0;
                PlayerPrefs.SetInt("deadCount", _deadCount);
                return true;
            }
            return false;
        }
        private void DeadCountUp()
        {
            _deadCount += 1;
            PlayerPrefs.SetInt("deadCount", _deadCount);
        }
    }
}