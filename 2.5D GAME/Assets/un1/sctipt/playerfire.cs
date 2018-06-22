using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerfire : Unit {
    public float m_firetime = 0.5f;
    public GameObject redray;
    public int m_ammonumber = 15;
    public float m_reloadtime = 1f;
    public KeyCode reloadkey = KeyCode.R;
    public float m_AddLife = 1f;
    public float m_AddLifeTime = 0f;
    [HideInInspector]
    public bool cantfire = false;
    PlayerMovement pmove;
    float firetime;
    int ammonumber;
    float reloadtime;
    float addlifetime;
    public  bool reload = false;
    Animator anim;
    GameObject gamelevel;
    // Use this for initialization
    void Start() {
        pmove = GetComponent<PlayerMovement>();
        firetime = m_firetime;
        if (redray)
            redray.SetActive(false);
        ammonumber = m_ammonumber;
        reloadtime = m_reloadtime;
        addlifetime = m_AddLifeTime;
        anim = GetComponent<Animator>();
        gamelevel = GameObject.Find("GameLevel");
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (m_AddLifeTime > 0 && GetComponent<Unit>().Hp > 0)
        {
            if (gamelevel && gamelevel.GetComponent<GameLevelSet>())
            {
                GameLevelSet gamelevelnum = gamelevel.GetComponent<GameLevelSet>();
                if (GetComponent<Unit>().Hp < gamelevelnum.PlayerLife[gamelevelnum.Level])
                {
                    if (addlifetime <= 0)
                    {
                        GetComponent<Unit>().Hp += m_AddLife;
                        addlifetime = m_AddLifeTime;
                    }
                    else
                        addlifetime -= Time.deltaTime;
                }
            }
        }
        if (pmove && GetComponent<Unit>().Hp > 0)
        {
            if (Input.GetButton("Fire1") && pmove.fire == true && anim.GetLayerWeight(1) != 0)
            {
                if (firetime <= 0 && m_ammonumber > 0)
                {
                    PlayerAttack();
                    firetime = m_firetime;
                    m_ammonumber--;
                }
                else
                    firetime -= Time.deltaTime;
            }
            if (pmove.miaozhun == true && anim.GetLayerWeight(1) != 0)
            {
                if (redray)
                    redray.SetActive(true);
            }
            else
            {
                if (redray)
                    redray.SetActive(false);
            }
            if (m_ammonumber <= 0)
            {
                if (cantfire == false)
                    cantfire = !cantfire;
                reload = true;
            }
            if (Input.GetKey(reloadkey) && m_ammonumber < ammonumber)
            {
                if (cantfire == false)
                    cantfire = !cantfire;
                reload = true;
            }
            if (reload)
                reloadammo();
        }
    }
    void Update() {
        
    }
    void reloadammo() {
        if (m_reloadtime > 0)
        {
            m_reloadtime -= Time.deltaTime;
        }
        else
        {
            m_ammonumber = ammonumber;
            m_reloadtime = reloadtime;
            cantfire = !cantfire;
            reload = false;
        }
    }
}
