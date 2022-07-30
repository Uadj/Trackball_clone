using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlatformSpawner platformSpawner;
    [SerializeField]
    private UIController uIController;
    [Header("SFX")]
    [SerializeField]
    private AudioClip gameOverClip;
    [SerializeField]
    private AudioClip gameClearClip;
    [Header("VFX")]
    [SerializeField]
    private GameObject gameOverEffect;
    [SerializeField]
    private GameObject gameClearEffect;
    private RandomColor randomColor;
    private AudioSource audioSource;
    public bool IsGamePlay { private set; get; } = false;

    private int brokenPlatformCount = 0;
    private int totalPlatformCount;
    private int currentScore = 0;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentScore = PlayerPrefs.GetInt("CURRENTSCORE");
        uIController.CurrentScore = currentScore;
        totalPlatformCount = platformSpawner.SpawnPlatform();

        //platformSpawner.SpawnPlatform();
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }
    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0) || PlayerPrefs.GetInt("DEACTIVATEMAIN")==1)
            {
                GameStart();
                yield break;
            }
            yield return null;
        }
    }
    private void GameStart()
    {
        IsGamePlay = true;
        uIController.GameStart();
    }
    public void GameClear()
    {
        IsGamePlay = false;
        audioSource.clip = gameClearClip;
        audioSource.Play();
        gameClearEffect.SetActive(true);

        UpdateHighScore();
        uIController.GameClear();

        PlayerPrefs.SetInt("LEVEL", PlayerPrefs.GetInt("LEVEL") + 1);
        PlayerPrefs.SetInt("CURRENTSCORE", currentScore);
        StartCoroutine(nameof(SceneLoadOnClick));
    }
    public void GameOver(Vector3 position)
    {
        IsGamePlay = false;
        audioSource.clip = gameOverClip;
        audioSource.Play();
        gameOverEffect.transform.position = position;
        gameOverEffect.SetActive(true);

        UpdateHighScore();
        uIController.GameOver(currentScore);

        PlayerPrefs.SetInt("CURRENTSCORE", 0);
        StartCoroutine(nameof(SceneLoadOnClick));
    }
    private void UpdateHighScore()
    {
        if(currentScore > PlayerPrefs.GetInt("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", currentScore);
        }
    }
    private IEnumerator SceneLoadOnClick()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            yield return null;
        }
    }
    public void OnCollisionWithPlatform(int addedScore = 1)
    {
        brokenPlatformCount++;
        uIController.LevelProgressBar = (float)brokenPlatformCount / (float)totalPlatformCount;

        currentScore += addedScore;
        uIController.CurrentScore = currentScore;
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("CURRENTSCORE", 0);
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 0);
    }
    [ContextMenu("Reset All PlayerPrefs")]
    private void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
