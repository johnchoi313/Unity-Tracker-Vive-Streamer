using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArenaUnity;
using ArenaUnity.Components;

public class ArenaMarkerMapper : MonoBehaviour
{
    public List<ArenaArmarker> markers;

    // Update is called once per frame
    void Update() {
        
        if(Input.GetKeyDown(KeyCode.M)) {
            FindAllMarkers();
        }

    }

    public void ConnectObjectToMarker() {
        
    }

    public void FindAllMarkers() {    
        ArenaArmarker[] foundMarkers = FindObjectsOfType<ArenaArmarker>();
        Debug.Log(foundMarkers + " : " + foundMarkers.Length);
        markers = new List<ArenaArmarker>();

        foreach(ArenaArmarker marker in foundMarkers) {

            //ArenaObject arenaObject = marker.parent.GetComponent<ArenaObject>();

            //Debug.Log(arenaObject.json);

            Debug.Log("Name: " + marker.gameObject.name + 
                      " | MarkerID: " + marker.json.Markerid + 
                      " | MarkerType: " + marker.json.Markertype + 
                      " | Size(mm): " + marker.json.Size);

            markers.Add(marker);
        }
    }
}
