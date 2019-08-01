﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Camera))]

public class DrawLine : MonoBehaviour
{
    public GameObject arrow;
    public GameObject turingArrow;
    public static DrawLine Instance;
    public Material lineMaterial;
    public float lineWidth;
    public GameObject inputValue;
    public List<GameObject> Lines = new List<GameObject>();
    public List<Relation> Relations = new List<Relation>();

    private new Camera camera;
    private Vector3 lineStartPoint;
    private Vector3 lineEndPoint;
    private State startState;
    private State EndState;
    private GameObject Arrow;
    private int counter = 1;
    private bool canDraw = true;
    private bool recursive;
    private bool isRight;
    private Relation antiRecursiveRel;
    private Relation Rel;
    private Relation relToAdd;
    private char? value;
    private char? changeValue;
    private char? RightOrLeft;
    private float iniRot = 0.0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        camera = GetComponent<Camera>();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            inputValue.transform.Find("InputField").GetComponent<InputField>().characterLimit = 1;
        }
        
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            inputValue.transform.Find("InputField").GetComponent<InputField>().characterLimit = 3;
        }
    }

    private void Update()
    {
        if (DrawState.Instance.settings.value == 3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.tag == "State")
                {
                    foreach (var s in DrawState.Instance.States)
                    {
                        if (s.stateGameObject == hit.collider.gameObject)
                        {
                            startState = s;
                            lineStartPoint = hit.collider.gameObject.transform.position;
                        }
                    }
                }

                else
                {
                    canDraw = false;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.tag == "State")
                {
                    foreach (var s in DrawState.Instance.States)
                    {
                        if (s.stateGameObject == hit.collider.gameObject)
                        {
                            EndState = s;
                            lineEndPoint = hit.collider.gameObject.transform.position;
                        }
                    }
                }

                else
                {
                    canDraw = false;
                }

                foreach (var rel in Relations)
                {
                    if (rel.startState == startState && rel.endState == EndState)
                    {
                        canDraw = false;
                    }

                    if (rel.startState == EndState && rel.endState == startState)
                    {
                        recursive = true;
                    }
                }

                if (lineEndPoint != lineStartPoint && hit.collider.gameObject.tag != "MainCamera" && !inputValue.activeSelf && canDraw && !recursive)
                {
                    Vector3 dir = lineEndPoint - lineStartPoint;
                    Vector3 pos = (lineEndPoint + lineStartPoint) / 2;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    var gameObject = new GameObject();
                    gameObject.name = "Line" + counter;
                    gameObject.tag = "Line";
                    var lineRenderer = gameObject.AddComponent<LineRenderer>();
                    lineRenderer.material = lineMaterial;
                    lineRenderer.SetPositions(new Vector3[] {lineStartPoint, lineEndPoint});
                    lineRenderer.startWidth = lineWidth;
                    lineRenderer.endWidth = lineWidth;
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                    {
                        Arrow = Instantiate(arrow, pos, Quaternion.AngleAxis(angle + 90, Vector3.forward));
                        Arrow.transform.SetParent(lineRenderer.GetComponent<Transform>());
                        Relation rel = new Relation(startState, EndState, Arrow, lineRenderer, lineStartPoint,
                            lineEndPoint);
                        Relations.Add(rel);
                        Rel = rel;
                        foreach (var s in DrawState.Instance.States)
                        {
                            if (s.Name == startState.Name)
                            {
                                s.relatedOutLines.Add(rel);
                            }

                            else if (s.Name == EndState.Name)
                            {
                                s.relatedInLines.Add(rel);
                            }
                        }
                    }
                    
                    else if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        Arrow = Instantiate(turingArrow, pos, Quaternion.AngleAxis(angle + 90, Vector3.forward));
                        Arrow.transform.SetParent(lineRenderer.GetComponent<Transform>());
                        TuringRelation rel = new TuringRelation(startState, EndState, Arrow, lineRenderer, lineStartPoint,
                            lineEndPoint);
                        Relations.Add(rel);
                        Rel = rel;
                        foreach (var s in DrawState.Instance.States)
                        {
                            if (s.Name == startState.Name)
                            {
                                s.relatedOutLines.Add(rel);
                            }

                            else if (s.Name == EndState.Name)
                            {
                                s.relatedInLines.Add(rel);
                            }
                        }
                    }

                    inputValue.SetActive(true);
                    Lines.Add(lineRenderer.gameObject);
                    counter += 1;
                }

                if (lineEndPoint != lineStartPoint && hit.collider.gameObject.tag != "MainCamera" && !inputValue.activeSelf && canDraw && recursive)
                {
                    Vector3 dir = lineEndPoint - lineStartPoint;
                    Vector2 lineStartPoint2D = lineStartPoint;
                    Vector2 lineEndPoint2D = lineEndPoint;
                    Vector3 newLineStartPoint = lineStartPoint2D + (-(Vector2.Perpendicular(dir)).normalized * 0.4f);
                    Vector3 newLineEndPoint = lineEndPoint2D + (-(Vector2.Perpendicular(dir)).normalized * 0.4f);
                    newLineStartPoint = new Vector3(newLineStartPoint.x, newLineStartPoint.y, -8.700012f);
                    newLineEndPoint = new Vector3(newLineEndPoint.x, newLineEndPoint.y, -8.700012f);
                    Vector3 pos = (newLineEndPoint + newLineStartPoint) / 2;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    var gameObject = new GameObject();
                    gameObject.name = "Line" + counter;
                    var lineRenderer = gameObject.AddComponent<LineRenderer>();
                    lineRenderer.material = lineMaterial;
                    lineRenderer.SetPositions(new Vector3[] {newLineStartPoint, newLineEndPoint});
                    lineRenderer.startWidth = lineWidth;
                    lineRenderer.endWidth = lineWidth;
                    if (SceneManager.GetActiveScene().buildIndex == 0)
                    {
                        Arrow = Instantiate(arrow, pos, Quaternion.AngleAxis(angle + 90, Vector3.forward));
                        Arrow.transform.SetParent(lineRenderer.GetComponent<Transform>());
                        Relation rel = new Relation(startState, EndState, Arrow, lineRenderer, newLineStartPoint,
                            newLineEndPoint);
                        rel.isRecursive = true;
                        Relations.Add(rel);
                        Rel = rel;
                        foreach (var s in DrawState.Instance.States)
                        {
                            if (s.Name == startState.Name)
                            {
                                s.relatedOutLines.Add(rel);
                            }
                            else if (s.Name == EndState.Name)
                            {
                                s.relatedInLines.Add(rel);
                            }
                        }
                    }
                    
                    else if (SceneManager.GetActiveScene().buildIndex == 1)
                    {
                        Arrow = Instantiate(turingArrow, pos, Quaternion.AngleAxis(angle + 90, Vector3.forward));
                        Arrow.transform.SetParent(lineRenderer.GetComponent<Transform>());
                        TuringRelation rel = new TuringRelation(startState, EndState, Arrow, lineRenderer, newLineStartPoint,
                            newLineEndPoint);
                        rel.isRecursive = true;
                        Relations.Add(rel);
                        Rel = rel;
                        foreach (var s in DrawState.Instance.States)
                        {
                            if (s.Name == startState.Name)
                            {
                                s.relatedOutLines.Add(rel);
                            }
                            else if (s.Name == EndState.Name)
                            {
                                s.relatedInLines.Add(rel);
                            }
                        }
                    }

                    inputValue.SetActive(true);
                    Lines.Add(lineRenderer.gameObject);
                    foreach (var relation in Relations)
                    {
                        if (relation.endState == startState && relation.startState == EndState)
                        {
                            antiRecursiveRel = relation;
                        }
                    }

                    Vector3 _newLineStartPoint = lineEndPoint2D - (-(Vector2.Perpendicular(dir)).normalized * 0.4f);
                    Vector3 _newLineEndPoint = lineStartPoint2D - (-(Vector2.Perpendicular(dir)).normalized * 0.4f);
                    _newLineStartPoint = new Vector3(_newLineStartPoint.x, _newLineStartPoint.y, -8.700012f);
                    _newLineEndPoint = new Vector3(_newLineEndPoint.x, _newLineEndPoint.y, -8.700012f);
                    Vector3 _pos = (_newLineEndPoint + _newLineStartPoint) / 2;
                    antiRecursiveRel.Line.SetPositions(new Vector3[] {_newLineStartPoint, _newLineEndPoint});
                    antiRecursiveRel.Arrow.transform.position = _pos;
                    antiRecursiveRel.isRecursive = true;
                    counter += 1;
                }

                if (lineEndPoint == lineStartPoint && hit.collider.gameObject.tag != "MainCamera" && !inputValue.activeSelf
                    && canDraw && !recursive)
                {
                    startState.stateGameObject.transform.Find("ReturnRel").GetComponent<SpriteRenderer>().enabled = true;
                    Arrow = startState.stateGameObject;
                    TuringRelation rel = new TuringRelation(startState, startState, startState.stateGameObject.transform.position,
                        startState.stateGameObject.transform.position);
                    rel.isReturn = true;
                    Relations.Add(rel);
                    Rel = rel;
                    foreach (var s in DrawState.Instance.States)
                    {
                        if (s.Name == EndState.Name)
                        {
                            s.relatedOutLines.Add(rel);
                        }
                    }
                    inputValue.SetActive(true);
                }
            }
        }
        canDraw = true;
        recursive = false;
    }

    public void DFALockInput()
    {
        value = inputValue.transform.Find("InputField").GetComponent<InputField>().text[0];
        if (inputValue.transform.Find("InputField").GetComponent<InputField>().text.Trim() != "")
        {
            inputValue.SetActive(false);
        }

        if (Rel.isRecursive)
        {
            Arrow.transform.Find("Text2").GetComponent<TextMeshPro>().text = value.ToString();
            Arrow.transform.Find("Text2").transform.eulerAngles = Vector3.forward * iniRot;
        }

        else
        {
            Arrow.transform.Find("Text").GetComponent<TextMeshPro>().text = value.ToString();
            Arrow.transform.Find("Text").transform.eulerAngles = Vector3.forward * iniRot;
        }
        
        Rel.value = value.Value;

        value = null;
        inputValue.transform.Find("InputField").GetComponent<InputField>().Select();
        inputValue.transform.Find("InputField").GetComponent<InputField>().text = "";
    }

    public void TuringLockInput()
    {
        value = inputValue.transform.Find("InputField").GetComponent<InputField>().text[0];
        RightOrLeft = inputValue.transform.Find("InputField").GetComponent<InputField>().text[1];
        changeValue = inputValue.transform.Find("InputField").GetComponent<InputField>().text[2];
        
        if (inputValue.transform.Find("InputField").GetComponent<InputField>().text.Trim() != "")
        {
            inputValue.SetActive(false);
        }
        
        ((TuringRelation) Rel).value = value.Value;
        ((TuringRelation) Rel).valueToChange = changeValue.Value;

        if (RightOrLeft == 'r' || RightOrLeft == 'R')
        {
            ((TuringRelation) Rel).isRight = true;
            RightOrLeft = 'R';
        }

        else if (RightOrLeft == 'l' || RightOrLeft == 'L')
        {
            RightOrLeft = 'L';
        }

        if (Rel.isRecursive)
        {
            Arrow.transform.Find("Text2").GetComponent<TextMeshPro>().text = value + " " + RightOrLeft + " " + changeValue;
            Arrow.transform.Find("Text2").transform.eulerAngles = Vector3.forward * iniRot;
        }

        else
        {
            Arrow.transform.Find("Text").GetComponent<TextMeshPro>().text = value + " " + RightOrLeft + " " + changeValue;
            Arrow.transform.Find("Text").transform.eulerAngles = Vector3.forward * iniRot;
        }
        
        inputValue.transform.Find("InputField").GetComponent<InputField>().text = "";
    }

    public void TuringEdit()
    {
        value = EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text[0];
        RightOrLeft = EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text[1];
        changeValue = EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text[2];
        
        if (EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text.Trim() != "")
        {
            EditArrow.Instance.editArrowPanel.SetActive(false);
        }
        
        ((TuringRelation) Rel).value = value.Value;
        ((TuringRelation) Rel).valueToChange = changeValue.Value;

        if (RightOrLeft == 'r' || RightOrLeft == 'R')
        {
            ((TuringRelation) Rel).isRight = true;
            RightOrLeft = 'R';
        }

        else
        {
            RightOrLeft = 'L';
        }

        if (Rel.isRecursive)
        {
            Arrow.transform.Find("Text2").GetComponent<TextMeshPro>().text = value + " " + RightOrLeft + " " + changeValue;
            Arrow.transform.Find("Text2").transform.eulerAngles = Vector3.forward * iniRot;
        }

        else
        {
            Arrow.transform.Find("Text").GetComponent<TextMeshPro>().text = value + " " + RightOrLeft + " " + changeValue;
            Arrow.transform.Find("Text").transform.eulerAngles = Vector3.forward * iniRot;
        }
        
        EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text = "";
    }

    public void DFAEdit()
    {
        value = EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text[0];
        if (EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text.Trim() != "")
        {
            EditArrow.Instance.editArrowPanel.SetActive(false);
        }

        if (Rel.isRecursive)
        {
            Arrow.transform.Find("Text2").GetComponent<TextMeshPro>().text = value.ToString();
            Arrow.transform.Find("Text2").transform.eulerAngles = Vector3.forward * iniRot;
        }

        else
        {
            Arrow.transform.Find("Text").GetComponent<TextMeshPro>().text = value.ToString();
            Arrow.transform.Find("Text").transform.eulerAngles = Vector3.forward * iniRot;
        }
        
        Rel.value = value.Value;

        value = null;
        EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().Select();
        EditArrow.Instance.editArrowPanel.transform.Find("InputField").GetComponent<InputField>().text = "";
    }
    
    public Vector3? GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction;
    }
}
