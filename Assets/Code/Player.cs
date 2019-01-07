using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int playerID = 0;

    private Rigidbody myRigidbody;

    public float movementForce;

    private int health;
    public int maxHealth = 100;

	void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        health = maxHealth;
	}

    public void Damage(int damage)
    {
        health -= damage;
        if (health < 0) Destroy(gameObject);
    }
	
	void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis(string.Format("HorizontalP{0}", playerID)), Input.GetAxis(string.Format("VerticalP{0}", playerID)), .0f) * movementForce;

        myRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Force);
	}
}
