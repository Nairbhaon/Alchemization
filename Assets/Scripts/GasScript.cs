using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GasScript : MonoBehaviour
{
    Tilemap map;
    ElementScript eScript;
    RuleTile tile;
    // Start is called before the first frame update
    void Start()
    {
        map = transform.parent.gameObject.GetComponent<Tilemap>();
        eScript = GetComponent<ElementScript>();
        tile = eScript.tileType;
    }

    void UpdateTile()
    {
        List<Vector3Int> valids = new List<Vector3Int>();
        Vector3Int pos = map.WorldToCell(transform.position);
        pos.y += 1;
        if(map.GetTile(pos) == null)
        {
            valids.Add(pos);
        }
        pos.y -= 1;
        pos.x -= 1;
        if (map.GetTile(pos) == null)
        {
            valids.Add(pos);
        }
        pos.x += 2;
        if (map.GetTile(pos) == null)
        {
            valids.Add(pos);
        }
        pos.y += 1;
        if (map.GetTile(pos) == null)
        {
            valids.Add(pos);
        }
        pos.x -= 2;
        if (map.GetTile(pos) == null)
        {
            valids.Add(pos);
        }
        pos.x += 1;
        pos.y -= 1;

        if (valids.Count != 0)
        {
            Vector3Int moveTo = valids[Random.Range(0, valids.Count)];
            TileUtilities.MoveTile(pos, moveTo);
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTile();

    }
}
