using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody rb;
    Animator anim;

    public float moveSpeed;
    private Vector3 Movevec;
    private float Dmag;
    private Vector3 Dvec;
    private Vector3 thrustVes;
    private Vector3 detapos;
    private float Dup;
    private float Dright;
    private float h;
    private float v;
    private float velocityH;
    private float velocityV;

    // Use this for initialization
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        PlayerMove();
        PlayerAnim();
    }

    void PlayerMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitt = new RaycastHit();
        Physics.Raycast(ray, out hitt, 100, LayerMask.GetMask("Ground"));

        if (hitt.transform != null)
        {
            transform.LookAt(new Vector3(hitt.point.x, transform.position.y, hitt.point.z));
        }

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Dup = Mathf.SmoothDamp(Dup, v, ref velocityH, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, h, ref velocityV, 0.1f);

        Vector2 temp = Algorithm(new Vector2(Dup, Dright));
        float Dright2 = temp.y;
        float Dup2 = temp.x;
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));

        Movevec = Dmag * transform.forward * moveSpeed;
        rb.velocity = new Vector3(Movevec.x, rb.velocity.y, Movevec.z);

    }

    void PlayerAnim()
    {
        float runLerp = Mathf.Lerp(anim.GetFloat("forward"), 1.0f, 0.5f);
        anim.SetFloat("forward", Dmag * runLerp);
    }

    private Vector2 Algorithm(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);

        return output;
    }
}
