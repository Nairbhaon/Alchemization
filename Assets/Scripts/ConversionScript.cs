using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConversionScript : MonoBehaviour
{
    public List<RuleTile> outcomes;
    public List<TileBase> conditions;
    public bool tempReq = false;
    public float tempMin = 0;
    public float tempMax = 0;
    public bool valueReq = false;
    public int valueIndex;
    public float valueMin;
    public float valueMax;

    ElementScript eScript;
    Tilemap map;
    
    
    // Start is called before the first frame update
    void Start()
    {
        map = transform.parent.gameObject.GetComponent<Tilemap>();
        eScript = map.GetInstantiatedObject(map.WorldToCell(transform.position)).GetComponent<ElementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int pos = map.WorldToCell(transform.position);
        bool conCheck = true;
        if (conditions.Count > 0)
        {
            
            List<TileBase> surrounds = new List<TileBase>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        surrounds.Add(map.GetTile(pos + new Vector3Int(i, j, 0)));
                    }
                }
            }
            
            bool found;
            List<TileBase> aval = surrounds;
            foreach (TileBase t in conditions)
            {
                found = false;
                for(int i = 0; i < aval.Count; i++)
                {
                    if(aval[i] == t)
                    {
                        found = true;
                        aval.Remove(aval[i]);
                        break;
                    }
                }
                
                if (!found)
                {
                    conCheck = false;
                    break;
                }
            }
        }

        if (tempReq)
        {
            if (eScript.temp < tempMin || eScript.temp > tempMax) conCheck = false;
        }
        if (valueReq)
        {
            if (eScript.valuesToTransfer[valueIndex] < valueMin || eScript.valuesToTransfer[valueIndex] > valueMax) conCheck = false;
        }

        if (conCheck)
        {
            //Debug.Log("converted");
            TileUtilities.MoveTile(pos, pos, outcomes[Random.Range(0,outcomes.Count)]);
            //map.SetTile(pos, outcome);
        }
    }
}
