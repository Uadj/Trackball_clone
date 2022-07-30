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

    [Header("VFX")]
    [SerializeField]
    private Material playerMaterial;
    [SerializeField]
    private Transform splashImage;
    [SerializeField]
    private ParticleSystem[] splashParticles;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;

    private Vector3 splashWeight = new Vector3(0, 0.22f, 0.1f);
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
        OnSplashImage(collision.transform);
        OnSplashParticle();
    }
    private void OnSplashImage(Transform target)
    {
        Transform image = Instantiate(splashImage, target);

        image.position = transform.position - splashWeight;
        image.localEulerAngles = transform.position - splashWeight;
        float randomScale = Random.Range(0.3f, 0.5f);
        image.localScale = new Vector3(randomScale, randomScale, 1);

        image.GetComponent<MeshRenderer>().material.color = playerMaterial.color;
    }
    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void OnSplashParticle()
    {
        for(int i=0; i<splashParticles.Length; ++i)
        {
            if (splashParticles[i].gameObject.activeSelf) continue;

            splashParticles[i].gameObject.SetActive(true);
            splashParticles[i].transform.position = transform.position - splashWeight;

            var mainModule = splashParticles[i].main;
            mainModule.startColor = playerMaterial.color;
            break;
        }
    }
}
