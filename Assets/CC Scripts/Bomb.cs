using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Detonate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject victim in enemies)
        {
            victim.BroadcastMessage("Damage");
        }
    }
}
