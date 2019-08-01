using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveAndLoad : MonoBehaviour
{
    public string fileName = "data.xml";
    public string SavePath
    {
        get { return Application.persistentDataPath + fileName; }
    }

    [Serializable]
    public class DFAData
    {
        public List<State> SavedState = new List<State>();
        public List<Relation> SavedRelation = new List<Relation>();
    }

    public void Save()
    {
        DFAData data = new DFAData();
        data.SavedRelation = DrawLine.Instance.Relations;
        data.SavedState = DrawState.Instance.States;
        
        Debug.Log(SavePath);
        
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DFAData));
        FileStream writeStream = new FileStream(SavePath, FileMode.OpenOrCreate);
        
        xmlSerializer.Serialize(writeStream, data);
    }

    public void Load()
    {
        GameObject[] AllGameobjects = FindObjectsOfType<GameObject>();

        foreach (var go in AllGameobjects)
        {
            if (!go.GetComponent<Camera>() && !go.GetComponent<DFABrain>() && !go.GetComponent<Canvas>() && go.GetComponent<EventSystem>())
            {
                Destroy(go);
            }
        }
        
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(DFAData));
        FileStream readStream = new FileStream(SavePath, FileMode.Open);
        
        DFAData LoadedData = xmlSerializer.Deserialize(readStream) as DFAData;

        foreach (var s in LoadedData.SavedState)
        {
            Instantiate(s.stateGameObject, s.statePosition, Quaternion.identity);
            s.stateGameObject.transform.Find("Name").GetComponent<TextMeshPro>().text = s.Name;
        }

        foreach (var rel in LoadedData.SavedRelation)
        {
            if (!rel.isReturn)
            {
                Instantiate(rel.Line);
                Vector3 pos = (rel.endPoint + rel.startPoint) / 2;
                Instantiate(rel.Arrow);
                if (rel.isRecursive)
                {
                    rel.Arrow.transform.Find("Text2").GetComponent<TextMeshPro>().text = rel.value.ToString();
                }

                else
                {
                    rel.Arrow.transform.Find("Text").GetComponent<TextMeshPro>().text = rel.value.ToString();
                }
            }

            else if (rel.isReturn)
            {
                rel.startState.stateGameObject.transform.Find("ReturnRel").GetComponent<SpriteRenderer>().enabled = true;
                rel.startState.stateGameObject.transform.Find("Text").GetComponent<TextMeshPro>().text = rel.value.ToString();
            }
        }
    }
}
