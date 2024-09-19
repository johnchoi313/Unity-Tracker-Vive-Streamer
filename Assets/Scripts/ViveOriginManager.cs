using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//-------------------------------------//

public class ViveOriginManager : MonoBehaviour {

    public List<SteamDeviceLabeler> devices;

    public Transform originDevice;

    public Transform viveParent;

    public void SelectOriginDevice(int index) {

    }
    public void SetOriginToDevice() {
        viveParent.position -= originDevice.localPosition;
        viveParent.localEulerAngles -= originDevice.localEulerAngles;
    }


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

    public TMP_Text uiText;

}
