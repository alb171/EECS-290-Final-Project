using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.AI;

public class Main : MonoBehaviour {
    public Transform[] target;
    public float speed;
    public float pushedWaitTime;
    public float pulledTime;

    private bool pushed;
    private bool pulled;

    private int current;
    private Vector3 savedPos;
    private float speedQuadraticMultiplier = 0;
    private float speedQuadraticMultiplierPulled = 0;
    private Transform targetBlackHole;
    private float pulledSpeedMultiplier = 1f;   // speed multiplier when main character is pulled

    public void SetTargetBlackHole(Transform blackHole)
    {
        savedPos = this.transform.position;
        targetBlackHole = blackHole;
        this.pulled = true;
        StartCoroutine(ResetTarget());
    }

    private IEnumerator ResetTarget()
    {
        yield return new WaitForSeconds(pulledTime);
        this.pulled = false;
        this.pushed = true;
    }

    private void PulledMovement()
    {
        if (speedQuadraticMultiplierPulled < 4f)
        {
            speedQuadraticMultiplierPulled += 0.1f;
        }
        Vector3 pos = Vector3.MoveTowards(this.transform.position, targetBlackHole.position, (speedQuadraticMultiplierPulled * speedQuadraticMultiplierPulled) * pulledSpeedMultiplier * speed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().MovePosition(pos);

        if (transform.position == targetBlackHole.position)
        {
            speedQuadraticMultiplier = 0.5f;
        }
    }

    private void PushedMovement()
    {
        // A quadratic multiplier. Basically the character, when returning to the original position will move slowly at first, then accelerate until it reaches the original speed
        if (speedQuadraticMultiplier < 1f)
        {
            speedQuadraticMultiplier += 0.1f;
        }
        Vector3 pos = Vector3.MoveTowards(transform.position, savedPos, (speedQuadraticMultiplier * speedQuadraticMultiplier) * speed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().MovePosition(pos);
        if (transform.position == savedPos) //target[current].position)
        {
            pushed = false;
            speedQuadraticMultiplier = 0f;
        }
    }

    private void NormalMovement()
    {
        if (transform.position != target[current].position)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[current].position, speed * Time.fixedDeltaTime);
            GetComponent<Rigidbody2D>().MovePosition(pos);
        }
        else if (current == target.Length - 1)
        {

        }
        else
        {
            current++;
        }
    }

    void FixedUpdate () {
        if (pulled)
        {
            PulledMovement();
        }
        else if (!pushed)
        {
            NormalMovement();
        }
        else
        {
            PushedMovement();
        }
	}

    public void Die()
    {
        transform.position = target[0].position;
    }

    // GetPushed: called from attacks
    public void GetPushed()
    {
        StartCoroutine(PushedAndReturn());
    }

    private IEnumerator PushedAndReturn()
    {
        savedPos = this.transform.position;
        // after being pushed, the main character will be stunned for pushedWaitTime seconds before starting to move back to the original position
        yield return new WaitForSeconds(pushedWaitTime);
        pushed = true;
    }
}
