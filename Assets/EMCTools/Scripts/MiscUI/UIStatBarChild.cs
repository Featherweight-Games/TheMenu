using UnityEngine;

namespace EMCTools
{
    public abstract class UIStatBarChild : MonoBehaviour
    {
        abstract public void OnFill();
        abstract public void OnEmpty();
    }
}