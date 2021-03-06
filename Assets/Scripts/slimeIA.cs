﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeIA : MonoBehaviour {

    private GameController _GameController;
    private Rigidbody2D slimeRb;
    private Animator slimeAnimator;

    public float speed;
    public float timeToWalk;

    public GameObject hitBox;


	// Use this for initialization
	void Start () {

        _GameController = FindObjectOfType(typeof(GameController)) as GameController;

        slimeRb = GetComponent<Rigidbody2D>();
        slimeAnimator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "hitBox")
        {
            Destroy(hitBox);
            _GameController.playSFX(_GameController.sfxEnemyDeath, 0.2f);

        }
    }
}
