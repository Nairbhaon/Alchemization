using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUtilities
{
    
    public static void SwapTiles(Vector3Int t1, Vector3Int t2)
    {
        Tilemap map;
        map = GameObject.FindObjectOfType<Tilemap>();
        RuleTile t1Tile = map.GetInstantiatedObject(t1).GetComponent<ElementScript>().tileType;
        RuleTile t2Tile = map.GetInstantiatedObject(t2).GetComponent<ElementScript>().tileType;
        List<float> t1Transfer = map.GetInstantiatedObject(t1).GetComponent<ElementScript>().valuesToTransfer;
        List<float> t2Transfer = map.GetInstantiatedObject(t2).GetComponent<ElementScript>().valuesToTransfer;
        float temp1 = map.GetInstantiatedObject(t1).GetComponent<ElementScript>().temp;
        float temp2 = map.GetInstantiatedObject(t2).GetComponent<ElementScript>().temp;
        map.SetTile(t1, t2Tile);
        if (t2Transfer != null)
        {
            ElementScript t1EScript = map.GetInstantiatedObject(t1).GetComponent<ElementScript>();
            t1EScript.valuesToTransfer = t2Transfer;
            t1EScript.temp = temp2;
        }
        map.SetTile(t2, t1Tile);
        if (t1Transfer != null)
        {
            ElementScript t2EScript = map.GetInstantiatedObject(t2).GetComponent<ElementScript>();
            t2EScript.valuesToTransfer = t1Transfer;
            t2EScript.temp = temp1;
        }

    }
    public static void MoveTile(Vector3Int startPos, Vector3Int targetPos)
    {
        Tilemap map;
        map = GameObject.FindObjectOfType<Tilemap>();
        RuleTile tileType = map.GetInstantiatedObject(startPos).GetComponent<ElementScript>().tileType;
        List<float> valuesTransfer = map.GetInstantiatedObject(startPos).GetComponent<ElementScript>().valuesToTransfer;
        float transferTemp = map.GetInstantiatedObject(startPos).GetComponent<ElementScript>().temp;
        map.SetTile(startPos, null);
        map.SetTile(targetPos, tileType);
        ElementScript newInst = map.GetInstantiatedObject(targetPos).GetComponent<ElementScript>();
        newInst.valuesToTransfer = valuesTransfer;
        newInst.temp = transferTemp;
        
    }
    public static void MoveTile(Vector3Int startPos, Vector3Int targetPos, RuleTile tile)
    {
        Tilemap map;
        map = GameObject.FindObjectOfType<Tilemap>();
        //RuleTile tileType = map.GetInstantiatedObject(startPos).GetComponent<ElementScript>().tileType;
        List<float> valuesTransfer = map.GetInstantiatedObject(startPos).GetComponent<ElementScript>().valuesToTransfer;
        float transferTemp = map.GetInstantiatedObject(startPos).GetComponent<ElementScript>().temp;
        map.SetTile(startPos, null);
        map.SetTile(targetPos, tile);
        ElementScript newInst = map.GetInstantiatedObject(targetPos).GetComponent<ElementScript>();
        newInst.valuesToTransfer = valuesTransfer;
        newInst.temp = transferTemp;

    }
}
