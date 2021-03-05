using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(FieldOfView))]
public class BotMouse : AbstarctEnemy
{
    public Material InfectedMaterial;
    public enum enInstruction
    {
        idle,
        move,
        attact,
        use
    }
    public enInstruction Instruction;
    private MouseState _state { get; set; }
    public bool isInfected { get; set; }
    public float Immunity = 50;
    public Vector3 targetPoint;
    public AbstractCreature targetEnemy;
    public FieldOfView fov;
    public float AttackRate;
    private float _timer;
    private Transform _build;
    private ZombieManager manager;


    void Awake()
    {
        fov = GetComponent<FieldOfView>();
        setState(new HealthyMouse());
        isInfected = false;
    }
    protected override void Start()
    {
        base.Start();
        _timer = 0;
        Instruction = enInstruction.idle;
        targetPoint = this.transform.position;
        setAgentToState();
        fov.OnFieldOfView += setTargetFromFOV;
    }
    void kek(Vector3 pos)
    {
        var k = GameObject.CreatePrimitive(PrimitiveType.Quad);
        k.transform.position = pos;
        Destroy(k, 5);
    }
    private void setTargetFromFOV(List<AbstractCreature> creatures)
    {
        if(targetEnemy != null)
        {
            
            return;
        }

        if (creatures.Count > 0)
        {
            float dest = float.MaxValue;
            foreach (var target in creatures)
            {
                var distance = Vector3.Distance(target.transform.position, this.transform.position);
                if (target.GetComponent<BotMouse>())
                {
                    if (distance < dest &&  target.GetComponent<BotMouse>().isInfected)
                    {
                        targetEnemy = target;
                        dest = distance;

                    }
                }
            }
        }

        if (targetEnemy != null)
        {
            
            Instruction = enInstruction.attact;
        }
    }

    public void setState(MouseState state)
    {
        _state = state;
        _state.setContext(this);
    }
    public void setAgentToState()
    {
        _state.setAgent(this._myAgent);
    }
    public override void Damage(float damage)
    {
        //base.Damage(damage);
        this.health -= damage;
        if(this.health <= 0)
        {
            Debug.Log($"Mouse {this.tag} is death");
            Destroy(this.gameObject);
        }
    }
    public void Damage(ZombieManager manager, float damage)
    { 
        //base.Damage(damage);
        this.health -= damage;
        if (this.health <= 0)
        {
            Debug.Log($"Mouse {this.tag} is death");
            Destroy(this.gameObject);
        }
        else if (health <= Immunity)
        {
            Infected(manager);
        }

    }
    public void Infected(ZombieManager manager)
    {
        isInfected = true;
        GetComponent<Renderer>().material = InfectedMaterial;
        _state.Infected(manager);
        this.manager = manager;
    }
    public void MoveTo()
    {
        targetEnemy = null;
        _state.MoveTo();
    }
    public void Attack(AbstractCreature enemy)
    {
        if (_timer > AttackRate)
        {
            targetEnemy = enemy;
            _state.Attack();
        }
    }
    public void setBuild(Transform build)
    {
        _build = build;
    }

    private void Update()
    {
        if (Instruction == BotMouse.enInstruction.move)
        {
            MoveTo();
        }
        else if (Instruction == BotMouse.enInstruction.attact)
        {
            _state.Attack();
        }
        else if (Instruction == BotMouse.enInstruction.idle)
        {
            _myAgent.ResetPath();
            //Continue
        }else if (Instruction == enInstruction.use)
        {
            Use();
        }
        _state.incTimer(Time.deltaTime);
    }
    public void Use()
    {
        if (_build == null)
        {
            Instruction = BotMouse.enInstruction.idle;
            return;
        }
        if (Vector3.Distance(transform.position, _build.transform.position) > 1)
        {
            _myAgent.isStopped = false;
            _myAgent.SetDestination(_build.transform.position);
        }
        else 
        {
            if (_build.gameObject.GetComponent<Teleport>())
            {
                _build.gameObject.GetComponent<Teleport>().Use(manager);
                
            }
        }
    }
    private void OnDestroy()
    {
        _state.DestroyRat();
    }
    void OnCollisionEnter(Collision collision)
    {
        return;
        if (collision.gameObject.tag == "Mouse" && !_myAgent.isStopped)
        {
            _myAgent.isStopped = true;

        }
    }


}
