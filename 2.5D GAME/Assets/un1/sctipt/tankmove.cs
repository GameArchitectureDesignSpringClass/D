using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tankmove : MonoBehaviour {
    public Vector3 StartPos = Vector3.zero;
    public float MinEnemyDist = 2f;
    public float MaxEnemyDist = 10f;
    NavMeshAgent anget;
    Transform enemy;
    Transform pos;
    Vector3 pos_postion;
    Rigidbody rigbody;
    bool losetarget = false;
    // Use this for initialization
    void Start() {
        anget = GetComponent<NavMeshAgent>();
        enemy = GameObject.FindWithTag("Player").transform;
        pos = transform;
        pos_postion = pos.position;
        rigbody = GetComponent<Rigidbody>();
        anget.stoppingDistance = 1.5f;
        anget.enabled = false;
        transform.Translate(StartPos);

    }
    // Update is called once per frame
    void Update() {

        if (anget != null && enemy != null)
        {
            if (anget.isOnNavMesh == true) // onground
            {
                if (Mathf.Abs(Vector3.Distance(pos.position, enemy.position)) < MaxEnemyDist)
                    losetarget = false;
                else
                    losetarget = true;
                if (Mathf.Abs(Vector3.Distance(pos.position, enemy.position)) > MinEnemyDist && losetarget == false)
                    moveto(enemy.position);
                else if (losetarget == false)
                    moveto(transform.position);
                else
                    moveto(pos_postion);
                if (losetarget == false)
                {
                    lookenemy();
                }
                if (anget.hasPath == true)
                    animupdate();

            }
            else //fall
            {
                rigbody.isKinematic = false;
                GetComponent<BoxCollider>().isTrigger = true;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.25f))
            {
                if (hit.transform.tag == "ground")
                    onground();
            }
            if (anget.isOnNavMesh == true)
            {
                if (Physics.Raycast(transform.position + transform.forward * -2f, Vector3.down, 1f) == false)
                    if (Physics.Raycast(transform.position + transform.forward * 2f, Vector3.down, 1f) == false)
                    {
                        if (GetComponent<CapsuleCollider>())
                            GetComponent<CapsuleCollider>().enabled = false;
                        if (GetComponent<BoxCollider>())
                            GetComponent<BoxCollider>().enabled = false;
                        GetComponent<Unit>().Hp = 0;
                    }
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
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ground")
            onground();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "ground")
            onground();
    }
    void onground() {
        rigbody.isKinematic = true;
        GetComponent<BoxCollider>().isTrigger = false;
        anget.enabled = true;
    }
    void lookenemy() {
        Transform child_1 = transform.GetChild(0);
        if (child_1 != null)
            child_1 = child_1.GetChild(3);
        if (child_1 != null)
        {
            Vector3 rot = Vector3.RotateTowards(child_1.forward, enemy.transform.position - child_1.position, Time.deltaTime * 2, 0);
            child_1.rotation = Quaternion.LookRotation(rot);
            child_1.rotation = Quaternion.Euler(0, child_1.rotation.eulerAngles.y, 0);
        }
    }
    void animupdate() {
        Transform child;
        if (transform.GetChild(0) != null)
        {
            child = transform.GetChild(0);
            if (child.GetChild(1) != null)
            {
                Transform child_1,child_2;
                child_1 = child.GetChild(1);
                child_2 = child.GetChild(2);
                if (child_1.GetComponent<MeshRenderer>().material != null)
                {
                    child_1.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, -Time.time));
                    child_1.GetComponent<MeshRenderer>().material.SetTextureOffset("_BumpMap", new Vector2(0, -Time.time));
                    child_2.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, -Time.time));
                    child_2.GetComponent<MeshRenderer>().material.SetTextureOffset("_BumpMap", new Vector2(0, -Time.time));
                }
            }
        }
    }
}
