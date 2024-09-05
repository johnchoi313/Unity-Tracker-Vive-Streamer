/*
 * RealtimeConsoleListView.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */

namespace Mosframe {

    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Realtime Console List View
    /// </summary>
    public class RealtimeConsoleListView : DynamicVScrollView {

        // Awake
        protected override void Awake() {

            this.totalItemCount = 0;
            this.init();
            base.Awake();
        }
        public override void init() {

            this.direction = Direction.Vertical;

            // [ ScrollRect ]

            var scrollRect = this.GetComponent<ScrollRect>();
            scrollRect.horizontal   = false;
            scrollRect.vertical     = true;
            scrollRect.scrollSensitivity = 15;

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
            contentRect.setSizeFromTop(1.0f);
            scrollRect.content = contentRect;


            // [ ScrollRect / Viewport / Content / PrototypeItem ]

            this.resetPrototypeItem( contentRect );


            // [ ScrollRect / Scrollbar ]

            var scrollbarName = this.direction == Direction.Horizontal ? "Scrollbar Horizontal" : "Scrollbar Vertical";
            var scrollbarRect = new GameObject( scrollbarName, typeof(Scrollbar), typeof(Image) ).GetComponent<RectTransform>();
            scrollbarRect.SetParent( contentRect, false );
            scrollbarRect.setSizeFromRight( 0.01f );
            scrollbarRect.SetParent( scrollRect.transform, true );

            var scrollbar = scrollbarRect.GetComponent<Scrollbar>();
            scrollbar.direction = Scrollbar.Direction.BottomToTop;
            scrollRect.verticalScrollbar = scrollbar;

            // [ ScrollRect / Scrollbar / Image ]

            var scrollbarImage = scrollbarRect.GetComponent<Image>();
            //scrollbarImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
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
            //scrollbarHandleImage.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            scrollbarHandleImage.color = new Color(0.5f,0.5f,1.0f,0.5f);
            scrollbarHandleImage.type   = Image.Type.Sliced;
            scrollbar.handleRect = scrollbarHandleRect;

            // [ ScrollRect / ScrollbarHandleSize ]

            var scrollbarHandleSize = scrollRect.GetComponent<ScrollbarHandleSize>();
            if( scrollbarHandleSize == null ) {
                scrollbarHandleSize = scrollRect.gameObject.AddComponent<ScrollbarHandleSize>();
                scrollbarHandleSize.maxSize = 1.0f;
                scrollbarHandleSize.minSize = 0.1f;
            }

            // [ Layer ]

            this.gameObject.setLayer( this.transform.parent.gameObject.layer, true );
        }

        // reset prototype item 

        protected override void resetPrototypeItem ( RectTransform contentRect ) {

            // [ ScrollRect / Viewport / Content / PrototypeItem ]

            var itemRect = new GameObject( "Prototype Item", typeof(RectTransform) ).GetComponent<RectTransform>();
            itemRect.SetParent( contentRect, false );
            itemRect.setSizeFromTop( 0.13f );
            var itemRectSize = itemRect.getSize();
            itemRect.setSize( new Vector2( itemRectSize.x-itemRectSize.x*0.011f, 24f ) );
            var item = itemRect.gameObject.AddComponent<RealtimeConsoleListViewItem>();
            item.init();

            this.itemPrototype = itemRect;
        }
    }
}
