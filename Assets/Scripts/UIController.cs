using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private TextMeshProUGUI currentLevel;
    [SerializeField]
    private TextMeshProUGUI nextLevel;
    [SerializeField]
    private TextMeshProUGUI currentScore;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI textCurrentScore;
    [SerializeField]
    private TextMeshProUGUI textHighScore;
    [Header("Main")]
    [SerializeField]
    private GameObject mainPanel;
    [Header("InGame")]
    [SerializeField]
    private Image levelProgessBar;
    [Header("GameClear")]
    [SerializeField]
    private GameObject gameClearPanel;
    [SerializeField]
    private TextMeshProUGUI textLevelCompleted;
    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("LEVEL") + 1).ToString();
        nextLevel.text = (PlayerPrefs.GetInt("LEVEL") + 2).ToString();

        if (PlayerPrefs.GetInt("DEACTIVATEMAIN") == 0) mainPanel.SetActive(true);
        else mainPanel.SetActive(false);
    }
    public void GameClear()
    {
        textLevelCompleted.text = $"LEVEL {PlayerPrefs.GetInt("LEVEL") + 1}\nCOMPLETED!";
        gameClearPanel.SetActive(true);
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 1);
    }
    public void GameStart()
    {
        mainPanel.gameObject.SetActive(false);
    }
    public void GameOver(int currentScore)
    {
        textCurrentScore.text = $"SCORE\n{currentScore}";
        textHighScore.text = $"HIGH SCORE\n{PlayerPrefs.GetInt("HIGHSCORE")}";
        gameOverPanel.SetActive(true);
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 0);
    }
    public float LevelProgressBar { set => levelProgessBar.fillAmount = value; }
    public int CurrentScore { set => currentScore.text = value.ToString(); }
    // Update is called once per frame
    void Update()
    {
        
    }
}
