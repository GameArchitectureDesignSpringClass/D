using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fire : Unit {
    public string p_name = "";
    public float firedist = 5f;
    public float m_waittime = 3f;
    public int m_firetime = 10;
    //public GameObject m_obstacle;
    Animator anim;
    int thisid=0;
    GameObject enemy;
    GameObject parent;
    float enemydist;
    float waittime=0f;
    float Colliderradius=0f;
    //GameObject obstacle;
    // Use this for initialization
    /*private void Awake()
    {
        GameObject gamelevel = GameObject.Find("GameLevel");
        if (gamelevel)
        {
            GameLevelSet level = gamelevel.GetComponent<GameLevelSet>();
            if (level)
            {
                if (level.Level < level.EnemyLife.Count)
                    Hp *= level.EnemyLife[level.Level];
            }
        }
    }*/
    void Start () {
        if (GetComponent<Animator>())
            anim = GetComponent<Animator>();
        if (p_name != "")
        {
            switch (p_name) {
                case "xiaobing1":thisid = 1;break;
                case "xiaobing2": thisid = 2; break;
                case "xiaobing3": thisid = 3; break;
                case "dunge": thisid = 4; break;
                case "dungeguard": thisid = 5; break;
                case "shoulinghand": thisid = 6; break;
            }
        }
        enemy = GameObject.FindWithTag("Player");
        if(GetComponent<CapsuleCollider>())
            Colliderradius = GetComponent<CapsuleCollider>().radius;
        if (transform.parent && transform.parent.GetComponent<callenemy>())
        {
            parent = transform.parent.gameObject;
            transform.parent = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (anim != null && thisid != 0 && enemy!=null)
        {
            enemydist = Mathf.Abs(Vector3.Distance(transform.position, enemy.transform.position));
            AnimatorStateInfo animinfo = anim.GetCurrentAnimatorStateInfo(0);
            if (enemydist <= firedist && enemy.GetComponent<Unit>().Hp > 0)
            {
                if (animinfo.IsName("Grounded") && anim.GetBool("death") == false)
                {
                    if (waittime == 0)
                    {
                        /*if (obstacle != null)
                        {
                            Destroy(obstacle);
                        }*/
                        string p = "";
                        switch (thisid)
                        {
                            case 1: p = ""; break;
                            case 2: p = ""; break;
                            case 3: p = "com"; break;
                            case 4: p = "fire"; break;
                            case 5: p = "guard"; break;
                            case 6: p = "hand"; break;
                        }
                        anim.SetBool(p, true);
                        transform.LookAt(enemy.transform);
                        waittime += 0.1f;
                        //GetComponent<NavMeshAgent>().enabled = false;
                        /*if (m_obstacle != null)
                        {
                            obstacle = GameObject.Instantiate(m_obstacle, transform);
                        }*/
                    }
                    else if (waittime < m_waittime)
                        waittime += Time.deltaTime;
                    else
                    {
                        waittime = 0;
                        //GetComponent<NavMeshAgent>().enabled = true;
                    }
                }
                else
                {
                    string p = "";
                    switch (thisid)
                    {
                        case 1: p = ""; break;
                        case 2: p = ""; break;
                        case 3: p = "com"; break;
                        case 4: p = "fire"; break;
                        case 5: p = "guard"; break;
                        case 6: p = "hand"; break;
                    }
                    anim.SetBool(p, false);
                }
            }
        }
	}
    private void OnAnimatorMove()
    {
        if (anim)
        {
            AnimatorStateInfo animinfo = anim.GetCurrentAnimatorStateInfo(0);
            string p = "";
            switch (thisid)
            {
                case 1: p = ""; break;
                case 2: p = ""; break;
                case 3: p = "com"; break;
                case 4: p = "fire"; break;
                case 5: p = "guard"; break;
                case 6: p = "hand"; break;
            }
            if (animinfo.IsName("Grounded") == false && animinfo.normalizedTime >= 0.9f)
            {
                anim.SetBool(p, false);
            }
            if (animinfo.IsName("fire") && animinfo.normalizedTime >= 0.1f && animinfo.normalizedTime <= 0.9f && p == "fire")
            {
                    int firetime = (int)(animinfo.normalizedTime * 100);
                    if (firetime % m_firetime == 0)
                        PlayerAttack();
            }
            if (animinfo.IsName("com") && animinfo.normalizedTime >= 0.3f && animinfo.normalizedTime <= 0.5f && p == "com")
            {
                int firetime = (int)(animinfo.normalizedTime * 100);
                if (firetime % m_firetime == 0)
                    PlayerAttack();
            }
            if (animinfo.IsName("hand") && animinfo.normalizedTime >= 0.3f && animinfo.normalizedTime <= 0.5f && p == "hand")
            {
                int firetime = (int)(animinfo.normalizedTime * 100);
                if (firetime % m_firetime == 0)
                    PlayerAttack();
            }
                if (Hp == 0 && animinfo.IsName("death") == false && anim.GetBool("death") == false)
            {
                anim.SetBool("death", true);
                if (GetComponent<NavMeshAgent>())
                    GetComponent<NavMeshAgent>().enabled = false;
                if (GetComponent<movetoenemy>())
                    GetComponent<movetoenemy>().enabled = false;
                if (GetComponent<tankmove>())
                    GetComponent<tankmove>().enabled = false;
                if (GetComponent<Rigidbody>())
                {
                    GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Rigidbody>().freezeRotation = true;
                    anim.applyRootMotion = false;
                }
                if (GameObject.Find("shuliang"))
                {
                    GameObject.Find("shuliang").transform.GetChild(0).GetComponent<shuliangMenu>().killcount++;
                }
                if (parent)
                    parent.GetComponent<callenemy>().enemycount--;
            }
           // else if (anim.GetBool("death"))
                //anim.SetBool("death", false);
            else if (animinfo.IsName("death") && animinfo.normalizedTime <= 0.8f)
            {
                anim.SetBool("death", false);
                if (GetComponent<CapsuleCollider>())
                {
                    GetComponent<CapsuleCollider>().direction = 2;
                    if (animinfo.normalizedTime < 0.7f)
                        GetComponent<CapsuleCollider>().radius = Colliderradius * 0.8f;
                    else
                        GetComponent<CapsuleCollider>().radius = Colliderradius * 0.25f;
                }
            }
            else if (animinfo.IsName("death") && animinfo.normalizedTime >= 1.5f)
            {
                anim.SetBool("death", false);
                GetComponent<Fire>().enabled = false;
                if (GetComponent<BoxCollider>())
                    GetComponent<BoxCollider>().enabled = false;
                if (GetComponent<SphereCollider>())
                    GetComponent<SphereCollider>().enabled = false;
                if (GetComponent<CapsuleCollider>())
                    GetComponent<CapsuleCollider>().enabled = false;
                Destroy(gameObject, 1f);
            }
        }
    }
}
