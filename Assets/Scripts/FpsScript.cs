using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsScript : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    int FPScount;

    public void Update()
    {
        FPScount = (int)(1 / Time.smoothDeltaTime);
    }

    public void FixedUpdate()
    {
        displayText.text = "FPS: " + FPScount.ToString();
    }
}



