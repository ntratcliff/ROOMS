using UnityEngine;
using System.Collections;

public class DefaultsManager : MonoBehaviour
{
    public Quaternion Rotation;
    public Material Material;
    public bool Initialized = false;
    // Use this for initialization
    void Start()
    {
        if(!Initialized)
            Initialize();
    }

    public void Initialize()
    {
        Rotation = transform.rotation;
        MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
        if (renderer)
        {
            Material = renderer.material;
        }

        DefaultsManager[] childManagers = this.gameObject.GetComponentsInChildren<DefaultsManager>();
        foreach (DefaultsManager m in childManagers)
        {
            if (m != this.GetComponent<DefaultsManager>())
                m.Initialize();
        }

        Initialized = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetAllDefaults()
    {
        SetDefaultMaterial();
        SetDefaultRotation();
    }

    public void SetDefaultRotation()
    {
        transform.rotation = Rotation;
    }

    public void SetDefaultMaterial()
    {
        DefaultsManager[] childManagers = this.gameObject.GetComponentsInChildren<DefaultsManager>();
        foreach (DefaultsManager m in childManagers)
        {
            if (m != this.GetComponent<DefaultsManager>())
                m.SetDefaultMaterial();
        }

        MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
        if (renderer)
        {
            renderer.material = Material;
        }
    }

    public void SetMaterial(Material mat)
    {
        DefaultsManager[] childManagers = this.gameObject.GetComponentsInChildren<DefaultsManager>();
        foreach (DefaultsManager m in childManagers)
        {
            if (m != this.GetComponent<DefaultsManager>())
                m.SetMaterial(mat);
        }

        MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
        if (renderer)
        {
            renderer.material = mat;
        }
    }
}
