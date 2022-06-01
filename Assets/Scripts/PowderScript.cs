using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PowderScript : MonoBehaviour
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
        Vector3Int thisPos = map.WorldToCell(transform.position);
        Vector3Int currentpos = thisPos;
        currentpos.y -= 1;
        if (map.GetTile(currentpos) == null) //down
        {
            TileUtilities.MoveTile(thisPos, currentpos);
            return;
        }
        else if(map.GetInstantiatedObject(currentpos).GetComponent<ElementScript>().density < eScript.density && map.GetInstantiatedObject(currentpos).GetComponent<ElementScript>().moveable) //check if density is lower
        {
            TileUtilities.SwapTiles(map.WorldToCell(transform.position), currentpos);
            return;
        }
        currentpos.x += 1;
        if (map.GetTile(currentpos) == null && map.GetTile(currentpos - new Vector3Int(2,0,0)) == null)
        {
            currentpos.x -= 1;
            currentpos.y += 1;
            int Randomizor = Random.Range(0, 2);
            Randomizor *= 2;
            Randomizor -= 1;
            //map.SetTile(currentpos, null);
            currentpos.y -= 1;
            currentpos.x += Randomizor;
            //map.SetTile(currentpos, tile);
            TileUtilities.MoveTile(thisPos, currentpos);
            return;

        }
        else
        {
            if (map.GetTile(currentpos) == null) //right
            {
                TileUtilities.MoveTile(thisPos, currentpos);
                return;
            }
            else if (map.GetInstantiatedObject(currentpos).GetComponent<ElementScript>().density < eScript.density && map.GetInstantiatedObject(currentpos).GetComponent<ElementScript>().moveable) //check if density is lower
            {
                TileUtilities.SwapTiles(map.WorldToCell(transform.position), currentpos);
                return;
            }
            currentpos.x -= 2;
            if (map.GetTile(currentpos) == null ) //left
            {
                TileUtilities.MoveTile(thisPos, currentpos);
                return;
            }
            else if (map.GetInstantiatedObject(currentpos).GetComponent<ElementScript>().density < eScript.density && map.GetInstantiatedObject(currentpos).GetComponent<ElementScript>().moveable) //check if density is lower
            {
                TileUtilities.SwapTiles(map.WorldToCell(transform.position), currentpos);
                return;
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateTile();

    }
}
