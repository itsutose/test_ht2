using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class front : MonoBehaviour
{
    public GameObject myObject; // The object to bring to the front

    void Start()
    {
        var renderer = myObject.GetComponent<Renderer>();
        renderer.sortingOrder = 5000; // Set a high enough value
    }
}
