using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class MouseState : MonoBehaviour
{
    protected BotMouse _context;
    protected NavMeshAgent _myAgent;
    protected ZombieManager zombieManager;
    protected float _timer;
    

    public virtual void incTimer(float inc)
    {
        _timer += inc;
    }
    public void setContext(BotMouse mouse)
    {
        this._context = mouse;
    }
    public void setAgent(NavMeshAgent agent)
    {
        _myAgent = agent;
    }
    public abstract void MoveTo();
    public abstract void Attack();

    public abstract void Infected(ZombieManager manager);
    public abstract void DestroyRat();

}
