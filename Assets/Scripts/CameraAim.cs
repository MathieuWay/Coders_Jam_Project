﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    public Transform player;
    public Transform playerGun;
    public Transform playerTransformCamera;
    private Camera playerCamera;
    public Transform reticule;

    //Parameters
    [Range(1, 20)]
    public float aimRange;
    [Range(1, 20)]
    public float cameraRange;
    public AnimationCurve cameraOffsetSmoothing;

    //DEBUG
    Vector3 mouseViewportPos = Vector3.zero;
    Vector3 aimDirection = Vector3.zero;
    Vector3 aimPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = playerTransformCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Cursor.visible = false;
        mouseViewportPos = playerCamera.ScreenToViewportPoint(Input.mousePosition);
        aimDirection = new Vector3(Mathf.Lerp(-1, 1, mouseViewportPos.x) * Screen.width / Screen.height, Mathf.Lerp(-1, 1, mouseViewportPos.y), 0);
        //aimDirection.x *= Screen.width / Screen.height;
        //Debug.Log("Viewport:"+mouseViewportPos+"    /aim:"+ aimDirection);

        Vector3 aimPosition = player.position + aimDirection * aimRange;
        Vector3 cameraPosition = Vector3.Lerp(player.position, player.position + aimDirection * cameraRange, cameraOffsetSmoothing.Evaluate(aimDirection.magnitude / 1));




        reticule.position = aimPosition;
        playerTransformCamera.position = cameraPosition;

        //Body Direction
        if (aimDirection.x <= 0)
            player.localScale = new Vector3(-1, 1, 1);
        else
            player.localScale = new Vector3(1, 1, 1);

        //Gun Aim

        Quaternion rotation = Quaternion.LookRotation(aimDirection);
        rotation.x = 0;
        rotation.y = 0;
        playerGun.rotation = rotation;
    }
}
