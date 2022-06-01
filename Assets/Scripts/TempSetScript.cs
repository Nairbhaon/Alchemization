using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSetScript : MonoBehaviour
{
    public float targetTemp;
    ElementScript eScript;
    // Start is called before the first frame update
    void Start()
    {
        eScript = GetComponent<ElementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        eScript.temp = targetTemp;
    }
}
