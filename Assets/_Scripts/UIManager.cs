using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text goldDisplayer;
    public Text goldPerClickDisplayer;
    public Text goldPerSecDisplayer;
    
    private void Update()
    {
        goldDisplayer.text = "PEACE: " + DataController.Instance.Gold;
        goldPerClickDisplayer.text = "PEACE PER CLICK: " + DataController.Instance.GoldPerClick;
        goldPerSecDisplayer.text = "PEACE PER SEC: " + DataController.Instance.GetGoldPerSec();
    }

}
