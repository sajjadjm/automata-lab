using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    public static WebRequest Instance;
    public string serialNumber;
    public GameObject inputSerial;
    public Text err;
    public int validateFlag;

    private string result;

    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("validateFlag"))
        {
            PlayerPrefs.SetInt("validateFlag", 0);
        }

        validateFlag = PlayerPrefs.GetInt("validateFlag");
    }

    IEnumerator GetText() {
        UnityWebRequest www = UnityWebRequest.Get("http://git.playoffapp.ir:8898/apps/sajjad/validate?serial=" + serialNumber);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) 
        {
            Debug.Log(www.error);
        }
        else 
        {
            Debug.Log(www.downloadHandler.text);
            result = www.downloadHandler.text;
            if (result == "OK")
            {
                PlayerPrefs.SetInt("validateFlag", 1);
                inputSerial.SetActive(false);
            }
            else if(result == "403")
            {
                err.gameObject.SetActive(true);
            }
        }
    }

    public void SerialLockInput()
    {
        if (inputSerial.transform.Find("Input").GetComponent<InputField>().text != "")
        {
            serialNumber = inputSerial.transform.Find("Input").GetComponent<InputField>().text;
        }
        
        StartCoroutine(GetText());
    }
}
