using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyController : MonoBehaviour {

    public Animator anim;

    [HideInInspector] public bool isBought = false;
    public string buyName;
    public int buyPrice;

    private static BuyController instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        DataController.Instance.LoadBuy(this);
        Debug.Log("DataController.Instance.LoadBuy(this);");
    }

    public void OnMouseDown()
    {
        if (isBought != true)
        {
            if (DataController.Instance.Gold >= buyPrice)
            {
                isBought = true;
                DataController.Instance.Gold -= buyPrice;
                anim.SetTrigger("Bought");

                DataController.Instance.SaveBuy(this);

                if (isBought == true)
                    Debug.Log("isBought == true  " + buyName);
                else if (isBought == false)
                    Debug.Log("isBought == false  " + buyName);
            }
        }
    }
}
