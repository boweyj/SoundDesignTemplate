using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] private float horizontal_speed;
    [SerializeField] private float jump_force;
    [SerializeField] private float gravity;

    [SerializeField] private LayerMask ground_mask;

    [SerializeField] private AudioClip jump_sfx;

    private Vector2 movement_vector;
    private bool do_jump = false;
    private Vector2 delta;

    private bool is_grounded = false;

    private Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue input)
    {
        movement_vector = input.Get<Vector2>().normalized;
    }

    public void OnJump()
    {
        do_jump = true;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.05f, ground_mask);

        if (hit && movement_vector.y <= 0)
        {
            is_grounded = true;
            delta.y = 0;
        }
            
        delta.x = movement_vector.x * horizontal_speed * Time.fixedDeltaTime;
        
        if(do_jump && is_grounded)
        {
            delta.y = jump_force * Time.fixedDeltaTime;
            do_jump = false;
            is_grounded = false;
            GameManager.instance.sound_manager.PlaySFX(jump_sfx);
        }
        if (!is_grounded)
            delta.y -= gravity * Time.fixedDeltaTime;

        rbody.MovePosition(rbody.position + delta);
    }
}
