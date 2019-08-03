using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    private string folderPath = "C:/Users/SAJJAD/Documents/";
    private void Start()
    {
        PlayerPrefs.SetInt("counter", 0);
    }

    public void Shot()
    {
        int counter = PlayerPrefs.GetInt("counter");
        if(!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
        ScreenCapture.CaptureScreenshot(folderPath + "picture" + PlayerPrefs.GetInt("counter") + ".png");
        PlayerPrefs.SetInt("counter", counter + 1);
    }
}
