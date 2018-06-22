using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class shell : MonoBehaviour
{

    public float radius;
    public GameObject StartExplod;
    public GameObject ShellExplosion;
    public GameObject ShellExplosionNoDamage;
    public float power;
    public int damage;
    public List<string> masktag;
    public List<string> nodamagetag;
    public bool cross = false;
    public bool resetparent = true;
    public AudioClip m_Snd;
    bool IsAI;
    string rootname = "";


    private void Start()
    {
        if (StartExplod != null && transform.parent)
            Destroy(Instantiate(StartExplod, transform.parent.position + transform.parent.forward * 0.1f, transform.parent.rotation,transform.parent), 0.15f);
        if (transform.root != null)
            rootname = transform.root.name;
        if(resetparent == true)
            transform.parent = null;
        if (m_Snd && !GameObject.Find("Main"))
        {
            GameObject sndobj = GameObject.Find("Snd");
            if (sndobj)
            {
                PlaySnd sndobjsnd = sndobj.GetComponent<PlaySnd>();
                if (sndobjsnd)
                {
                    sndobjsnd.Snd.Add(m_Snd);
                }
            }
        }
    }

   /* void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name != rootname && (collision.transform.tag == "enemy" || collision.transform.tag == "ground" || collision.transform.tag == "Player"))
        {
            if (ShellExplosion != null)
            {
                Instantiate(ShellExplosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }

            Collider[] cols = Physics.OverlapSphere(transform.position, radius);
            if (cols.Length > 0)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    Rigidbody rb = cols[i].GetComponent<Rigidbody>();


                    if (rb != null)
                    {
                        rb.AddExplosionForce(power, transform.position, radius);
                    }

                    Unit u = cols[i].GetComponent<Unit>();


                    if (u != null)
                    {

                        u.Health(damage);

                    }
                }
            }
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name != rootname)
        {
                bool ismasktag = false;
            if (masktag.Count > 0)
            {
                foreach (var m in masktag)
                {
                    if (other.transform.tag == m)
                    {
                        ismasktag = true;
                        break;
                    }
                }
                if (ismasktag == false)
                    return;
            }
            if (nodamagetag.Count > 0)
            {
                foreach (var m in nodamagetag)
                {
                    if (other.transform.tag == m)
                    {
                        if (ShellExplosionNoDamage != null)
                        {
                            Destroy(Instantiate(ShellExplosionNoDamage, transform.position, transform.rotation), 1f);
                            Destroy(gameObject);
                        }
                        return;
                    }
                }
            }
            if (ShellExplosion != null)
            {
                Destroy(Instantiate(ShellExplosion, transform.position, transform.rotation),1f);
                if(cross == false)
                    Destroy(gameObject);
            }
            if (radius > 0)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);
                if (cols.Length > 0)
                {
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (nodamagetag.Count > 0)
                        {
                            foreach (var m in nodamagetag)
                            {
                                if (cols[i].transform.tag == m)
                                    break;
                                else
                                    setdamage(cols[i]);
                            }
                        }
                        else
                            setdamage(cols[i]);
                    }
                }
            }
            else
            {
                Unit u = other.GetComponent<Unit>();
                if (u != null)
                {
                    u.Health(damage);
                }
            }
        }
    }
    void setdamage(Collider col)
    {
        Rigidbody rb = col.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(power, transform.position, radius);
        }
        Unit u = col.GetComponent<Unit>();
        if (u != null)
        {
            u.Health(damage);
        }
    }
}
