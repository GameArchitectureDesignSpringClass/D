using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xianjing : MonoBehaviour {
    public GameObject dropobj;
    public Vector3 range = Vector3.zero;
    public float m_droptime = 10f;
    public float destime = 5f;
    float droptime;
    Unit playeru;
    // Use this for initialization
    private void Awake()
    {
        
    }
    void Start () {
        GameObject gamelevel = GameObject.Find("GameLevel");
        if (gamelevel)
        {
            GameLevelSet level = gamelevel.GetComponent<GameLevelSet>();
            if (level)
            {
                int levelnum;
                levelnum = level.Level;
                m_droptime *= level.DropShereTime[levelnum];
            }
        }
        droptime = m_droptime;
        playeru = GameObject.FindWithTag("Player").GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playeru == null ||  playeru.Win == false)
        {
            if (range != Vector3.zero && dropobj != null)
            {
                if (droptime > 0)
                    droptime -= Time.deltaTime;
                else
                {
                    float pos_x, pos_y, pos_z;
                    pos_y = range.y;
                    pos_x = Random.Range(-range.x * 0.5f, range.x * 0.5f);
                    pos_z = Random.Range(-range.z * 0.5f, range.z * 0.5f);
                    Vector3 pos = transform.position + new Vector3(pos_x, pos_y, pos_z);
                    GameObject obj = GameObject.Instantiate(dropobj, pos, transform.rotation, transform);
                    obj.transform.parent = null;
                    Destroy(obj, destime);
                    droptime = m_droptime;
                }
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
            GetComponent<xianjing>().enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
            GetComponent<xianjing>().enabled = false;
    }
}
