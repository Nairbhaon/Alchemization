using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConductorScript : MonoBehaviour
{
    public float conductDelay;
    [Range(0.0f,1.0f)]
    public float conductChance;
    public int cooldownTime;
    public int powerTime;
    public bool powered;
    public int poweredTime;
    public int cooldown = 0;
    Tilemap map;
    // Start is called before the first frame update
    void Start()
    {
        map = transform.parent.gameObject.GetComponent<Tilemap>();
    }
    public void sendPower()
    {
        cooldown = cooldownTime;
        poweredTime = powerTime;
        powered = true;
        //Debug.Log("Sent");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3Int pos = map.WorldToCell(transform.position);
        GameObject currentSelect = null;
        ConductorScript cs;
        if (powered)
        {
            if (powerTime - poweredTime == conductDelay)
            {
                pos.y += 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
                pos.x -= 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
                pos.y -= 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
                pos.y -= 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
                pos.x += 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
                pos.x += 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
                pos.y += 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
                pos.y += 1;
                currentSelect = map.GetInstantiatedObject(pos);
                if (currentSelect != null)
                {
                    if (currentSelect.TryGetComponent<ConductorScript>(out cs))
                    {
                        if (cs.cooldown <= 0)
                        {
                            cs.sendPower();
                        }
                    }
                }
                cs = null;
                currentSelect = null;
            }
            
            poweredTime -= 1;
            if (poweredTime <= 0)
            {
                powered = false;
            }
        }
        cooldown -= 1;
    }
}
