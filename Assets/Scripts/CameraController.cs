using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform lastPlatform;

    private float platformWeight = 4;

    private void Update()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        if(transform.position.y > target.position.y && transform.position.y > lastPlatform.position.y + platformWeight)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }
}
