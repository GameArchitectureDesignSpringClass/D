using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destroyselfintime : MonoBehaviour {
    public float time = 0.5f;
	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, time);
    }
	
	// Update is called once per frame
	void Update () {
    }
}
