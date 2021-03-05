using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractAttack : MonoBehaviour
{
    public float damage = 10;
    public GameObject AttackPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected void Attack()
    {
        //Fight.Action(AttackPoint.transform.position, 10f, 8, damage, true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
