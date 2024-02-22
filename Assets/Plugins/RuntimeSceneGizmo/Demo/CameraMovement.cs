using UnityEngine;

namespace RuntimeSceneGizmo
{
	public class CameraMovement : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private float sensitivity = 0.5f;
#pragma warning restore 0649

		private Vector3 prevMousePos;
		private Transform mainCamParent;

		public float moveSpeed = 1;
		public float scrollSpeed = 1;

		private void Awake()
		{
			mainCamParent = Camera.main.transform.parent;
		}

		private void Update()
		{

			if( Input.GetMouseButtonDown( 1 ) ) { prevMousePos = Input.mousePosition; }
			else if( Input.GetMouseButton( 1 ) )
			{
				Vector3 mousePos = Input.mousePosition;
				Vector2 deltaPos = ( mousePos - prevMousePos ) * sensitivity;

				Vector3 rot = mainCamParent.localEulerAngles;
				while( rot.x > 180f )
					rot.x -= 360f;
				while( rot.x < -180f )
					rot.x += 360f;

				rot.x = Mathf.Clamp( rot.x - deltaPos.y, -89.8f, 89.8f );
				rot.y += deltaPos.x;
				rot.z = 0f;

				mainCamParent.localEulerAngles = rot;
				prevMousePos = mousePos;
			}

			//Move Forward/Backward/Left/Right with WASD
			transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
			
			//Move Forward/Backward with Mouse Wheel, or Up/Down with Shift + Mouse Wheel
			if(Input.GetKey(KeyCode.LeftShift)) {
				transform.position += new Vector3(0,Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime,0);
			} else {
				transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime);
			}
		
			//Move Up/Down using R/F keys
            if(Input.GetKey(KeyCode.R)) { transform.position += new Vector3(0,moveSpeed * Time.deltaTime,0); }
            if(Input.GetKey(KeyCode.F)) { transform.position += new Vector3(0,-moveSpeed * Time.deltaTime,0); }
		}
	}
}