using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class createTiles : MonoBehaviour
{
    public RuleTile tileCreate;
    public Tilemap map;
    public DrawScript tocreate;
    public RuleTile creatorTile;

    // Start is called before the first frame update
    void Start()
    {
        tocreate = GetComponentInParent<RefForCreateDestroy>().toAccess;
        map = gameObject.GetComponentInParent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (tileCreate != creatorTile)
        {
            Vector3Int cellpos = map.WorldToCell(gameObject.transform.position);
            //goes clockwise in checking
            //comment the elses if all tiles should be created at the same time
            if (map.GetTile(cellpos + new Vector3Int(0, 1, 0)) == null)
            {
                tocreate.PlaceTile(cellpos + new Vector3Int(0, 1, 0), tileCreate);
                //map.SetTile(cellpos + new Vector3Int(0, 1, 0), tileCreate);
            }
            else if (map.GetTile(cellpos + new Vector3Int(1, 0, 0)) == null)
            {
                tocreate.PlaceTile(cellpos + new Vector3Int(1, 0, 0), tileCreate);
                //map.SetTile(cellpos + new Vector3Int(1, 0, 0), tileCreate);
            }
            else if (map.GetTile(cellpos + new Vector3Int(0, -1, 0)) == null)
            {
                tocreate.PlaceTile(cellpos + new Vector3Int(0, -1, 0), tileCreate);
                //map.SetTile(cellpos + new Vector3Int(0, -1, 0), tileCreate);
            }
            else if (map.GetTile(cellpos + new Vector3Int(-1, 0, 0)) == null)
            {
                tocreate.PlaceTile(cellpos + new Vector3Int(-1, 0, 0), tileCreate);
                //map.SetTile(cellpos + new Vector3Int(-1, 0, 0), tileCreate);
            }
        }
    }
}
