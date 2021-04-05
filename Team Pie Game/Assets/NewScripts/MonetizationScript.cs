using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class MonetizationScript : MonoBehaviour , IUnityAdsListener
{
    string googleID = "3704609";

    string myPlacementId = "rewardedVideo";

    bool testMode = false;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(googleID, testMode);
    }

    public void PlayVideoAd()
    {
        UIManager.instance.DeathScreenControl(false);
        Time.timeScale = 0;
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Time.timeScale = 1;
            UIManager.instance.AdShow();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
            Time.timeScale = 1;
            UIManager.instance.AdSkipped();

        }
        else if (showResult == ShowResult.Failed)
        {
            Time.timeScale = 1;
            UIManager.instance.AdSkipped();
            //Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId)
        {
            //Advertisement.Show(myPlacementId);
        }
    }
}
