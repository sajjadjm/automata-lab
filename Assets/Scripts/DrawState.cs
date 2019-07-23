using System;
using System.Collections;
using System.Collections.Generic;
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
                    State s = new State(state, state.name);
                    States.Add(s);
                }

                else if(settings.value == 5)
                {
                    state = Instantiate(stateSprite[1], statePosition, Quaternion.identity);
                    state.name = "state" + counter;
                    State s = new State(state, state.name);
                    s.isEnd = true;
                    States.Add(s);
                }
                
                else if (settings.value == 6 && canDrawStartState)
                {
                    state = Instantiate(stateSprite[0], statePosition, Quaternion.identity);
                    state.transform.Find("StartMark").gameObject.SetActive(true);
                    canDrawStartState = false;
                    state.name = "StartState";
                    State s = new State(state, state.name);
                    s.isStart = true;
                    States.Add(s);
                }
                
                counter++;
            }
        }

        if (Input.GetMouseButtonUp(0) && (settings.value == 1 || settings.value == 5 || settings.value == 6))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "State")
            {
                 hit.collider.gameObject.transform.Find("Number").gameObject.GetComponent<StateNumber>().Inc(stateNumber);
                 stateNumber++;
            }
        }
    }
}