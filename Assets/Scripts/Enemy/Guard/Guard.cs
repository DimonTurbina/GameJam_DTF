using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Guard : AbstarctEnemy
{
    public List<Transform> Breakpoints;
    private Transform _currentBreackpoint;
    private bool isGoBack;
    private AnimState _animState;
    public enum enInstruction
    {
        idle,
        move,
        attack
    }
    public enInstruction Instruction;
    public AbstractCreature targetEnemy;
    public FieldOfView fov;
    private Animator _animator;

    public float WaitTime;
    private float _waitTimer;

    public float AttackRate;
    private float _attackTimer;

    void Awake()
    {
        fov = GetComponent<FieldOfView>();
    }
    protected override void Start()
    {
        base.Start();
        isGoBack = false;
        //Breakpoints = new List<Transform>();
        _attackTimer = 0;
        _waitTimer = 0;
        Instruction = enInstruction.idle;
        _animator = GetComponent<Animator>();
        _animState = GetComponent<AnimState>();
        

        fov.OnFieldOfView += setTargetFromFOV;
    }
    // Update is called once per frame
    private void setTargetFromFOV(List<AbstractCreature> creatures)
    {
        if (targetEnemy != null)
        {

            return;
        }

        if (creatures.Count > 0)
        {
            float dest = float.MaxValue;
            foreach (var target in creatures)
            {
                var distance = Vector3.Distance(target.transform.position, transform.position);
                if (target.GetComponent<BotMouse>())
                {
                    if (distance < dest && target.GetComponent<BotMouse>().isInfected)
                    {
                        targetEnemy = target;
                        dest = distance;
                    }
                }
            }
        }

        if(targetEnemy != null)
        {
            
            Instruction = enInstruction.attack;
            _currentBreackpoint = null;
        }
    }

    public void MoveTo()
    {
        targetEnemy = null;

        if (_currentBreackpoint != null)
        {
            if (Vector3.Distance(transform.position, _currentBreackpoint.position) < 1)
            {
                Instruction = enInstruction.idle;
            }
            _myAgent.isStopped = false;
            _myAgent.SetDestination(_currentBreackpoint.position);
        }
        else
        {
            Instruction = enInstruction.idle;
        }
    }
    
    private void wait()
    {
        if(_waitTimer > WaitTime)
        {
            _waitTimer = 0;

            if (_currentBreackpoint == null)
            {
                setNearBreackpoint();
            }
            else
            {
                nextBreackpoint();
            }
            Instruction = enInstruction.move;
        }
    }

    private void attack()
    {
        if (targetEnemy == null)
        {
            Instruction = enInstruction.idle;
            return;
        }

        var dist = Vector3.Distance(transform.position, targetEnemy.transform.position);
        if (dist > 30)
        {
            _myAgent.isStopped = true;
            _myAgent.ResetPath();
            Instruction = enInstruction.idle;
        }
        else if (dist > 3.5)
        {
            _animState.AnimInstruction = AnimState.enAnimation.move;
            _myAgent.isStopped = false;
            _myAgent.SetDestination(targetEnemy.transform.position);
        }  
        else if (_attackTimer > AttackRate)
        {
            _animState.AnimInstruction = AnimState.enAnimation.attact;
            return;
            _attackTimer = 0;

            _myAgent.isStopped = true;
            _myAgent.ResetPath();

            targetEnemy.Damage(damage);
        }
    }

    private void nextBreackpoint()
    {

        if (Breakpoints.IndexOf(_currentBreackpoint) == Breakpoints.Count - 1)
        {
            isGoBack = true;
        }
        else if (Breakpoints.IndexOf(_currentBreackpoint) == 0)
        {
            isGoBack = false;
        }

        if (isGoBack)
        {
            _currentBreackpoint = Breakpoints[Breakpoints.IndexOf(_currentBreackpoint) - 1]; 
        }
        else
        {
            _currentBreackpoint = Breakpoints[Breakpoints.IndexOf(_currentBreackpoint) + 1];
        }
    }

    private void setNearBreackpoint()
    {

        float minDistance = float.MaxValue;
        Transform nearPoint = null;
        foreach(var point in Breakpoints)
        {
            var distance = Vector3.Distance(point.position, transform.position);
            if (distance < minDistance)
            {
                nearPoint = point;
                minDistance = distance;
            }
        }
        
        _currentBreackpoint = nearPoint;

    }
    void Update()
    {

        

        if (Instruction == enInstruction.move)
        {
            MoveTo();
            _animState.AnimInstruction = AnimState.enAnimation.move;
        }
        else if (Instruction == enInstruction.attack)
        {
            attack();
        }
        else if (Instruction == enInstruction.idle)
        {
            //            _myAgent.isStopped = true;
            _animState.AnimInstruction = AnimState.enAnimation.idle;
            wait();
            _waitTimer += Time.deltaTime;
            //Continue
        }


        _attackTimer += Time.deltaTime;
    }

    public void Kick()
    {
        if (targetEnemy == null)
        {
            Instruction = enInstruction.idle;
            return;
        }
        else
        {
            targetEnemy.Damage(damage);
        }
    }
}
