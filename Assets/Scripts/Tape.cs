using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tape : MonoBehaviour
{
    public static Tape Instance;
    public GameObject Curser;
    public List<GameObject> Sectors = new List<GameObject>();
    public int counter = 0;
    
    private int sectorCounter;
    private GameObject startSector;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var s in Sectors)
        {
            s.transform.Find("#").gameObject.SetActive(true);
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
        if (counter == TuringBrain.Instance.Steps.Count - 1)
        {
            TuringBrain.Instance.Steps[counter].stateGameObject.GetComponent<SpriteRenderer>().color = Color.red;
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
            startSector = Sectors[Sectors.IndexOf(startSector) + 1];
            if (TuringBrain.Instance.Rels[counter].valueToChange == '1')
            {
                startSector.transform.Find("1").gameObject.SetActive(true);
                startSector.transform.Find("0").gameObject.SetActive(false);
                startSector.transform.Find("#").gameObject.SetActive(false);
            }
            
            else if (TuringBrain.Instance.Rels[counter].valueToChange == '0')
            {
                startSector.transform.Find("0").gameObject.SetActive(true);
                startSector.transform.Find("1").gameObject.SetActive(false);
                startSector.transform.Find("#").gameObject.SetActive(false);
            }
            
            else if (TuringBrain.Instance.Rels[counter].valueToChange == '#')
            {
                startSector.transform.Find("#").gameObject.SetActive(true);
                startSector.transform.Find("0").gameObject.SetActive(false);
                startSector.transform.Find("1").gameObject.SetActive(false);
            }
        }
        
        else
        {
            Curser.transform.position = new Vector3(Sectors[Sectors.IndexOf(startSector) - 1].transform.position.x, Curser.transform.position.y, Curser.transform.position.z);
            startSector = Sectors[Sectors.IndexOf(startSector) - 1];
            if (TuringBrain.Instance.Rels[counter].valueToChange == '1')
            {
                startSector.transform.Find("1").gameObject.SetActive(true);
                startSector.transform.Find("0").gameObject.SetActive(false);
                startSector.transform.Find("#").gameObject.SetActive(false);
            }
            
            else if (TuringBrain.Instance.Rels[counter].valueToChange == '0')
            {
                startSector.transform.Find("0").gameObject.SetActive(true);
                startSector.transform.Find("1").gameObject.SetActive(false);
                startSector.transform.Find("#").gameObject.SetActive(false);
            }
            
            else if (TuringBrain.Instance.Rels[counter].valueToChange == '#')
            {
                startSector.transform.Find("#").gameObject.SetActive(true);
                startSector.transform.Find("0").gameObject.SetActive(false);
                startSector.transform.Find("1").gameObject.SetActive(false);
            }
        }
        
        counter++;
    }
}
