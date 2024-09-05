using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArenaUnity;
using TMPro;

public class ArenaClientSceneUIConnector : MonoBehaviour
{

    public GameObject arenaClientSceneObject;
    public ArenaClientScene acs;
    
    public GameObject arenaClientScenePrefab;
    public bool arenaConnected = false;



    public TMP_InputField arenaNamespaceField;
    public TMP_InputField arenaScenenameField;

    [SerializeField]
    public string arenaNamespace;

    public void SetNamespace(string input) {
        if(string.IsNullOrEmpty(input)) { 
            Debug.LogWarning("Namespace cannot be null or empty!");
            return; 
        }
        PlayerPrefs.SetString("namespace", input);
        arenaNamespace = input;
        arenaNamespaceField.text = arenaNamespace;

    }

    [SerializeField]    
    public string arenaScenename;

    public void SetScenename(string input) {
        if(string.IsNullOrEmpty(input)) { 
            Debug.LogWarning("Scenename cannot be null or empty!");
            return; 
        }
        PlayerPrefs.SetString("scenename", input);
        arenaScenename = input;
        arenaScenenameField.text = arenaScenename;
    }
    
    public void ConnectArena() {
        
        if(string.IsNullOrEmpty(arenaNamespace) || string.IsNullOrEmpty(arenaScenename)) { 
            Debug.LogWarning("Neither Namespace nor Scenename can be null or empty! Failed to connect.");
            return; 
        }

        arenaClientSceneObject = Instantiate(arenaClientScenePrefab);
        acs = arenaClientSceneObject.GetComponent<ArenaClientScene>();

        acs.namespaceName = arenaNamespace;
        acs.sceneName = arenaScenename;

        //acs._ConnectArena();

    }

    public void DisconnectArena() {
        if(acs) {
            acs.DisconnectArena();
            Destroy(arenaClientSceneObject);
        }
    }

    public void SignoutArena() {
        if(acs) {
            ArenaClientScene.SignoutArena();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        arenaNamespace = PlayerPrefs.GetString("namespace", "");
        SetNamespace(arenaNamespace);
        
        arenaScenename = PlayerPrefs.GetString("scenename", "");        
        SetScenename(arenaScenename);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
