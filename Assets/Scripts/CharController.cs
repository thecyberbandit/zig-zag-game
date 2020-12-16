using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {

    public Transform rayStart;

    Rigidbody rb;
    bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;
    public GameObject crystalEffect;

	// Use this for initialization
	void Start () {

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
	}

    private void FixedUpdate()
    {
        if(!gameManager.gameStarted)
        {
            return;
        }

        else
        {
            anim.SetTrigger("gameStarted");
        }

        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    // Update is called once per frame
    void Update () {
		
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }

        RaycastHit hit;

        if(!Physics.Raycast(rayStart.position,-transform.up,out hit,Mathf.Infinity))
        {
            anim.SetTrigger("isFalling");
            rb.isKinematic = false;
        }

        else
        {
            anim.SetTrigger("notFallingAnymore");
            rb.isKinematic = true;
        }

        if(transform.position.y < -4)
        {
            gameManager.EndGame();
        }
	}

    public void Switch()
    {
        if(!gameManager.gameStarted)
        {
            return;
        }

        walkingRight = !walkingRight;

        if(walkingRight)
        {
            transform.rotation = Quaternion.Euler(0, 45, 0);
        }

        else
        {
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {
            gameManager.IncreaseScore();

            GameObject g = Instantiate(crystalEffect, rayStart.transform.position, Quaternion.identity);

            Destroy(g, 2);
            Destroy(other.gameObject);
        }
    }
}
