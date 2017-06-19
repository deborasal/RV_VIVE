using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class properties : MonoBehaviour
{
    public int size;
    public bool touched;
    public bool beingCarried;

    private void Start()
    {
        beingCarried = false;
        touched = false;
    }

    public int GetSize()
    {
        return size;
    }

    public bool GetTouched()
    {
        bool aux = touched;
        touched = false;
        return aux;
    }

    public void SetBeingCarried(bool value)
    {
        beingCarried = value;
        touched = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        touched = true;
    }
}
