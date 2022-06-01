using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneSelect : MonoBehaviour
{
    public GameObject buttonprefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        string[] files = Directory.GetDirectories(Application.persistentDataPath);
        foreach (string filen in files)
        {
            GameObject q = Instantiate(buttonprefab,transform);
            //taken from : https://www.techiedelight.com/remove-extension-from-file-name-csharp/
            q.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Path.GetFileNameWithoutExtension(filen);
            UnityEngine.UI.Button qScript = q.GetComponent<UnityEngine.UI.Button>();
            qScript.onClick.AddListener(delegate { LoadSpecScene(Path.GetFileNameWithoutExtension(filen)); });
            if (SceneManager.GetActiveScene().buildIndex == 0)
                q.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = 30;


            GameObject b = Instantiate(buttonprefab, q.transform);
            RectTransform btrans = b.GetComponent<RectTransform>();
            btrans.anchorMax = new Vector2(.5f, .5f);
            btrans.anchorMin = new Vector2(.5f, .5f);
            if (SceneManager.GetActiveScene().buildIndex==1)
                btrans.sizeDelta = new Vector2(30, 28.04f);
            else if (SceneManager.GetActiveScene().buildIndex == 0)
                btrans.sizeDelta = new Vector2(40, 28.04f);
            if (SceneManager.GetActiveScene().buildIndex == 1)
                btrans.localPosition = new Vector3Int(34, 0, 0);
            else if (SceneManager.GetActiveScene().buildIndex == 0)
                btrans.localPosition = new Vector3Int(80, 0, 0);
            b.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Delete";
            if (SceneManager.GetActiveScene().buildIndex == 1)
                b.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = 8;
            else if (SceneManager.GetActiveScene().buildIndex == 0)
                b.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().fontSize = 12;
            b.GetComponent<UnityEngine.UI.Image>().color = new Color32(241, 120, 120, 255); //Color.red;
            UnityEngine.UI.Button bScript = b.GetComponent<UnityEngine.UI.Button>();
            bScript.onClick.AddListener(delegate { DeleteSpecScene(filen,q); });
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void LoadSpecScene(string sceneName)
    {
        LoadScene.foldername = "/" + sceneName;
        SceneManager.LoadScene(1);
    }
    
    public void DeleteSpecScene(string sceneName, GameObject buttonParent)
    {
        Directory.Delete(sceneName, true);
        GameObject.Destroy(buttonParent);
    }
}
