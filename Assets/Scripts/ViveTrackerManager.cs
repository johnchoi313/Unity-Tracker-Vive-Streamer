using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//-------------------------------------//



public class ViveTrackerManager : MonoBehaviour {

    public Transform viveOrigin;

    
    public TMP_InputField oxField;
    public TMP_InputField oyField;
    public TMP_InputField ozField;
    public TMP_InputField orField;

    public float ox,oy,oz,or = 0;

    public void setOriginX(string v) { 
        float x = 0; float.TryParse(v, out x); ox = x; setOrigin(); 
        PlayerPrefs.SetFloat("OriginX", x);
        oxField.text = v;
    }
    
    public void setOriginY(string v) { 
        float y = 0; float.TryParse(v, out y); oy = y; setOrigin();
        PlayerPrefs.SetFloat("OriginY", y);
        oyField.text = v;
    }
    
    public void setOriginZ(string v) { 
        float z = 0; float.TryParse(v, out z); oz = z; setOrigin(); 
        PlayerPrefs.SetFloat("OriginZ", z);
        ozField.text = v;
    }
    
    public void setOriginR(string v) { 
        float r = 0; float.TryParse(v, out r); or = r; setOrigin(); 
        PlayerPrefs.SetFloat("OriginR", r);
        orField.text = v;
    }

    // Start is called before the first frame update
    void Start()
    {
        setOriginX(""+PlayerPrefs.GetFloat("OriginX", 0));
        setOriginY(""+PlayerPrefs.GetFloat("OriginY", 0));
        setOriginZ(""+PlayerPrefs.GetFloat("OriginZ", 0));
        setOriginR(""+PlayerPrefs.GetFloat("OriginR", 0));
    }



    public void setOrigin() {
        transform.position = new Vector3(ox,oy,oz);
        transform.rotation = Quaternion.identity;
        transform.Rotate(0,or,0);
    }


    public List<ViveTrackerArenaObject> viveTrackerArenaObjects;

    public TMP_Text uiText;


    void Update() {
        //if(Input.GetKeyDown(KeyCode.Space)) { ConnectAllViveTrackers(); }
        //if(Input.GetKeyDown(KeyCode.Backspace)) { DisconnectAllViveTrackers(); }
    }

    /*

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

    */





}
