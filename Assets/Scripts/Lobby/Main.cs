using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main Instance {get; private set; }

    private void Awake() {
        Instance = this;
    }
    private void OnDestroy() {
        Instance = null;
    }

    [SerializeField]
    private GameObject RankingImage;
    [SerializeField]
    private GameObject SettingImage;
    [SerializeField]
    private GameObject MissionImage;
    [SerializeField]
    private GameObject UpgradeImage;

    public bool ImageDisplaying;

    [SerializeField]
    private TextMeshProUGUI GoldText;

    // Start is called before the first frame update
    void Start()
    {
        ImageDisplaying = false;
    }

    public void displaySetting() {
        if (!ImageDisplaying) {
            SettingImage.gameObject.SetActive(true);
            ImageDisplaying = true;
        }
    }
    public void displayRanking() {
        if (!ImageDisplaying) {
            RankingImage.gameObject.SetActive(true);
            ImageDisplaying = true;
        }
    }
    public void displayMission() {
        if (!ImageDisplaying) {
            MissionImage.gameObject.SetActive(true);
            ImageDisplaying = true;
        }
    }
    public void displayUpgrade() {
        if (!ImageDisplaying) {
            UpgradeImage.gameObject.SetActive(true);
            ImageDisplaying = true;
        }
    }

    public void closeSetting() {
        SettingImage.gameObject.SetActive(false);
        ImageDisplaying = false;
    }
    public void closeRanking() {
        RankingImage.gameObject.SetActive(false);
        ImageDisplaying = false;
    }
    public void closeMission() {
        MissionImage.gameObject.SetActive(false);
        ImageDisplaying = false;
    }
    public void closeUpgrade() {
        UpgradeImage.gameObject.SetActive(false);
        ImageDisplaying = false;
    }

    public void gameStart() {
        SceneManager.LoadScene("Cave");
    }

}
