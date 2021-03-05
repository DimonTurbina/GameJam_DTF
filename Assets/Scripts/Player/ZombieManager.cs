using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private List<BotMouse> Infected;
    //private AbstractCreature selectedEnemy;
    public GameObject PrefabInfectedRat;
    public bool isPause;
    public Vector3 startPoint;
    public LayerMask whatCanBeClickOn;
    public LayerMask creationLayer;

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        Infected = new List<BotMouse>();
        var rat = Instantiate(PrefabInfectedRat, startPoint, new Quaternion()).GetComponent<BotMouse>();
        rat.Infected(this);
    }
    public void AddRat(BotMouse rat)
    {
        Infected.Add(rat);
    }

    public void RemoveRat(BotMouse mouse)
    {
        Infected.Remove(mouse);
        if(Infected.Count <= 0)
        {
            Debug.Log("The end");
        }
    }
    public int CountRat()
    {
        return Infected.Count;
    }
    public GameObject GetRat()
    {
        if (Infected.Count > 0)
        {
            foreach (var infected in Infected.ToArray())
            {
                if (infected != null)
                {
                    return infected.gameObject;
                }
            }
        }
        return null;
    }

    void createCube(Vector3 point)
    {
        var kek = GameObject.CreatePrimitive(PrimitiveType.Cube);
        kek.transform.position = point;
        Destroy(kek, 5);
    }

    public void MoveTo(Vector3 point)
    {
        Vector3[] points;
        if (Infected.Count == 1)
        {
            points = new Vector3[1] { point };
        }
        else
        {
            points = GetPositions(point, Infected.Count);
        }

        var infected = Infected.ToArray();
        for (int i = 0; i < infected.Length; i++)
        {
            infected[i].targetPoint = points[i];
            infected[i].Instruction = BotMouse.enInstruction.move;
        }
/*        foreach (var infected in Infected.ToArray())
        {
            if (infected != null)
            {
                infected.targetPoint = point;
                infected.Instruction = BotMouse.enInstruction.move;
            }

        }*/
        //Move();
    }
    public void Move()
    {
        foreach (var infected in Infected.ToArray())
        {
            if (infected != null)
            {
                infected.MoveTo();
                infected.Instruction = BotMouse.enInstruction.move;
            }

        }
    }
    public void Attack(AbstractCreature enemy)
    {
        foreach (var infected in Infected.ToArray())
        {
            if (infected != null)
            {
                infected.targetEnemy = enemy;
                infected.Instruction = BotMouse.enInstruction.attact;
            }


        }
    }

    public void UseBuilding(Transform build)
    {
        var points = GetPositions(transform.position, Infected.Count);
        var infected = Infected.ToArray();

        for (int i = 0; i < infected.Length; i++)
        {
            infected[i].setBuild(build);
            infected[i].Instruction = BotMouse.enInstruction.use;
        }
    }
    public void TeleportateTo(Transform transform)
    {

        var points = GetPositions(transform.position, Infected.Count);
        var infected = Infected.ToArray();
        for (int i = 0; i < infected.Length; i++)
        {
            infected[i].transform.position = points[i];
            infected[i].setBuild(null);
            infected[i].Instruction = BotMouse.enInstruction.idle;
        }

    }
    private Vector3[] GetPositions(Vector3 center, int count)
    {
        Vector3[] points = new Vector3[count];
        if (count == 0)
        {
            return points;
        }
        var angle = 360 / count;
        float targetAngle = 0;
        for (int i = 0; i < count; i++)
        {
            points[i] = new Vector3(Mathf.Cos(targetAngle) + center.x, center.y, Mathf.Sin(targetAngle) + center.z);
            targetAngle += angle;
            //createCube(points[i]);
        }
        return points;
    }
    void kek(Vector3 point)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = point;
        Destroy(cube, 5);
    }
    // Update is called once per frame
    void Update()
    {
        if (isPause)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray, 1000);

            int groundhitIndx = -1;
            for (var i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.layer == 9)//Ground
                {
                    //kek(hit.transform.position);
                    groundhitIndx = i;
                }
                else if(hits[i].collider.gameObject.layer == 8)//Creation
                {

                    Debug.Log(hits[i].collider.name);
                    AbstractCreature selectedEnemy = null;
                    if (hits[i].collider.gameObject.GetComponent<AbstractCreature>())
                    {

                        if (hits[i].collider.gameObject.GetComponent<BotMouse>() && hits[i].collider.gameObject.GetComponent<BotMouse>().isInfected)
                        {
                            return;
                        }
                        selectedEnemy = hits[i].collider.gameObject.GetComponent<AbstractCreature>();
                        Debug.Log($"Selected {selectedEnemy.name}");
                    }

                    if (selectedEnemy != null)
                    {
                        Attack(selectedEnemy);
                    }
                    return;
                }else if(hits[i].collider.gameObject.layer == 11)//SpecialBuilding
                {
                    UseBuilding(hits[i].transform);
                    return;
                }
            }

            if(groundhitIndx >= 0)
            {
                MoveTo(hits[groundhitIndx].point);
            }
            
            /*var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            selectedEnemy = null;

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
                }
                return;
            }


            if (Physics.Raycast(ray, out hit, 200, whatCanBeClickOn))
            {
                selectedEnemy = null;

                MoveTo(hit.point);
            }*/

        }
    }
}
