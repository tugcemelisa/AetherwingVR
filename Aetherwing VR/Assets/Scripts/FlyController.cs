using UnityEngine;

public class FlyController : MonoBehaviour
{
    public Transform headTransform;
    public Transform leftHand;
    public Transform rightHand;
    public Rigidbody rb;
    public Animator animator;

    public float flySpeed = 4f;
    public float handDistanceThreshold = 0.8f;
    public float walkThreshold = 0.1f;
    public float flyHoldTime = 0.5f;

    private bool isFlying = false;
    private bool isWalking = false;
    private float handHoldTimer = 0f;

    void Update()
    {
        float handDistance = Vector3.Distance(leftHand.position, rightHand.position);

        
        if (handDistance > handDistanceThreshold)
        {
            handHoldTimer += Time.deltaTime;
        }
        else
        {
            handHoldTimer = 0f;
        }

        isFlying = handHoldTimer >= flyHoldTime;

        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        isWalking = !isFlying && horizontalVelocity.magnitude > walkThreshold;

        
        if (animator != null)
        {
            animator.SetBool("isFlying", isFlying);
            animator.SetBool("isWalking", isWalking);
        }

       
        if (isFlying)
        {
            Vector3 flyDirection = headTransform.forward;
            rb.linearVelocity = flyDirection * flySpeed;
        }

        
        if (isWalking)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
