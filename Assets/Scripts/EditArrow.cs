using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditArrow : MonoBehaviour
{
    public static EditArrow Instance;
    public GameObject editArrowPanel;

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

            if (hit.collider.gameObject.tag == "Arrow")
            {
                editArrowPanel.SetActive(true);
            }
        }
    }
}
