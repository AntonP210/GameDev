using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public static int Energy;
    public static int Wave = 0;

    public static int Health;


    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI healthText;


    public int startMoney = 200;
    public int startEnergy = 100;
    public int HealthPoints = 10;
    public static int totalWaves;

    private void Start()
    {
        ResetStats();
        Money = startMoney;
        Health = HealthPoints;
        

        healthText.text = HealthPoints.ToString() + "/" + Health.ToString();
        moneyText.text = Money.ToString();
        waveText.text = totalWaves.ToString() + "/" + 0;
    }
    public static void ResetStats()
    {
        Wave = 0;
        Money = 0;
    }
    private void Update()
    {
        UpdateGoldGUI();
        UpdateEnergyGUI();
        UpdateHPGUI();
        UpdateWaveGUI();
    }
    private void UpdateEnergyGUI()
    {
        //energyText.text = startEnergy.ToString()+"/"+Energy.ToString();
    }
    private void UpdateHPGUI()
    {
        healthText.text = HealthPoints.ToString() + "/" + Health.ToString();
    }
    private void UpdateWaveGUI()
    {
        waveText.text = totalWaves.ToString() + "/" + Wave.ToString();
    }
    private void UpdateGoldGUI()
    {
        moneyText.text = Money.ToString();
    }
}
