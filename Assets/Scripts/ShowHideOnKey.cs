using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct KeyCodeGameObjectPair {
    public KeyCode keycode;
    public GameObject gameobject;
}

public class ShowHideOnKey : MonoBehaviour {
    public List<KeyCodeGameObjectPair> KeyObjectPairs;
    void Update() { 
        foreach(KeyCodeGameObjectPair pair in KeyObjectPairs) {
            if(Input.GetKeyDown(pair.keycode)) { pair.gameobject.SetActive(!pair.gameobject.activeSelf); } 
        }   
    }
}
