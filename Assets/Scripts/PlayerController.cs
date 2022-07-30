using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Paramerter")]
    [SerializeField]
    private float bounceForce = 5;

    [Header("SFX")]
    [SerializeField]
    private AudioClip bounceClip;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (rigidbody.velocity.y > 0) return;
        rigidbody.velocity = new Vector3(0, bounceForce, 0);
        PlaySound(bounceClip);
    }
    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}
