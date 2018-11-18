using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FocusOnTarget))]
public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move
    }

    protected Rigidbody m_rigidbody;
    protected IEnumerator m_coroutine;

    protected Vector3 m_velocity;

    public Stats stats;

    public State state = State.Idle;

    public LayerMask floorLayers;

    protected virtual void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CurrentState();
    }

    private void CurrentState()
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

    protected virtual void Idle()
    {
        if (stats.speed == Stats.MIN_SPEED) return;
        stats.speed = Stats.MIN_SPEED;
    }

    protected virtual void Move()
    {
        m_velocity = transform.forward * stats.speed;
        m_velocity.y = m_rigidbody.velocity.y;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            state = State.Idle;
            stats.speed = Stats.MIN_SPEED;
            m_velocity = Vector3.zero;
            m_velocity.y = m_rigidbody.velocity.y;
            m_rigidbody.velocity = m_velocity;

            m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Target")
        {
            state = State.Move;
            stats.speed = Stats.MAX_SPEED;
            m_rigidbody.constraints = ~RigidbodyConstraints.FreezeAll;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}

[Serializable]
public class Stats
{
    public float speed = 2.0f;
    public float jumpForce = 10.0f;

    public const float MAX_SPEED = 0.5f;
    public const float MIN_SPEED = 0.0f;
}