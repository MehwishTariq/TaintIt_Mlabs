using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Sprite[] CountDown;
    public Image countDownImage;
    public Image fadePanel;
    public TextMeshPro score, coins, progress;
    public Image progressBar;
    public GameObject completePanel, failPanel, StartPanel, ScorePanel, TimePanel, InGamePanel;
    public TextMeshPro time;
    public int coinsAmt = 0;
    public int scoreNo = 0;
    public float clockTime = 0f;
    public float minutes { get; set; }
    public float seconds { get; set; }
    public bool allowTimer = false;

    public void ChangeProgress(float progVal, float totalCount)
    {
        progress.text = ((int)((progVal / totalCount) * 100)).ToString() + " %";
        progressBar.DOFillAmount(progVal / totalCount, 0.3f);
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        minutes = 0;
        seconds = 0;
        clockTime = GameManager.instance.dataHolder.GetClockTime();
        GameManager.onProgress += ChangeProgress;
    }
    private void Update()
    {
        if (allowTimer)
        {
            clockTime -= Time.deltaTime;
            if (clockTime <= 0)
            {
                GameManager.instance.LevelFail = true;
                GameManager.instance.GameFailed();
            }
        }

        minutes = Mathf.FloorToInt(clockTime / 60);
        seconds = Mathf.FloorToInt(clockTime % 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.onGameComplete += ShowCompletePanel;
        GameManager.instance.onGameFail += ShowFailPanel;
    }

    private void OnDisable()
    {
        GameManager.instance.onGameComplete -= ShowCompletePanel;
        GameManager.instance.onGameFail -= ShowFailPanel;
    }

    public void ShowCompletePanel()
    {
        PlayerPrefs.SetInt("TotalCoins", PlayerPrefs.GetInt("TotalCoins", 0) + coinsAmt);
        failPanel.SetActive(false);
        ScorePanel.SetActive(false);
        TimePanel.SetActive(false);
        InGamePanel.SetActive(false);
        StartPanel.SetActive(false);
        completePanel.SetActive(true);
        SetCoins();
        allowTimer = false;
    }

    public void ShowFailPanel()
    {
        ScorePanel.SetActive(false);
        InGamePanel.SetActive(false);
        TimePanel.SetActive(false);
        completePanel.SetActive(false);
        StartPanel.SetActive(false);
        failPanel.SetActive(true);
        allowTimer = false;
    }

    public void HideStartPanel()
    {
        completePanel.SetActive(false);
        failPanel.SetActive(false);
        StartPanel.SetActive(false);
        countDownImage.gameObject.SetActive(true);
        StartCoroutine(CountDownStart());
    }

    bool toggle;

    public void Pause_ResumeGame()
    {
        toggle = !toggle;
        if (toggle)
        {
            Time.timeScale = 0;
            fadePanel.DOFade(0, 0).SetUpdate(true);
            fadePanel.gameObject.SetActive(true);
            fadePanel.DOFade(0.5f, 0.3f).SetUpdate(true);
        }
        else
        {
            fadePanel.DOFade(0f, 0.3f).SetUpdate(true).OnComplete(() => {
                Time.timeScale = 1;
                fadePanel.gameObject.SetActive(false);
            });
        }
    }
    
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score", 0) + scoreNo);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void SetScore()
    {
        score.GetComponent<RectTransform>().DOPunchScale(new Vector3(1.2f,1.2f,1.2f), 0.1f);
        score.text = scoreNo.ToString();
    }

    public void SetCoins()
    {
        coinsAmt = PlayerPrefs.GetInt("TotalCoins", 0);
        coins.text = coinsAmt.ToString();
    }

    IEnumerator CountDownStart()
    {
        countDownImage.sprite = CountDown[0];
        yield return new WaitForSeconds(0.3f);
        countDownImage.sprite = CountDown[1];
        yield return new WaitForSeconds(0.3f);
        countDownImage.sprite = CountDown[2];
        yield return new WaitForSeconds(0.3f);
        countDownImage.gameObject.SetActive(false);
        //ScorePanel.SetActive(true);
        TimePanel.SetActive(true);
        InGamePanel.SetActive(true);
        allowTimer = true;
    }
}
