using UnityEngine;
using UnityEngine.Events;

public class DragAndDrop : MonoBehaviour
{
    [Header("Config")]
    [Tooltip("Cámara desde la que se hace el raycast. Si queda null usa Camera.main.")]
    public Camera cam;

    [Tooltip("Layer(s) que puede recibir el raycast del plano de arrastre.")]
    public LayerMask dragPlaneLayer = Physics.DefaultRaycastLayers;

    // ── estado interno ──────────────────────────────────────────────
    private bool      _dragging;
    private float     _fixedY;          // Y original del objeto
    private Vector3   _offset;          // distancia cursor → pivote en XZ
    private Plane     _dragPlane;       // plano horizontal en Y fijo

    // ────────────────────────────────────────────────────────────────

    public UnityEvent onDragStart;
    public UnityEvent onDrop;
    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void OnMouseDown()
    {
        onDragStart?.Invoke();
        _fixedY    = transform.position.y;
        _dragPlane = new Plane(Vector3.up, new Vector3(0f, _fixedY, 0f));

        // Calculamos el offset para que el objeto no "salte" al cursor
        if (_dragPlane.Raycast(GetMouseRay(), out float enter))
        {
            Vector3 hitPoint = GetMouseRay().GetPoint(enter);
            _offset   = transform.position - hitPoint;
            _offset.y = 0f;             // sólo interesa XZ
        }

        _dragging = true;
    }

    void OnMouseDrag()
    {
        if (!_dragging) return;

        if (_dragPlane.Raycast(GetMouseRay(), out float enter))
        {
            Vector3 worldPoint = GetMouseRay().GetPoint(enter);
            transform.position = new Vector3(
                worldPoint.x + _offset.x,
                _fixedY,                    // ← Y nunca cambia
                worldPoint.z + _offset.z
            );
        }
    }

    void OnMouseUp()
    {
       _dragging = false;
       onDrop?.Invoke(); 
    }  

    // ── helpers ─────────────────────────────────────────────────────
    private Ray GetMouseRay() => cam.ScreenPointToRay(Input.mousePosition);
}
