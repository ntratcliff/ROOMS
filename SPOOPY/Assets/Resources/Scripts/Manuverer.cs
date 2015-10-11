using UnityEngine;
using System.Collections;

public class Manuverer : MonoBehaviour
{
    public Color SelectedColor = Color.yellow;
    public Color LastColor;
    public GameObject SelectedObject;

    private bool lastMouseDown;
    private Vector3 lastMousePos;

    private Vector3 screenPoint;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        lastMouseDown = Input.GetMouseButton(0);
        lastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject frameSelectedObj = getRoomObjectAtMouse();
            if (frameSelectedObj)
                Debug.Log("Selected Object = " + frameSelectedObj.name);
            if (!SelectedObject) //possible click on new object
            {
                Debug.Log("Selected Object = " + frameSelectedObj.name);
                if (frameSelectedObj)
                    select(frameSelectedObj);
            }
            else
            {
                if (frameSelectedObj
                    && SelectedObject != frameSelectedObj) //clicked on a new object
                {
                    deselect(SelectedObject);
                    select(frameSelectedObj);
                }
                else if (!frameSelectedObj
                    && SelectedObject) //clicked on nothing
                {
                    deselect(SelectedObject);
                }
                else //clicked on object
                {
                    //if (lastMousePos != Input.mousePosition)
                    //{
                        //Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(lastMousePos);
                        //offset = Camera.main.ScreenToWorldPoint(offset);
                        //SelectedObject.transform.position = SelectedObject.transform.position + offset;
                        
                    //}
                }
            }

            if (SelectedObject)
            {
                screenPoint = Camera.main.WorldToScreenPoint(SelectedObject.transform.position);
                offset = SelectedObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }
        if (Input.GetMouseButtonUp(0)
            && SelectedObject)
        {
            //deselect(SelectedObject);
        }

        if (Input.GetMouseButton(0)
            && SelectedObject) //dragging object
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            SelectedObject.transform.position = cursorPosition;
        }

        lastMouseDown = Input.GetMouseButton(0);
        lastMousePos = Input.mousePosition;
    }

    private GameObject getRoomObjectAtMouse()
    {
        Debug.Log("Mouse click at " + Camera.main.ScreenPointToRay(Input.mousePosition));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Name = " + hit.collider.name);
            Debug.Log("Tag = " + hit.collider.tag);
            Debug.Log("Hit Point = " + hit.point);
            Debug.Log("Object position = " + hit.collider.gameObject.transform.position);

            if (!hit.transform.CompareTag("RoomObject"))
            {
                return FindParentWithTag(hit.transform.gameObject, "RoomObject");
            }

            return hit.transform.gameObject;
        }

        return null;
    }

    private void deselect(GameObject obj)
    {
        MeshRenderer[] renderers = SelectedObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            r.material.color = LastColor;
        }

        SelectedObject = null;
    }

    private void select(GameObject obj)
    {
        SelectedObject = obj;
        MeshRenderer[] renderers = SelectedObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            LastColor = r.material.color;
            r.material.color = SelectedColor;
        }
    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag)
    {
        Transform t = childObject.transform;
        while (t.parent != null)
        {
            if (t.parent.tag == tag)
            {
                return t.parent.gameObject;
            }
            t = t.parent.transform;
        }
        return null; // Could not find a parent with given tag.
    }
}
