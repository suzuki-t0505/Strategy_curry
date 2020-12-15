using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private CharacterController characterController;
    private GameManager gameManager;
    private UnitAnimator unitAnimator;



    // キャラクターの速度
    private Vector3 velocity;

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    // キャラクターの歩くスピード
    [SerializeField]
    private float walkSpeed = 2f;
    // キャラクターの走るスピード
    [SerializeField]
    private float runSpeed = 4f;
    // 回転スピード
    [SerializeField]
    private float rotateSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
        unitAnimator = GetComponent<UnitAnimator>();
    }

    public void Move()
    {
        Walk();
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    // 歩く
    private void Walk()
    {
        if (characterController.isGrounded)
        {
            velocity = Vector3.zero;

            // 移動
            var moveInput = Input.GetAxis("Vertical");
            WalkAndRun(moveInput);

            // 回転
            var rotateInput = Input.GetAxis("Horizontal");
            if (rotateInput != 0)
            {
                transform.Rotate(new Vector3(0f, rotateInput * rotateSpeed * Time.deltaTime, 0f));
            }
        }
    }

    private void WalkAndRun(float moveInput)
    {
        if (moveInput != 0f)
        {
            //animator.SetFloat("Speed", moveInput);
            unitAnimator.WalkAnima(moveInput);
            if (moveInput > 0.5f)
            {
                velocity += transform.forward * moveInput * runSpeed;
            }
            else
            {
                velocity += transform.forward * moveInput * walkSpeed;
            }
        }
        else
        {
            //animator.SetFloat("Speed", 0f);
            unitAnimator.StopWalkAnima();
        }
    }

    public void Stop()
    {
        velocity = Vector3.zero;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}


