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
        goldDisplayer.text = "Peace: " + DataController.Instance.Gold;
        goldPerClickDisplayer.text = "Peace per letter: " + DataController.Instance.GoldPerClick;
        goldPerSecDisplayer.text = "Peace per sec: " + DataController.Instance.GetGoldPerSec();
    }

    
}
