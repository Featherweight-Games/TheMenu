using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
	private static bool valid = false;
	private static T instance = null;

	public static T Instance {
		get {
			if (valid == false) {
#if UNITY_EDITOR
				if(Application.isPlaying == false && valid == false) {
					return ScanForInstance();
				}
#else
				Debug.LogErrorFormat("No instance of {0} exists", typeof(T).Name);
#endif
				return null;
			}
			return instance;
		}
	}

	public static bool InstanceExists {
		get {
#if UNITY_EDITOR
			if(Application.isPlaying == false && valid == false) {
				ScanForInstance();
				return valid;
			}
#endif
			return valid;
		}
	}

	public virtual void Awake() {
		if (valid == true) {
			Debug.LogWarningFormat("Second singleton instance of {0} at {1}. This instance has been ignored.", typeof(T).Name, gameObject);
			return;
		}

		instance = this as T;
		valid = true;
	}

	public virtual void OnDestroy() {
		if (instance != this) {
			return;
		}

		instance = null;
		valid = false;
	}

#if UNITY_EDITOR
	private static T ScanForInstance() {
		if(Application.isPlaying == true) {
			return null;
		}

		instance = FindObjectOfType<T>();
		if(instance == null) {
			Debug.LogErrorFormat("No instance of {0} exists", typeof(T).Name);
			return null;
		}

		valid = true;
		return instance;
	}
#endif
}
