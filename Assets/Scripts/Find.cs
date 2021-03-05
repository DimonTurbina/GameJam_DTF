using UnityEngine;
using System.Collections;

public class Find : MonoBehaviour
{

    // функция возвращает ближайший объект из массива, относительно указанной позиции
    private static GameObject NearTarget(Vector3 position, Collider[] array)
    {
        Collider current = null;
        float dist = Mathf.Infinity;

        foreach (Collider coll in array)
        {
            float curDist = Vector3.Distance(position, coll.transform.position);

            if (curDist < dist)
            {
                current = coll;
                dist = curDist;
            }
        }

        return (current != null) ? current.gameObject : null;
    }

    // point - точка контакта
    // radius - радиус поражения
    // layerMask - номер слоя, с которым будет взаимодействие
    // damage - наносимый урон
    // allTargets - должны-ли получить урон все цели, попавшие в зону поражения
    public static AbstractCreature FindCreature(Vector3 point, float radius, int layerMask, bool allTargets)
    {
        Collider[] colliders = Physics.OverlapSphere(point, radius, 1 << layerMask);
        if (!allTargets)
        {
            GameObject obj = NearTarget(point, colliders);
            if (obj != null && obj.GetComponent<AbstractCreature>())
            {
                //obj.GetComponent<AbstractCreature>().Damage(damage);
                return obj.GetComponent<AbstractCreature>();
            }
        }

        foreach (Collider hit in colliders)
        {


            if (hit.GetComponent<AbstractCreature>())
            {
                return hit.GetComponent<AbstractCreature>();
               // hit.GetComponent<BotMouse>().Damage(damage);
            }
        }
        return null;
    }
}