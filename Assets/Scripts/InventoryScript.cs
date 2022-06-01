using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    [System.Serializable]
    public struct Element
    {
        public RuleTile tile;
        public int amount;
    }

    public List<Element> elements;

    public bool CheckForElement(RuleTile checkTile)
    {
        foreach (Element element in elements)
        {
            if (element.tile == checkTile)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        return false;
    }
}