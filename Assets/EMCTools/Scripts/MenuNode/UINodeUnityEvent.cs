using UnityEngine;
using UnityEngine.Events;

namespace EMCTools
{
    public class UINodeUnityEvent : UIMenuNode
    {
        [SerializeField] private UnityEvent unityEvent;

        public override void OnAction(Action action, int playerNumber)
        {
            base.OnAction(action, playerNumber);
            unityEvent.Invoke();
        }
    }
}