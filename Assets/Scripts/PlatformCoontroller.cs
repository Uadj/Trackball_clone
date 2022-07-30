using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCoontroller : MonoBehaviour
{

    [SerializeField]
    private float removeDuration = 1;
    public bool isCollision { private set; get; } = false;
    public void BreakAllParts()
    {
        if (!isCollision)
        {
            isCollision = true;
        }
        if(transform.parent != null)
        {
            transform.parent = null;
        }
        PlatformPartController[] parts = transform.GetComponentsInChildren<PlatformPartController>();

        foreach(PlatformPartController part in parts)
        {
            part.BreakingPart();
        }
        StartCoroutine(nameof(RemovePart));
    }
    private IEnumerator RemovePart()
    {
        yield return new WaitForSeconds(removeDuration);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    Invoke(nameof(BreakAllParts), Mathf.Abs(transform.position.y));    
    //}

    // Update is called once per frame

    
}
