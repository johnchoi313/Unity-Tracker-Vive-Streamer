/*
 * RealtimeConsoleDetailView.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe {

    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Realtime Console Detail View
    /// </summary>
    [RequireComponent( typeof( ScrollRect ) )]
    public class RealtimeConsoleDetailView : UIBehaviour {

        public Text text;

        // Awake
        protected override void Awake () {

            this.init();
            base.Awake();
        }

        public virtual void init () {


            // [ ScrollRect ]

            var scrollRect = this.GetComponent<ScrollRect>();
            scrollRect.horizontal   = false;
            scrollRect.vertical     = true;

            // [ ScrollRect / Viewport ]

            var viewportRect = new GameObject( "Viewport", typeof(RectTransform), typeof(Mask), typeof(Image) ).GetComponent<RectTransform>();
            viewportRect.SetParent( scrollRect.transform, false );
            viewportRect.setFullSize();
            var viewportImage = viewportRect.GetComponent<Image>();
            //viewportImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            viewportImage.type = Image.Type.Sliced;
            var viewportMask = viewportRect.GetComponent<Mask>();
            viewportMask.showMaskGraphic = false;
            scrollRect.viewport = viewportRect;

            // [ ScrollRect / Viewport / Content ]

            var contentRect = new GameObject( "Content", typeof(RectTransform) ).GetComponent<RectTransform>();
            contentRect.SetParent( viewportRect, false );
            contentRect.setSizeFromLeft( 0.98f );
            scrollRect.content = contentRect;

            // [ ScrollRect / Viewport / Content / Text ]

            var contentTextRect = new GameObject( "Text", typeof(RectTransform), typeof(Text) ).GetComponent<RectTransform>();
            contentTextRect.SetParent( contentRect, false );
            contentTextRect.setSizeFromLeft( 1.0f );

            this.text = contentTextRect.GetComponent<Text>();
            this.text.font                  = Resources.Load<Font>( "default" );
            this.text.fontSize              = 12;
            this.text.alignment             = TextAnchor.MiddleCenter;
            this.text.horizontalOverflow    = HorizontalWrapMode.Wrap;
            this.text.verticalOverflow      = VerticalWrapMode.Truncate;
            this.text.alignment             = TextAnchor.UpperLeft;
            this.text.color                 = new Color( 0.8f, 0.8f, 0.8f, 1.0f );
            this.text.text                  = "Detail View";

            // [ ScrollRect / Scrollbar ]

            var scrollbarRect = new GameObject( "Scrollbar Vertical", typeof(Scrollbar), typeof(Image) ).GetComponent<RectTransform>();
            scrollbarRect.SetParent( viewportRect, false );
            scrollbarRect.setSizeFromRight( 0.01f );
            scrollbarRect.SetParent( scrollRect.transform, true );

            var scrollbar = scrollbarRect.GetComponent<Scrollbar>();
            scrollbar.direction = Scrollbar.Direction.BottomToTop;
            scrollRect.verticalScrollbar = scrollbar;

            // [ ScrollRect / Scrollbar / Image ]

            var scrollbarImage = scrollbarRect.GetComponent<Image>();
            //scrollbarImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>( "UI/Skin/Background.psd" );
            scrollbarImage.color = new Color(0.1f,0.1f,0.1f,0.5f);
            scrollbarImage.type = Image.Type.Sliced;

            // [ ScrollRect / Scrollbar / slidingArea ]

            var slidingAreaRect = new GameObject( "Sliding Area", typeof(RectTransform) ).GetComponent<RectTransform>();
            slidingAreaRect.SetParent( scrollbarRect, false );
            slidingAreaRect.setFullSize();

            // [ ScrollRect / Scrollbar / slidingArea / Handle ]

            var scrollbarHandleRect = new GameObject( "Handle", typeof(Image) ).GetComponent<RectTransform>();
            scrollbarHandleRect.SetParent( slidingAreaRect, false );
            scrollbarHandleRect.setFullSize();
            var scrollbarHandleImage = scrollbarHandleRect.GetComponent<Image>();
            //scrollbarHandleImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>( "UI/Skin/UISprite.psd" );
            scrollbarHandleImage.color = new Color(0.5f,0.5f,1.0f,0.5f);
            scrollbarHandleImage.type = Image.Type.Sliced;
            scrollbar.handleRect = scrollbarHandleRect;

            // [ ScrollRect / ScrollbarHandleSize ]

            var scrollbarHandleSize = scrollRect.GetComponent<ScrollbarHandleSize>();
            if( scrollbarHandleSize == null ) {
                scrollbarHandleSize = scrollRect.gameObject.AddComponent<ScrollbarHandleSize>();
                scrollbarHandleSize.maxSize = 1.0f;
                scrollbarHandleSize.minSize = 0.1f;
            }
        }
    }
}
