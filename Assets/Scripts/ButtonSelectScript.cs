using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectScript : MonoBehaviour
{
    [System.Serializable]
    public class ElementButton
    {
        public string displayName;
        public Color color;
        public RuleTile tile;
        public float startingAmount;
    }

    public GameObject manager;
    public List<ElementButton> buttons;
    List<GameObject> buttonInstances = new List<GameObject>();
    public GameObject buttonPrefab;

    public DrawScript drawScript;

    // Start is called before the first frame update
    void Start()
    {
        drawScript = FindObjectOfType<DrawScript>();

        foreach (ElementButton eb in buttons)
        {
            GameObject b = Instantiate(buttonPrefab, transform);
            buttonInstances.Add(b);

            //b.transform.SetParent(this.transform);
            b.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = eb.displayName;
            b.GetComponent<UnityEngine.UI.Image>().color = eb.color;

            //b.transform.localScale = new Vector3(1, 1, 1);
            //b.transform.position = new Vector3(0, 0, 0);
            //Debug.Log(b.transform.position);

            UnityEngine.UI.Button bScript = b.GetComponent<UnityEngine.UI.Button>();
            bScript.onClick.AddListener(delegate { FindObjectOfType<DrawScript>().SetCurrentTile(eb.tile); });
            EventTrigger tScript = b.GetComponent<UnityEngine.EventSystems.EventTrigger>();

            EventTrigger trigger = GetComponentInParent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => { FindObjectOfType<DrawScript>().EnableDraw(false); });
            //trigger.delegates.Add(entry);
            trigger.triggers.Add(entry);

            
            entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener((eventData) => { FindObjectOfType<DrawScript>().EnableDraw(true); });
            //trigger.delegates.Add(entry);
            trigger.triggers.Add(entry);
        }
    }

    public void UpdateElementButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            foreach (DrawScript.Element element in drawScript.elements)
            {
                // Updates text
                if (buttons[i].tile == element.tile)
                {
                    if (element.elementCount >= 999999)
                    {
                        buttonInstances[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = buttons[i].displayName + "\n" + "∞";
                    }
                    else
                    {
                        buttonInstances[i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = buttons[i].displayName + "\n" + element.elementCount;
                    }

                    if (element.elementCount <= 0)
                    {
                        buttonInstances[i].SetActive(false);
                    }
                    else
                    {
                        buttonInstances[i].SetActive(true);
                    }
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElementButtons();
    }
}
