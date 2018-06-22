using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenesctrl : MonoBehaviour {
    public string scenename = "";
	// Use this for initialization
	void Start () {
        if (scenename != "")
        {
            SceneManager.UnloadSceneAsync(scenename);
            SceneManager.LoadSceneAsync(scenename);
        }
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
