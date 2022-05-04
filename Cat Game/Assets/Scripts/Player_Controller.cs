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
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = new PlayerControls();
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
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        Debug.Log("dashed");
    }
}
