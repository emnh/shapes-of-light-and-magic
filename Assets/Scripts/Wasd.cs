using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Wasd : MonoBehaviour
{
    public Simulator Simulator;
    public float Speed = 100.0f;
    public Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mp = Mouse.current.position.ReadValue();
        var mouseOver = mp.x < 0.0f || mp.x >= 1.0f || mp.y < 0.0f || mp.y >= 1.0f;
        //if (!Application.isFocused || !mouseOver || EventSystem.current.IsPointerOverGameObject())
        if (!Application.isFocused || !mouseOver)
        {
            return;
        }

        float f = Speed * Time.deltaTime;
        if (Keyboard.current.shiftKey.isPressed)
        {
            f *= 10.0f;
        }
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            //Debug.Log("Can scroll up: " + MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f)).y);
            var canScrollUp = MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1.0f, 0.0f)).y < Simulator.MapSizeY - f;
            if (canScrollUp)
            {
                MainCamera.transform.position += f * Vector3.up;
            }
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            var canScrollDown = MainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.0f, 0.0f)).y > -(Simulator.MapSizeY - f);
            if (canScrollDown)
            {
                MainCamera.transform.position += f * Vector3.down;
            }
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            var canScrollLeft = MainCamera.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 0.0f)).x > -(Simulator.MapSizeX - f);
            if (canScrollLeft)
            {
                MainCamera.transform.position += f * Vector3.left;
            }
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            var canScrollRight = MainCamera.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 0.0f)).x < Simulator.MapSizeX - f;
            if (canScrollRight)
            {
                MainCamera.transform.position += f * Vector3.right;
            }
        }
        if (Keyboard.current.qKey.isPressed || Keyboard.current.pageUpKey.isPressed)
        {
            MainCamera.orthographicSize -= f;
        }
        if (Keyboard.current.eKey.isPressed || Keyboard.current.pageDownKey.isPressed)
        {
            MainCamera.orthographicSize += f;
        }
        if (Keyboard.current.homeKey.wasPressedThisFrame)
        {
            MainCamera.transform.position = Vector3.zero + 10.0f * Vector3.back;
        }

        MainCamera.orthographicSize = Math.Max(3.0f, Math.Min(50.0f, MainCamera.orthographicSize));
    }
}
