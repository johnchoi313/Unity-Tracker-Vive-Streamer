using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using SFB;

[System.Serializable]
public class ConfigSettingsUI : MonoBehaviour
{

    private static string SLASH;

    [Header("Connections")]
    public Camera mainCamera;
    public Material shirtMaterialA;
    public Material shirtMaterialB;
    public Material shirtMaterialC;
    public Material pantsMaterial;

    [Header("UI")]
    public GameObject colorPicker;
    public GameObject UI;

    [Header("Data")]
    public GraphicsSettingsData data;

    // Start is called before the first frame update
    void Start() {
        SLASH = (Application.platform == RuntimePlatform.Android ||
                 Application.platform == RuntimePlatform.IPhonePlayer ||
                 Application.platform == RuntimePlatform.OSXPlayer ||
                 Application.platform == RuntimePlatform.OSXEditor ||
                 Application.platform == RuntimePlatform.WindowsEditor ||
                 Application.platform == RuntimePlatform.WindowsPlayer)?"/":"\\";

        //LoadGraphicsSettings(PlayerPrefs.GetString("GraphicsSettingsPath", ""));
        SetDataToCurrentGraphicsSettings();
    }

    public void loadDefaultGraphicsSettings() {
        data = new GraphicsSettingsData();
        SetGraphicsSettings(data);
    }

    public void SetDataToCurrentGraphicsSettings() {
        
        //Show UI
        data.visibleUI = UI.activeSelf;

        //Set Transform
        data.cameraPosition = mainCamera.transform.position;
        data.cameraRotation = mainCamera.transform.localEulerAngles;

        //Set Backgound color
        data.backgroundColor = mainCamera.backgroundColor;

        //Set Clothes Color
        data.shirtColorA = shirtMaterialA.color;
        data.shirtColorB = shirtMaterialB.color;
        data.shirtColorC = shirtMaterialC.color;
        data.pantsColor = pantsMaterial.color;

    }


    public void SetGraphicsSettings(GraphicsSettingsData newData) {
        data = newData;

        //Show UI
        UI.SetActive(data.visibleUI);

        //Set Transform
        mainCamera.transform.position = data.cameraPosition;
        mainCamera.transform.localEulerAngles = data.cameraRotation;

        //Set Backgound color
        mainCamera.backgroundColor = data.backgroundColor;

        //Set Clothes Color
        shirtMaterialA.color = data.shirtColorA;
        shirtMaterialB.color = data.shirtColorB;
        shirtMaterialC.color = data.shirtColorC;
        pantsMaterial.color = data.pantsColor;
    
    }

    ///------------------------------------------------///
    ///------------SAVING/LOADING FUNCTIONS------------///
    ///------------------------------------------------///
    //---Standalone File Browser Load Settings---//
    public void LoadGraphicsSettings(string path) { if(string.IsNullOrEmpty(path)) { Debug.Log("No graphics settings file found. Loading defaults."); return; }
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<GraphicsSettingsData>(json);
            SetGraphicsSettings(data);
            Debug.Log("Graphics Settings loaded from: " + path);
            setHomeFile(path);
        }
    }
    public void DesktopFileBrowserLoadSettings() {
        var extensions = new [] { new ExtensionFilter("Files", "json") };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Load Graphics Settings", getHomePath(), extensions, false);
        if(paths.Length > 0) { LoadGraphicsSettings(paths[0]); }
    }    
    //---Standalone File Browser Save Settings---//
    public void SaveGraphicsSettings(string path) { if(string.IsNullOrEmpty(path)) { Debug.LogWarning("Null or empty path. Cannot save graphics settings."); return; }
        File.WriteAllText(path, JsonUtility.ToJson(data));
        Debug.Log("Graphics Settings saved to: " + path);
        setHomeFile(path);
    }
    public void DesktopFileBrowserSaveSettings() { 
        var extensions = new [] { new ExtensionFilter("Files", "json") };
        string path = StandaloneFileBrowser.SaveFilePanel("Save Graphics Settings", getHomePath(), "Graphics Settings", extensions);        
        SaveGraphicsSettings(path);
    }  

    public void setHomeFile(string path) {
        PlayerPrefs.SetString("GraphicsSettingsPath",path);
    }
    public string getHomeFile() {
        return PlayerPrefs.GetString("GraphicsSettingsPath",Application.persistentDataPath);   
    }
    public string getHomePath() {
        return File.Exists(getHomeFile()) ? Path.GetDirectoryName(getHomeFile()) : Application.persistentDataPath;
    }

}

[System.Serializable]
public class GraphicsSettingsData {

    [Header("UI")]
    public bool visibleUI = true;

    [Header("Camera Transform")]
    public Vector3 cameraPosition = new Vector3(0, 1.3f, 1.3f);
    public Vector3 cameraRotation = new Vector3(0, 180, 0);

    [Header("Background")]
    public Color backgroundColor = new Color32(137, 141, 169, 255);
   
    [Header("Clothes")]
    public Color shirtColorA = new Color32(198, 0, 1, 255);
    public Color shirtColorB = new Color32(198, 0, 1, 255);
    public Color shirtColorC = new Color32(198, 0, 1, 255);
    public Color pantsColor = new Color32(58, 125, 224, 255);
   
    [Header("Blur")]
    public bool useBlur = true;
    public float blurAmount = 0.05f;

    [Header("Bloom")]
    public bool useBloom = true;
    public Color bloomColor = new Color32(255, 255, 255, 255);
    public float bloomAmount = 0.3f;
    public float bloomDiffuse = 1;
    public float bloomThreshold = 0;
    public float bloomSoftness = 0;
    
    [Header("Chromatic Aberration")]
    public bool useChromaticAberration = true;
    public float chromaticAberration = 0.08f;
    public float chromaticAberrationFishEyeDistortion = 0.2f;
    public float chromaticAberrationGlitch = 0;
    
    [Header("Distortion")]
    public bool useDistortion = true;
    public float distortionAmount = 0.08f;

    [Header("Vignette")]
    public bool useVignette = true;
    public Color vignetteColor = new Color32(46,44,118,255);
    public float vignetteAmount = 0.05f;
    public float vignetteSoftness = 0.5f;

    [Header("Color Grading")]
    public bool useColorGrading = true;
    public Color colorGradingColorShift = new Color32(255, 255, 255, 255);
    public int colorGradingHue = -4;
    public float colorGradingContrast = 0.2f;
    public float colorGradingBrightness = 0;
    public float colorGradingSaturation = -0.05f;
    public float colorGradingExposure = 0;
    public float colorGradingGamma = -0.2f;
    public float colorGradingSharpness = 0.2f;

    [Header("Ambient Occlusion")]
    public bool useAmbientOcclusion = true;
    public float ambientOcclusionIntensity = 0.7f;
    public float ambientOcclusionBlur = 3;
    public float ambientOcclusionRadius = 0.9f;
    public float ambientOcclusionArea = 1.4f;
    public bool ambientOcclusionFastMode = false;

    [Header("Motion Blur")]
    public bool useMotionBlur = false;
    public float motionBlurDistance = 0;
    public int motionBlurFastFilter = 4;

    [Header("Depth of Field")]
    public bool useDepthofField = true;
    public float depthOfFieldBlur = 0.1f;
    public float depthOfFieldFocus = 1;
    public float depthOfFieldAperture = 0.5f;

    [Header("Anti-Aliasing")]
    public bool useAntiAliasing = true;
    public float antiAliasingSharpness = 4;
    public float antiAliasingThreshold = 0.2f;

}