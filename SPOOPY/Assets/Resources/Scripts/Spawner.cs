using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public float MaxDist;
    private Maneuverer maneuverer;
    private GameObject spawnMenu;
    // Use this for initialization
    void Start()
    {
        GameObject maneuvererObject = GameObject.Find("Maneuverer");
        if (!maneuvererObject)
            Debug.LogWarning("Could not find Manuverer!");
        maneuverer = maneuvererObject.GetComponent<Maneuverer>();

        spawnMenu = GameObject.FindGameObjectWithTag("Spawn Menu");
        if (!spawnMenu)
            Debug.LogWarning("Could not find spawn menu!");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(this.transform.position, Camera.main.transform.forward, Color.red);
        if (Input.GetKeyDown(KeyCode.Q)) //open/close spawn menu
        {
            spawnMenu.SetActive(!spawnMenu.activeSelf);
        }
    }

    public void Spawn(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        Ray ray = new Ray(this.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, MaxDist))
        {
            obj.transform.position = hit.point;
        }
        else
        {
            obj.transform.position = ray.GetPoint(MaxDist);
        }

        obj.GetComponent<DefaultsManager>().Initialize();

        if (maneuverer.SelectedObject)
        {
            maneuverer.Deselect(maneuverer.SelectedObject);
        }
        maneuverer.Select(obj);
    }
}
