using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class callenemy : MonoBehaviour {
    public List<GameObject> prfabs;
    public List<Vector2> id;
    public Vector3 range = Vector3.zero;
    public bool ones = false;
    public int enemycount = 0;
    int id_x, id_y;
    float mul=1;
	// Use this for initialization
	void Start () {
        id_x = id_y = 0;
        GetComponent<callenemy>().enabled = false;
        if (GameObject.Find("GameLevel").GetComponent<GameLevelSet>())
        {
            mul = GameObject.Find("GameLevel").GetComponent<GameLevelSet>().EnemyCount[GameObject.Find("GameLevel").GetComponent<GameLevelSet>().Level];
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (id_x < id.Count)
        {
            if (id[id_x].x <= prfabs.Count)
            {
                if (id_y < (int)(id[id_x].y * mul + 0.5))
                {
                    Vector2 p = id[id_x];
                    int intp_x = (int)(p.x - 1 + 0.5);
                    //int intp_y = (int)(p.y + 0.5);
                    Vector2 randompos = Vector2.zero;
                    randompos.x = Random.Range(-(range.x * 0.5f - 1), (range.x * 0.5f - 1));
                    randompos.y = Random.Range(-(range.z * 0.5f - 1), (range.z * 0.5f - 1));
                    GameObject obj = GameObject.Instantiate(prfabs[intp_x], transform);
                    obj.transform.localPosition = new Vector3(randompos.x, 0.5f, randompos.y);
                    if (ones)
                        obj.transform.parent = null;
                    else
                        enemycount++;
                    id_y++;
                }
                else
                {
                    id_x++;
                    id_y = 0;
                }
            }
        }
        else
        {
            id_x = id_y = 0;
            GetComponent<callenemy>().enabled = false;
            if (ones == true)
                Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (enemycount < 4)
            {
                if (GameObject.Find("GameLevel").GetComponent<GameLevelSet>())
                {
                    mul = GameObject.Find("GameLevel").GetComponent<GameLevelSet>().EnemyCount[GameObject.Find("GameLevel").GetComponent<GameLevelSet>().Level];
                }
                GetComponent<callenemy>().enabled = true;
            }
            /*if (prfabs.Count > 0 && id.Count > 0)
                foreach (var p in id)
                {
                    int intp_x = (int)(p.x - 1 + 0.5);
                    int intp_y = (int)(p.y + 0.5);
                    if (intp_x < prfabs.Count)
                    {
                        for (int i = 0; i < intp_y; i++)
                        {
                            Vector2 randompos = Vector2.zero;
                            randompos.x = Random.Range(-(range.x * 0.5f - 1), (range.x * 0.5f - 1));
                            randompos.y = Random.Range(-(range.z * 0.5f - 1), (range.z * 0.5f - 1));
                            GameObject obj = GameObject.Instantiate(prfabs[intp_x], transform);
                            obj.transform.localPosition = new Vector3(randompos.x, 0.5f, randompos.y);
                            obj.transform.parent = null;
                        }
                    }
                }*/
            
        }
    }
}
