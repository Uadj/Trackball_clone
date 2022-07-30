using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private PlatformShape[] allPlatforms;
    [SerializeField]
    private Transform lastPlatform;

    public int SpawnPlatform()
    {
        Transform[] platforms = SetupPlatformFigure();
        int platformCount = 200; //SetupPlatformCount();
        var indexs = SetupStartAndEndIndex(platforms);

        for(int i=0; i<platformCount; ++i)
        {
            Transform platform = Instantiate(platforms[Random.Range(indexs.Item1, indexs.Item2)]);

            platform.position = new Vector3(0, -i * 0.5f, 0);
            platform.eulerAngles = new Vector3(0, -i * 5, 0);

            if(i!=0 && i%5 == 0  && Random.Range(0, 2) == 1)
            {
                platform.eulerAngles += Vector3.up * 180;

            }
            platform.SetParent(transform);
        }

        lastPlatform.position = new Vector3(0, -platformCount * 0.5f, 0);

        return platformCount;
    }

    private Transform[] SetupPlatformFigure()
    {
        int index = Random.Range(0, allPlatforms.Length);

        Transform[] selectedPlatforms = new Transform[allPlatforms[index].platforms.Length];
        for(int i=0; i< allPlatforms[index].platforms.Length; ++i)
        {
            selectedPlatforms[i] = allPlatforms[index].platforms[i];
        }

        return selectedPlatforms;

    }

    private (int, int ) SetupStartAndEndIndex(Transform[] platforms)
    {
        int level = PlayerPrefs.GetInt("LEVEL");

        float startDuration = 0.05f;
        float endDuration = 0.1f;

        int startIndex = Mathf.Min((int)(level * startDuration), platforms.Length - 1);

        int endIndex = Mathf.Min((int)(level * endDuration) + 2, platforms.Length);

        return (startIndex, endIndex);
    }

    private int SetupPlatformCount()
    {
        int level = PlayerPrefs.GetInt("LEVEL");
        int baseCount = 20;

        int platformCount = baseCount * ((level + 10) / 10) + (int)(level % 10 * 1.5f);

        return platformCount;
    }
    [System.Serializable]
    private struct PlatformShape
    {
        public Transform[] platforms;
    }


}
