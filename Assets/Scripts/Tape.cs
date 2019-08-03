using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tape : MonoBehaviour
{
    public static Tape Instance;
    public GameObject Curser;
    public List<GameObject> Sectors = new List<GameObject>();
    public int counter = 0;
    public GameObject startSector;
    public bool canResetTape = true;
    
    private int sectorCounter;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var s in Sectors)
        {
            s.transform.Find("Text").GetComponent<TextMeshPro>().text = "#";
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.gameObject.tag == "Sector")
            {
                Curser.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, Curser.transform.position.y, Curser.transform.position.z);
                startSector = hit.collider.gameObject;
            }
        }
    }

    public void showSteps()
    {
        if (canResetTape)
        {
            foreach (var s in Sectors)
            {
                s.transform.Find("Text").GetComponent<TextMeshPro>().text = "#";
            }
            
            for (int i = 0; i < TuringBrain.Instance.inputValue.Length; i++)
            {
                Sectors[7 + i].transform.Find("Text").GetComponent<TextMeshPro>().text = TuringBrain.Instance.inputValue[i].ToString();
            }

            canResetTape = false;
        }
        
        if (counter == TuringBrain.Instance.Steps.Count - 1)
        {
            TuringBrain.Instance.Steps[counter].stateGameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }

        else
        {
            TuringBrain.Instance.Steps[counter].stateGameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        
        if (counter > 0 && TuringBrain.Instance.Steps[counter - 1].stateGameObject.name != TuringBrain.Instance.Steps[counter].stateGameObject.name)
        {
            TuringBrain.Instance.Steps[counter - 1].stateGameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (TuringBrain.Instance.Rels[counter].isRight)
        {
            Curser.transform.position = new Vector3(Sectors[Sectors.IndexOf(startSector) + 1].transform.position.x, Curser.transform.position.y, Curser.transform.position.z);
            startSector.transform.Find("Text").GetComponent<TextMeshPro>().text = TuringBrain.Instance.Rels[counter].valueToChange.ToString();
            startSector = Sectors[Sectors.IndexOf(startSector) + 1];
        }
        
        else
        {
            Curser.transform.position = new Vector3(Sectors[Sectors.IndexOf(startSector) - 1].transform.position.x, Curser.transform.position.y, Curser.transform.position.z);
            startSector.transform.Find("Text").GetComponent<TextMeshPro>().text = TuringBrain.Instance.Rels[counter].valueToChange.ToString();
            startSector = Sectors[Sectors.IndexOf(startSector) - 1];
        }
        
        counter++;
    }
}
