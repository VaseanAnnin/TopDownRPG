using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Status_Manager : MonoBehaviour
{
    private float clamps = 0;
    public Image healthBar;
    public float healthAmount = 100f;
    public Image hungerBar;
    public float hungerAmount = 100f;
    public Image sanityBar;
    public float sanityAmount = 100f;
    public Image inventoryHighlight;

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
            Debug.Log("Bubbles1");
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

        if (Input.mouseScrollDelta.y != 0)
        {
            Shift(Input.mouseScrollDelta.y);
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

    public void Shift(float direction)
    {
        //Gets the rect transform of the inventory highlight box
        RectTransform rectTransform = inventoryHighlight.GetComponent<RectTransform>();

        //For some reason I need to assign a variable to the ANCHORED position before I can change it, but here I'm setting the current
        //position equal to the anchored position of the highlight box
        Vector2 currentPos = rectTransform.anchoredPosition;

        //Initialized a variable called clamps which binds the highlight box within a certain range
        if (direction > 0)
            //adds 104 to the clamp anytime the box moves up
            clamps += 104;
        else
            //vise versa
            clamps -= 104;

        //Moves the box 104 pixels
        currentPos.x += 104 * direction;

        //Controls the snapping
        if (clamps > 416)
        {
            currentPos.x -= clamps;
            clamps = 0;
        }
        else if (clamps < 0)
        {
            currentPos.x += 520;
            clamps = 416;
        }

        rectTransform.anchoredPosition = currentPos;
    }
}
