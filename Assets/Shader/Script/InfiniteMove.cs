using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMove : MonoBehaviour
{
	[SerializeField] private float rangeMove = 1f;
	[SerializeField] private float moveSpeed = 0.5f;
	[SerializeField] private bool moveRight = true;

	void Update()
	{
		if (transform.position.x > rangeMove)
			moveRight = false;
		if (transform.position.x < -rangeMove)
			moveRight = true;

		if (moveRight)
			transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
		else
			transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
	}
}
