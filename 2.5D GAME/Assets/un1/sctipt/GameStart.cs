using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {
    public List<GameObject> CreatObj;
    public List<GameObject> DestroyObj;
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start () {
        if (GameObject.Find("GameLevel"))
            if (GameObject.Find("GameLevel").GetComponent<GameLevelSet>().Level == 4)
                GameObject.Find("GameLevel").GetComponent<GameLevelSet>().Level = 1;
        foreach (var i in DestroyObj)
        {
            Destroy(i);
        }
        foreach (var i in CreatObj)
        {
            Instantiate(i);
        }
        GetComponent<GameStart>().enabled = false;
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
