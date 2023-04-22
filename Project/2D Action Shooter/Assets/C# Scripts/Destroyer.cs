using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float lifeTIme;
    private void Start()
    {
        Destroy(gameObject, lifeTIme);
    }

}
