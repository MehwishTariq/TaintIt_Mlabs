using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameDataHolder dataHolder;
    public LevelManager levelManager;
    public WaveSystem waveSystem;
    public Transform TriggerArea;

    public Transform[] camPos;
    public Transform viewCam, OpponentOnLevelDone;
    public GameObject confetti;
    public bool LevelComplete, LevelFail;
    public int numberOfKilled { get; set; }
    public int numberOfEnemies { get; set; }
    public event Action onGameStart, onGameComplete, onGameFail, onNewWaveCreation;

    public delegate void ProgressMeter(float Val, float total);
    public static event ProgressMeter onProgress;

    public bool EnemyReached = false;
    public bool gameStarted = false;
    static int adsShown = 0;
    public float WaveDelay;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelComplete = false;
        LevelFail = false;
        numberOfEnemies = dataHolder.GetEnemiesCount();
        WaveDelay = dataHolder.waveDelay();
    }

    public void UpdateProgres(int Val, int total)
    {
        onProgress?.Invoke(Val, total);
    }



    public void InvokeNewWave()
    {
        onNewWaveCreation?.Invoke();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0) && !gameStarted)
        //    StartCoroutine(StartGame());
    }

    public void PlayGame()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        ChangeView();
        UIManager.instance.HideStartPanel();
        yield return new WaitForSeconds(1f);

        onGameStart?.Invoke();
        gameStarted = true;

        FbAnalytics.LogFirebaseLevelStartEvent(PlayerPrefs.GetInt(Utility.LevelNo, 1));

        if (PlayerPrefs.GetInt(Utility.LevelNo, 1) == 1 || PlayerPrefs.GetInt(Utility.LevelNo, 1) == 10 ||
            PlayerPrefs.GetInt(Utility.LevelNo, 1) == 50)
            FbAnalytics.LogFirebaseStartEvent(PlayerPrefs.GetInt(Utility.LevelNo,1));

    }

    public void ChangeView()
    {
        viewCam.DOMove(camPos[1].position, 0.5f);
        viewCam.DORotate(camPos[1].eulerAngles, 0.5f);
    }

    public void ChangeViewBack()
    {
        viewCam.DOMove(camPos[0].position, 0.5f);
        viewCam.DORotate(camPos[0].eulerAngles, 0.5f);
    }
    public void GameComplete()
    {
        ChangeViewBack();
        OpponentOnLevelDone.gameObject.SetActive(true);
        OpponentOnLevelDone.GetComponent<Animator>().Play("Crying");
        AudioManager.instance.Play("ConfettiPop");
        confetti.SetActive(true);
        onGameComplete?.Invoke();

        if (UIManager.instance.seconds > 15)
        {
            AdsManager.instance.ShowInterstitialAd();
            adsShown++;

            if (adsShown == 1 || adsShown == 5 || adsShown == 25)
                FbAnalytics.LogFirebaseInterstitialEvent(adsShown);
        }

        FbAnalytics.LogFirebaseLevelCompleteEvent(PlayerPrefs.GetInt(Utility.LevelNo) - 1);

        if (PlayerPrefs.GetInt(Utility.LevelNo) - 1 == 1 || PlayerPrefs.GetInt(Utility.LevelNo) - 1 == 10 ||
            PlayerPrefs.GetInt(Utility.LevelNo) - 1 == 50)
            FbAnalytics.LogFirebaseCompleteEvent(PlayerPrefs.GetInt(Utility.LevelNo) - 1);

    }

    public void GameFailed()
    {
        ChangeViewBack();
        OpponentOnLevelDone.gameObject.SetActive(true);
        OpponentOnLevelDone.GetComponent<Animator>().Play("Dancing");
        onGameFail?.Invoke();


        FbAnalytics.LogFirebaseLevelFailEvent(PlayerPrefs.GetInt(Utility.LevelNo));

    }
}
