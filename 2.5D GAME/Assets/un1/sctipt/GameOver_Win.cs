using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver_Win : MonoBehaviour {
    int count = 0;
    GameObject plane, player;
	// Use this for initialization
	void Start () {
        plane = GameObject.Find("plane");
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Unit>().Win = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (plane)
        {
            for (int i = 0; i < 10; i++)
            {
                if (count < plane.transform.childCount)
                {
                    Transform child = plane.transform.GetChild(count);
                    if (child.GetComponent<Rigidbody>() == null && child.GetComponent<BoxCollider>())
                    {
                        child.gameObject.AddComponent<Rigidbody>();
                        child.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.9f, 0.9f, 0.9f);
                        //Destroy(child.gameObject, 5f);
                    }
                    count++;
                }
            }
        }
    }
}
