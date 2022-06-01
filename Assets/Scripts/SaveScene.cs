using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.Tilemaps;

[System.Serializable]
public class SaveScene : MonoBehaviour
{
    public Tilemap map;

    public string foldername = "";
    public TMP_InputField getfoldername;
    public string json;
    public string openScene = "";
    public DrawScript toElementSave;
    public RuleTile Creator;
    public RuleTile Collector;
    [System.Serializable]
    public struct CreateJSON
    {
        public RuleTile type;
        public Vector3Int pos;
        public List<float> TransferValues;
        public float heat;
    }
    //also need to save any extra variables that will need to be saved whenever they are added for example like pH, radiation, electricity, etc if they are added.
    //probably need to have inclusion of amounts for the factory aspect whenever that happens

    [SerializeField]
    public struct ListContainer
    {
        //from : https://www.reddit.com/r/Unity3D/comments/bbzzab/serializing_a_list/
        public List<CreateJSON> lists;
        public ListContainer(List<CreateJSON> _lists)
        {
            lists = _lists;
        }
    }
    
    [SerializeField]
    public struct ElementListContainer
    {
        public List<DrawScript.Element> lists;
        public ElementListContainer(List<DrawScript.Element> _lists)
        {
            lists = _lists;
        }
    }

    [System.Serializable]
    public struct setCreatorBlock
    {
        public Vector3Int pos;
        public RuleTile held;
    }

    [SerializeField]
    public struct CreatorBlockList
    {
        public List<setCreatorBlock> list;
        public CreatorBlockList(List<setCreatorBlock> _lists)
        {
            list = _lists;
        }
    }

    [System.Serializable]
    public struct setCollectorBlock
    {
        public Vector3Int pos;
        public RuleTile tocollect;
    }

    [SerializeField]
    public struct CollectorBlockList
    {
        public List<setCollectorBlock> lists;
            public CollectorBlockList(List<setCollectorBlock> _lists)
            {
                lists = _lists;
            }
    }


    private void Start()
    {
        openScene = LoadScene.foldername;
    }

    /*public void savefile()
    {
        //filepath to the folder where everything is saved is Application.persistentDataPath

        //from : https://answers.unity.com/questions/16433/get-list-of-all-files-in-a-directory.html
        string[] files = Directory.GetFiles(Application.persistentDataPath);
        foreach (string filen in files)
        {
            //taken from : https://www.techiedelight.com/remove-extension-from-file-name-csharp/
            Debug.Log(System.IO.Path.GetFileNameWithoutExtension(filen));
            //gives the file names of all of the stuff in the application.persistentdatapath folder
            //used for outputting all the saved "scenes"
        }
    }
    */

    public void Save()
    {
        if (foldername != "")
        {
            ElementtoJson();
            if (Directory.Exists(Application.persistentDataPath + foldername))
            {
                File.Delete(Application.persistentDataPath + foldername + "/creators.json");
                File.Delete(Application.persistentDataPath + openScene + "/tiles.json");
                File.Delete(Application.persistentDataPath + foldername + "/collectors.json");
            }
            else
                Directory.CreateDirectory(Application.persistentDataPath + foldername);
            //File.Delete(Application.persistentDataPath + foldername + "/creators.json");
            json = JSONSaveScene();
            //from : https://prasetion.medium.com/saving-data-as-json-in-unity-4419042d1334
            //File.Delete(Application.persistentDataPath + foldername + "/tiles.json");
            File.WriteAllText(Application.persistentDataPath + foldername + "/tiles.json", json);
                //need to save the values of the resources
        }
        else if (openScene!="")
        {
            ElementtoJson();
            if (Directory.Exists(Application.persistentDataPath + foldername))
            {
                File.Delete(Application.persistentDataPath + foldername + "/creators.json");
                File.Delete(Application.persistentDataPath + openScene + "/tiles.json");
                File.Delete(Application.persistentDataPath + foldername + "/collectors.json");
            }
            else
                Directory.CreateDirectory(Application.persistentDataPath + foldername);
            json = JSONSaveScene();
            
            File.WriteAllText(Application.persistentDataPath + openScene + "/tiles.json", json);
        }
        else
        {
            SaveAs();
        }
    }

    public void SaveAs()
    {
        if (getfoldername.text != "")
        {
            foldername = "/" + getfoldername.text;
            if (!Directory.Exists(Application.persistentDataPath + foldername))
            {
                ElementtoJson();
                if (openScene != "")
                {
                    Directory.Delete(Application.persistentDataPath + openScene, true);
                }
                openScene = foldername;
                Directory.CreateDirectory(Application.persistentDataPath + foldername);
                json = JSONSaveScene();
                //from : https://prasetion.medium.com/saving-data-as-json-in-unity-4419042d1334
                File.WriteAllText(Application.persistentDataPath + foldername + "/tiles.json", json);
                //need to save the values of the resources
            }
            else
            {
                ElementtoJson();
                if (openScene != "")
                {
                    Directory.Delete(Application.persistentDataPath + openScene, true);
                }
                openScene = foldername;
                File.Delete(Application.persistentDataPath + foldername + "/tiles.json");
                File.Delete(Application.persistentDataPath + foldername + "/creators.json");
                File.Delete(Application.persistentDataPath + foldername + "/collectors.json");
                json = JSONSaveScene();
                File.WriteAllText(Application.persistentDataPath + foldername + "/tiles.json", json);
                //replace over it?
            }
        }
        else
        {

        }
    }

    public string JSONSaveScene()
    {
        List<CreateJSON> theScene = new List<CreateJSON>();
        CreateJSON tile = new CreateJSON();
        List<setCreatorBlock> Creators = new List<setCreatorBlock>();
        setCreatorBlock creator = new setCreatorBlock();
        List<setCollectorBlock> Collectors = new List<setCollectorBlock>();
        setCollectorBlock collector = new setCollectorBlock();
        foreach (Transform child in map.transform)
        {
            tile.type = child.GetComponent<ElementScript>().tileType;
            tile.pos = Vector3Int.zero;
            tile.pos = map.WorldToCell(child.transform.position);
            tile.TransferValues = child.GetComponent<ElementScript>().valuesToTransfer;
            tile.heat = child.GetComponent<ElementScript>().temp;
            theScene.Add(tile);
            if (tile.type == Creator)
            {
                creator.held = child.GetComponent<createTiles>().tileCreate;
                creator.pos = tile.pos;
                Creators.Add(creator);
            }
            else if (tile.type == Collector)
            {
                collector.tocollect = child.GetComponent<CollectorScript>().tileToCollect;
                collector.pos = tile.pos;
                Collectors.Add(collector);
            }
        }
        ListContainer theContainer = new ListContainer(theScene);
        string JSONString = JsonUtility.ToJson(theContainer);
        CreatorBlockList theCreatorList = new CreatorBlockList(Creators);
        string JSONCreator = JsonUtility.ToJson(theCreatorList);
        File.WriteAllText(Application.persistentDataPath + foldername + "/creators.json", JSONCreator);
        CollectorBlockList theCollectorList = new CollectorBlockList(Collectors);
        string JSONCollector = JsonUtility.ToJson(theCollectorList);
        File.WriteAllText(Application.persistentDataPath + foldername + "/collectors.json", JSONCollector);
        return JSONString;
    }

    public void ElementtoJson()
    {
        List<DrawScript.Element> toHold = toElementSave.elements;
        ElementListContainer theContainer = new ElementListContainer(toHold);
        string JSONString = JsonUtility.ToJson(theContainer);
        PlayerPrefs.SetString("Elements", JSONString);
        PlayerPrefs.Save();
    }

    public void ClearMap()
    {
        map.ClearAllTiles();
    }
}



