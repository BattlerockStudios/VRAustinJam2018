using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : Character
{
    public Timers timers;

    public bool isAlive = true;
    public bool isOnGround = false;

    protected override void Awake()
    {
        base.Awake();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isAlive == true)
        {
            base.Update();
        }
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (isOnGround == true)
            {
                timers.windowOfTimeToShortHop = 0.15f;
            }
        }

        if (timers.windowOfTimeToShortHop > 0)
        {
            //Short jump
            if (Input.GetMouseButtonUp(0) && isOnGround)
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

    protected override void Idle()
    {
        base.Idle();
    }

    protected override void Move()
    {
        base.Move(); 

        Jump();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((floorLayers.value & 1 << collision.contacts[0].otherCollider.gameObject.layer) == 0) return;

        if (Mathf.Clamp01(collision.contacts[0].point.y) >= 0)
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.contacts.Length <= 0)
        {
            isOnGround = false;
            return;
        }

        if ((floorLayers.value & 1 << collision.contacts[0].otherCollider.gameObject.layer) == 0) return;

        isOnGround = false;
    }
}

[Serializable]
public class Timers
{
    public float windowOfTimeToShortHop = .15f;
}