using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ElementScript : MonoBehaviour
{
    Tilemap map;
    public RuleTile tileType;
    public List<float> valuesToTransfer = new List<float>();
    public bool moveable;
    public float density;
    public float temp;
    public float transferRate = 0.5f;
    //only vertical
    public static int bound = 200;
    
    // Start is called before the first frame update
    void Start()
    {
        map = transform.parent.gameObject.GetComponent<Tilemap>();
        Vector3Int pos = map.WorldToCell(transform.position);
        if (pos.y > bound || pos.y < bound * -1)
        {
            transform.parent.gameObject.GetComponent<RefForCreateDestroy>().toAccess.CollectTile(pos, null);
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3Int pos = map.WorldToCell(transform.position);

        if (transferRate != 0)
        {
            List<Vector3Int> nexts = new List<Vector3Int>();
            //Vector3Int pos = map.WorldToCell(transform.position);
            pos.y += 1;
            if (map.GetTile(pos) != null)
            {
                nexts.Add(pos);
            }
            pos.y -= 1;
            pos.x -= 1;
            if (map.GetTile(pos) != null)
            {
                nexts.Add(pos);
            }
            pos.x += 2;
            if (map.GetTile(pos) != null)
            {
                nexts.Add(pos);
            }
            pos.x -= 1;
            pos.y -= 1;
            if (map.GetTile(pos) != null)
            {
                nexts.Add(pos);
            }
            pos.y += 1;


            foreach (Vector3Int v3 in nexts)
            {
                ElementScript otherE = map.GetInstantiatedObject(v3).GetComponent<ElementScript>();
                if(otherE.transferRate != 0)
                {
                    float diff = temp - otherE.temp;
                    diff *= transferRate;
                    otherE.temp += diff;
                    temp -= diff;

                }
            }


        }
    }

    

}
