using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanw : MonoBehaviour {

    public GameObject prefab;
    public float timer;
    public float cd;

	// Use this for initialization
	void Start() {
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        if(timer > cd)
        {
            Instantiate(prefab);
            timer = 0f;
        }
	}
}
