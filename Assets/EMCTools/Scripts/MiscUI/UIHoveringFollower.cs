using UnityEngine;

// Hovers over the Transform specified

namespace EMCTools
{
    public class UIHoveringFollower : UIHovering
    {
        [SerializeField] [Tooltip("The transform that this UI should follow and hover over")] Transform target;

        protected override void Start()
        {
            base.Start();
            if (!target) Debug.LogWarning("Tracking hovering UI, " + name + ", is not following anything, since its Transform reference is null");
        }

        protected override void Update()
        {
            if (target) worldSpacePosition = target.position;
            base.Update();
        }
    }
}