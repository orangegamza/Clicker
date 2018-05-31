using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {

    public AudioSource soundFx;

    public string upgradeName;
    public Text upgradeDisplay;

    [HideInInspector]
    public int goldByUpgrade;
    public int startGoldByUpgrade;

    [HideInInspector]
    public int currentCost = 1;
    public int startCurrentCost = 1;

    [HideInInspector]
    public int level = 1;

    public float upgradePow = 1.07f;
    public float costPow = 3.14f;

    private void Start()
    {
        DataController.Instance.LoadUpgradeButton(this);
        UpdateUI();
    }

    public void PurchaseUpgrade()
    {
        if (DataController.Instance.Gold >= currentCost)
        {
            DataController.Instance.Gold -= currentCost;
            DataController.Instance.GoldPerClick += goldByUpgrade;
            level++;

            UpdateUpgrade();
            UpdateUI();
            DataController.Instance.SaveUpgradeButton(this);

            soundFx.Play();
        }
    }

    public void UpdateUpgrade()
    {
        goldByUpgrade = startGoldByUpgrade * (int) Mathf.Pow(upgradePow, level);
        currentCost = startCurrentCost * (int)Mathf.Pow(costPow, level);
    }

    public void UpdateUI()
    {
        upgradeDisplay.text = upgradeName + "\n\nCost: " + currentCost + 
            "\nLevel :" + level + "\nNext Gained Peace: " + goldByUpgrade;
    }
}
