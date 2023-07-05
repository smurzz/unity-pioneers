using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoxDefaultMovement : MonoBehaviour
{
	public Transform player;
	public GameObject gameOverWindow;
	private CharacterController characterController;
	private Animator animator;
	[SerializeField] AudioSource deathSound;
	[SerializeField] private float moveSpeed = 4f;
	[SerializeField] private float rotationSpeed = 10f;
	[SerializeField] private float minDistance = 1f;
	[SerializeField] private float maxDistance = 10f;
	private bool gameOver = false;

	private void Start()
	{
		gameOverWindow.SetActive(false);

		characterController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!gameOver)
		{
			Vector3 direction = player.position - transform.position;
			direction.y = 0f;

			// Rotate towards the player
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

			float distance = Vector3.Distance(transform.position, player.position);

			if (distance > minDistance && distance < maxDistance)
			{
				// Move towards the player
				Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
				characterController.Move(movement);

				// Play animation
				animator.SetBool("isRunning", true);
			}
			else
			{
				// Stop moving and play idle animation
				animator.SetBool("isRunning", false);
			}
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player") && !gameOver)
		{
			other.gameObject.GetComponent<PlayerMovementBackwarts>().enabled = false;
			Die();
		}
	}

	private void Die()
	{
		deathSound.Play();
		gameOverWindow.SetActive(true);
		gameOver = true;
	}

}
