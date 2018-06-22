using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class changeaudio : MonoBehaviour {
    public AudioClip m_audio;
    public AudioSource audiosource;
    public bool usetrigger = false;
    public bool ones = true;
    bool used = false;
	// Use this for initialization
	void Start () {
        audiosource = GameObject.FindWithTag("Sound").GetComponent<AudioSource>();
        if (m_audio && audiosource)
        {
            audiosource.Stop();
            audiosource.clip = m_audio;
            audiosource.Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (usetrigger && !used)
                Start();
            if (ones && !used)
            {
                usetrigger = false;
                used = true;
            }
        }
    }
}
