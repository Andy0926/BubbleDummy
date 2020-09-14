using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantFlyingEnemy : MonoBehaviour
{
	[SerializeField]
	float moveSpeed = 0.01f;

	[SerializeField]
	float frequency = 0.01f;

	[SerializeField]
	float magnitude = 0.01f;

	[SerializeField] float maxPosLeft = 0f;
	[SerializeField] float maxPosRight = 8f;

	bool facingLeft = false;

	Vector3 pos, localScale;

	// Use this for initialization
	void Start()
	{

		pos = transform.position;

		localScale = transform.localScale;

	}
	// Update is called once per frame
	void Update()
	{

		CheckWhereToFace();

		if (facingLeft)
			MoveLeft();
		else
			MoveRight();
	}

	void CheckWhereToFace()
	{
		if (pos.x < maxPosLeft)
			facingLeft = false;

		else if (pos.x > maxPosRight)
			facingLeft = true;

		if (((facingLeft) && (localScale.x < 0)) || ((!facingLeft) && (localScale.x > 0)))
		{
			localScale.x *= -1;
			//Debug.Log("Third");
		}

		transform.localScale = localScale;

	}

	void MoveLeft()
	{
		pos -= transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
		//Debug.Log("left"+pos);

	}

	void MoveRight()
	{
		pos += transform.right * Time.deltaTime * moveSpeed;
		transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
		//Debug.Log("right"+pos);

	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag.Equals("Bubble"))
		{
			Destroy(collision.gameObject);
			SoundManagerScript.PlaySound("Hit");
			Debug.Log("Hit");
			//GameControl.totalLife = 3;
			//Debug.Log("Mutant Die");

		}
	}
}
