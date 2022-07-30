using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlatformSpawner platformSpawner;
    [SerializeField]
    private UIController uIController;

    private RandomColor randomColor;
    public bool IsGamePlay { private set; get; } = false;

    private int brokenPlatformCount = 0;
    private int totalPlatformCount;
    private int currentScore = 0;
    private void Awake()
    {
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
        IsGamePlay = true;
        uIController.GameStart();
    }
    public void OnCollisionWithPlatform(int addedScore = 1)
    {
        brokenPlatformCount++;
        uIController.LevelProgressBar = (float)brokenPlatformCount / (float)totalPlatformCount;

        currentScore += addedScore;
        uIController.CurrentScore = currentScore;
    }
}
