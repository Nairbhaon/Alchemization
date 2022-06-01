using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class DrawScript : MonoBehaviour
{
    // Colection mechanic
    [System.Serializable]
    public class Element
    {
        public string displayName;
        public RuleTile tile;
        public float elementCount;

        public Element(string name, RuleTile ruleTile, float count)
        {
            displayName = name;
            tile = ruleTile;
            elementCount = count;
        }
    }

    public List<Element> elements;

    public RuleTile currentTile;
    public Tilemap map;
    public int tool;
    public int toolSize = 2;
    public int maxSize = 20;
    public int minSize = 2;
    Camera cam;
    public Color lineColor = Color.magenta;
    public float lineWidth = .1f;
    public LineRenderer lineRenderer;
    public TextMeshProUGUI toolSizeText;

    Vector2 mousePos;
    Vector2 lineP1;
    Vector2 lineP2;
    bool makingLine;
    bool canDraw = true;
    public TileBase creator;
    public TileBase collector;
    public ButtonSelectScript buttonSelectScript;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<Camera>();
        lineRenderer.startColor = lineColor;
        lineRenderer.startColor = lineColor;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 0;

        buttonSelectScript = FindObjectOfType<ButtonSelectScript>();
        foreach (ButtonSelectScript.ElementButton button in buttonSelectScript.buttons)
        {
            elements.Add(new Element(button.displayName, button.tile, button.startingAmount));
            
        }

        if (PlayerPrefs.GetString("Elements", "") != "")
        {
            string json = PlayerPrefs.GetString("Elements");
            SaveScene.ElementListContainer thecontainer = JsonUtility.FromJson<SaveScene.ElementListContainer>(json);
            foreach (DrawScript.Element x in thecontainer.lists)
            {
                foreach (DrawScript.Element y in elements)
                {
                    if (x.displayName == y.displayName)
                    {
                        y.elementCount = x.elementCount;
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        toolSizeText.text = "Tool Size:\n" + toolSize;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pos = map.WorldToCell(mousePos);
        
        // Place tiles
        if (Input.GetMouseButton(0) && canDraw)
        {
            switch (tool)
            {
                case 0:

                    //point
                    if (map.GetTile(pos) == null)
                        //map.SetTile(pos, currentTile);
                        PlaceTile(pos, currentTile);
                    else if (map.GetTile(pos) == creator && currentTile != creator)
                        map.GetInstantiatedObject(pos).GetComponent<createTiles>().tileCreate = currentTile;
                    else if (map.GetTile(pos) == collector && currentTile != collector)
                        map.GetInstantiatedObject(pos).GetComponent<CollectorScript>().tileToCollect = currentTile;
                    break;
                case 1:
                    //line


                    break;
                case 2:
                    //circle
                    createCircle(pos,currentTile);

                    break;
                case 3:
                    //square
                    createSquare(pos, currentTile);
                    break;

            }
            // Placement of tiles
            
        }
        else if (Input.GetMouseButton(1)) // Remove tiles
        {
            switch (tool)
            {
                case 0:
                    //maybe way to just erase the created tile for the creator rather than erasing the creator?
                    //issue is that this will probably trigger twice b/c people are slow
                    //map.SetTile(pos, null);
                    CollectTile(pos, null);
                    break;
                case 2:
                    createCircle(pos, null);
                    break;
                case 3:
                    createSquare(pos, null);
                    break;
            }
            // Erasing tiles
            
        }

        //line
        if (tool == 1 && canDraw)
        {
            if (makingLine)
            {
                lineRenderer.SetPosition(1, mousePos);
            }

            if (Input.GetMouseButtonDown(0))
            {
                lineP1 = mousePos;
                makingLine = true;


                lineRenderer.positionCount = 2;
                var points = new Vector3[2];
                points[0] = lineP1;
                points[1] = mousePos;
                lineRenderer.SetPositions(points);

            }
            else if(makingLine && Input.GetMouseButtonUp(0))
            {
                lineP2 = mousePos;
                Vector2 lineDiff = lineP2 - lineP1;
                float lineLength = lineDiff.magnitude;
                for(int i = 0; i < lineLength/ map.transform.parent.localScale.x; i++)
                {
                    PlaceTile(map.WorldToCell(lineP1 + ((lineDiff.normalized*map.transform.parent.localScale.x) * i)), currentTile);
                }
                lineRenderer.positionCount = 0;
                makingLine = false;

            }
            
        }

        
    }

    public void EnableDraw(bool enabled)
    {
        canDraw = enabled;
    }

    public bool CollectTile(Vector3Int pos, RuleTile tile) // deletes tile and adds it to the inventory count
    {
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].tile == map.GetTile(pos))
            {
                if (map.GetInstantiatedObject(pos).TryGetComponent(out FluidScript fl))
                {
                    elements[i].elementCount += fl.amount;
                }
                else
                {
                    elements[i].elementCount += 1;
                }
                map.SetTile(pos, tile);
                return true;
            }
        }
        //do this so that you can delete tiles that aren't in element
        map.SetTile(pos, tile);
        return false;
        
    }
    public bool PlaceTile(Vector3Int pos, RuleTile tile) // Placed tile and subtracts it from the inventory count
    {
        
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].tile == tile)
            {
                if (elements[i].elementCount > 0)
                {
                    elements[i].elementCount -= 1;
                    map.SetTile(pos, tile);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        map.SetTile(pos, tile);
        return false;
    }

    public void SetCurrentTile(RuleTile inTile)
    {
        currentTile = inTile; 

    }

    public void SetTool(int t)
    {
        tool = t;
    }


    public void createCircle(Vector3Int position, RuleTile placeErase)
    {
        for (int i= -toolSize; i<toolSize; i++)
        {
            for (int j= -toolSize; j<toolSize; j++)
            {
                if(Mathf.Sqrt((i*i) + (j * j)) < toolSize)
                {
                    if (map.GetTile(position + new Vector3Int(i,j,0)) == null)
                    {
                        PlaceTile(position + new Vector3Int(i, j, 0), placeErase);
                    }
                    else if (placeErase == null)
                    {
                        CollectTile(position + new Vector3Int(i, j, 0), placeErase);
                    }
                }
            }
        }
    }

    public void createSquare(Vector3Int position, RuleTile placeErase)
    {
        for (int i = -toolSize+1; i < toolSize; i++)
        {
            for (int j = -toolSize+1; j < toolSize; j++)
            {
                    if (map.GetTile(position + new Vector3Int(i, j, 0)) == null)
                    {
                        PlaceTile(position + new Vector3Int(i, j, 0), placeErase);
                    }
                    else if (placeErase == null)
                    {
                        CollectTile(position + new Vector3Int(i, j, 0), placeErase);
                    }
             
            }
        }
    }

    public void increaseToolSize()
    {
        if (toolSize<maxSize)
        toolSize++;
    }

    public void decreaseToolSize()
    {
        if (toolSize>minSize)
        toolSize--;
    }

}
