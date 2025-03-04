using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public abstract class PuzzleObject : MonoBehaviour
{
    [Header("Puzzle Base")]
    protected Canvas canvas;

    [Header("Puzzle")]
    [SerializeField]
    protected UnityEvent solvedFunction;

#if UNITY_EDITOR
    private UnityAction action = null;

    private void OnValidate() => UnityEditor.EditorApplication.update += _OnValidate;
    protected virtual void _OnValidate()
    {
        UnityEditor.EditorApplication.update -= _OnValidate;
        if (this == null || !UnityEditor.EditorUtility.IsDirty(this)) return;

        Init();

        if (action == null)
        {
            //func = UnityEventBase.GetValidMethodInfo(this, "DestroySelf", new System.Type[] { typeof(GameObject) });
            action = () => { Destroy(gameObject); };
            UnityEventTools.AddPersistentListener(solvedFunction, action);
        }
    }
#endif

    protected virtual void Start()
    {
        Init();
    }

    private void Init()
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }

        if (canvas != null &
            canvas.worldCamera == null)
        {
            canvas.worldCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        }
    }
}
