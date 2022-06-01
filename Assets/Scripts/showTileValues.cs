using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class showTileValues : MonoBehaviour
{
    public Tilemap map;
    public TextMeshProUGUI selfText;
    public Camera cam;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        selfText = GetComponent<TextMeshProUGUI>();
        cam = Camera.main;
    }
        
    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pos = map.WorldToCell(mousePos);
        if (map.GetTile(pos) != null)
        {
            selfText.text = "Tile Type : " + map.GetTile(pos).name + "\nPosition : (" + pos.x + ", " + pos.y + ")\nHeat : " + map.GetInstantiatedObject(pos).GetComponent<ElementScript>().temp;
            if (map.GetInstantiatedObject(pos).GetComponent<ElementScript>().valuesToTransfer.Count != 0)
                selfText.text += "\nAmount : " + map.GetInstantiatedObject(pos).GetComponent<ElementScript>().valuesToTransfer[0];
            else
                selfText.text += "\nAmount : N/A";
            if (map.GetTile(pos).name == "Creator")
                selfText.text += "\nSelected Block : " + map.GetInstantiatedObject(pos).GetComponent<createTiles>().tileCreate.name;
            else if (map.GetTile(pos).name == "Collector")
                selfText.text += "\nSelected Block : " + map.GetInstantiatedObject(pos).GetComponent<CollectorScript>().tileToCollect.name;
        }
        else
            selfText.text = "Tile Type : None\nPosition : (" + pos.x + ", " + pos.y + ")\nHeat : N/A\nAmount : 0.0";
    }
}
