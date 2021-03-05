using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieManager))]
public class Spawn : MonoBehaviour
{
    public Vector3 spawnPoint;
    
    public BotMouse mouse;
    // Start is called before the first frame update
    void Start()
    {
/*        var mouseCopy = Instantiate(mouse, spawnPoint, new Quaternion());
        var manager = GetComponent<ZombieManager>();
        if (manager != null)
        {
            mouseCopy.Infected(manager);
        }
        else
        {
            Camera.main.GetComponent<MoveCamera>().gameObject = mouseCopy.gameObject;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
