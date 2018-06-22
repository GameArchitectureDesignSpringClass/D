using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_speed;
    public Vector3 rightpos;
    public Vector3 leftpos;
    public Vector3 lefthandepos;
    public KeyCode ThrowKey = KeyCode.G;

    private float Dmag;
    Vector3 Devc;
    Vector3 Movevec;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;
    float speed;
    bool Isdie;
    bool inputEnable;

    private float h;
    private float v;
    [HideInInspector]
    public bool fire;
    [HideInInspector]
    public bool miaozhun;


    void Awake()
    {
        miaozhun = false;
        fire = false;
        inputEnable = true;
        Isdie = false;
        floorMask = LayerMask.GetMask("Ground");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        speed = m_speed;
    }

    void FixedUpdate()
    {
        if(inputEnable == true)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }

        if(fire == true || miaozhun == true)
        {
            speed = m_speed * 0.8f;
        }
        else
        {
            speed = m_speed;
        }

        Move(h, v);

        if(Input.GetMouseButton(1) && Isdie == false)
        {
            miaozhun = true;
            Turning();
        }
        else
        {
            miaozhun = false;
            turn(h,v);
        }
        
        Animating(h,v);
        PlayerShoot();

    }
    private void Update()//die
    {
        if (GetComponent<Unit>())
        {
            AnimatorStateInfo animinfo = anim.GetCurrentAnimatorStateInfo(0);
            if (GetComponent<Unit>().Hp == 0 && animinfo.IsName("die") == false)
                anim.SetBool("Die",true);
            if (transform.position.y < -3)
                GetComponent<Unit>().Hp = 0;
        }
    }
    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void turn(float h, float v)
    {
        Vector2 temp = Algorithm(new Vector2(h, v));
        float Dright2 = temp.y;
        float Dup2 = temp.x;
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Devc = Dup2 * Vector3.right + Dright2 * Vector3.forward;
        if (Dmag > 0.1)
        {
            Vector3 targent = Vector3.Slerp(transform.forward, Devc, 0.5f);
            transform.forward = targent;
            //Debug.Log(Devc);
        }
        
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
        Vector2 temp = Algorithm(new Vector2(h, v));
        float Dright2 = temp.y;
        float Dup2 = temp.x;
        Devc = Dup2 * Vector3.right + Dright2 * Vector3.forward;
        //Devc = Vector3.Slerp(transform.forward, Devc, 0.5f);

    }

    void Animating(float h,float v)
    {

        Vector3 localdevc = transform.InverseTransformVector(Devc);

        float runLerp = Mathf.Lerp(anim.GetFloat("forward"), 1.0f, 0.5f);
        //if (miaozhun == false)
        //{
            anim.SetFloat("forward", localdevc.z);
            anim.SetFloat("right", localdevc.x);
        //}
        /*else if (transform.forward == Vector3.left || transform.forward * -1 == Vector3.right)
        {
            anim.SetFloat("forward", Devc.x);
            anim.SetFloat("right", Devc.z);
        }
        else
        {
            anim.SetFloat("forward", Devc.z);
            anim.SetFloat("right", Devc.x);
        }*/

        //Debug.Log(localdevc.x);
        //playerRigidbody.velocity = new Vector3(Movevec.x, 0, Movevec.z);
    }

    void PlayerShoot()
    {
        if (Input.GetButton("Fire1") && GetComponent<playerfire>().cantfire == false && Isdie == false)
        {
            anim.SetBool("Attack", true);
        }
        else anim.SetBool("Attack", false);
        if (Input.GetKey(ThrowKey) && GetComponent<playerthrow>().cantfire == false && Isdie == false)
        {
            anim.SetBool("Throw", true);
        }
        else anim.SetBool("Throw", false);

    }
    private Vector2 Algorithm(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2);

        return output;
    }

    //private void OnAnimatorIK()
    //{
    //    Transform righthande = anim.GetBoneTransform(HumanBodyBones.RightLowerArm);
    //    righthande.localEulerAngles += rightpos;
    //    anim.SetBoneLocalRotation(HumanBodyBones.RightLowerArm, Quaternion.Euler(righthande.localEulerAngles));

    //    Transform lefthande = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
    //    lefthande.localEulerAngles += leftpos;
    //    anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(lefthande.localEulerAngles));

    //    Transform lefthande_ = anim.GetBoneTransform(HumanBodyBones.LeftHand);
    //    lefthande_.localEulerAngles += lefthandepos;
    //    anim.SetBoneLocalRotation(HumanBodyBones.LeftHand, Quaternion.Euler(lefthande_.localEulerAngles));
    //}

    void Death()
    {
        anim.SetTrigger("Die");
    }

    
    void DieOnEnterl()
    {

        inputEnable = false;
        h = 0;
        v = 0;
        Isdie = true;
        anim.SetBool("Die", false);
    }

    void FireOnEnterl()
    {
        fire = true;
    }

    void FireOnExit()
    {
        fire = false;
    }

}
