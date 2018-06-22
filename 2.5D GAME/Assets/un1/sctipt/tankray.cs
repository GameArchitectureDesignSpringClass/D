using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tankray : MonoBehaviour {
    Vector3 pos;
    public Vector3 newscale = new Vector3(0.15f,0.15f,0.15f);
    float ftime = 0;
	// Use this for initialization
	void Start () {
        pos = transform.localPosition;
	}

    // Update is called once per frame
    private void Update()
    {
        if (ftime <= 0)
        {
            /*RaycastHit hit;
            if (Physics.Raycast(transform.parent.transform.position, transform.up, out hit, 25f, ~0, QueryTriggerInteraction.Collide))
            {
                if (hit.transform.tag != "Untagged")
                {
                    transform.localPosition = new Vector3(pos.x, pos.y, pos.z + hit.distance * 1.37f);
                    transform.localScale = new Vector3(newscale.x, newscale.y + hit.distance * 1.37f, newscale.z);
                    ftime = 0.2f;
                    return;
                }
            }*/
            transform.localPosition = new Vector3(pos.x, pos.y, pos.z + 50);
            transform.localScale = new Vector3(newscale.x, newscale.y + 50f, newscale.z);
            ftime = 0.2f;
        }
        else
            ftime -= Time.deltaTime;
    }
}
