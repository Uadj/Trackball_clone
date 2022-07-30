using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [Header("Paramerter")]
    [SerializeField]
    private float bounceForce = 5;
    [SerializeField]
    private float DropForce = -19;

    [Header("SFX")]
    [SerializeField]
    private AudioClip bounceClip;
    [SerializeField]
    private AudioClip normalBreakClip;
    [SerializeField]
    private AudioClip powerBreakClip;
    [Header("VFX")]
    [SerializeField]
    private Material playerMaterial;
    [SerializeField]
    private Transform splashImage;
    [SerializeField]
    private ParticleSystem[] splashParticles;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private bool isClicked = false;
    private PlayerPowerMode playerPowerMode;

    private Vector3 splashWeight = new Vector3(0, 0.22f, 0.1f);
    private void Awake()
    {
        playerPowerMode = GetComponent<PlayerPowerMode>();
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!gameController.IsGamePlay) return;
        UpdateMouseButton();
        UpdateDropToSmash();
        playerPowerMode.UpdatePowerMode(isClicked);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isClicked)
        {
            if (rigidbody.velocity.y > 0) return;
            OnJumpProcess(collision);
        }
        else
        {
            //if (collision.gameObject.CompareTag("BreakPart"))
            //{
            //    var platform = collision.transform.parent.GetComponent<PlatformCoontroller>();
            //    if(platform.isCollision == false)
            //    {
            //        platform.BreakAllParts();
            //        PlaySound(normalBreakClip);
            //        gameController.OnCollisionWithPlatform();
            //    }
            //}
            //else if (collision.gameObject.CompareTag("NonBreakPart"))
            //{
            //    rigidbody.isKinematic = true;
            //    Debug.Log("GameOver");
            //}
            if (playerPowerMode.IsPowerMode)
            {
                if (collision.gameObject.CompareTag("BreakPart") || collision.gameObject.CompareTag("NonBreakPart"))
                {
                    OnCollisionWithBreakPart(collision, powerBreakClip, 2);
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("BreakPart"))
                {
                    OnCollisionWithBreakPart(collision, normalBreakClip, 1);
                }
                else if (collision.gameObject.CompareTag("NonBreakPart"))
                {
                    /*rigidbody.isKinematic = true;
                    Debug.Log("GameOver");*/
                    gameController.GameOver(transform.position);
                    gameObject.SetActive(false);
                }
            }
        }
        if(collision.gameObject.CompareTag("LastPlatform") && gameController.IsGamePlay)
        {
            playerPowerMode.DeactivateAll();
            gameController.GameClear();
        }
        //rigidbody.velocity = new Vector3(0, bounceForce, 0);
        //PlaySound(bounceClip);
        //OnSplashImage(collision.transform);
        //OnSplashParticle();
        
    }
    private void OnCollisionStay(Collision collision)
    {
        if (rigidbody.velocity.y > 0) return;
        //Debug.Log("OnCollisonStay");
        if (isClicked && !collision.gameObject.CompareTag("LastPlatform")) return;
        OnJumpProcess(collision);
    }
    private void OnJumpProcess(Collision collision)
    {
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
    private void UpdateMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }
    private void UpdateDropToSmash()
    {
        if(Input.GetMouseButton(0) && isClicked)
        {
            rigidbody.velocity = new Vector3(0, DropForce, 0);
        }
    }
    private void OnCollisionWithBreakPart(Collision collision, AudioClip clip, int addedScore)
    {
        var platform = collision.transform.parent.GetComponent<PlatformCoontroller>();

        if(platform.isCollision == false)
        {
            platform.BreakAllParts();
            PlaySound(clip);
            gameController.OnCollisionWithPlatform(addedScore);
        }
    }
}
