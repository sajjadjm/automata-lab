using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("counter", 0);
    }

    public void Shot()
    {
        int counter = PlayerPrefs.GetInt("counter");
        ScreenCapture.CaptureScreenshot("picture" + PlayerPrefs.GetInt("counter") + ".png");
        PlayerPrefs.SetInt("counter", counter + 1);
    }
}
