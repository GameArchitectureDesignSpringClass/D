using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tofoward1 : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindWithTag("Player"))
        {
            transform.forward = GameObject.FindWithTag("Player").transform.forward;
        }
        Vector3 rot = transform.rotation.eulerAngles;
        rot.x -= 18;
        transform.rotation = Quaternion.Euler(rot);
    }
}
