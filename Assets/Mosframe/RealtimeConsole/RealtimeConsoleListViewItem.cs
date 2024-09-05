/*
 * RealtimeConsoleListViewItem.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe {

    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Realtime Console ListView Item
    /// </summary>
    public class RealtimeConsoleListViewItem : MonoBehaviour, IDynamicScrollViewItem, IPointerClickHandler {

        // [ background colors ]

        private readonly Color[] bgColors = new Color[] {

            new Color(0.1f,0.1f,0.1f,0.7f),
            new Color(0.2f,0.2f,0.2f,0.7f),
        };

        // [ public variables ]

        public Image    icon;
        public Text     message;
        public Image    background;

        // [ private variables ]

        private RealtimeConsole.MessageInfo  _messageInfo;

        // update item event
        public void onUpdateItem ( int index ) {

            if( RealtimeConsole.Instance == null ) return;
            if( RealtimeConsole.Instance.storage.Count <= index ) return;

            this._messageInfo = RealtimeConsole.Instance.storage[ index ];

            switch( this._messageInfo.type ) {
            case LogType.Error: this.icon.sprite = RealtimeConsole.Instance.errorIcon; break;
            case LogType.Warning: this.icon.sprite = RealtimeConsole.Instance.warningIcon; break;
            default: this.icon.sprite = RealtimeConsole.Instance.infoIcon; break;
            }

            this.background.color = this.bgColors[ Mathf.Abs( index ) % this.bgColors.Length ];

            // display to only two lines.

            var lines = this._messageInfo.message.Split('\n');
            var sb = new StringBuilder();
            sb.Append( lines[ 0 ] );
            for( var c = 1; c<lines.Length; ++c ) {
                sb.Append( '\n' ).Append( lines[ c ] );
            }
            this.message.text = sb.ToString();
        }

        // click event
        public void OnPointerClick ( PointerEventData eventData ) {

            var console = this.GetComponentInParent<RealtimeConsole>();
            console.detailText.text = GUIUtility.systemCopyBuffer = new StringBuilder( this._messageInfo.message ).Append( '\n' ).Append( this._messageInfo.stackTrace ).ToString();
        }

        public void init () {

            // [ background ]

            this.background = this.gameObject.AddComponent<Image>();
            this.background.type = Image.Type.Sliced;
            this.background.color = new Color( 0.5f, 0.5f, 0.5f, 0.5f );

            // [ icon ]

            var iconRect = new GameObject( "Icon", typeof(RectTransform) ).GetComponent<RectTransform>();
            iconRect.SetParent( this.transform, false );
            iconRect.setSizeFromLeft( 0.1f );
            var iconRectSize = iconRect.getSize();
            iconRect.setSize( iconRectSize-iconRectSize*0.06f );
            this.icon = iconRect.gameObject.AddComponent<Image>();
            var fitter = this.icon.gameObject.AddComponent<AspectRatioFitter>();
            fitter.aspectMode   = AspectRatioFitter.AspectMode.FitInParent;
            fitter.aspectRatio  = 1.0f;

            // [ message ]

            var messageRect = new GameObject( "Message", typeof(RectTransform) ).GetComponent<RectTransform>();
            messageRect.SetParent( this.transform, false );
            messageRect.setFullSize();
            messageRect.offsetMin = new Vector2( iconRect.getSize().x + 2f, -1f );
            messageRect.offsetMax = new Vector2( 0f, 0f );

            this.message = messageRect.gameObject.AddComponent<Text>();
            this.message.font = Resources.Load<Font>("default");
            this.message.color = new Color( 0.8f, 0.8f, 0.8f, 1.0f );
            this.message.fontSize = 12;
            this.message.lineSpacing = 0.8f;
            //this.message.resizeTextForBestFit = true;
            //this.message.resizeTextMinSize = 6;
            //this.message.resizeTextMaxSize = 40;
            this.message.text = "Test : 한글테스트 1234567890\nABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        }
    }
}