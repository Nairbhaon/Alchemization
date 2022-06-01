using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CombineScript : MonoBehaviour
{
    public RuleTile outcome;
    public List<TileBase> conditions;
    public bool tempReq = false;
    public float tempMin = 0;
    public float tempMax = 0;

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

        if (tempReq && (eScript.temp > tempMax || eScript.temp < tempMin)) return;

        Vector3Int pos = map.WorldToCell(transform.position);
        List<List<Vector3Int>> found = new List<List<Vector3Int>>();
        for(int i = 0; i < conditions.Count; i++)
        {
            found.Add(new List<Vector3Int>());
        }
        for(int i = -1; i < 2; i++)
        {
            for(int j = -1; j < 2; j++)
            {
                if(i != 0 || j != 0) //don't count self
                {
                    Vector3Int currentPos = pos + new Vector3Int(i, j, 0);
                    TileBase t = map.GetTile(currentPos);
                    for(int k = 0; k < conditions.Count; k++)
                    {
                        if(conditions[k] == t)
                        {
                            if (map.GetInstantiatedObject(currentPos).GetComponent<ElementScript>().valuesToTransfer.Count > 0)
                            {
                                if (map.GetInstantiatedObject(currentPos).GetComponent<ElementScript>().valuesToTransfer[0] >= 1)
                                {
                                    found[k].Add(currentPos);
                                }
                            }
                            else
                            {
                                found[k].Add(currentPos);
                            }
                        }
                    }
                }
            }
        }

        //make sure all conditions are met
        bool met = true;
        for(int i = 0; i < conditions.Count; i++)
        {
            if(found[i].Count < 1) //no element found
            {
                met = false;
            }
        }

        if (met)
        {
            float totalTemp = 0;
            for(int i = 0; i < conditions.Count; i++)
            {
                int chosen = Random.Range(0, conditions.Count);
                totalTemp += map.GetInstantiatedObject(found[i][chosen]).GetComponent<ElementScript>().temp;
                map.SetTile(found[i][chosen], null);
            }
            totalTemp += eScript.temp;
            totalTemp /= conditions.Count + 1;
            eScript.temp = totalTemp;
            TileUtilities.MoveTile(pos, pos, outcome);
        }



    }
}
