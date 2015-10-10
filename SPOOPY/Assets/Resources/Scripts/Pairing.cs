using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum FurnatureType
//{
//    Desk,
//    Bed,
//    BunkBed,
//    FilingCabinet
//}

public class Pairing : MonoBehaviour
{
    //public static Dictionary<KeyValuePair<FurnatureType, FurnatureType>, int> PairingStrengths = new Dictionary<KeyValuePair<FurnatureType, FurnatureType>, int>()
    //{
    //    {new KeyValuePair<FurnatureType, FurnatureType>(FurnatureType.Desk, FurnatureType.FilingCabinet), 5},
    //    {new KeyValuePair<FurnatureType, FurnatureType>(FurnatureType.Desk, FurnatureType.Bed), 2},
    //    {new KeyValuePair<FurnatureType, FurnatureType>(FurnatureType.FilingCabinet, FurnatureType.Bed), 2},
    //    {new KeyValuePair<FurnatureType, FurnatureType>(FurnatureType.Desk, FurnatureType.BunkBed), 1},
    //    {new KeyValuePair<FurnatureType, FurnatureType>(FurnatureType.Bed, FurnatureType.Bed), 1},
    //};

    //public FurnatureType FurnatureType;
    //public bool StrongObject; //check parings first
    //public List<GameObject> StrongPairs; //first round pairings
    //public List<GameObject> WeakPairs; //second round pairings
    //public List<GameObject> CurrentPairs; //pairs made by program
    //private GameObject room;
    //// Use this for initialization
    //void Start()
    //{
    //    GameObject room = GameObject.FindGameObjectWithTag("Room");
    //    if (!room)
    //        Debug.LogWarning(this.name + " - Failed to locate Room!");

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //public int GetStrength(GameObject other)
    //{
    //    return PairingStrengths.
    //}

    //public List<GameObject> GetStrongPairsInScene()
    //{
    //    GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("RoomObject");
    //    return getPairs(StrongPairs, roomObjects);
    //}

    //public List<GameObject> GetWeakPairsInScene()
    //{
    //    GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("RoomObject");
    //    return getPairs(WeakPairs, roomObjects);
    //}

    //public List<GameObject> GetPairsInScene()
    //{
    //    GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("RoomObject");
    //    List<GameObject> pairs = new List<GameObject>();
    //    pairs.AddRange(getPairs(StrongPairs, roomObjects));
    //    pairs.AddRange(getPairs(WeakPairs, roomObjects));
    //    return pairs;
    //}

    //private List<GameObject> getPairs(List<GameObject> baseList, GameObject[] roomObjects)
    //{
    //    List<GameObject> pairs = new List<GameObject>();
    //    foreach (GameObject obj in roomObjects)
    //    {
    //        if (obj != this.gameObject
    //            && baseList.Contains(obj))
    //        {
    //            //TODO: Sort by strength?
    //            pairs.Add(obj);
    //        }
    //    }

    //    return pairs;
    //}

}
