using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void CallMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void CallCloset()
    {
        SceneManager.LoadScene("Closet");
    }
}
