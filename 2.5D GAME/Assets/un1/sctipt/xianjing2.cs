using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xianjing2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            gameObject.AddComponent<Rigidbody>();
            GetComponent<BoxCollider>().size = new Vector3(0.9f, 0.9f, 0.9f);
        }

    }
}
