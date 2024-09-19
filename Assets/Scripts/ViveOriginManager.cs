using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//-------------------------------------//

public class ViveOriginManager : MonoBehaviour {

    public List<Transform> SteamVRTransforms;

    public Transform originDevice;
    public Transform viveParent;


    public void SelectOriginDevice(int index) {
        originDevice = SteamVRTransforms[index-1];
    }
    public void SetOriginToDevice() {

        viveParent.rotation = Quaternion.identity;
        viveParent.Rotate(0,-originDevice.localEulerAngles.y,0);
        
        viveParent.position = new Vector3(0,0,0);
        viveParent.position -= originDevice.position;

        ox = viveParent.position.x;
        oy = viveParent.position.y;
        oz = viveParent.position.z;
        or = viveParent.eulerAngles.y;

        PlayerPrefs.SetFloat("OriginX", ox);
        PlayerPrefs.SetFloat("OriginY", oy);
        PlayerPrefs.SetFloat("OriginZ", oz);
        PlayerPrefs.SetFloat("OriginR", or);
        
        oxField.text = ox.ToString("0.000");
        oyField.text = oy.ToString("0.000");
        ozField.text = oz.ToString("0.000");
        orField.text = or.ToString("0.000");
    
    }

    public TMP_InputField oxField;
    public TMP_InputField oyField;
    public TMP_InputField ozField;
    public TMP_InputField orField;

    public float ox,oy,oz,or = 0;

    public void setOriginX(string v) { 
        float x = 0; float.TryParse(v, out x); ox = x; setOrigin(); 
        setOriginX(x);
    }
    public void setOriginX(float x) { 
        PlayerPrefs.SetFloat("OriginX", x);
        oxField.text = x.ToString("0.000");
    }

    public void setOriginY(string v) { 
        float y = 0; float.TryParse(v, out y); oy = y; setOrigin();
        setOriginY(y);
    }
    public void setOriginY(float y) { 
        PlayerPrefs.SetFloat("OriginY", y);
        oyField.text = y.ToString("0.000");
    }

    public void setOriginZ(string v) { 
        float z = 0; float.TryParse(v, out z); oz = z; setOrigin(); 
        setOriginZ(z);
    }
    public void setOriginZ(float z) { 
        PlayerPrefs.SetFloat("OriginZ", z);
        ozField.text = z.ToString("0.000");
    }
    
    public void setOriginR(string v) { 
        float r = 0; float.TryParse(v, out r); or = r; setOrigin(); 
        setOriginR(r);
    }
    public void setOriginR(float r) { 
        PlayerPrefs.SetFloat("OriginR", r);
        orField.text = r.ToString("0.000");
    }

    // Start is called before the first frame update
    void Start() {
        setOriginX(""+PlayerPrefs.GetFloat("OriginX", 0));
        setOriginY(""+PlayerPrefs.GetFloat("OriginY", 0));
        setOriginZ(""+PlayerPrefs.GetFloat("OriginZ", 0));
        setOriginR(""+PlayerPrefs.GetFloat("OriginR", 0));
    }

    public void setOrigin() {
        viveParent.position = new Vector3(ox,oy,oz);
        viveParent.rotation = Quaternion.identity;
        viveParent.Rotate(0,or,0);
    }

}
