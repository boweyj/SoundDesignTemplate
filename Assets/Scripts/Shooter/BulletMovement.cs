using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float bullet_speed;
    [SerializeField] private float despawn_distance;

    private Rigidbody2D rbody;

    private Vector2 movement_direction;
    private bool is_moving = false;

    private float distance_travelled;

    public void InitializeBullet(Vector2 direction)
    {
        rbody = GetComponent<Rigidbody2D>();
        movement_direction = direction.normalized;
        is_moving = true;
        distance_travelled = 0;
    }

    private void FixedUpdate()
    {
        if(is_moving)
        {
            Vector2 movement_delta = movement_direction * bullet_speed * Time.fixedDeltaTime;
            rbody.MovePosition(rbody.position + movement_delta);
            distance_travelled += movement_delta.magnitude;

            if(distance_travelled >= despawn_distance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyHealth h = collision.GetComponent<EnemyHealth>();
            h.Damage(1);
            Destroy(gameObject);
        }
    }
}
