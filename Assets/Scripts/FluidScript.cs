using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FluidScript : MonoBehaviour
{
    Tilemap map;
    public RuleTile tile;
    public float amount;
    public float downFlow;
    public float compression = 0.25f;
    public int iterations = 1;
    public float flowSpeed = 1f;
    public float minAmount = 0.001f;
    float MinFlow = 0;//0.005f;

    bool exists = true;

    ElementScript eScript;
    //float lastElementValue;
    float lastAmount;
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        map = transform.parent.gameObject.GetComponent<Tilemap>();
        eScript = GetComponent<ElementScript>();
        child.transform.localScale = new Vector3(/*Mathf.Clamp(amount / 1f, 0, 1)*/1, Mathf.Clamp(amount / 1f, 0, 1), 1);
        child.transform.localPosition = new Vector3(0, 0 - (1 - Mathf.Clamp(amount / 1f, 0, 1)) / 2, 0);
        if (eScript.valuesToTransfer.Count == 0) eScript.valuesToTransfer.Add(1);
    }

    float calcVertFlow(float a, float b)
    {
        float sum = a + b;
        float value = 0;

        if (sum <= 1)
        {
            value = 1;
        }
        else if (sum < 2 * 1 + compression)
        {
            value = (1 + (sum * compression)) / (1 + compression);
        }
        else
        {
            value = (sum + compression) / 2f;
        }

        return value;
    }

    void UpdateTile()
    {
        float flow;
        FluidScript otherScript;
        Vector3Int currentpos = map.WorldToCell(transform.position);
        currentpos.y -= 1;
        if (map.GetTile(currentpos) == null) //down
        {
            flow = Mathf.Clamp((calcVertFlow(amount, 0) - 0) * flowSpeed,0,amount);
            if (flow > MinFlow)
            {
                map.SetTile(currentpos, tile);
                otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
                otherScript.amount = flow;
                otherScript.GetComponent<ElementScript>().temp = eScript.temp;
                amount -= flow;
            }
        }
        else if(map.GetTile(currentpos) == tile)
        {
            otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
            flow = Mathf.Clamp((calcVertFlow(amount, otherScript.amount) - otherScript.amount) * flowSpeed,0,amount);
            if (flow > MinFlow)
            {
                otherScript.GetComponent<ElementScript>().temp = ((otherScript.GetComponent<ElementScript>().temp * otherScript.amount) + (eScript.temp * flow)) / (otherScript.amount + flow);
                otherScript.amount += flow;
                amount -= flow;
            }
        }

        if(amount <= minAmount)
        {
            currentpos.y += 1;
            map.SetTile(currentpos, null);
            exists = false;
            return;
        }



        //sides

        //left
        currentpos.y += 1;
        currentpos.x -= 1;
        if(map.GetTile(currentpos) == null)
        {
            flow = ((amount - 0) / 4f) * flowSpeed;
            if (flow/flowSpeed > MinFlow)
            {
                map.SetTile(currentpos, tile);
                otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
                if (otherScript == null) Debug.Log("null");
                otherScript.amount = flow;
                otherScript.GetComponent<ElementScript>().temp = eScript.temp;
                amount -= flow;
            }
        }
        else if(map.GetTile(currentpos) == tile)
        {
            otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
            flow = ((amount - otherScript.amount) / 4f) * flowSpeed;
            if (flow/flowSpeed > MinFlow)
            {
                otherScript.GetComponent<ElementScript>().temp = ((otherScript.GetComponent<ElementScript>().temp * otherScript.amount) + (eScript.temp * flow)) / (otherScript.amount + flow);
                otherScript.amount += flow;
                amount -= flow;
            }
        }
        if (amount <= minAmount)
        {
            currentpos.x += 1;
            map.SetTile(currentpos, null);
            exists = false;
            return;
        }
        //right
        currentpos.x += 2;
        if (map.GetTile(currentpos) == null)
        {
            flow = ((amount - 0) / 3f)*flowSpeed;
            if (flow/flowSpeed > MinFlow)
            {
                map.SetTile(currentpos, tile);
                otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
                otherScript.amount = flow;
                otherScript.GetComponent<ElementScript>().temp = eScript.temp;
                amount -= flow;
            }
        }
        else if (map.GetTile(currentpos) == tile)
        {
            otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
            flow = ((amount - otherScript.amount) / 3f)*flowSpeed;
            if (flow/flowSpeed > MinFlow)
            {
                otherScript.GetComponent<ElementScript>().temp = ((otherScript.GetComponent<ElementScript>().temp * otherScript.amount) + (eScript.temp * flow)) / (otherScript.amount + flow);
                otherScript.amount += flow;
                amount -= flow;
            }
        }
        if (amount <= minAmount)
        {
            currentpos.x -= 1;
            map.SetTile(currentpos, null);
            exists = false;
            return;
        }

        //go up

        currentpos.x -= 1;
        currentpos.y += 1;
        if(map.GetTile(currentpos) == null)
        {
            flow = (amount - calcVertFlow(amount, 0))*flowSpeed;
            if (flow/flowSpeed > MinFlow)
            {
                map.SetTile(currentpos, tile);
                otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
                otherScript.amount = flow;
                otherScript.GetComponent<ElementScript>().temp = eScript.temp;
                amount -= flow;
            }
        }
        else if(map.GetTile(currentpos) == tile)
        {

            otherScript = map.GetInstantiatedObject(currentpos).GetComponent<FluidScript>();
         

            flow = (amount - calcVertFlow(amount,otherScript.amount))*flowSpeed;

            if (flow/flowSpeed > MinFlow)
            {
                otherScript.GetComponent<ElementScript>().temp = ((otherScript.GetComponent<ElementScript>().temp * otherScript.amount) + (eScript.temp * flow)) / (otherScript.amount + flow);
                otherScript.amount += flow;
                amount -= flow;
            }
        }

        if (amount <= minAmount)
        {
            currentpos.y -= 1;
            map.SetTile(currentpos, null);
            exists = false;
            return;
        }

        return;



    }



    // Update is called once per frame
    void FixedUpdate()
    {

        if (eScript.valuesToTransfer[0] != lastAmount && eScript.valuesToTransfer[0] != 1) //if eScript was changed then a swap happened as long as eScript is not 1 (the default)
        {
            amount = eScript.valuesToTransfer[0];
        }
        //transform.localScale = new Vector3(Mathf.Clamp(amount / 1f, 0, 1), Mathf.Clamp(amount / 1f, 0, 1), 1);
        child.transform.localScale = new Vector3(/*Mathf.Clamp(amount / 1f, 0, 1)*/1, Mathf.Clamp(amount / 1f, 0, 1), 1);
        child.transform.localPosition = new Vector3(0, 0 - (1 - Mathf.Clamp(amount / 1f, 0, 1)) / 2, 0);
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - (1 - Mathf.Clamp(amount / 1f, 0, 1)) / 2, transform.localPosition.z);

        for (int i = 0; i < iterations; i++) {
            //if (amount > minAmount)
            //{
                UpdateTile();
            //}
            if (!exists)
            {
                break;
            }
        } 

        eScript.valuesToTransfer[0] = amount;
        lastAmount = amount;
    }
}
