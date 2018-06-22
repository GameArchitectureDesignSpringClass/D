using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerthrow : Unit {
    public int m_ammonumber;
    public float m_reloadtime;
    [HideInInspector]
    public bool cantfire = false;
    PlayerMovement pmove;
    int ammonumber;
    float reloadtime;
    bool reload = true;
    // Use this for initialization
    void Start() {
        pmove = GetComponent<PlayerMovement>();
        ammonumber = m_ammonumber;
        reloadtime = m_reloadtime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pmove && GetComponent<Unit>().Hp > 0)
        {
            if (m_ammonumber <= 0)
            {
                if (cantfire == false)
                    cantfire = !cantfire;
                //reload = true;
            }
            if (reload && m_ammonumber < ammonumber)
                reloadtime -= Time.deltaTime;
            if (reloadtime <= 0 && m_ammonumber < ammonumber)
            {
                reloadtime = m_reloadtime;
                m_ammonumber++;
                cantfire = false;
            }
        }
    }
    void throwfire()
    {
        if (pmove && GetComponent<Unit>().Hp > 0)
        {
                if (m_ammonumber > 0)
                {
                    PlayerAttack();
                    m_ammonumber--;
                }
        }
    }
}
