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
        if (Input.GetMouseButtonDown(0))
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

                if (hit.transform.CompareTag("RoomObject"))
                {
                    SelectedObject = hit.transform.gameObject;
                }
                else
                {
                    SelectedObject = FindParentWithTag(hit.transform.gameObject, "RoomObject");
                }

                Debug.Log("Selected Object = " + SelectedObject.name);

                MeshRenderer[] renderers = SelectedObject.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer r in renderers)
                {
                    LastColor = r.material.color;
                    r.material.color = SelectedColor;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)
            && SelectedObject)
        {
            MeshRenderer[] renderers = SelectedObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in renderers)
            {
                r.material.color = LastColor;
            }

            SelectedObject = null;
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
