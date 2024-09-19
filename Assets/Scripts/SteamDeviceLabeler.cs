using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SteamDeviceData {
    public string name = "None";
    public Vector3 position = new Vector3(0,0,0);
    public Quaternion rotation = Quaternion.identity;
}

public class SteamDeviceLabeler : MonoBehaviour
{
    public GameObject device;
    public TMP_Text text;

    public int playerNum = 0; //Player 1,2,3,4. 0 for no assignment.

    public Color colorNotSet;
    public Color colorSet;
    
    public SteamDeviceData data;

    public void UpdateSteamDeviceData() {
        data.name = device.name;
        data.position = device.transform.position; 
        data.rotation = device.transform.rotation; 
    }
    
    public void UpdatePlayerNum(int value) {
        playerNum = value;
        //Show assigned playerNum if set
        if(playerNum > 0) {
            text.text = device.name + " [Player " + playerNum + "]";
            text.color = colorSet;
            text.fontStyle  = FontStyles.Bold;
        } else {
            text.text = device.name;
            text.color = colorNotSet;
            text.fontStyle  = FontStyles.Normal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSteamDeviceData();

        //Show assigned playerNum if set
        if(playerNum > 0) {
            text.text = device.name + " [Player " + playerNum + "]";
            text.color = colorSet;
            text.fontStyle  = FontStyles.Bold;
        } else {
            text.text = device.name;
            text.color = colorNotSet;
            text.fontStyle  = FontStyles.Normal;
        }
        //Show 
        if(device.GetComponent<MeshRenderer>() || device.transform.childCount > 0) {
            gameObject.GetComponent<MeshRenderer>().enabled = (true);
        } 
        //Hide
        else {
            gameObject.GetComponent<MeshRenderer>().enabled = (false);
        } 

        //
        transform.rotation = Camera.main.transform.rotation;
        transform.position = device.transform.position + new Vector3(0,0.3f,0);
    }
}
