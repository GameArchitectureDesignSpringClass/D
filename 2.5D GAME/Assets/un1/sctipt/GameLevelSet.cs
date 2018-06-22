using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelSet : MonoBehaviour {
    public int Level = 0;
    public List<float> PlayerLife;
    public List<float> PlayerAddLifeTime;
    public List<float> PlayerFireTime;
    public List<int> PlayerAmmoCount;
    public List<float> PlayerReloadTime;
    public List<bool> PlayerAmmoIsCross;
    public List<int> PlayerAmmoDamage;
    public List<int> PlayerThrowCount;
    public List<float> PlayerTrowReLoadTime;
    public List<float> EnemyLife;
    public List<float> DropShereTime;
    public List<int> KillEnemyCount;
    public List<float> EnemyCount;
    GameObject drop;
    // Use this for initialization
    private void Awake()
    {
        if (Level >= PlayerLife.Count)
            Level = PlayerLife.Count - 1;
        if (Level >= PlayerFireTime.Count)
            Level = PlayerFireTime.Count - 1;
        if (Level >= PlayerAmmoCount.Count)
            Level = PlayerAmmoCount.Count - 1;
        if (Level >= PlayerReloadTime.Count)
            Level = PlayerReloadTime.Count - 1;
        if (Level >= PlayerAmmoIsCross.Count)
            Level = PlayerAmmoIsCross.Count - 1;
        if (Level >= EnemyLife.Count)
            Level = EnemyLife.Count - 1;
        if (Level >= PlayerAmmoDamage.Count)
            Level = PlayerAmmoDamage.Count - 1;
        if (Level >= DropShereTime.Count)
            Level = DropShereTime.Count - 1;
        if (Level >= PlayerAddLifeTime.Count)
            Level = PlayerAddLifeTime.Count - 1;
        if (Level >= KillEnemyCount.Count)
            Level = KillEnemyCount.Count - 1;
        if (Level >= EnemyCount.Count)
            Level = EnemyCount.Count - 1;
    }
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Level == 3 && drop == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            GameObject shere = GameObject.Find("dropsphere");
            if (player && shere)
            {
                Vector3 pos = player.transform.position;
                pos.y = shere.transform.position.y;
                drop =  Instantiate(shere, pos, shere.transform.rotation, player.transform);
                drop.GetComponent<xianjing>().enabled = true;
                drop.GetComponent<xianjing>().range = new Vector3(15, 0, 15);
            }
        }
    }
}
