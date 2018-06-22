using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movetoenemy : MonoBehaviour {
    public Vector3 StartPos = Vector3.zero;
    public float MinEnemyDist = 2f;
    public float MaxEnemyDist = 10f;
    //public GameObject m_obstacle;
    NavMeshAgent anget;
    Transform enemy;
    Transform pos;
    Vector3 pos_postion;
    Rigidbody rigbody;
    Animator anim;
    bool losetarget = false;
    int pri;
    bool aiactive = false;
    //GameObject obstacle;
    // Use this for initialization
    void Start() {
        anget = GetComponent<NavMeshAgent>();
        enemy = GameObject.FindWithTag("Player").transform;
        pos = transform;
        pos_postion = pos.position;
        rigbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //anget.stoppingDistance = 1.5f;
        anget.enabled = false;
        transform.Translate(StartPos);
        pri = anget.avoidancePriority;
    }
	// Update is called once per frame
	void Update () {

        if (anget != null && enemy != null)
        {
            AnimatorStateInfo animinfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animinfo.IsName("Grounded") == false && anget.isOnNavMesh == true && anget.isActiveAndEnabled == true)
            {
                anget.avoidancePriority = 0;
               //anget.updatePosition = false;
                anget.updateRotation = false;
            }
            else
            {
                anget.avoidancePriority = pri;
                //anget.updatePosition = true;
                anget.updateRotation = true;
            }
            if (anget.isOnNavMesh == true && animinfo.IsName("Grounded")) // onground
            {
                if(aiactive == false)
                    aiactive = !aiactive;
                if (Mathf.Abs(Vector3.Distance(pos.position, enemy.position)) < MaxEnemyDist && enemy.GetComponent<Unit>().Hp > 0)
                    losetarget = false;
                else
                    losetarget = true;
                if (Mathf.Abs(Vector3.Distance(pos.position, enemy.position)) > MinEnemyDist && losetarget == false)
                    moveto(enemy.position);
                else if (losetarget == false)
                    moveto(transform.position);
                else
                    moveto(pos_postion);
                if (anget.hasPath == false && animinfo.IsName("Grounded"))
                {
                    lookenemy();
                }

            }
            else //fall
            {
                if (anim != null)//&& Physics.Raycast(transform.position,Vector3.down,0.3f) == false)
                {
                    anim.SetBool("OnGround", false);
                    anim.SetFloat("Jump", rigbody.velocity.y);
                }
                anget.enabled = false;
                rigbody.isKinematic = false;
                if (GetComponent<SphereCollider>())
                    GetComponent<SphereCollider>().isTrigger = true;
                else if (GetComponent<CapsuleCollider>())
                    GetComponent<CapsuleCollider>().isTrigger = true;
            }
            RaycastHit hit;
            if (anim.GetBool("OnGround") == false &&  Physics.Raycast(transform.position, Vector3.down,out hit, 0.3f))
            {
                if (hit.transform.tag == "ground")
                    onground();
            }
        }
	}
    void moveto(Vector3 targetpos) {
        //if (Mathf.Abs(Vector3.Distance(pos.position, targetpos)) > 0)
        //{
        if (anget != null)
        {
            anget.destination = targetpos;
        }
        if (anim != null && anget != null)
        {
            if (Mathf.Abs(anget.velocity.x) > Mathf.Abs(anget.velocity.y))
                anim.SetFloat("Forward", Mathf.Abs(anget.velocity.x));
            else
                anim.SetFloat("Forward", Mathf.Abs(anget.velocity.y));
        }
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ground" && anim.GetBool("OnGround") == false)
            onground();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ground" && anim.GetBool("OnGround") == false && aiactive == false)
        {
            onground();
            if (aiactive == false)
                aiactive = !aiactive;
        }
    }
    void onground() {
        rigbody.isKinematic = true;
        if (GetComponent<SphereCollider>())
            GetComponent<SphereCollider>().isTrigger = false;
        else if (GetComponent<CapsuleCollider>())
            GetComponent<CapsuleCollider>().isTrigger = false;
        if (anim != null)
        {
            anim.SetBool("OnGround", true);
            anim.SetFloat("Jump", 0);
        }
        if (anget != null)
        {
            /*if (transform.rotation.eulerAngles.x != 0 || transform.rotation.eulerAngles.z != 0)
                transform.rotation = Quaternion.Euler(0, 0, 0);*/
            //else
                anget.enabled = true;
        }
    }
    void lookenemy() {
        Vector3 rot = Vector3.RotateTowards(transform.forward, enemy.transform.position - transform.position, Time.deltaTime * 2, 0);
        if (anim != null && Mathf.Abs(transform.rotation.y - Quaternion.LookRotation(rot).y) > 0.001f)
        {
            anim.SetFloat("Forward", 0.5f);
        }
        transform.rotation = Quaternion.LookRotation(rot);
    }
    private void OnAnimatorMove()
    {
        AnimatorStateInfo anininfo = anim.GetCurrentAnimatorStateInfo(0);
        if (anininfo.IsName("Grounded") == false)
        {
            if (anget.isActiveAndEnabled)
            {
                moveto(transform.position);
            }
            /*anget.enabled = false;
            if (m_obstacle != null && obstacle == null)
                obstacle = GameObject.Instantiate(m_obstacle, transform.position, transform.rotation);*/
        }
        /*else
        {
            Destroy(obstacle);
            obstacle = null;
            anget.enabled = true;
            anget.nextPosition = transform.position;
        }*/
        //if (anininfo.IsName("Grounded"))
        //{
        if (Physics.Raycast(transform.position + transform.forward * -0.5f + transform.up, Vector3.down, 3f) == false && aiactive == true)
        {
            if (Physics.Raycast(transform.position + transform.up, Vector3.down, 3f) == false)
            {
                if (GetComponent<CapsuleCollider>())
                    GetComponent<CapsuleCollider>().enabled = false;
                if (GetComponent<BoxCollider>())
                    GetComponent<BoxCollider>().enabled = false;
                if (GetComponent<SphereCollider>())
                    GetComponent<SphereCollider>().enabled = false;
                GetComponent<Unit>().Hp = 0;
            }
        }
        //}
    }
}
