using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Status_Manager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public Image hungerBar;
    public float hungerAmount = 100f;
    public Image sanityBar;
    public float sanityAmount = 100f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Starve(20);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Magic(20);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Starve(float hunger)
    {
        hungerAmount -= hunger;
        hungerBar.fillAmount = hungerAmount / 100f;
    }

    public void Eat(float foodValue)
    {
        hungerAmount += foodValue;
        hungerAmount = Mathf.Clamp(hungerAmount, 0, 100);

        hungerBar.fillAmount = hungerAmount / 100f;
    }

    public void Magic(float spell)
    {
        sanityAmount -= spell;
        sanityBar.fillAmount = sanityAmount / 100f;
    }

    public void Meditation(float sanityGained)
    {
        sanityAmount += sanityGained;
        sanityAmount = Mathf.Clamp(sanityAmount, 0, 100);

        sanityBar.fillAmount = sanityAmount / 100f;
    }
}
