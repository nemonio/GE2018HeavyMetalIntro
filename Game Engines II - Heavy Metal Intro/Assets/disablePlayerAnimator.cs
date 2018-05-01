using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disablePlayerAnimator : MonoBehaviour {

	public Animator playerAnimator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable() {

		playerAnimator.enabled = !playerAnimator.enabled;

	}
}
