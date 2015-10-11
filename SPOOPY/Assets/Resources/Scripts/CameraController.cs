using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float LookSpeed;
    Maneuverer manuverer;
    // Use this for initialization
    void Start()
    {
        GameObject manuvererObject = GameObject.Find("Maneuverer");
        if (!manuvererObject)
            Debug.LogWarning("Could not find Manuverer!");
        manuverer = manuvererObject.GetComponent<Maneuverer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manuverer.SelectedObject) //no object selected
        {
            if (Input.GetMouseButton(1)) //right mouse pressed, look around
            {
                Camera.main.transform.rotation = Quaternion.Euler(
                    Camera.main.transform.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * LookSpeed,
                    Camera.main.transform.rotation.eulerAngles.y - Input.GetAxis("Mouse X") * LookSpeed,
                    Camera.main.transform.rotation.eulerAngles.z);
            }
        }
        Vector3 delta = transform.forward * Input.GetAxis("Vertical");
        delta += transform.right * Input.GetAxis("Horizontal");
        if (Input.GetAxis("Camera Y") != 0)
        {
            delta.y = Input.GetAxis("Camera Y");
        }
        transform.position += delta;
        //transform.position += new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
    }
}
