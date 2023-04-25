using System;
using UnityEngine;

public class SavedObjectModel
{
    public String name;
    public Vector3 position;
    public Quaternion rotation;
    public Transform parent;

    public SavedObjectModel(string name, Vector3 position, Quaternion rotation, Transform parent)
    {
        this.parent = parent;
        this.rotation = rotation;
        this.position = position;
        this.name = name;
    }
}