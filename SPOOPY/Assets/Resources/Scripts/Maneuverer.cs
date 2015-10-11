﻿using UnityEngine;
using System.Collections;
//using UnityEditor;

public class Maneuverer : MonoBehaviour
{
    public Material SelectedMaterial;
    //public Material LastMaterial;
    public float ScrollSpeed;
    public float RotateSpeed;
    public float RotateSnapThreshold;
    public float RotateSnapDegrees;
    public GameObject SelectedObject;

    private Vector3 screenPoint;
    private Vector3 offset;

    private Vector3 lastMousePos;
    private float rot;
    // Use this for initialization
    void Start()
    {
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
                if (frameSelectedObj)
                    Select(frameSelectedObj);
            }
            else
            {
                if (frameSelectedObj
                    && SelectedObject != frameSelectedObj) //clicked on a new object
                {
                    Deselect(SelectedObject);
                    Select(frameSelectedObj);
                }
                else if (!frameSelectedObj
                    && SelectedObject) //clicked on nothing
                {
                    Deselect(SelectedObject);
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

        if (SelectedObject)
        {
            if (Input.GetMouseButton(0)) //dragging object
            {
                Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
                SelectedObject.transform.position = cursorPosition;
            }

            if (Input.GetMouseButton(2)) //rotating object
            {
                if (!Input.GetKey(KeyCode.LeftControl))
                    SelectedObject.transform.Rotate(0, Input.GetAxis("Mouse X") * RotateSpeed, 0);
                else if (Mathf.Abs(rot) > RotateSnapThreshold)
                {
                    SelectedObject.transform.Rotate(0, RotateSnapDegrees * Mathf.Sign(rot), 0);
                    rot = 0;
                }

                rot += Input.GetAxis("Mouse X");
            }
            else
            {
                rot = 0;
            }

            if (Input.GetKeyDown(KeyCode.R)) //reset rotation
            {
                //SelectedObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                DefaultsManager defaultsMan = SelectedObject.GetComponent<DefaultsManager>();
                if (defaultsMan)
                {
                    defaultsMan.SetDefaultRotation();
                }
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                GameObject obj = SelectedObject;
                Deselect(SelectedObject);
                Destroy(obj);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                GameObject sel = SelectedObject;
                Deselect(SelectedObject);
                GameObject obj = Instantiate(sel);
                obj.transform.position += obj.transform.forward;
                obj.transform.position += obj.transform.up;
                obj.GetComponent<DefaultsManager>().Initialize();
                Select(obj);
            }
        }

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

    public void Deselect(GameObject obj)
    {
        //MeshRenderer[] renderers = SelectedObject.GetComponentsInChildren<MeshRenderer>();
        //foreach (MeshRenderer r in renderers)
        //{
        //    r.material = LastMaterial;
        //}
        DefaultsManager defaultsMan = SelectedObject.GetComponent<DefaultsManager>();
        if (defaultsMan)
        {
            defaultsMan.SetDefaultMaterial();
        }

        SelectedObject.GetComponent<Rigidbody>().isKinematic = false;
        SelectedObject.GetComponent<Rigidbody>().detectCollisions = true;
        SelectedObject = null;
    }

    public void Select(GameObject obj)
    {
        SelectedObject = obj;
        SelectedObject.GetComponent<DefaultsManager>().SetMaterial(SelectedMaterial);
        SelectedObject.GetComponent<Rigidbody>().isKinematic = true;
        SelectedObject.GetComponent<Rigidbody>().detectCollisions = false;
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
