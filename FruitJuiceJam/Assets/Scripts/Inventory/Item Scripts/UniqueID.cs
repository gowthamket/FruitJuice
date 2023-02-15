using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
//using System.ComponentModel;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public class UniqueID : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();

    //[SerializeField] public static SerializableDictionary<string, GameObject> idDataBase = new SerializableDictionary<string, GameObject>();

    private void Awake()
    {

    }

    private void OnDestroy()
    {

    }

    private void Generate()
    {

    }
}



