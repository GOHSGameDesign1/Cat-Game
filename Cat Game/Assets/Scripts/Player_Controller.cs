using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
    public PlayerControls playerMovement;
    
    private Vector2 moveDirection;
    private InputAction move;
    private InputAction dash;

    public Rigidbody2D rb;
    public float moveSpeed;
    private bool isDashing;
    public float dashDuration;
    private float currentDashTimer;
    public float dashSpeed;
    private float dashDirectionX;
    private float dashDirectionY;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = new PlayerControls();
        isDashing = false;
    }

    private void OnEnable()
    {
        move = playerMovement.Player.Move;
        move.Enable();

        dash = playerMovement.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    private void OnDisable()
    {
        move.Disable();
        dash.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            Move();
            return;
        }

        DashFunction();

        currentDashTimer -= Time.deltaTime;

        if (currentDashTimer <= 0)
        {
            isDashing = false;
        }

    }

    private void Dash(InputAction.CallbackContext context)
    {
        if(moveDirection.x != 0 || moveDirection.y != 0)
        {
            isDashing = true;
            currentDashTimer = dashDuration;
            rb.velocity = Vector2.zero;
            dashDirectionX = moveDirection.x;
            dashDirectionY = moveDirection.y;
            Debug.Log("dashed");
            Debug.Log("OGMOGMOGMOSMGOSM");
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void DashFunction() 
    {
        rb.velocity = new Vector2(transform.right.x * moveDirection.x * dashSpeed, transform.up.y * moveDirection.y * dashSpeed);
    }
}
