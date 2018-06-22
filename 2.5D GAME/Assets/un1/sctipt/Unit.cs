using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{


    public float Hp;
    public GameObject Diemodel;
    //Tank wpeaon
    public float WeaponSpeed;
    public GameObject WpeaonShell;
    public Transform WpeaonPosition;
    int PlayerAmmoDamage = -1;
    bool ammoiscross = false;
    [HideInInspector]
    public bool Win = false;
    public  void Awake()//LevelSet
    {
        GameObject gamelevel = GameObject.Find("GameLevel");
        if (gamelevel)
        {
            GameLevelSet level = gamelevel.GetComponent<GameLevelSet>();
            if (level)
            {
                int levelnum;
                levelnum =  level.Level;
                if (gameObject.layer == 10)
                {
                    if (levelnum < level.EnemyLife.Count)
                        Hp *= level.EnemyLife[levelnum];
                }
                if (gameObject.layer == 0)
                {
                    if (levelnum < level.PlayerLife.Count)
                        Hp = level.PlayerLife[levelnum];
                }
                if (GetComponent<playerfire>())
                {
                    playerfire m_playerfire = GetComponent<playerfire>();
                    m_playerfire.m_firetime = level.PlayerFireTime[levelnum];
                    m_playerfire.m_ammonumber = level.PlayerAmmoCount[levelnum];
                    m_playerfire.m_reloadtime = level.PlayerReloadTime[levelnum];
                    ammoiscross = level.PlayerAmmoIsCross[levelnum];
                    PlayerAmmoDamage = level.PlayerAmmoDamage[levelnum];
                    m_playerfire.m_AddLifeTime = level.PlayerAddLifeTime[levelnum];
                }
                if (GetComponent<playerthrow>())
                {
                    playerthrow m_playerthrow = GetComponent<playerthrow>();
                    m_playerthrow.m_ammonumber = level.PlayerThrowCount[levelnum];
                    m_playerthrow.m_reloadtime = level.PlayerTrowReLoadTime[levelnum];
                }
            }
        }
    }
    public void Health(int damage)
    {
        if (Win == false)
        {
            if (Hp > damage)
            {
                Hp -= damage;

            }
            else
            {
                PlayerDie();
            }
        }

    }
    public void PlayerDie()
    {

        //Destroy(gameObject);

        if (Diemodel != null && Hp > 0)
        {
            Destroy(Instantiate(Diemodel, transform.position, transform.rotation),2f);
        }
        Hp = 0;
    }

    public void PlayerAttack()
    {

        if (WpeaonShell != null)
        {
            GameObject newshoot = Instantiate(WpeaonShell, WpeaonPosition.position, WpeaonPosition.rotation) as GameObject;
            newshoot.transform.parent = WpeaonPosition;
            Rigidbody r = newshoot.GetComponent<Rigidbody>();
            r.velocity = WpeaonPosition.forward * WeaponSpeed;
            if (newshoot.GetComponent<shell>() && newshoot.GetComponent<shell>().damage < PlayerAmmoDamage)
            {
                if (ammoiscross)
                    newshoot.GetComponent<shell>().cross = true;
                if(PlayerAmmoDamage > -1)
                    newshoot.GetComponent<shell>().damage = PlayerAmmoDamage;
            }
        }

        else
        {
            return;
        }

    }
}