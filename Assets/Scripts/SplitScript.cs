using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SplitScript : MonoBehaviour
{
    public RuleTile thisTile;
    public List<RuleTile> otherTiles;
    public float tempMin;

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
        if (eScript.temp < tempMin) return;

        Vector3Int pos = map.WorldToCell(transform.position);
        List<Vector3Int> spots = new List<Vector3Int>();
        
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i != 0 || j != 0) //don't count self
                {
                    Vector3Int currentPos = pos + new Vector3Int(i, j, 0);
                    
                    if(map.GetTile(currentPos) == null)
                    {
                        spots.Add(currentPos);
                    }
                }
            }
        }

        if (spots.Count < otherTiles.Count) return; //not enough space


        for(int i = 0; i < otherTiles.Count; i++)
        {
            int chosen = Random.Range(0, spots.Count);
            map.SetTile(spots[chosen], otherTiles[i]);
            map.GetInstantiatedObject(spots[chosen]).GetComponent<ElementScript>().temp = eScript.temp;
            spots.RemoveAt(chosen);
        }
        TileUtilities.MoveTile(pos, pos, thisTile);


    }
}
