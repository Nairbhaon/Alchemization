using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

public class LoadScene : MonoBehaviour
{
    SaveScene.ListContainer held = new SaveScene.ListContainer(new List<SaveScene.CreateJSON>());
    static public string foldername = "";
    public Tilemap map;
    public DrawScript setValues;
    // Start is called before the first frame update
    void Start()
    {
        
        //currently need to add the "/" in before hand
        if(Directory.Exists(Application.persistentDataPath + foldername) && foldername!="")
        {
            map.ClearAllTiles();
            string json = File.ReadAllText(Application.persistentDataPath + foldername + "/tiles.json");
            held = JsonUtility.FromJson<SaveScene.ListContainer>(json);
            foreach (SaveScene.CreateJSON x in held.lists)
            {
                map.SetTile(x.pos, x.type);
                map.GetInstantiatedObject(x.pos).GetComponent<ElementScript>().temp = x.heat;
                map.GetInstantiatedObject(x.pos).GetComponent<ElementScript>().valuesToTransfer = x.TransferValues;
            }
            if (File.Exists(Application.persistentDataPath + foldername + "/creators.json"))
            {
                string jsonCreator = File.ReadAllText(Application.persistentDataPath + foldername + "/creators.json");
                SaveScene.CreatorBlockList hold = JsonUtility.FromJson<SaveScene.CreatorBlockList>(jsonCreator);
                if (hold.list.Count != 0)
                foreach (SaveScene.setCreatorBlock x in hold.list)
                {
                    map.GetInstantiatedObject(x.pos).GetComponent<createTiles>().tileCreate = x.held;
                }
            }
            if (File.Exists(Application.persistentDataPath + foldername + "/collectors.json"))
            {
                string jsonCollector = File.ReadAllText(Application.persistentDataPath + foldername + "/collectors.json");
                SaveScene.CollectorBlockList held = JsonUtility.FromJson<SaveScene.CollectorBlockList>(jsonCollector);
                if (held.lists.Count != 0)
                {
                    foreach(SaveScene.setCollectorBlock x in held.lists)
                    {
                        map.GetInstantiatedObject(x.pos).GetComponent<CollectorScript>().tileToCollect = x.tocollect;
                    }
                }
            }

        }

        Time.timeScale = 1;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void JSONtoElement()
    {
        string json = PlayerPrefs.GetString("Elements");
        setValues.elements = JsonUtility.FromJson<SaveScene.ElementListContainer>(json).lists;
    }
}
