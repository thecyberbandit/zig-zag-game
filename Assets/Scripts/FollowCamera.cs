using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public Transform target;
    private Vector3 offset;

	// Use this for initialization
	void Start () {

        offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	private void LateUpdate () {

        transform.position = target.position + offset; 
	}
}
