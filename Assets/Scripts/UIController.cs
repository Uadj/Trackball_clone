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
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float LevelProgressBar { set => levelProgessBar.fillAmount = value; }
    public int CurrentScore { set => currentScore.text = value.ToString(); }
    // Update is called once per frame
    void Update()
    {
        
    }
}
