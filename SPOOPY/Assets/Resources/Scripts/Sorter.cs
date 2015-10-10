using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class Sorter : MonoBehaviour
{
    //List<GameObject> roomObjects;
    //// Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public void DoPairing()
    //{
    //    roomObjects = GameObject.FindGameObjectsWithTag("RoomObject").ToList();

    //    List<GameObject> strongObjects = (List<GameObject>)(
    //        from obj in roomObjects 
    //        where obj.GetComponent<Pairing>().StrongObject.Equals(true) 
    //        select obj);

    //    List<GameObject> weakObjects = (List<GameObject>)(
    //        from obj in roomObjects
    //        where obj.GetComponent<Pairing>().StrongObject.Equals(false)
    //        select obj);

    //    foreach(GameObject sobj in strongObjects)
    //    {
    //        Pairing sobjPairing = sobj.GetComponent<Pairing>();
    //        GameObject bestPair;
    //        int bestStrength;
    //        foreach (GameObject obj in roomObjects)
    //        {
    //            Pairing objPairing = obj.GetComponent<Pairing>();
    //            if (!obj.Equals(this.gameObject)
    //                && !objPairing.CurrentPairs.Contains(this.gameObject))
    //            {

    //            }
    //        }
    //    }
    //}
}
