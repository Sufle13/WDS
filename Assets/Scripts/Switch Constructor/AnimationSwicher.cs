using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
public class AnimationSwicher : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string animationNameParam;
    [SerializeField] bool animationState;

    public void AnimationChange(bool animationState)
    {
        if (animator != null)
        {
            if (animationState)
            {
                animator.SetBool(animationNameParam, true);
                this.animationState = true;
            }
            else
            {
                animator.SetBool(animationNameParam, false);
                this.animationState = false;
            }

        }
    }
}
