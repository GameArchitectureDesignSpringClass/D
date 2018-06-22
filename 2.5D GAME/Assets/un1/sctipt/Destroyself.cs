using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destroyself : MonoBehaviour {
    public float dist = 1.2f;
    Transform enemy;
    bool aiactive = false;
    GameObject parent;
    // Use this for initialization
    void Start () {
        enemy = GameObject.FindWithTag("Player").transform;
        if (transform.parent && transform.parent.GetComponent<callenemy>())
        {
            parent = transform.parent.gameObject;
            //transform.parent = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<movetoenemy>() != null || GetComponent<tankmove>() != null)
        {
            if (GetComponent<movetoenemy>())
            {
                if (Vector3.Distance(transform.position, enemy.position) <= GetComponent<movetoenemy>().MaxEnemyDist && aiactive == false)
                    aiactive = true;
            }
            else if (GetComponent<tankmove>())
            {
                if (Vector3.Distance(transform.position, enemy.position) <= GetComponent<tankmove>().MaxEnemyDist && aiactive == false)
                    aiactive = true;
            }
            if (enemy != null && GetComponent<movetoenemy>() != null && aiactive == true)
            {
                if (Vector3.Distance(transform.position, enemy.position) > GetComponent<movetoenemy>().MaxEnemyDist * dist)
                {
                    if (parent && GetComponent<Fire>().Hp > 0)
                        parent.GetComponent<callenemy>().enemycount--;
                    Destroy(gameObject);
                }
                return;
            }
            if (enemy != null && GetComponent<tankmove>() != null && aiactive == true)
            {
                if (Vector3.Distance(transform.position, enemy.position) > GetComponent<tankmove>().MaxEnemyDist * dist)
                {
                    if (parent && GetComponent<tankfire>().Hp > 0)
                        parent.GetComponent<callenemy>().enemycount--;
                    Destroy(gameObject);
                }
                return;
            }
        }
        else
        {
            if (parent && GetComponent<Fire>() && GetComponent<Fire>().Hp > 0)
                parent.GetComponent<callenemy>().enemycount--;
            if (parent && GetComponent<tankfire>() && GetComponent<tankfire>().Hp > 0)
                parent.GetComponent<callenemy>().enemycount--;
            Destroy(gameObject, 1.5f);
        }
    }
}
