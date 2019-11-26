using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class DataController : MonoBehaviour {

    private static DataController instance;

    public static DataController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataController>();

                if (instance == null)
                {
                    GameObject container = new GameObject("DataController");

                    instance = container.AddComponent<DataController>();
            
                }
            }
            return instance;
        }
    }

    private ItemButton[] itemButtons;
    private BuyController[] buyControllers;

    DateTime GetLastPlayDate()
    {
        if (!PlayerPrefs.HasKey("Time"))
            return DateTime.Now;

        string timeBinaryInString = PlayerPrefs.GetString("Time");
        long timeBinaryInLong = Convert.ToInt64(timeBinaryInString);

        return DateTime.FromBinary(timeBinaryInLong);
    }

    void UpdateLastPlayDate()
    {
        PlayerPrefs.SetString("Time", DateTime.Now.ToBinary().ToString());
    }

    private void OnApplicationQuit()
    {
        UpdateLastPlayDate();
    }

    public long Gold
    {
        get
        {
            if (!PlayerPrefs.HasKey("Gold"))
                return 0;

            string tmpGold = PlayerPrefs.GetString("Gold");
            return long.Parse(tmpGold);
        }
        set
        {
            PlayerPrefs.SetString("Gold", value.ToString());
        }
    }

    public int GoldPerClick
    {
        get
        {
            return PlayerPrefs.GetInt("GoldPerClick", 1);
        }
        set
        {
            PlayerPrefs.SetInt("GoldPerClick", value);
        }
    }

    public int TimeAfterLastPlay
    {
        get
        {
            DateTime currentTime = DateTime.Now;
            DateTime lastPlayDate = GetLastPlayDate();

            return (int) currentTime.Subtract(lastPlayDate).TotalSeconds;
        }
    }

    private void Awake()
    {
        itemButtons = FindObjectsOfType<ItemButton>();
        buyControllers = FindObjectsOfType<BuyController>();

        
        
    }

    private void Start()
    {
        Gold += GetGoldPerSec() * TimeAfterLastPlay;
        InvokeRepeating("UpdateLastPlayDate", 0f, 5f);
    }

    public void LoadUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        upgradeButton.level = PlayerPrefs.GetInt(key + "_level", 1);
        upgradeButton.goldByUpgrade = PlayerPrefs.GetInt(key + "_goldByUpgrade",
            upgradeButton.startGoldByUpgrade);
        upgradeButton.currentCost = PlayerPrefs.GetInt(key + "_cost",
            upgradeButton.startCurrentCost);
    }

    public void SaveUpgradeButton(UpgradeButton upgradeButton)
    {
        string key = upgradeButton.upgradeName;

        PlayerPrefs.SetInt(key + "_level", upgradeButton.level);
        PlayerPrefs.SetInt(key + "_goldByUpgrade", upgradeButton.goldByUpgrade);
        PlayerPrefs.SetInt(key + "_cost", upgradeButton.currentCost);
    }

    public void LoadItemButton(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        itemButton.level = PlayerPrefs.GetInt(key + "_level", 0);
        itemButton.currentCost = PlayerPrefs.GetInt(key + "_cost", itemButton.startCurrentCost);
        itemButton.goldPerSec = PlayerPrefs.GetInt(key + "_goldPerSec");

        if (PlayerPrefs.GetInt(key + "_isPurchased") == 1)
        {
            itemButton.isPurchased = true;
        }
        else
        {
            itemButton.isPurchased = false;
        }
    }

    public void SaveItemButton(ItemButton itemButton)
    {
        string key = itemButton.itemName;

        PlayerPrefs.SetInt(key + "_level", itemButton.level);
        PlayerPrefs.SetInt(key + "_cost", itemButton.currentCost);
        PlayerPrefs.SetInt(key + "_goldPerSec", itemButton.goldPerSec);

        if (itemButton.isPurchased == true)
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_isPurchased", 0);
        }
    }

    public void LoadBuy(BuyController buyController)
    {
        string key = buyController.buyName;

        if (PlayerPrefs.GetInt(key + "_isBought") == 1)
        {
            buyController.isBought = true;
        }
        else
        {
            buyController.isBought = false;
        }
    }

    public void SaveBuy(BuyController buyController)
    {
        string key = buyController.buyName;

        if (buyController.isBought == true)
        {
            PlayerPrefs.SetInt(key + "_isBought", 1);
        }
        else
        {
            PlayerPrefs.SetInt(key + "_isBought", 0);
        }
    }

    public void GetBuy()
    {
        List<string> buys = new List<string>(new string[]
        {
            "cake", "unicorn", "lamp", "rocket", "star"
        });

        foreach (string b in buys)
        {
            if (PlayerPrefs.GetInt(b + "_isBought") == 1)
                GameObject.Find(b).SetActive(true);
        }
    }

    public int GetGoldPerSec()
    {
        int goldPerSec = 0;
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if(itemButtons[i].isPurchased == true)
                goldPerSec += itemButtons[i].goldPerSec;
        }

        return goldPerSec;
    }
}
