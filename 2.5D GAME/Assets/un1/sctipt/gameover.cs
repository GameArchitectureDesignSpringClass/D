using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour {
    public GameObject scenectrl;
    GameObject player;
    GameObject child;
    float m_time = 124f;
	// Use this for initialization
	void Start () {
        if (transform.childCount > 1)
            child = transform.GetChild(1).gameObject;
	}

    // Update is called once per frame
    void Update() {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            return;
        }
        if (player && child && scenectrl)
        {
            Unit playerunit = player.GetComponent<Unit>();
            if (playerunit)
            {
                if (playerunit.Hp <= 0)
                {
                    child.SetActive(true);
                    if(m_time > 8)
                        m_time = 8f;
                }
            }
            if (m_time <= 0)
            {
                Cursor.visible = true;
                Instantiate(scenectrl);
                m_time = 12450f;
            }
            else if (m_time <= 8f)
                m_time -= Time.deltaTime;
        }
	}
}
