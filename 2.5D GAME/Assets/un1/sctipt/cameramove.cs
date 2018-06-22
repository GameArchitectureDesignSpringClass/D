using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour {
    GameObject player;
    Vector3 m_dist = Vector3.zero;
    Vector3 dist;
	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        if (player != null)
            m_dist = player.transform.position - transform.position;
        dist = m_dist;
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null && player.GetComponent<Unit>().Hp > 0)
        {
            Vector3 target = player.transform.position - dist;
            transform.position = target;
            transform.LookAt(player.transform);
            //int count = 0;
            RaycastHit hit;
            int mask = 1 << 10;
            mask += 1 << 11;
            Physics.Raycast(transform.position, transform.forward, out hit, 150f, ~mask, QueryTriggerInteraction.Ignore);
            if (hit.transform.tag != "Player")
            {
                //dist.y *= 1.1f;
                dist.z *= 0.98f;
                transform.LookAt(player.transform);
                //Physics.Raycast(transform.position, transform.forward, out hit, 150f, ~mask, QueryTriggerInteraction.Ignore);
                /*count++;
                if (count > 10000)
                {
                    Debug.Log("cant find player");
                    break;
                }*/
            }
            else if (dist.z < m_dist.z)
            {
                Vector3 newpos = transform.position;
                newpos.z *= 0.98f;
                Quaternion newrot = Quaternion.LookRotation(player.transform.position-newpos);
                Vector3 newforwad = Vector3.Slerp(Vector3.forward, Vector3.down, newrot.eulerAngles.x / 90f);
               // Debug.Log(newforwad + ",," + newrot.eulerAngles + ",," + newpos);
                Physics.Raycast(newpos, newforwad, out hit, 150f, ~mask, QueryTriggerInteraction.Ignore);
               // Debug.DrawRay(newpos, newforwad * 10, Color.red);
                if (hit.transform.tag == "Player")
                {
                    dist.z *= 1.02f;
                    transform.LookAt(player.transform);
                }
            }

            //Debug.Log(hit.transform.tag);
           // Debug.DrawRay(newpos, newrot.eulerAngles * hit.distance * 1.37f, Color.red);

        }
	}
    private void LateUpdate()
    {
        if (player == null)
            Start();
    }
}
