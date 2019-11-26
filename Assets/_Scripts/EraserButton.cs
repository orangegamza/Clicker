using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserButton : MonoBehaviour {

    public static EraserButton instance = null;

    [HideInInspector] public bool isReset = false;

    public List<ItemButton> items;
    public List<UpgradeButton> upgrades;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnClick()
    {
        PlayerPrefs.DeleteAll();
        isReset = true;

        foreach (ItemButton itemButton in items)
        {
            DataController.Instance.LoadItemButton(itemButton);
            ItemButton.instance.UpdateItem();
        }

        foreach (UpgradeButton upgradeButton in upgrades)
        {
            DataController.Instance.LoadUpgradeButton(upgradeButton);
            UpgradeButton.instance.UpdateUpgrade();
        }

        isReset = false;
    }

}
