using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXp;
    public float requireXp;
    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI  levelText;
    public TextMeshProUGUI  xpText;
    [Header("Multipliers")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;
    // Start is called before the first frame update
    void Start()
    {
        frontXpBar.fillAmount = currentXp / requireXp;
        backXpBar.fillAmount = currentXp / requireXp;
        requireXp = CalculateRequiredXp();
        levelText.text = "Level " + level;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();
        if(Input.GetKeyDown(KeyCode.Equals)){
            GainExperienceFlatRate(20);
        }
        if(currentXp > requireXp){
            LevelUp();
        }
    }
    public void UpdateXpUI(){
        float xpFraction = currentXp / requireXp;
        float FXP = frontXpBar.fillAmount;
        float BXP = backXpBar.fillAmount;
        if(FXP < xpFraction){
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if(delayTimer > 3){
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXp + "/" + requireXp;
    }
    public void GainExperienceFlatRate(float xpGained){
        currentXp += xpGained;
        lerpTimer = 0f;
    }
    public void GainExperienceScalable(float xpGained, int passedLevel){
        if(passedLevel < level){
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentXp += xpGained * multiplier;
        }
        else{
            currentXp += xpGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void LevelUp(){
        level++;
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
        currentXp = Mathf.RoundToInt(currentXp - requireXp);
        GetComponent<PlayerHealth>().IncreaseHealth(level);
        requireXp = CalculateRequiredXp();
        levelText.text = "Level " + level;
    }
    private int CalculateRequiredXp(){
        int solveForRequiredXp = 0;
        for(int levelCycle = 1; levelCycle <= level; levelCycle++){
            solveForRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXp / 4;
    }
}
