/*
 * RealtimeConsole.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */


namespace Mosframe {

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Realtime Console
    /// </summary>
    public class RealtimeConsole : MonoBehaviour {

        public const string SaveFileName    = "console.log";

        public static int   MaxStorageSize  = 1000;

        // singleton

        public static RealtimeConsole Instance;

        public readonly List<MessageInfo>  storage = new List<MessageInfo>();

        public Sprite   infoIcon    { get; private set; }
        public Sprite   warningIcon { get; private set; }
        public Sprite   errorIcon   { get; private set; }
        public Text     detailText  { get{ return this.detailView.text; } }
        public bool     isOpen      { get { return this.transform.localPosition == this.openedPosition; } }


        public void open () {

           this.transform.localPosition = this.openedPosition;
        }
        public void close () {

            this.transform.localPosition = this.closedPosition;
        }


      
        void Awake() {

            Instance = this;

            this.init();

            Application.logMessageReceived += this.onLogMessageReceived;

            // [ icons ]

            this.infoIcon   = Resources.Load<Sprite>( "console-icon-info" );
            this.warningIcon= Resources.Load<Sprite>( "console-icon-warning" );
            this.errorIcon  = Resources.Load<Sprite>( "console-icon-error" );

            //if( !Application.isEditor ) {

                this.StartCoroutine( this.onSeedData() );
            //}
        }
        void Update () {

            if( this.lastScreenSIze != this.screenSize ) {

                this.lastScreenSIze = this.screenSize;
                this.StartCoroutine( this.reflash() );
                return;
            }

            // Right Shift + C

            if( Input.GetKey( KeyCode.RightShift ) && Input.GetKeyDown( KeyCode.C ) ) {

                if( this.isOpen )
                    this.close();
                else
                    this.open();
            }

            if( this.isOpen ) {

                // Right Shift + S

                if( Input.GetKey( KeyCode.RightShift ) && Input.GetKeyDown( KeyCode.S ) ) {

                    this.save();
                }
            }

            // Right Shift + I

            if( Input.GetKey( KeyCode.RightShift ) && Input.GetKeyDown( KeyCode.I ) ) {

                this.StartCoroutine( this.onSeedData() );
            }
        }
        void OnDestroy() {

            Application.logMessageReceived -= this.onLogMessageReceived;
        }

        
  
        void init() {

            // [ cliear ]

            while( this.transform.childCount>0 ) {
                DestroyImmediate( this.transform.GetChild( 0 ).gameObject );
            }

            // [ RectTransform ]

            var rectTransform = this.gameObject.GetComponent<RectTransform>();
            rectTransform.setSizeFromBottom(0.5f);


            // [ List View ]
            {
                var listViewRect = new GameObject( "List View", typeof(RectTransform) ).GetComponent<RectTransform>();
                listViewRect.SetParent( this.transform, false );
                listViewRect.setSizeFromTop(0.7f);
                this.listView = listViewRect.gameObject.AddComponent<RealtimeConsoleListView>();
            }

            // [ Detail View ]
            {
                var detailViewRect = new GameObject( "Detail View", typeof(RectTransform) ).GetComponent<RectTransform>();
                detailViewRect.SetParent( this.transform, false );
                detailViewRect.setSizeFromBottom(0.3f);
                var image = detailViewRect.gameObject.AddComponent<Image>();
                image.color = new Color(0.3f,0.3f,0.3f,0.5f);
                this.detailView = detailViewRect.gameObject.AddComponent<RealtimeConsoleDetailView>();
            }

            // [ Layer ]

            this.gameObject.setLayer( this.transform.parent.gameObject.layer, true );

            // [ state ]

            this.lastScreenSIze = this.screenSize;
            this.close();
        }

        IEnumerator onSeedData() {

            yield return null;

            Debug.Log( RichText.White("▼ [ System Info ] --------------------------------------------------------------") );
            var properties = typeof(SystemInfo).GetProperties( BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public ).OrderBy(x=>x.Name); // linq
            //var properties = new List<PropertyInfo>( typeof(SystemInfo).GetProperties( BindingFlags.Instance|BindingFlags.Static|BindingFlags.Public ) );
            //properties.Sort( (x,y)=>{ return x.Name.CompareTo(y.Name); } );
            foreach( var property in properties ) {

                Debug.Log( string.Format( "{0} = {1}", RichText.Orange(property.Name), RichText.White(property.GetValue(null,null)) ) );
            }
            Debug.Log( RichText.White("▲ [ System Info ] --------------------------------------------------------------") );

            this.save();

            yield return null;

            this.listView.scrollToLastPos();
        }

        IEnumerator reflash() {

            var canvasScaler = this.transform.parent.GetComponent<CanvasScaler>();
            if( canvasScaler != null ) {
                canvasScaler.referenceResolution = new Vector2(Screen.width,Screen.height);
            }

            yield return null;

            this.init();
        }

        void onLogMessageReceived( string message, string stackTrace, LogType type ) {

            this.storage.Add( new MessageInfo(){ message = string.Format( "[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), message ), stackTrace=stackTrace, type=type } );
            if( this.storage.Count > MaxStorageSize ) {
                this.storage.RemoveAt(0);
            }

            this.listView.totalItemCount = this.storage.Count;
        }
        void save () {

            var fileName = Path.Combine( Application.persistentDataPath, SaveFileName );
            var dirName = Path.GetDirectoryName(fileName);
            if( !Directory.Exists(dirName) ) {

                Directory.CreateDirectory(dirName);
            }

            var sb = new StringBuilder();

            foreach( var s in this.storage ) {

                sb.Append( s.message ).Append("\r\n");
            }

            File.WriteAllText( fileName, sb.ToString() );
        }


        Vector2 lastScreenSIze;

        Vector2 screenSize  { get { return new Vector2(Screen.width,Screen.height); } }
        Vector3 openedPosition { get { return new Vector3( -Screen.width/2f, -Screen.height/2f, 0f ); } }
        Vector3 closedPosition { get { return new Vector3( -Screen.width/2f, -Screen.height*3f, 0f ); } }


        RealtimeConsoleListView     listView;
        RealtimeConsoleDetailView   detailView;


        // message info
        public class MessageInfo {

            public string message;
            public string stackTrace;
            public LogType type;
        }
    }
}
