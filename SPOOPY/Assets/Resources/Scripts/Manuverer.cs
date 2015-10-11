using UnityEngine;
using System.Collections;

public class Manuverer : MonoBehaviour
{
    public Color SelectedColor = Color.yellow;
    public Color LastColor;
    public float ScrollSpeed;
    public GameObject SelectedObject;

    private Vector3 screenPoint;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
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
            }

            if (SelectedObject)
            {
                screenPoint = Camera.main.WorldToScreenPoint(SelectedObject.transform.position);
                offset = SelectedObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }

        //Scroll Wheel Push/Pull
        if (Input.GetAxis("Mouse ScrollWheel") != 0f
            && SelectedObject)
        {
            SelectedObject.transform.position = SelectedObject.transform.position + Camera.main.transform.forward * Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;

            screenPoint = Camera.main.WorldToScreenPoint(SelectedObject.transform.position);
            offset = SelectedObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        if (Input.GetMouseButton(0)
            && SelectedObject) //dragging object
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            SelectedObject.transform.position = cursorPosition;
        }
        
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
        SelectedObject.GetComponent<Rigidbody>().isKinematic = false;
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
        SelectedObject.GetComponent<Rigidbody>().isKinematic = true;

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
