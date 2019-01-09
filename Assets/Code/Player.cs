using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int playerID = 0;

    public GameObject healthBar;
    private float healthBarFullWidth;

    private Rigidbody myRigidbody;

    public float movementForce;

    private int health;
    public int maxHealth = 100;

	void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();

        health = maxHealth;

        GetComponentInChildren<GrenadeLauncher>().PlayerID = playerID;

        healthBarFullWidth = healthBar.transform.localScale.x;
	}

    public void Damage(int damage)
    {
        health -= damage;

        healthBar.transform.localScale = new Vector3(healthBarFullWidth * ((float)health / maxHealth), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if (health < 0) Destroy(gameObject);
    }
	
	void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis(string.Format("HorizontalP{0}", playerID)), Input.GetAxis(string.Format("VerticalP{0}", playerID)), .0f) * movementForce;

        myRigidbody.AddForce(movement * Time.deltaTime, ForceMode.Force);

        transform.localScale = new Vector3(1.0f, 1.0f, -Mathf.Sign(myRigidbody.velocity.x));
	}
}
