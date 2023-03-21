using UnityEngine;

// Updates this RectTransform's local position (screen space) so that it visually hovers over the Vector3 position provided (world space) // TODO: May rely on specific anchored settings?

namespace EMCTools
{
    [RequireComponent(typeof(RectTransform))]
    public class UIHovering : MonoBehaviour
    {
        [SerializeField] [Tooltip("The world space position to be converted into a screen space position, for this hovering UI")] protected Vector3 worldSpacePosition;
        [SerializeField] [Tooltip("If left empty, will use the current screen dimensions")] protected Vector2 screenDimensions; // TODO: Update this value if screen size were to change
        [SerializeField] [Tooltip("Number of pixels this UI should be off-set from the target's screen space position")] protected Vector2 screenSpaceOffset;
        [SerializeField] [Tooltip("If left empty, will use the main camera in scene")] protected Camera gameCamera;
        protected RectTransform rectTransform;

        protected bool isScreenDimensionsValid => screenDimensions.x > 0 && screenDimensions.y > 0;

        protected virtual void Start()
        {
            // Get components
            rectTransform = GetComponent<RectTransform>();
            if (!gameCamera) gameCamera = Camera.main;
            if (!isScreenDimensionsValid) screenDimensions = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        }

        protected virtual void Update()
        {
            UpdatePosition(worldSpacePosition);
        }

        protected virtual void UpdatePosition(Vector3 newPosition)
        {
            if (!gameCamera || !isScreenDimensionsValid) return; // TODO: Should only verify this once, not every frame?

            rectTransform.localPosition = (Vector2)gameCamera.WorldToScreenPoint(worldSpacePosition) - screenDimensions;
            rectTransform.localPosition = Vector3Int.RoundToInt((Vector2)rectTransform.localPosition + screenDimensions * 0.5f + screenSpaceOffset);
            //rectTransform.localPosition = Vector3Int.RoundToInt( new Vector2(rectTransform.localPosition.x, rectTransform.localPosition.y) + new Vector2(320, 180 - 24) );
        }
    }
}