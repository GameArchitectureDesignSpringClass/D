using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tofoward : MonoBehaviour {
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
        rot.x += 8;
        transform.GetChild(0).rotation = Quaternion.Euler(rot);
        transform.GetChild(1).rotation = Quaternion.Euler(rot);
    }
}
