using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditArrow : MonoBehaviour
{
    public static EditArrow Instance;
    public GameObject editArrowPanel;
    public TuringRelation TurRel;
    public Relation DFARel;
    public string textName;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (DrawState.Instance.settings.value == 7 && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider.gameObject.tag == "Text" || hit.collider.gameObject.tag == "Text2")
            {
                GameObject arrow = hit.collider.gameObject.transform.parent.parent.gameObject;
                textName = hit.collider.gameObject.name;
                
                foreach (var rel in DrawLine.Instance.Relations)
                {
                    if (rel.Line.gameObject.name == arrow.name)
                    {
                        TurRel = (TuringRelation)rel;
                        DFARel = rel;
                    }
                }
                editArrowPanel.SetActive(true);
            }
        }
    }
}
