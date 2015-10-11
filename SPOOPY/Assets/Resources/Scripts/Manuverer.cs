using UnityEngine;
using System.Collections;

public class Manuverer : MonoBehaviour
{
    public Color SelectedColor = Color.yellow;
    public Color LastColor;
    public GameObject SelectedObject;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)
            && !SelectedObject) //possible click on new object
        {
            GameObject frameSelectedObj = getRoomObjectAtMouse();
            Debug.Log("Selected Object = " + frameSelectedObj.name);
            if (frameSelectedObj)
                select(frameSelectedObj);
        }
        if (Input.GetMouseButtonDown(0)
            && SelectedObject)
        {
            GameObject frameSelectedObj = getRoomObjectAtMouse();
            if(frameSelectedObj)
                Debug.Log("Selected Object = " + frameSelectedObj.name);

            if (frameSelectedObj
                && SelectedObject != frameSelectedObj)
            {
                deselect(SelectedObject);
                select(frameSelectedObj);
            }
            else if (!frameSelectedObj
                && SelectedObject)
            {
                deselect(SelectedObject);
            }
        }
        if (Input.GetMouseButtonUp(0)
            && SelectedObject)
        {
            //deselect(SelectedObject);
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
