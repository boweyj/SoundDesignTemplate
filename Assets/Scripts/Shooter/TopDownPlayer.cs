using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayer : MonoBehaviour
{

    [SerializeField] private Transform bullet_spawn_position;
    [SerializeField] private GameObject bullet_prefab;

    [SerializeField] private float movement_speed;

    [SerializeField] private AudioClip shooting_sfx;

    private Rigidbody2D rbody;
    private Vector2 movement_input;
    private Vector2 mouse_position;
    private bool do_shoot = false;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        movement_input = value.Get<Vector2>().normalized;
    }

    public void OnShoot()
    {
        do_shoot = true;
    }

    public void OnMouseMove(InputValue value)
    {
        mouse_position = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
    }

    private void FixedUpdate()
    {
        Vector2 movement_delta = movement_input * movement_speed * Time.fixedDeltaTime;
        rbody.MovePosition(rbody.position + movement_delta);
    }

    private void Update()
    {
        Vector2 heading = mouse_position - (Vector2)transform.position;
        Vector2 cursor_pos = (Vector2)transform.position + heading.normalized;
        bullet_spawn_position.position = cursor_pos;

        if(do_shoot)
        {
            GameManager.instance.sound_manager.PlaySFX(shooting_sfx);
            do_shoot = false;
            GameObject new_bullet = Instantiate(bullet_prefab, bullet_spawn_position.position, Quaternion.identity);
            BulletMovement bullet = new_bullet.GetComponent<BulletMovement>();
            bullet.InitializeBullet(heading);
        }
    }
}
