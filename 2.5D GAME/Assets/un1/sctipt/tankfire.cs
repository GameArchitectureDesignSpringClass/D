using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tankfire : Unit {
    public float firedist = 5f;
    public float m_waittime = 5f;
    public float m_firewaittime = 1f;
    public GameObject tankray;
    float waittime = 0f;
    float firewaittime = 0f;
    GameObject enemy;
    GameObject parent;
    // Use this for initialization
    void Start () {
        enemy = GameObject.FindWithTag("Player");
        if (tankray != null)
            tankray.SetActive(false);
        firewaittime = m_firewaittime;
        if (transform.parent && transform.parent.GetComponent<callenemy>())
        {
            parent = transform.parent.gameObject;
            transform.parent = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Hp <= 0)
        {
            GetComponent<tankmove>().enabled = false;
            GetComponent<tankfire>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<BoxCollider>().enabled = false;
            if (parent)
                parent.GetComponent<callenemy>().enemycount--;
            return;
        }
        if (waittime == 0 && enemy != null)
        {
            if (Mathf.Abs(Vector3.Distance(enemy.transform.position, transform.position)) <= firedist && Mathf.Abs(Vector3.Distance(enemy.transform.position, transform.position)) > 4f && enemy.GetComponent<Unit>().Hp > 0)
            {
                if (GetComponent<tankmove>())
                    GetComponent<tankmove>().enabled = false;
                if (GetComponent<NavMeshAgent>() && GetComponent<NavMeshAgent>().isOnNavMesh)
                    GetComponent<NavMeshAgent>().destination = transform.position;
                tankray.SetActive(true);
                waittime = -1;
            }
        }
        else if (waittime == -1)
        {
            if (firewaittime <= 0)
            {
                PlayerAttack();
                waittime = 0.1f;
                firewaittime = m_firewaittime;
                tankray.SetActive(false);
            }
            else
                firewaittime -= Time.deltaTime;
        }
        else if (waittime >= m_waittime * 0.1f && waittime < m_waittime)
        {
            if (GetComponent<tankmove>())
                GetComponent<tankmove>().enabled = true;
            waittime += Time.deltaTime;
        }
        else if (waittime < m_waittime)
        {
            waittime += Time.deltaTime;
        }
        else
        {
            waittime = 0;
        }
	}
}
