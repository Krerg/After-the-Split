﻿
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    AudioSource audioData;

    public enum Animation
    {
        action,
        idle,
        run_start,
        run_cycle,
        run_end,
        jump_start,
        jump_up_cycle,
        jump_down_cycle,
        jump_end,
        push_start,
        push_cycle,
        push_end,
        pull_start,
        pull_cycle,
        pull_end,
    }

    

    SkeletonAnimation spineAnimation;
    Animation currentAnimation;
    bool wasFalling;
    bool wasInAir;
    bool wasMoving;

    public bool isSara = false;

    void Start()
    {
        currentAnimation = Animation.idle;
        spineAnimation = GetComponent<SkeletonAnimation>();
        spineAnimation.AnimationName = currentAnimation.ToString();
        spineAnimation.AnimationState.End += OnAnimationEnded;
        audioData = GetComponent<AudioSource>();
    }

    public void Jump()
    {
        SetAnimation(Animation.jump_start, false);
    }

    public void Action()
    {
        print("Use");
        SetAnimation(Animation.action, false);
    }

    public void BeginPush()
    {
        SetAnimation(Animation.push_start, false);
    }

    public void EndPush()
    {
        if (currentAnimation == Animation.push_start || currentAnimation == Animation.push_cycle)
            SetAnimation(Animation.push_end, false);
    }

    public void BeginPull()
    {
        SetAnimation(Animation.pull_start, false);
    }

    public void EndPull()
    {
        if (currentAnimation == Animation.pull_start || currentAnimation == Animation.pull_cycle)
            SetAnimation(Animation.pull_end, false);
    }

    public void UpdateAnimation(bool isMoving, bool inAir, bool isFalling)
    {
        wasInAir = inAir;
        wasFalling = isFalling;
        wasMoving = isMoving;

        switch (currentAnimation) {
            case Animation.action:
                SetAnimation(Animation.idle, true);
                break;
                

            case Animation.idle:
                if (inAir && isFalling)
                    SetAnimation(Animation.jump_down_cycle, true);
                else if (isMoving)
                    SetAnimation(Animation.run_start, false);
                break;

            case Animation.run_start:
                if (inAir && isFalling)
                    SetAnimation(Animation.jump_down_cycle, true);
                else if (!isMoving)
                    SetAnimation(Animation.run_end, false);
                break;

            case Animation.run_cycle:
                if (inAir && isFalling)
                    SetAnimation(Animation.jump_down_cycle, true);
                else if (!isMoving)
                    SetAnimation(Animation.run_end, false);
                break;

            case Animation.run_end:
                if (inAir && isFalling)
                    SetAnimation(Animation.jump_down_cycle, true);
                else if (isMoving)
                    SetAnimation(Animation.run_start, false);
                break;

            case Animation.jump_start:
                if (!inAir)
                    SetAnimation(Animation.jump_end, false);
                break;

            case Animation.jump_up_cycle:
                if (!inAir)
                    SetAnimation(Animation.jump_end, false);
                else if (isFalling)
                    SetAnimation(Animation.jump_down_cycle, true);
                break;

            case Animation.jump_down_cycle:
                if (!inAir)
                    SetAnimation(Animation.jump_end, false);
                break;

            case Animation.jump_end:
                if (inAir)
                    SetAnimation(Animation.jump_start, false);
                break;
            
        }
    }

    void OnAnimationEnded(TrackEntry track)
    {
        switch (currentAnimation) {
            case Animation.action:
                
                print("Use ended");
                break;
            case Animation.run_start:
                audioData.loop = true;
                audioData.Play(0);
                SetAnimation(Animation.run_cycle, true);
                break;

            case Animation.run_end:
                audioData.Stop();
                SetAnimation(Animation.idle, true);
                break;

            case Animation.jump_start:
                audioData.Stop();
                /*if (track.Animation.Name == "idle")
                {
                    SetAnimation(Animation.jump_start, false);
                    break;
                } */
                SetAnimation(wasFalling ? Animation.jump_down_cycle : Animation.jump_up_cycle, true);
                break;

            case Animation.jump_end:
                audioData.Stop();
                SetAnimation(wasMoving ? Animation.run_cycle : Animation.idle, true);
                break;

            case Animation.push_start:
                audioData.Stop();
                SetAnimation(Animation.push_cycle, true);
                break;

            case Animation.pull_start:
                audioData.Stop();
                SetAnimation(Animation.pull_cycle, true);
                break;

            case Animation.push_end:
            case Animation.pull_end:
                audioData.Stop();
                if (wasInAir)
                    SetAnimation(wasFalling ? Animation.jump_down_cycle : Animation.jump_up_cycle, true);
                else if (wasMoving)
                    SetAnimation(Animation.run_cycle, true);
                else
                    SetAnimation(Animation.idle, true);
                break;
        }
    }

    void SetAnimation(Animation anim, bool loop)
    {
        if (currentAnimation != anim || spineAnimation.loop != loop) {
            currentAnimation = anim;
            spineAnimation.loop = loop;
            spineAnimation.AnimationName = currentAnimation.ToString();
        }
    }
}
