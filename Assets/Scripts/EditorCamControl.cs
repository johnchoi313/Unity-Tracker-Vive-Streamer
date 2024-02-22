using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCamControl : MonoBehaviour {

    public float moveSpeed = 1;
    private float xAngle = 1, yAngle = 0;
    public float xSpeed = 1, ySpeed = 1;
    public float xMax = 60;

    [HideInInspector]
    public bool freezeMotion = false;

    public bool locked = false;
    public bool clickToRotate = false;

    public bool freefly = false;

    private Vector3 initialRotation;
    private Vector3 initialPosition;
    
    void Start() {
        yAngle = transform.localEulerAngles.y;
        xAngle = transform.localEulerAngles.x;

        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update () {

        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Space)) { locked = !locked; }

        //Reset Rotation
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R)) {
            yAngle = initialRotation.x;
            yAngle = initialRotation.y;
            transform.position = initialPosition;
            transform.eulerAngles = initialRotation;
            locked = true;     
        }

        if (!locked && Application.platform != RuntimePlatform.IPhonePlayer) {
            //Move
            if(!freezeMotion) {
                transform.localEulerAngles = new Vector3(freefly ? xAngle : 0, yAngle, 0);
                transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
            }
            //Rotate
            if(!clickToRotate || Input.GetMouseButton(0)) {
                xAngle -= Input.GetAxis("Mouse Y") * xSpeed * Time.deltaTime;
                yAngle += Input.GetAxis("Mouse X") * ySpeed * Time.deltaTime;
            }
            
            //Rotate using alt keys (Q/E)
            if(Input.GetKey(KeyCode.Q)) { yAngle -= ySpeed * Time.deltaTime * 2; }
            if(Input.GetKey(KeyCode.E)) { yAngle += ySpeed * Time.deltaTime * 2; }

            //Move Up/Down using R/F keys
            if(Input.GetKey(KeyCode.R)) { transform.position += new Vector3(0,moveSpeed * Time.deltaTime,0); }
            if(Input.GetKey(KeyCode.F)) { transform.position += new Vector3(0,-moveSpeed * Time.deltaTime,0); }

            //Clamp X Angle
            if (xAngle > xMax) { xAngle = xMax; }
            if (xAngle < -xMax) { xAngle = -xMax; }

            //Fix angles
            transform.localEulerAngles = new Vector3(xAngle, yAngle, 0);
        }
    }
}
