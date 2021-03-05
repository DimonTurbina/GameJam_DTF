using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverableTransform : MonoBehaviour
{
    public event Action<Transform> OnChangePosition;
    private Vector3 _lastPosition;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _lastPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_transform.position != _lastPosition)
        {
            if(OnChangePosition != null)
            {
                OnChangePosition.Invoke(_transform);
            }
        }
        _lastPosition = _transform.position;
    }
}
