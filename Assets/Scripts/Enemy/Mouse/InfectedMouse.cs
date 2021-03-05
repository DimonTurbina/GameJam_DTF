using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class InfectedMouse : MouseState
{
    public InfectedMouse(ZombieManager manager)
    {
        zombieManager = manager;
    }
    protected void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
    }



    public override void Attack()
    {
        Debug.Log($"Attack infected mouse {_context.name}");

        if (_context.targetEnemy == null)
        {
            _context.Instruction = BotMouse.enInstruction.idle;
            return;
        }
        if (Vector3.Distance(_context.transform.position, _context.targetEnemy.transform.position) > 3.5)
        {
            _myAgent.isStopped = false;
            _myAgent.SetDestination(_context.targetEnemy.transform.position);
        } 
        else if (_timer > _context.AttackRate)
        {
            _timer = 0;
            _myAgent.isStopped = true;
            _myAgent.ResetPath();
            if (_context.targetEnemy.GetComponent<BotMouse>())
            {
                if (!_context.targetEnemy.GetComponent<BotMouse>().isInfected)
                {
                    _context.targetEnemy.GetComponent<BotMouse>().Damage(zombieManager, _context.damage);
                }
                else
                {
                    _context.targetEnemy = null;
                    _context.Instruction = BotMouse.enInstruction.idle;
                }
            }
            else
            {
                _context.targetEnemy.Damage(_context.damage);
            }
        }
    }

    public override void MoveTo()
    {

        if (Vector3.Distance(_context.transform.position, _context.targetPoint) > 1)
        {            
            _myAgent.isStopped = false;
            _myAgent.SetDestination(_context.targetPoint);
        }
        else
        {
            _myAgent.isStopped = true;
            _myAgent.ResetPath();
            _context.Instruction = BotMouse.enInstruction.idle;
        }

    }

    public override void DestroyRat()
    {
        zombieManager.RemoveRat(_context);
    }

    public override void Infected(ZombieManager manager)
    {
        //Debug.Log("I'm zombie");
        //???
    }
}
