using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------//
public class Optitracker : MonoBehaviour {

    public List<OptitrackArenaObject> optitrackArenaObjects;

    public OptitrackStreamingClient osc;


    public void CreateOptitrackObject () {

    }
    public void DeleteOptitrackObject () {

    }
    
    public void SaveOptitrackObjects() {

    }
    public void LoadOptitrackObjects() {
        
    }

    public void ConnectAllOptitrack() {
        foreach(OptitrackArenaObject optitrackArenaObject in optitrackArenaObjects) {             
            optitrackArenaObject.ConnectOptitrack(osc);
        }
    }
    public void DisconnectAllOptitrack() {
        foreach(OptitrackArenaObject optitrackArenaObject in optitrackArenaObjects) {             
            optitrackArenaObject.DisconnectOptitrack();
        }
    }

    // Start is called before the first frame update
    void Start() {
        
        
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            ConnectAllOptitrack();
        }
    }
}
