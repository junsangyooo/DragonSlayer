using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    EnemySpawner enemySpawner;
    //UI에 필요한 변수들
    [SerializeField]
    private Image DashCoolTime;
    private float dashingCooldown = 2.5f;
    [SerializeField]
    private GameObject RetrySuggestImage;

    [SerializeField]
    private GameObject gameOverImage;

    [SerializeField]
    private GameObject pauseImage;

    [SerializeField]
    private TextMeshProUGUI time;

    [SerializeField]
    private TextMeshProUGUI gold_text;
    private int gold;

    [SerializeField]
    private Image hp_fill;
    [SerializeField]
    private Image exp_fill;

    private bool isPaused;
    private bool pauseImageOn;

    private int playTime;
    public int getPlayTime() {return playTime;}

    // 게임 플레이에 필요한 변수들
    public bool playing;
    public bool GetPlaying() {return playing;}
    private bool retried;
    public List<GameObject> enemies = new List<GameObject>();

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        enemySpawner = GetComponent<EnemySpawner>();
        GameStart();
    }

    private void OnDestroy() {
        Instance = null;
    }

    private void GameStart() {
        playing = true;
        retried = false;
        isPaused = false;
        pauseImageOn = false;
        playTime = 0;
        enemySpawner.StartSpawning();
        StartCoroutine("PlayTime");
    }

    IEnumerator PlayTime() {
        while (playing) {
            yield return new WaitForSeconds(1f);
            playTime++;
            UpdateTime();
        }
    }

    public void UpdateTime() {
        int second = playTime % 60;
        int minute = (playTime - second) / 60;
        if (minute < 10 && second < 10) {
            time.text = "0" + minute.ToString() + ":0" + second.ToString();
        } else if (minute < 10) {
            time.text = "0" + minute.ToString() + ":" + second.ToString();
        } else if (second < 10) {
            time.text =  minute.ToString() + ":0" + second.ToString();
        } else {
            time.text = minute.ToString() + ":" + second.ToString();
        }
    }

    public void AddGold() {
        gold++;
        UpdateGold();
    }
    public void UpdateGold() {
        gold_text.text = gold.ToString();
    }
    public void UpdateHP() {
        hp_fill.fillAmount = Player.Instance.getCurrentHP() / Player.Instance.getMaxHP();
    }
    public void UpdateEXP() {
        exp_fill.fillAmount = Player.Instance.getCurrentExp() / Player.Instance.getMaxExp();
    }
    public void UpdateDashCool(float curTime) {
        DashCoolTime.fillAmount = curTime / dashingCooldown;
    }

    public void DisplayGameOverImage() {
        gameOverImage.gameObject.SetActive(true);
    }
    public void CloseGameOverImage() {
        gameOverImage.gameObject.SetActive(false);
    }
    public void DisplayRetryImage() {
        RetrySuggestImage.gameObject.SetActive(true);
    }
    public void CloseRetryImage() {
        RetrySuggestImage.gameObject.SetActive(false);
    }

    public void PauseButtonClicked() {
        isPaused = !isPaused;
        pauseImage.gameObject.SetActive(isPaused);
        if (pauseImageOn) {
            Resume();
        } else {
            Pause();
        }
        pauseImageOn = !pauseImageOn;
    }

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Resume() {
        Time.timeScale = 1;
    }

    private void Retry() {
        // 게임 pause하는 기능을 추가하고 다시하기 창 디스플레이
        retried = true;
        CloseRetryImage();
    }
    

    public void GameOver() {
        Pause();
        if (!retried) {
            DisplayRetryImage();
        } else {
            DisplayGameOverImage();
        }
    }

    public void GameEnd() {
        playing = false;
        OnDestroy();
    }

    private void GetBasicAttackDamage() {

    }
}

