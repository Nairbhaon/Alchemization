using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectorScript : MonoBehaviour
{
    public RuleTile tileToCollect;
    public Tilemap map;
    public DrawScript tocollect;
    public RuleTile collectorTile;
    // Start is called before the first frame update
    void Start()
    {
        tocollect = GetComponentInParent<RefForCreateDestroy>().toAccess;
        map = gameObject.GetComponentInParent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3Int cellpos = map.WorldToCell(gameObject.transform.position);
        //goes clockwise in checking
        //comment the elses if all tiles should be created at the same time
        if (map.GetTile(cellpos + new Vector3Int(0, 1, 0)) == tileToCollect)
        {
            tocollect.CollectTile(cellpos + new Vector3Int(0, 1, 0), null);
            //map.SetTile(cellpos + new Vector3Int(0, 1, 0), null);
        }
        if (map.GetTile(cellpos + new Vector3Int(1, 0, 0)) == tileToCollect)
        {
            tocollect.CollectTile(cellpos + new Vector3Int(1, 0, 0), null);
            //map.SetTile(cellpos + new Vector3Int(1, 0, 0), null);
        }
        if (map.GetTile(cellpos + new Vector3Int(0, -1, 0)) == tileToCollect)
        {
            tocollect.CollectTile(cellpos + new Vector3Int(0, -1, 0), null);
            //map.SetTile(cellpos + new Vector3Int(0, -1, 0), null);
        }
        if (map.GetTile(cellpos + new Vector3Int(-1, 0, 0)) == tileToCollect)
        {
            tocollect.CollectTile(cellpos + new Vector3Int(-1, 0, 0), null);
            //map.SetTile(cellpos + new Vector3Int(-1, 0, 0), null);
        }
        //need to add the stuff to the element numbers
    }
}
