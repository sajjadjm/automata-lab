using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawState : MonoBehaviour
{
    public static DrawState Instance;
    public Dropdown settings;
    public GameObject[] stateSprite = new GameObject [2];
    public List<State> States = new List<State>();
    public bool canDrawStartState = true;

    private Vector3 statePosition;
    private int counter = 1;
    private int stateNumber = 0;
    private bool canDraw;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && (settings.value == 1 || settings.value == 5 || settings.value == 6))
        {
            canDraw = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "MainCamera")
            {
                canDraw = false;
            }
            
            if ((settings.value == 1 || settings.value == 5 || settings.value == 6) && canDraw)
            {
                statePosition = DrawLine.Instance.GetMouseCameraPoint().Value;
                GameObject state = null;
                if (settings.value == 1)
                {
                    state = Instantiate(stateSprite[0], statePosition, Quaternion.identity);
                    state.name = "state" + counter;
                    state.gameObject.transform.Find("Name").GetComponent<TextMeshPro>().text = "S" + stateNumber;
                    stateNumber++;
                    State s = new State(state, state.name, statePosition);
                    States.Add(s);
                }

                else if(settings.value == 5)
                {
                    state = Instantiate(stateSprite[1], statePosition, Quaternion.identity);
                    state.name = "state" + counter;
                    state.gameObject.transform.Find("Name").GetComponent<TextMeshPro>().text = "S" + stateNumber;
                    stateNumber++;
                    State s = new State(state, state.name, statePosition);
                    s.isEnd = true;
                    States.Add(s);
                }
                
                else if (settings.value == 6 && canDrawStartState)
                {
                    state = Instantiate(stateSprite[0], statePosition, Quaternion.identity);
                    state.transform.Find("StartMark").gameObject.SetActive(true);
                    canDrawStartState = false;
                    state.name = "StartState";
                    state.gameObject.transform.Find("Name").GetComponent<TextMeshPro>().text = "S" + stateNumber;
                    stateNumber++;
                    State s = new State(state, state.name, statePosition);
                    s.isStart = true;
                    States.Add(s);
                }
                
                counter++;
            }
        }
    }
}