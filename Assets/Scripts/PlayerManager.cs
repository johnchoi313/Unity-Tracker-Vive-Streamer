using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public List<SteamDeviceLabeler> devices;

    int player1 = 0;
    int player2 = 0;
    int player3 = 0;
    int player4 = 0;

    public TMP_Dropdown player1dropdown;
    public TMP_Dropdown player2dropdown;
    public TMP_Dropdown player3dropdown;
    public TMP_Dropdown player4dropdown;
    

    public void AssignPlayer1(int index) {
        if (player1 != index) {
            //clear previous assignment
            if(player1 > 0) { devices[player1 - 1].playerNum = 0; }
            //set new player1
            player1 = index;
            //set text in player
            if(player1 > 0) { devices[player1 - 1].playerNum = 1; }
            //Save pref
            PlayerPrefs.SetInt("player1", player1);
        }
    }
    public void AssignPlayer2(int index) {
        if (player2 != index) {
            //clear previous assignment
            if(player2 > 0) { devices[player2 - 1].playerNum = 0; }
            //set new player1
            player2 = index;
            //set text in player
            if(player2 > 0) { devices[player2 - 1].playerNum = 2; }
            //Save pref
            PlayerPrefs.SetInt("player2", player2);
        }
    }
    public void AssignPlayer3(int index) {
        if (player3 != index) {
            //clear previous assignment
            if(player3 > 0) { devices[player3 - 1].playerNum = 0; }
            //set new player1
            player3 = index;
            //set text in player
            if(player3 > 0) { devices[player3 - 1].playerNum = 3; }
            //Save pref
            PlayerPrefs.SetInt("player3", player3);
        }
    }
    public void AssignPlayer4(int index) {
        if (player4 != index) {
            //clear previous assignment
            if(player4 > 0) { devices[player4 - 1].playerNum = 0; }
            //set new player1
            player4 = index;
            //set text in player
            if(player4 > 0) { devices[player4 - 1].playerNum = 4; }
            //Save pref
            PlayerPrefs.SetInt("player4", player4);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    
        AssignPlayer1(PlayerPrefs.GetInt("player1", 0));
        AssignPlayer2(PlayerPrefs.GetInt("player2", 0));
        AssignPlayer3(PlayerPrefs.GetInt("player3", 0));
        AssignPlayer4(PlayerPrefs.GetInt("player4", 0));
    
        player1dropdown.value = player1;
        player2dropdown.value = player2;
        player3dropdown.value = player3;
        player4dropdown.value = player4;

    }

    // Update is called once per frame
    void Update()
    {
    }
}