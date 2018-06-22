using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySnd : MonoBehaviour {
    public List<AudioClip> Snd;
    AudioSource[] auds;
    int nosnd = 0;
	// Use this for initialization
	void Start () {
        auds = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Snd.Count > 0 && auds.Length > 0)
        {
            for (int j = 0; j < Snd.Count; j++)
            {
                int i = 0;
                for (; i < auds.Length; i++)
                {
                    if (auds[i].clip == null || auds[i].isPlaying == false)
                    {
                        auds[i].clip = Snd[j];
                        auds[i].Play();
                        Snd.RemoveAt(j);
                        j--;
                        break;
                    }
                }
                if (i >= auds.Length)
                {
                    if (auds[nosnd].time >= auds[nosnd].clip.length * 0.42f)
                    {
                        auds[nosnd].clip = Snd[j];
                        auds[nosnd].Play();
                        Snd.RemoveAt(j);
                        j--;
                        nosnd++;
                        if (nosnd >= auds.Length)
                            nosnd = 0;
                        break;
                    }
                    else
                    {
                        Snd.RemoveAt(j);
                        j--;
                        break;
                    }
                }
            }
        }
	}
}
