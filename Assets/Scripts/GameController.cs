using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlatformSpawner platformSpawner;
    private RandomColor randomColor;
    private void Awake()
    {

        platformSpawner.SpawnPlatform();
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }
}
