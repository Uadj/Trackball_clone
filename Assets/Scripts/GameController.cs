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
    [Header("VFX")]
    [SerializeField]
    private GameObject gameOverEffect;

    private RandomColor randomColor;
    private AudioSource audioSource;
    public bool IsGamePlay { private set; get; } = false;

    private int brokenPlatformCount = 0;
    private int totalPlatformCount;
    private int currentScore = 0;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        totalPlatformCount = platformSpawner.SpawnPlatform();

        //platformSpawner.SpawnPlatform();
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }
    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
                yield break;
            }
            yield return null;
        }
    }
    private void GameStart()
    {
        Debug.Log("Start");
        IsGamePlay = true;
        uIController.GameStart();
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
}
