using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI add;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Elements", "") == "")
        {
            PlayerPrefs.SetString("Elements", "");
            PlayerPrefs.Save();
        }
        add.text = "Files Stored at :\n" + Application.persistentDataPath;
        //use something like this to open file explorer : https://answers.unity.com/questions/585086/opening-a-file-explorer-in-run-time.html
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void newGame()
    {
        LoadScene.foldername = "";
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OpenFileLocationWindows()
    {
        //this just opens the file explorer
        /*System.Diagnostics.Process p = new System.Diagnostics.Process();
        p.StartInfo = new System.Diagnostics.ProcessStartInfo("explorer.exe");
        p.Start();*/
        //taken from : https://answers.unity.com/questions/585086/opening-a-file-explorer-in-run-time.html?page=2&pageSize=5&sort=votes
        System.Diagnostics.Process.Start(Application.persistentDataPath);
    }

    public void OpenWebBrowser()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }

    public void ResetGame()
    {
        PlayerPrefs.SetString("Elements", "");
        PlayerPrefs.Save();
        string[] files = System.IO.Directory.GetDirectories(Application.persistentDataPath);
        foreach(string filen in files)
        {
            System.IO.Directory.Delete(filen, true);
        }
    }

    public void LoadAScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
