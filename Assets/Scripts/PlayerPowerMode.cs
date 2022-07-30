using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerMode : MonoBehaviour
{
    [SerializeField]
    private GameObject powerEffect;
    [SerializeField]
    private GameObject powerGameObject;
    [SerializeField]
    private Image powerGuage;

    private float powerAmount = 0;
    public float PowerAmount
    {
        set
        {
            powerAmount = value;
            if (powerAmount >= 1)
            {
                powerAmount = 1;
                IsPowerMode = true;
                powerGuage.color = Color.red;
                powerEffect.SetActive(true);
            }
            else if(powerAmount <= 0)
            {
                powerAmount = 0;
                IsPowerMode = false;
                powerGuage.color = Color.white;
                powerEffect.SetActive(false);
            }
        }
        get => powerAmount;
    }
    public bool IsPowerMode { private set; get; } = false;
    public void UpdatePowerMode(bool isClicked)
    {
        float increaseAmount = 0.8f;
        float decreaseAmountNormal = 0.5f;
        float decreaseAmountPower = 0.3f;
        float activateAmount = 0.3f;

        if (IsPowerMode)
        {
            PowerAmount -= Time.deltaTime * decreaseAmountPower;
        }
        else
        {
            if (isClicked) PowerAmount += Time.deltaTime * increaseAmount;
            else PowerAmount -= Time.deltaTime * decreaseAmountNormal;
        }
        if (powerAmount >= activateAmount || IsPowerMode) powerGameObject.SetActive(true);
        else powerGameObject.SetActive(false);

        powerGuage.fillAmount = PowerAmount;
    }
    public void DeactivateAll()
    {
        powerEffect.SetActive(false);
        powerGameObject.SetActive(false);
    }
}
