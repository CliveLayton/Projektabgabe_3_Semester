using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private GameObject targetingVisuals;
    [SerializeField] private Animator animator;

    private Vector3 lastPosition;

    //sets lastposition to the current position
    private void Awake()
    {
        lastPosition = transform.position;
    }

    //sets the numbers for the animation of movement
    private void LateUpdate()
    {
        Vector3 movement = transform.position - lastPosition;

        if (movement.magnitude > 0)
        {
            movement.Normalize();
            animator.SetFloat("moveDirectionX", movement.x);
            animator.SetFloat("moveDirectionY", movement.y);
        }

        lastPosition = transform.position;
    }

    /// <summary>
    /// gives the coroutine the parameter for targeting duration
    /// </summary>
    /// <param name="duration">float</param>
    public void PlayTargetingAnimation(float duration)
    {
        StartCoroutine(PlayTargetingAnimationCoroutine(duration));
    }

    //sets the viusal for targeting on and scales it to the shadow in the time of the duration
    private IEnumerator PlayTargetingAnimationCoroutine(float duration)
    {
        targetingVisuals.SetActive(true);
        targetingVisuals.transform.localScale = Vector3.zero;

        float timePassed = 0;
        while (timePassed < duration)
        {
            yield return null;
            timePassed += Time.deltaTime;
            float alpha = timePassed / duration;

            targetingVisuals.transform.localScale = new Vector3(alpha, alpha, alpha);
        }
        targetingVisuals.SetActive(false);
    }
}
