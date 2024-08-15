using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderNPC : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the spider moves
    public float wanderRange = 10f; // Maximum distance from the start point
    public float timeToChangeDirection = 3f; // Time interval to pick a new random direction

    private Vector3 startPosition; // Starting position of the spider
    private Vector3 targetPosition; // Target position to move towards
    private float timeSinceLastChange; // Time since the spider last changed direction

    private void Start()
    {
        startPosition = transform.position; // Store the spider's starting position
        PickNewTarget(); // Pick an initial target position
    }

    private void Update()
    {
        // Move the spider towards the target position
        MoveTowardsTarget();

        // Update time since the last direction change
        timeSinceLastChange += Time.deltaTime;

        // Check if it's time to pick a new target position
        if (timeSinceLastChange >= timeToChangeDirection)
        {
            PickNewTarget();
            timeSinceLastChange = 0f; // Reset the timer
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optionally, make the spider face the target direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void PickNewTarget()
    {
        // Generate a random position within the wander range
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += startPosition;

        // Set the target position while keeping it on the same Y level as the starting position
        targetPosition = new Vector3(randomDirection.x, startPosition.y, randomDirection.z);
    }
}