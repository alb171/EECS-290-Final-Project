using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    public float speed;
    private float pulledSpeedMultiplier = 5f;   // speed multiplier when enemy is pulled

    private bool moved;
    private bool pulled;

    public int numberOfRayHorizontal;
    public int numberOfRayVertical;

    public LayerMask collisionMask;

    private BoxCollider2D coll;
    public Bounds colliderBounds;
    public float skinWidth = 500f;
    public float horizontalRayLengthCoefficient = 1;
    public float verticalRayLengthCoefficient = 1;
    public float approxCenter;
    private RaycastOrigins raycastOrigins;

    private float raySpacingHorizontal;
    private float raySpacingVertical;
    private Vector2 velocity;
    private Vector2 negVelocity;
    private float speedQuadraticMultiplier = 0.5f;

    public CollisionInfo collisionInfo;
    public float shiftMovementDistanceLimit = 15f;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        CalculateBounds();
        CalculateRaySpacing();
    }

    public void CalculateBounds()
    {
        colliderBounds = coll.bounds;
        colliderBounds.Expand(-skinWidth * 2);

        raycastOrigins = new RaycastOrigins();
        raycastOrigins.topLeft = new Vector2(colliderBounds.min.x, colliderBounds.max.y);
        raycastOrigins.topRight = new Vector2(colliderBounds.max.x, colliderBounds.max.y);
        raycastOrigins.bottomLeft = new Vector2(colliderBounds.min.x, colliderBounds.min.y);
        raycastOrigins.bottomRight = new Vector2(colliderBounds.max.x, colliderBounds.min.y);
    }

    private void CalculateRaySpacing()
    {
        numberOfRayHorizontal = numberOfRayHorizontal < 2 ? 2 : numberOfRayHorizontal;
        numberOfRayVertical = numberOfRayVertical < 2 ? 2 : numberOfRayVertical;

        raySpacingVertical = (colliderBounds.extents.x * 2) / (numberOfRayHorizontal - 1);
        raySpacingHorizontal = (colliderBounds.extents.y * 2) / (numberOfRayVertical - 1);
    }

    private void RaycastHorizontal(ref Vector2 velocity)
    {
        float direction = Mathf.Sign(velocity.x);
        Vector2 raycastBase = direction < 0 ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
        //Debug.Log(direction);
        for (int i = 0; i < numberOfRayHorizontal; i++)
        {
            float distance = Mathf.Abs(velocity.x) * horizontalRayLengthCoefficient;
            Vector2 raycastOrigin = raycastBase + raySpacingHorizontal * i * Vector2.up;

            RaycastHit2D[] hits =
                Physics2D.RaycastAll(raycastOrigin, new Vector2(direction, 0), distance + skinWidth, collisionMask);
            Debug.DrawRay(raycastOrigin, new Vector2(velocity.x, 0), Color.red, Time.fixedDeltaTime, false);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.isTrigger)
                {
                    hit.collider.SendMessage("OnTriggerEnter2D", coll);
                }
                else
                {
                    if (Mathf.Abs(horizontalRayLengthCoefficient * velocity.x) > Mathf.Abs((hit.distance - skinWidth) * direction))
                    {
                        velocity.x = (hit.distance - skinWidth) * direction / horizontalRayLengthCoefficient;
                    }
                    collisionInfo.collideLeft = direction < 0;
                    collisionInfo.collideRight = direction > 0;
                }
            }
        }
    }

    private void RaycastVertical(ref Vector2 velocity)
    {
        float direction = Mathf.Sign(velocity.y);

        Vector2 raycastBase = direction < 0 ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
        //Debug.Log(direction);
        for (int i = 0; i < numberOfRayVertical; i++)
        {
            float distance = Mathf.Abs(velocity.y) * verticalRayLengthCoefficient;
            Vector2 raycastOrigin = raycastBase + raySpacingVertical * i * Vector2.right;

            RaycastHit2D[] hits =
                Physics2D.RaycastAll(raycastOrigin, new Vector2(0, direction), distance + skinWidth, collisionMask);
            Debug.DrawRay(raycastOrigin, new Vector2(0, velocity.y), Color.blue, Time.fixedDeltaTime, false);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.isTrigger)
                {
                    hit.collider.SendMessage("OnTriggerEnter2D", coll);
                }
                else
                {
                    if (Mathf.Abs(verticalRayLengthCoefficient * velocity.y) > Mathf.Abs((hit.distance - skinWidth) * direction))
                    {
                        velocity.y = (hit.distance - skinWidth) * direction / verticalRayLengthCoefficient;
                    }
                    collisionInfo.collideTop = direction > 0;
                    collisionInfo.collideBottom = direction < 0;
                }
            }
        }
    }
    // Use this for initialization
    void Start () {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        velocity = Vector2.one * speed;
        negVelocity = -1f * Vector2.one * speed;
    }

    // Update is called once per frame
    void Update() {
        if (!pulled)
        {
            float distance = Mathf.Sqrt((target.position.x - transform.position.x) * (target.position.x - transform.position.x)
                        + (target.position.y - transform.position.y) * (target.position.y - transform.position.y));
            if (distance >= shiftMovementDistanceLimit)
                StupidDiagonalMovement();
            else
                NormalMovement();

        }
        else
            PulledMovement();
    }

    private void PulledMovement()
    {
        
        if (speedQuadraticMultiplier < 4f)
        {
            speedQuadraticMultiplier += 0.1f;
        }
        Vector3 pos = Vector3.MoveTowards(this.transform.position, target.position, (speedQuadraticMultiplier * speedQuadraticMultiplier) * pulledSpeedMultiplier * speed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().MovePosition(pos);
        
        if (transform.position == target.position)
        {
            speedQuadraticMultiplier = 0.5f;
        }
    }

    private void StupidMixedMovement()
    {

    }

    private void StupidDiagonalMovement()
    {
        velocity = Vector2.one * speed;
        negVelocity = -1f * Vector2.one * speed;

        transform.position = new Vector3(transform.position.x, transform.position.y, 2);

        CalculateBounds();
        collisionInfo.Reset();

        RaycastHorizontal(ref velocity);
        RaycastHorizontal(ref negVelocity);
        RaycastVertical(ref velocity);
        RaycastVertical(ref negVelocity);

        if (target.position.x < (this.transform.position.x - approxCenter) && !collisionInfo.collideLeft)
        {
            this.transform.position += Vector3.left * speed * Time.deltaTime;
            //Debug.Log("Move left");
        }

        else if (target.position.x > (this.transform.position.x + approxCenter) && !collisionInfo.collideRight)
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
            //Debug.Log("Move right");
        }

        else if (target.position.x == this.transform.position.x)
            ;
        //Debug.Log("Left wall: " + collisionInfo.collideLeft);
        //Debug.Log("Right wall: " + collisionInfo.collideRight);

        if (target.position.y > (this.transform.position.y + approxCenter) && !collisionInfo.collideTop)
        {
            this.transform.position += Vector3.up * speed * Time.deltaTime;
            moved = true;
            //Debug.Log("Move up");
        }

        else if (target.position.y < (this.transform.position.y - approxCenter) && !collisionInfo.collideBottom)
        {
            this.transform.position += Vector3.down * speed * Time.deltaTime;
            moved = true;
            //Debug.Log("Move down");
        }

        else if (target.position.x == this.transform.position.x)
            ;

    }

    private void NormalMovement()
    {
        velocity = Vector2.one * speed;
        negVelocity = -1f * Vector2.one * speed;

        transform.position = new Vector3(transform.position.x, transform.position.y, 2);

        CalculateBounds();
        collisionInfo.Reset();

        RaycastHorizontal(ref velocity);
        RaycastHorizontal(ref negVelocity);
        RaycastVertical(ref velocity);
        RaycastVertical(ref negVelocity);

        moved = false;
        if (target.position.x < (this.transform.position.x - approxCenter) && !collisionInfo.collideLeft)
        {
            this.transform.position += Vector3.left * speed * Time.deltaTime;
            moved = true;
            //Debug.Log("Move left");
        }

        else if (target.position.x > (this.transform.position.x + approxCenter) && !collisionInfo.collideRight)
        {
            this.transform.position += Vector3.right * speed * Time.deltaTime;
            moved = true;
            //Debug.Log("Move right");
        }

        else if (target.position.x == this.transform.position.x)
            moved = false;

        //Debug.Log("Left wall: " + collisionInfo.collideLeft);
        //Debug.Log("Right wall: " + collisionInfo.collideRight);

        if (!moved)
        {
            if (target.position.y > (this.transform.position.y + approxCenter) && !collisionInfo.collideTop)
            {
                this.transform.position += Vector3.up * speed * Time.deltaTime;
                moved = true;
                //Debug.Log("Move up");
            }

            else if (target.position.y < (this.transform.position.y - approxCenter) && !collisionInfo.collideBottom)
            {
                this.transform.position += Vector3.down * speed * Time.deltaTime;
                moved = true;
                //Debug.Log("Move down");
            }
        }

        //Debug.Log("Top wall: " + collisionInfo.collideTop);
        //Debug.Log("Bottom wall: " + collisionInfo.collideBottom);
    }

    public void SetTarget(Transform target, bool pulled)
    {
        this.target = target;
        this.pulled = pulled;
        Debug.Log(pulled);
    }
}

public struct RaycastOrigins
{
    public Vector2 topLeft, topRight;
    public Vector2 bottomLeft, bottomRight;
}

public struct CollisionInfo
{
    public bool collideTop, collideBottom;
    public bool collideLeft, collideRight;
    public bool touchLeft, touchRight;
    public bool touchBottom, touchTop;
    public void Reset()
    {
        collideTop = collideBottom = collideLeft = collideRight = false;
    }
}
