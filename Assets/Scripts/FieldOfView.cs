using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AbstarctEnemy))]
public class FieldOfView : MonoBehaviour 
{
    public event Action<List<AbstractCreature>> OnFieldOfView;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    private BotMouse _rat;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    private void Start()
    {
        _rat = GetComponent<BotMouse>();
    }
    //[HideInInspector]
    //public List<Transform> visibleTargets = new List<Transform>();

    void FindVisibleTargets()
    {
        List<AbstractCreature> visibleTargets = new List<AbstractCreature>();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            if (targetsInViewRadius[i].GetComponent<AbstractCreature>())
            {

                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target.GetComponent<AbstractCreature>());
                    }
                }
                
            }
        }

        if (visibleTargets.Count > 0 && OnFieldOfView != null)
        {
            
            OnFieldOfView.Invoke(visibleTargets);
        }

        
    }

    // Update is called once per frame
    private void Update()
    {
        FindVisibleTargets();
    }
}
