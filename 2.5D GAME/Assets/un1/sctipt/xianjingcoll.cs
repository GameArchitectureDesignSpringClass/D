using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class xianjingcoll : MonoBehaviour {
    public int damage = 0;
    public bool broke = true;
    public GameObject warring;
	// Use this for initialization
	void Start () {
        if (warring)
        {
            Vector3 pos = transform.position;
            pos.y = 0.26f;
            Destroy(GameObject.Instantiate(warring, pos, Quaternion.Euler(0, 0, 0)), 2f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        Unit u = collision.gameObject.GetComponent<Unit>();
        if (u)
            u.Health(damage);
        if (broke && collision.transform.tag == "ground")
        {
            if(collision.gameObject.GetComponent<Rigidbody>() == null)
                collision.gameObject.AddComponent<Rigidbody>();
            Destroy(collision.gameObject, 2f);
            Destroy(gameObject, 2f);
            float force = GetComponent<Rigidbody>().mass * 2f;
            collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.down * force, transform.position);
            collision.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.92f, 0.92f, 0.92f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Unit u = other.gameObject.GetComponent<Unit>();
        if (u)
            u.Health(damage);
        if (broke && other.transform.tag == "ground")
        {
            if (other.gameObject.GetComponent<Rigidbody>() == null)
                other.gameObject.AddComponent<Rigidbody>();
            Destroy(other.gameObject, 2f);
            Destroy(gameObject, 2f);
            float force = GetComponent<Rigidbody>().mass * 2f;
            other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.down * force, transform.position);
            other.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.96f, 0.96f, 0.96f);
        }
    }
}
