using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move
    }

    private Rigidbody m_rigidbody;
    private IEnumerator m_coroutine;

    private Vector3 m_velocity;

    public Stats stats;
    public Timers timers;

    public State state = State.Idle;

    public LayerMask floorLayers;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.isAlive == true)
        {
            switch (state)
            {
                case State.Idle:
                    Idle();
                    break;
                case State.Move:
                    Move();
                    break;
                default:
                    break;
            }
            m_rigidbody.velocity = m_velocity;
        }
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (stats.isOnGround == true)
            {
                timers.windowOfTimeToShortHop = 0.15f;
            }
        }

        if (timers.windowOfTimeToShortHop > 0)
        {
            //Short jump
            if (Input.GetMouseButtonUp(0) && stats.isOnGround)
            {
                timers.windowOfTimeToShortHop = 0;
                m_velocity.y = Mathf.Sqrt(stats.jumpForce);
            }
            else
            {// Normal jump
                timers.windowOfTimeToShortHop -= Time.deltaTime;

                if (timers.windowOfTimeToShortHop <= 0)
                {
                    timers.windowOfTimeToShortHop = 0;
                    m_velocity.y = Mathf.Sqrt(2f * stats.jumpForce);
                }
            }
        }
    }

    private void Idle()
    {
        if (stats.speed == Stats.MIN_SPEED) return;
        stats.speed = Stats.MIN_SPEED;
    }

    private void Move()
    {
        m_velocity = transform.forward * stats.speed;
        m_velocity.y = m_rigidbody.velocity.y;       

        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            state = State.Idle;
            stats.speed = Stats.MIN_SPEED;
            m_velocity = Vector3.zero;
            m_velocity.y = m_rigidbody.velocity.y;
            m_rigidbody.velocity = m_velocity;

            m_rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Target")
        {
            state = State.Move;
            stats.speed = Stats.MAX_SPEED;
            m_rigidbody.constraints = ~RigidbodyConstraints.FreezeAll;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((floorLayers.value & 1 << collision.contacts[0].otherCollider.gameObject.layer) == 0) return;

        if (Mathf.Clamp01(collision.contacts[0].point.y) >= 0)
        {
            stats.isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.contacts.Length <= 0)
        {
            stats.isOnGround = false;
            return;
        }

        if ((floorLayers.value & 1 << collision.contacts[0].otherCollider.gameObject.layer) == 0) return;

        stats.isOnGround = false;
    }
}

[Serializable]
public class Stats
{
    public float speed = 2.0f;
    public float jumpForce = 10.0f;

    public bool isAlive = true;

    public bool isOnGround = false;

    public const float MAX_SPEED = 0.5f;
    public const float MIN_SPEED = 0.0f;
}

[Serializable]
public class Timers
{
    public float windowOfTimeToShortHop = .15f;
}