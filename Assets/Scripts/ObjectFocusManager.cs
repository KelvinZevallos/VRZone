﻿using System.Collections.Generic;
using UnityEngine;

public class ObjectFocusManager : MonoBehaviour
{
    private readonly List<ObjectFocus> objectsInRange = new List<ObjectFocus>();

    #region Singleton
    private static ObjectFocusManager _instance;
    public static ObjectFocusManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectFocusManager>();

                if (_instance == null)
                {
                    _instance = new GameObject("ObjectFocusManager : Singleton").AddComponent<ObjectFocusManager>();
                }
            }
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }
    #endregion

    #region Static Properties & Methods
    public static int Count { get { return Instance.objectsInRange.Count; } }

    public static void Add(ObjectFocus objectToAdd)
    {
        //Prevents adding more objects into the ObjectFocus List.
        if (Instance.objectsInRange.Contains(objectToAdd))
            return;

        Instance.objectsInRange.Add(objectToAdd);
    }

    public static void Remove(ObjectFocus objectToRemove)
    {
        Instance.objectsInRange.Remove(objectToRemove);
    }
    #endregion

    #region MonoBehaviours
    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        Instance = null;
    }

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

#if DEBUG
    private void OnGUI()
    {
        GUILayout.Label("Objects in Focus: " + Count.ToString());
    }
#endif
    #endregion
}
