using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MovingPlatform2D : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float moveSpeed = 1.6f;
    [SerializeField] private float waitSeconds = 0.35f;
    [SerializeField] private bool carryPlayer = true;

    private readonly HashSet<Rigidbody2D> riders = new HashSet<Rigidbody2D>();
    private Rigidbody2D body;
    private Transform target;
    private float waitUntil;
    private Vector2 previousPosition;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.bodyType = RigidbodyType2D.Kinematic;
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        GetComponent<Collider2D>().isTrigger = false;
        target = pointB != null ? pointB : pointA;
        previousPosition = body.position;
    }

    private void FixedUpdate()
    {
        Vector2 current = body.position;
        Vector2 next = current;

        if (target != null && Time.time >= waitUntil)
        {
            next = Vector2.MoveTowards(current, target.position, moveSpeed * Time.fixedDeltaTime);
            if (Vector2.Distance(next, target.position) < 0.02f)
            {
                next = target.position;
                target = target == pointA ? pointB : pointA;
                waitUntil = Time.time + waitSeconds;
            }
        }

        body.MovePosition(next);
        Vector2 delta = next - previousPosition;
        previousPosition = next;

        if (!carryPlayer || delta.sqrMagnitude <= 0.000001f)
        {
            return;
        }

        foreach (Rigidbody2D rider in riders)
        {
            if (rider != null)
            {
                rider.position += delta;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TryAddRider(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TryAddRider(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D otherBody = collision.rigidbody;
        if (otherBody != null)
        {
            riders.Remove(otherBody);
        }
    }

    private void TryAddRider(Collision2D collision)
    {
        if (!carryPlayer || collision.rigidbody == null)
        {
            return;
        }

        PlayerController2D player = collision.collider.GetComponentInParent<PlayerController2D>();
        if (player == null)
        {
            return;
        }

        for (int i = 0; i < collision.contactCount; i++)
        {
            ContactPoint2D contact = collision.GetContact(i);
            if (contact.normal.y < -0.45f)
            {
                riders.Add(collision.rigidbody);
                return;
            }
        }

        if (player.transform.position.y > transform.position.y + 0.12f)
        {
            riders.Add(collision.rigidbody);
        }
    }
}
