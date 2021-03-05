using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class HealthyMouse : MouseState
{
    public override void Infected(ZombieManager manager)
    {

        _context.setState(new InfectedMouse(manager));
        _context.setAgentToState();
        _context.gameObject.AddComponent<ClearSight>();
        _context.Instruction = BotMouse.enInstruction.idle;
        _context.fov.enabled = false;
        manager.AddRat(_context);

    }

    // Start is called before the first frame update
    public override void Attack()
    {

        if (_context.targetEnemy == null)
        {
            _context.Instruction = BotMouse.enInstruction.idle;
            return;
        }

        var dist = Vector3.Distance(_context.transform.position, _context.targetEnemy.transform.position);
        if (dist > 15)
        {
            _myAgent.isStopped = true;
            _myAgent.ResetPath();
            _context.Instruction = BotMouse.enInstruction.idle;
        }
        else if ( dist > 3.5)
        {
            _myAgent.isStopped = false;
            _myAgent.SetDestination(_context.targetEnemy.transform.position);
        }
        else if (_timer > _context.AttackRate)
        {
            _timer = 0;
            _myAgent.isStopped = true;
            _myAgent.ResetPath();

            _context.targetEnemy.Damage(_context.damage);
        }
    }

    public override void MoveTo()
    {
        _myAgent.isStopped = false;
        _myAgent.SetDestination(_context.targetPoint);
    }

    public override void DestroyRat()
    {
        //
    }


}
