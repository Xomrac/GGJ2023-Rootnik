using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jam
{

	public class TopdownController : MonoBehaviour
	{
		[SerializeField] private Rigidbody rb;
		public Rigidbody Rb => rb;

		[SerializeField] private SpriteRenderer spriteRenderer;
		public SpriteRenderer SpriteRenderer => spriteRenderer;

		[SerializeField] private float movementSpeed;
		public float MovementSpeed => movementSpeed;

		[SerializeField] private float flipThreshold;
		public float FlipThreshold => flipThreshold;

		[SerializeField] private Vector2 moveThreshold;
		public Vector2 MoveThreshold => moveThreshold;

		[SerializeField] private float maxSpeed;
		public float MaxSpeed => maxSpeed;

		private bool movingLeft;
		private bool movingRight;
		private Vector2 currentMovementSpeed;

		private void FixedUpdate()
		{
			Move();
		}
		void Flip(float speedValue)
		{
			if (speedValue > flipThreshold)
			{
				spriteRenderer.flipX = false;
			}
			else if (speedValue < (-flipThreshold))
			{
				spriteRenderer.flipX = true;
			}
		}
		bool IsMoving(Vector2 moveSpeed,Vector2 moveTreshold)
		{
			bool isMovingHorizontally = moveSpeed.x > moveTreshold.x || moveSpeed.x < (-moveTreshold.x);
			bool isMovingVertically = moveSpeed.y > moveTreshold.y || moveSpeed.y < (-moveTreshold.y);
			return isMovingHorizontally || isMovingVertically;
		}
		void Move()
		{
			currentMovementSpeed = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * (movementSpeed * Time.fixedDeltaTime);
			Flip(currentMovementSpeed.x);
			if (IsMoving(currentMovementSpeed,moveThreshold))
			{
				rb.isKinematic = false;
				rb.velocity = (new Vector3(currentMovementSpeed.x, 0, currentMovementSpeed.y));
			}
			else
			{
				//rb.isKinematic = true;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
			}
			// characterMovement.PlayerAnimator.SetBool("IsMoving", IsMoving(currentMovementSpeed,characterMovement.MoveTreshold));
		}

	}

}