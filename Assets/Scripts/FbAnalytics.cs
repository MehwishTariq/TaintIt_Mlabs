using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class FbAnalytics : MonoBehaviour
{
    
    FirebaseApp app;
        
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(Task =>
        {
            var dependencyStatus = Task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    //on 1, 5, 25 ads
    public static void LogFirebaseInterstitialEvent(int AdsNo)
    {
        FirebaseAnalytics.LogEvent("inter_" + AdsNo);
    }

    //on 1, 10, 50 levels
    public static void LogFirebaseStartEvent(int levelNo)
    {
        FirebaseAnalytics.LogEvent("start_" + levelNo);
    }

    //on 1, 10, 50 levels
    public static void LogFirebaseCompleteEvent(int levelNo)
    {
        FirebaseAnalytics.LogEvent("complete_" + levelNo);
    }
    public static void LogFirebaseLevelCompleteEvent(int levelNo)
    {
        FirebaseAnalytics.LogEvent("Level_Complete_" + levelNo);
    }

    public static void LogFirebaseLevelStartEvent(int levelNo)
    {
        FirebaseAnalytics.LogEvent("Level_Start_" + levelNo);
    }

    public static void LogFirebaseLevelFailEvent(int levelNo)
    {
        FirebaseAnalytics.LogEvent("Level_Fail_" + levelNo);
    }
}
