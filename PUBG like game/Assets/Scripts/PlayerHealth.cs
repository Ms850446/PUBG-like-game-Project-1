using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer; //used for animating healthBar
    public float maxHealth = 100f;
    public float chipSpeed = 2f; // control how quickly delayed bar takes to catch up the one that immediately set
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI  healthText;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if(Input.GetKeyDown(KeyCode.A)){
            TakeDamage(Random.Range(5,10));
        }
        if(Input.GetKeyDown(KeyCode.S)){
            RestoreHealth(Random.Range(5,10));
        }
    }
    public void UpdateHealthUI(){
        Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if(fillBack > hFraction){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }

        if(fillFront < hFraction){
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth);
    }
    public void TakeDamage(float damage){
        health -= damage;
        lerpTimer = 0f;
    }
    public void RestoreHealth(float healAmount){
        health += healAmount;
        lerpTimer = 0f;

    }
    public void IncreaseHealth(int level){
        maxHealth += (health * 0.01f) * ((100 - level) * 0.1f); 
        health = maxHealth;
    }
}
