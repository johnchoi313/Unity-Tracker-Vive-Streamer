using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArenaUnity;
using ArenaUnity.Components;

using TMPro;

//-------------------------------------//



public class ViveTrackerManager : MonoBehaviour {

    public Transform viveOrigin;

    public List<ViveTrackerArenaObject> viveTrackerArenaObjects;

    public ArenaMarkerMapper arenaMarkerMapper;

    public List<ArenaArmarker> viveMarkers;

    public TMP_Text uiText;


    void Update() {
        //if(Input.GetKeyDown(KeyCode.Space)) { ConnectAllViveTrackers(); }
        //if(Input.GetKeyDown(KeyCode.Backspace)) { DisconnectAllViveTrackers(); }
    }


    public void ConnectAllViveTrackers() {

        arenaMarkerMapper.FindAllMarkers();
        
        viveMarkers = new List<ArenaArmarker>();

        foreach(ArenaArmarker marker in arenaMarkerMapper.markers) {             
            //Debug.Log("Name: " + marker.gameObject.name + 
            //          " | MarkerID: " + marker.json.Markerid + 
            //          " | MarkerType: " + marker.json.Markertype + 
            //          " | Size(mm): " + marker.json.Size);

            if(marker.json.Markertype.ToString().ToLower() == "vive") {
                viveMarkers.Add(marker);

                foreach(ViveTrackerArenaObject viveTrackerArenaObject in viveTrackerArenaObjects) {
                    if(viveTrackerArenaObject.name.ToLower() == marker.json.Markerid.ToLower()) {
                        viveTrackerArenaObject.arenaObject = marker.gameObject;
                    }
                }

            }
        }


        uiText.text = "";
        foreach(ViveTrackerArenaObject viveTrackerArenaObject in viveTrackerArenaObjects) {
            string arenaObjectName = viveTrackerArenaObject.arenaObject ? viveTrackerArenaObject.arenaObject.name : "";
            uiText.text += viveTrackerArenaObject.gameObject.name + ": " +  arenaObjectName + "\r\n";
        }
    }


    public void DisconnectAllViveTrackers() {
        foreach(ViveTrackerArenaObject viveTrackerArenaObject in viveTrackerArenaObjects) {
 
            viveTrackerArenaObject.arenaObject = null;
            
        }

        uiText.text = "";
        foreach(ViveTrackerArenaObject viveTrackerArenaObject in viveTrackerArenaObjects) {
            string arenaObjectName = viveTrackerArenaObject.arenaObject ? viveTrackerArenaObject.arenaObject.name : "";
            uiText.text += viveTrackerArenaObject.gameObject.name + ": " +  arenaObjectName + "\r\n";
        }
    }







}
