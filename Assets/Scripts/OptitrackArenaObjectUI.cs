using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------------------------//

public class OptitrackArenaObjectUI: MonoBehaviour {
    public OptitrackArenaObject optitrackArenaObject;

}

[System.Serializable]
public class OptitrackArenaObject {

    public GameObject obj;
    public string name; 
    public int rigidBodyId;
    public bool connected = false;

    public Vector3 angleOffsets;
    public Vector3 positionOffsets;

    public void ConnectOptitrack(OptitrackStreamingClient osc) { obj = GameObject.Find(name);
        if (obj != null) {
            if(obj.GetComponent<OptitrackRigidBody>() == null) {
                obj.AddComponent<OptitrackRigidBody>();
            }
            obj.GetComponent<OptitrackRigidBody>().enabled = true;
            obj.GetComponent<OptitrackRigidBody>().StreamingClient = osc;
            obj.GetComponent<OptitrackRigidBody>().RigidBodyId = rigidBodyId;
            connected = true;
            Debug.Log("Connected Optitrack Rigidbody [" + rigidBodyId + "] to ARENA Object with name \"" + name + "\".");
        }

    }    
    public void DisconnectOptitrack() {
        if (obj != null) {
            if(obj.GetComponent<OptitrackRigidBody>() != null) {
                obj.GetComponent<OptitrackRigidBody>().enabled = false;
            }
        }
        connected = false;    
        Debug.Log("Disconnected Optitrack Rigidbody [" + rigidBodyId + "] to ARENA Object with name \"" + name + "\".");
    }
}

