using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : MonoBehaviour
{
    private RaycastHit2D initialState;
    private bool isin;
    private float deltaX, deltaY;
    private Vector2 mousePosition;
    private RaycastHit2D hit;
    private State selectedState;
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (DrawState.Instance.settings.value == 4 || DrawState.Instance.settings.value == 2))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - hit.collider.gameObject.transform.position.x;
            deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - hit.collider.gameObject.transform.position.y;
            if (hit.collider.gameObject.tag == "State" && hit.collider.gameObject.tag != "MainCamera" && hit.collider.gameObject.tag != "Sector")
            {
                initialState = hit;
            }

            foreach (var s in DrawState.Instance.States)
            {
                if (s.Name == initialState.collider.gameObject.name)
                {
                    selectedState = s;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && DrawState.Instance.settings.value == 2 && hit.collider.gameObject.tag != "MainCamera" && hit.collider.gameObject.tag != "Sector")
        {
            if (hit.collider.gameObject.name == "StartState")
            {
                DrawState.Instance.canDrawStartState = true;
            }

            Destroy(hit.collider.gameObject);
            
            foreach (var s in DrawState.Instance.States.ToArray())
            {
                if (s.Name == hit.collider.gameObject.name)
                {
                    DrawState.Instance.States.Remove(s);
                }
            }
            
            foreach (Relation rel in selectedState.relatedInLines.ToArray())
            {
                Destroy(rel.Arrow);
                Destroy(rel.Line);
                DrawLine.Instance.Relations.Remove(rel);
            }
            
            foreach (Relation rel in selectedState.relatedOutLines.ToArray())
            {
                Destroy(rel.Arrow);
                Destroy(rel.Line);
                DrawLine.Instance.Relations.Remove(rel);
            }
        }

        if (Input.GetMouseButton(0) && DrawState.Instance.settings.value == 4 && hit.collider.gameObject.tag != "MainCamera" && hit.collider.gameObject.tag != "Sector")
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit.collider.gameObject.transform.position = new Vector3(mousePosition.x - deltaX, mousePosition.y - deltaY, -8.700012f);

            foreach (var line in selectedState.relatedInLines)
            {
                Vector3 dir = line.Line.GetPosition(1) - line.Line.GetPosition(0);
                if (!line.isRecursive)
                {
                    line.Line.SetPosition(1, hit.collider.gameObject.transform.position);
                    line.Arrow.transform.position =
                        (line.Line.GetPosition(0) +
                         line.Line.GetPosition(1)) / 2;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    line.Arrow.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                }

                else
                {
                    Vector2 hit2D = hit.collider.gameObject.transform.position;
                    hit2D = hit2D + (-(Vector2.Perpendicular(dir)).normalized * 0.4f);
                    Vector3 newHit = new Vector3(hit2D.x, hit2D.y, -8.700012f);
                    line.Line.SetPosition(1, newHit);
                    line.Arrow.transform.position =
                        (line.Line.GetPosition(0) +
                         line.Line.GetPosition(1)) / 2;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    line.Arrow.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                }
            }
            
            foreach (var line in selectedState.relatedOutLines)
            {
                Vector3 dir = Vector3.zero;
                if (!line.isReturn)
                {
                     dir = line.Line.GetPosition(0) - line.Line.GetPosition(1);
                }

                if (!line.isRecursive && !line.isReturn)
                {
                    line.Line.SetPosition(0, hit.collider.gameObject.transform.position);
                    line.Arrow.transform.position =
                        (line.Line.GetPosition(0) +
                         line.Line.GetPosition(1)) / 2;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    line.Arrow.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); 
                }

                else if (line.isRecursive && !line.isReturn)
                {
                    Vector2 hit2D = hit.collider.gameObject.transform.position;
                    hit2D = hit2D + ((Vector2.Perpendicular(dir)).normalized * 0.4f);
                    Vector3 newHit = new Vector3(hit2D.x, hit2D.y, -8.700012f);
                    line.Line.SetPosition(0, newHit);
                    line.Arrow.transform.position =
                        (line.Line.GetPosition(0) +
                         line.Line.GetPosition(1)) / 2;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    line.Arrow.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); 
                }
            }
        }
    }
}
