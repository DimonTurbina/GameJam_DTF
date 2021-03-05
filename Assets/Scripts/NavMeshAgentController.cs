using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentController : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        Target.GetOrAddComponent<Transform, ObserverableTransform>().OnChangePosition += (target) =>
        {
            _agent.SetDestination(target.position);
        };
        _agent.SetDestination(Target.position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
