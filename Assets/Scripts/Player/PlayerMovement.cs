using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private InputMaster inputActions;
    private Vector2 movementInput;

    private Rigidbody2D rb;
    private new Collider2D collider;
    private Animator animator;

    private bool playerMoving;
    private Vector2 lastMove;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        inputActions = new InputMaster();
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 calculatedMovement = movementInput * speed; //* Time.deltaTime;
        playerMoving = !movementInput.Equals(Vector2.zero);
        if(playerMoving) lastMove = movementInput;
  
        UpdateAnimator();

        //rb.MovePosition((Vector2)transform.position + calculatedMovement * Time.deltaTime);
        rb.velocity = calculatedMovement;
    }

    private void UpdateAnimator() {
        animator.SetFloat("MoveX", movementInput.x);
        animator.SetFloat("MoveY", movementInput.y);
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }
}
