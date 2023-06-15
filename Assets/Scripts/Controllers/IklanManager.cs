// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Advertisements;

// public class IklanManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
// {
//     #if UNITY_ANDROID
//         string gameId = "5037763";
//         string Rewarded = "Rewarded_Android";
//     #elif UNITY_IOS
//         string gameId = "5037762";
//         string Rewarded = "Rewarded_iOS";
//     #endif

//     private void Start(){
//         Advertisement.Initialize(
//             gameId, 
//             false, 
//             this);
//     }

//     public void ShowRewarded(){
//         Advertisement.Load(placementId: Rewarded, loadListener: this);
//         Advertisement.Show(placementId: Rewarded, showListener: this);
//     }

//     // Inizialization Callbacks
//     public void OnInitializationComplete()
//     {
//         Debug.Log("OnInitializationComplete");
//     }

//     public void OnInitializationFailed(UnityAdsInitializationError error, string message)
//     {
//         Debug.Log("OnInitializationFailed: [ " + error + " ] " + message);
//     }

//     // Load Callbacks

//     public void OnUnityAdsAdLoaded(string placementId)
//     {
//         Debug.Log("OnUnityAdsAdLoaded: [ " + placementId + " ]");
//     }

//     public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
//     {
//         Debug.Log("OnUnityAdsFailedToLoad: [ " + placementId + " ] [ " + error + " ] " + message);
//     }

//     // Show Callback
//     public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
//     {
//         Debug.Log("OnUnityAdsShowFailure: [ " + placementId + " ] [ " + error + " ] " + message);
//     }

//     public void OnUnityAdsShowStart(string placementId)
//     {
//         Debug.Log("OnUnityAdsShowStart: [ " + placementId + " ]");
//     }

//     public void OnUnityAdsShowClick(string placementId)
//     {
//         Debug.Log("OnUnityAdsShowClick: [ " + placementId + " ]");
//     }

//     public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
//     {
//         Debug.Log("OnUnityAdsShowComplete: [ " + placementId + " ], " + "showCompletionState: " + showCompletionState);
//         Time.timeScale=1;
//     }

//     // Ads Callback
//     public void OnUnityAdsReady(string placementId)
//     {
//         Debug.Log("OnUnityAdsShowFailure: [ " + placementId + " ]");
//     }

//     public void OnUnityAdsDidError(string message)
//     {
//         Debug.Log("OnUnityAdsShowStart: [ " + message + " ]");
//     }

//     public void OnUnityAdsDidStart(string placementId)
//     {
//         Debug.Log("OnUnityAdsShowClick: [ " + placementId + " ]");
//     }

//     public void OnUnityAdsDidFinish(string placementId, UnityAdsShowCompletionState showResult)
//     {
//         Debug.Log("OnUnityAdsShowComplete: [ " + placementId + " ], " + showResult);
//     }

//     public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
//     {
//         Debug.Log("OnUnityAdsShowComplete: [ " + placementId + " ], " + showResult);
//     }
// }