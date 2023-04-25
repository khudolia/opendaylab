using System.Collections.Generic;
using UnityEngine;

public class ObjectResseter : MonoBehaviour
{
    private List<SavedObjectModel> objects = new ();
    
    private void Start()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();

        foreach (var rb in rigidbodies)
        {
            var o = rb.gameObject;

            if(o.CompareTag("MainCamera") || o.CompareTag("NonSave"))
               return;
            
            
            objects.Add(new SavedObjectModel(o.name, o.transform.position, o.transform.rotation, o.transform.parent));
        }
    }

    public void ResetObjects()
    {
        foreach (var o in objects)
        {
            var findObject = GameObject.Find(o.name);
            
            findObject.transform.position = o.position;
            findObject.transform.rotation = o.rotation;
            findObject.transform.parent = o.parent;
            
        }
    }
}
