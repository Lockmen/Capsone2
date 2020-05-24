﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{

    public float moveSpeed = 5;
    public Camera aRCamera;
    //Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    public Animator Anim;
    protected override void Start()
    {
        base.Start();
        Anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        //viewCamera = Camera.main;
    }
    public void Shooting()
    {
        gunController.OnTriggerHold();
    }
    void Update()
    {
        // Movement input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

        // Look input
        Ray ray = aRCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin,point,Color.red);
            controller.LookAt(point);
        }

        // Weapon input
        /*
        if (Input.GetMouseButton(0))
        {
            gunController.OnTriggerHold();
        }
        if (Input.GetMouseButtonUp(0))
        {
            gunController.OnTriggerRelease();
        }
        */

        Anim.SetFloat("Blend", moveVelocity.magnitude);
    }
}