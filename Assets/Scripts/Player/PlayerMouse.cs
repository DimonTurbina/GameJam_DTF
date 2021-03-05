using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(ZombieManager))]
public class PlayerMouse : AbstractCreature
{
    Camera cam;
    AbstractCreature selectedEnemy;
    private delegate void DoSelected(AbstractCreature creature);
    private DoSelected _del;
    private ZombieManager _zombieManager;

    Vector3 targetPos;
    public LayerMask whatCanBeClickOn;
    public LayerMask creationLayer;

    public enum enInstruction
    {
        idle,
        move,
        attack,
        infect,
    }
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        _zombieManager = GetComponent<ZombieManager>();
        cam = Camera.main;
        
    }

    void regDelegate(DoSelected func)
    {
        _del = func;
    }
    void kek(Vector3 pos)
    {
        Debug.Log("kek");
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        o.transform.position = pos;
        Destroy(o, 5);
        
    }

   /* void Infect(AbstractCreature enemy)
    {

        if (!enemy.GetComponent<BotMouse>())
        {
            return;
        }
        var mouse = enemy.GetComponent<BotMouse>();
        

        if (Vector3.Distance(this.transform.position, enemy.transform.position) > 3)
        {
            _myAgent.isStopped = false;
            //_myAgent.stoppingDistance = 4;
            MoveTo(enemy.transform.position);
           
        }
        else if (!mouse.isInfected)
        {

            mouse.Infected();
            _zombieManager.AddMouse(mouse);
            mouse.gameObject.AddComponent<ClearSight>();
            
        }
    }*/

    private void Attack(AbstractCreature enemy)
    {
        //Debug.Log($"Dest {Vector3.Distance(this.transform.position, enemy.transform.position)}");
        if (Vector3.Distance(this.transform.position, enemy.transform.position) > 3)
        {
            _myAgent.isStopped = false;
            //_myAgent.stoppingDistance = 4;
            _myAgent.SetDestination(enemy.transform.position);
        }
        else 
        {
            _myAgent.isStopped = true;
            _myAgent.ResetPath();
            Debug.Log($"NA FROM PLAYER");

            enemy.Damage(damage);
        }

        _zombieManager.Attack(enemy);
    }

    private void MoveTo(Vector3 point)
    {
        _myAgent.SetDestination(point);

        _zombieManager.MoveTo(point);

    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 200, creationLayer))
            {
                if (hit.collider.gameObject.GetComponent<AbstractCreature>() && hit.collider.gameObject != this.gameObject)
                {
                    if (hit.collider.gameObject.GetComponent<BotMouse>().isInfected)
                    {
                        return;
                    }
                    selectedEnemy = hit.collider.gameObject.GetComponent<AbstractCreature>();
                    Debug.Log($"Selected {selectedEnemy.name}");
                }
                if (selectedEnemy != null)
                {
                    //Infect(selectedEnemy);
                    //regDelegate(new DoSelected(Infect));
                }          
            }
        }

        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            selectedEnemy = null;
            _del = null;
            
            if (Physics.Raycast(ray, out hit, 200, creationLayer))
            {
                if (hit.collider.gameObject.GetComponent<AbstractCreature>() && hit.collider.gameObject != this.gameObject)
                {
                    if (hit.collider.gameObject.GetComponent<BotMouse>().isInfected)
                    {
                        return;
                    }
                    selectedEnemy = hit.collider.gameObject.GetComponent<AbstractCreature>();
                    Debug.Log($"Selected {selectedEnemy.name}");
                }
                if (selectedEnemy != null)
                {
                    Attack(selectedEnemy);
                    regDelegate(new DoSelected(Attack));
                }
                return;
            }


            if (Physics.Raycast(ray, out hit, 200, whatCanBeClickOn))
            {
                selectedEnemy = null;

                _myAgent.SetDestination(hit.point);

                _zombieManager.MoveTo(this.transform.position);
                Debug.Log($"Selected {hit.collider.name}");

            }

        }

        if (selectedEnemy != null)
        {
            _del?.Invoke(selectedEnemy);
        }
        else
        {
            _zombieManager.MoveTo(this.transform.position);
        }
    }
}

/*      if (selectedEnemy != null && Find.FindCreature(AttackPoint.transform.position, 0.5f, 8, true) == selectedEnemy)
                {
                    selectedEnemy.Damage(this.damage);
                }

        /*        if (Input.GetKey(KeyCode.Space))
                {
                    this.Infect();
                }
        if (selectedEnemy != null)
        {
            Attack(selectedEnemy);
        }

        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
var hits = Physics.RaycastAll(ray, 1000f);

int j = -1;


selectedEnemy = null;
            for (int i = 0; i<hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<AbstractCreature>() && hits[i].collider.gameObject != this.gameObject)
                {
    if (hits[i].collider.GetComponent<BotMouse>())
    {
        if (hits[i].collider.GetComponent<BotMouse>().isInfected)
        {
            return;
        }
    }

    selectedEnemy = hits[i].collider.GetComponent<AbstractCreature>();
    break;
}

                if (hits [i].collider.tag == "Ground")
                {
    j = i;
}
}

            if (selectedEnemy != null) 
            {
                Attack(selectedEnemy);
            }
            else if (j != -1)
            {
                selectedEnemy = null;
                _myAgent.isStopped = false;
                _myAgent.SetDestination(hits[j].point);

                foreach (var infected in Infected)
                {
                    infected.MoveTo(this.transform.position);
}
            }

        }
        Debug.Log(_myAgent.isStopped);*/