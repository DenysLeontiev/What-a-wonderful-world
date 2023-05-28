using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private const string MOVE_STATE_NAME = "isRunning";

    private Animator animalAnimator;

    [SerializeField] private float moveSpeed = 1f;

    private int moveTime;
    private float moveCounter;

    private int waitTime;
    private float waitCounter;

    private bool isMoving;
    private int randomMoveDirection;

    private void Start()
    {
        animalAnimator = GetComponent<Animator>();

        moveTime = Random.Range(3, 7);
        waitTime = Random.Range(4, 6);

        moveCounter = moveTime;
        waitCounter = waitTime;

        ChooseRandomMoveDirection();
    }

    private void Update()
    {
        animalAnimator.SetBool(MOVE_STATE_NAME, isMoving);

        if (isMoving)
        {

            moveCounter -= Time.deltaTime;

            Vector3 moveVector = transform.forward * moveSpeed * Time.deltaTime;
            switch (randomMoveDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                    transform.position += moveVector;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += moveVector;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
                    transform.position += moveVector;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                    transform.position += moveVector;
                    break;
            }

            if(moveCounter <= 0)
            {
                moveCounter = moveTime;
                isMoving = false;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            if(waitCounter <= 0)
            {
                ChooseRandomMoveDirection();
            }
        }
    }

    private void ChooseRandomMoveDirection()
    {
        randomMoveDirection = Random.Range(0, 4);

        waitCounter = waitTime;
        isMoving = true;
    }
}
