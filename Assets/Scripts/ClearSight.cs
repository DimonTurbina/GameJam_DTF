//And here the script for the camera to cast the ray / capsule ;

using UnityEngine;
using System.Collections;

public class ClearSight : MonoBehaviour
{
    private const int LayerBuilding = 10;
    void Update()
    {

        RaycastHit[] hits;
        // you can also use CapsuleCastAll()
        var direction = this.transform.position - Camera.main.transform.position;

        hits = Physics.RaycastAll(Camera.main.transform.position, direction, direction.magnitude, 1 << LayerBuilding);
        foreach (RaycastHit hit in hits)
        {
            Debug.Log(hit.collider.tag);

            Renderer R = hit.collider.GetComponent<Renderer>();
            if (R == null)
            {
                Debug.Log("NULL Renderer");
                continue;
            }
                 // no renderer attached? go to next hit
                          // TODO: maybe implement here a check for GOs that should not be affected like the player


            AutoTransparent AT = R.GetComponent<AutoTransparent>();
            if (AT == null) // if no script is attached, attach one
            {
                AT = R.gameObject.AddComponent<AutoTransparent>();
            }
            AT.BeTransparent(); // get called every frame to reset the falloff
        }
    }

}