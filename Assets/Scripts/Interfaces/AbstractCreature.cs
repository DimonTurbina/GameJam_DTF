using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AbstractCreature : MonoBehaviour
{
    public float speed = 100;
    public float gravity = -9.8f;
    public float health = 100;
    public float damage;
    protected NavMeshAgent _myAgent;
    public GameObject AttackPoint;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
    }

    public virtual void Damage(float damage)
    {
        health -= damage;

        Debug.Log("АЙ");

        if(health <= 0)
        {
            Debug.Log("Death");

            //TODO death mouse
            Destroy(this.gameObject);
        }
    }
}
