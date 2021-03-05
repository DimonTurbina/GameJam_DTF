using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private GameObject gameObject;
    public ZombieManager manager;
    public int height = 10;
    public int x = 10;
    public int z = 10;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(45, 0, 0);
    }

    // Update is called once per frame

    void Update()
    { 
        if(gameObject == null)
        {
            if(manager.CountRat() > 0)
            {
                var player = manager.GetRat();
                if (player != null)
                {
                    gameObject = player;
                }
            }
            else
            {
                return;
            }
        }
        if (gameObject != null)
        {
            this.transform.position = new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y + height, gameObject.transform.position.z + z);
        }

    }
}
