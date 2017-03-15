﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {
    public GameObject followTarget;
    public float moveSpeed;
    private Vector3 targetPos;

    private static bool cameraExists;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    // Use this for initialization
    void Start () {
        if (!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }   
        else
        {
            Destroy(gameObject);
        }
        
        if (boundBox == null && SceneManager.GetActiveScene().buildIndex != 9 && SceneManager.GetActiveScene().buildIndex != 11 && SceneManager.GetActiveScene().buildIndex != 12 && SceneManager.GetActiveScene().buildIndex != 13)
        {
            boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        if (SceneManager.GetActiveScene().buildIndex != 9 && SceneManager.GetActiveScene().buildIndex != 11 && SceneManager.GetActiveScene().buildIndex != 12 && SceneManager.GetActiveScene().buildIndex != 13)
        {
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;


    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (boundBox == null && SceneManager.GetActiveScene().buildIndex != 9 && SceneManager.GetActiveScene().buildIndex != 11 && SceneManager.GetActiveScene().buildIndex != 12 && SceneManager.GetActiveScene().buildIndex != 13)
        {
            boundBox = FindObjectOfType<Bounds>().GetComponent<BoxCollider2D>();
            minBounds = boundBox.bounds.min;
            maxBounds = boundBox.bounds.max;
        }

        if (SceneManager.GetActiveScene().buildIndex != 9  && SceneManager.GetActiveScene().buildIndex != 11 && SceneManager.GetActiveScene().buildIndex != 12 && SceneManager.GetActiveScene().buildIndex != 13)
        {
            float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    public void setBounds(BoxCollider2D newBounds)
    {
        boundBox = newBounds;
        minBounds = boundBox.bounds.min;
        maxBounds = boundBox.bounds.max;
    }
}
