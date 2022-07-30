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
    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("LEVEL") + 1).ToString();
        nextLevel.text = (PlayerPrefs.GetInt("LEVEL") + 2).ToString();
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
