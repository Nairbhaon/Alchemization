using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DecayScript : MonoBehaviour
{
    public float lifeTimeMin;
    public float lifeTimeMax;
    //eScript.valuestotransfer[1] is used for decay time
    ElementScript eScript;
    Tilemap map;
    // Start is called before the first frame update
    void Start()
    {
        map = transform.parent.gameObject.GetComponent<Tilemap>();
        eScript = GetComponent<ElementScript>();
        if(eScript.valuesToTransfer.Count == 0)
        {
            eScript.valuesToTransfer.Add(1);
        }
        if(eScript.valuesToTransfer.Count == 1)
        {
            eScript.valuesToTransfer.Add(-1);
        }
        
        if(eScript.valuesToTransfer[1] == -1)
        {
            eScript.valuesToTransfer[1] = Random.Range(lifeTimeMin, lifeTimeMax);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        eScript.valuesToTransfer[1] -= Time.deltaTime;
        if(eScript.valuesToTransfer[1] <= 0)
        {
            map.SetTile(map.WorldToCell(transform.position), null);
        }
    }
}
