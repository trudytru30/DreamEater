/*va en el PADRE con Rigidbody*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushableBox : MonoBehaviour, IGrabable
{
    [Header("Hold / Push Settings")]
    [SerializeField] private float holdDistance = 1.0f;     // Distancia delante del player si no hay grabOrigin
    [SerializeField] private float followForce = 60f;       // Fuerza para seguir al objetivo
    [SerializeField] private float maxFollowSpeed = 6f;     // Velocidad máxima mientras está agarrada
    [SerializeField] private float breakDistance = 2.5f;    // Si se separa demasiado del player, suelta
    [SerializeField] private float grabbedDrag = 6f;        // Drag mientras empuja/agarrada
    [SerializeField] private bool freezeRotationWhileHeld = true;

    [Header("Axis Lock (Single Vector)")]
    [SerializeField] private Vector3 movementAxis = Vector3.forward; // Eje permitido (mundo o local según flag)
    [SerializeField] private bool axisInLocalSpace = false;          // Si true, movementAxis es local a la caja
    [SerializeField] private float axisCorrectionStrength = 25f;     // Corrección hacia la línea del eje

    private Rigidbody _rb;

    private Transform _grabber;     // Transform del player
    private Transform _anchor;      // Punto de agarre (si existe "grabOrigin" en el player)
    private bool _isHeld;

    private float _defaultDrag;
    private float _defaultAngularDrag;
    private RigidbodyConstraints _defaultConstraints;

    private Vector3 _axisOrigin; // Punto de referencia para la línea del eje (en mundo)

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _defaultDrag = _rb.linearDamping;
        _defaultAngularDrag = _rb.angularDamping;
        _defaultConstraints = _rb.constraints;

        _rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate()
    {
        if (!_isHeld || _grabber == null) return;

        // Objetivo: anchor (grabOrigin) si existe; si no, un punto delante del player
        Vector3 targetPos = _anchor != null
            ? _anchor.position
            : (_grabber.position + _grabber.forward * holdDistance);

        // Mantener Y estable para no “levantar” la caja
        targetPos.y = _rb.position.y;

        UpdateHoldPosition(targetPos, transform.rotation);
        LockPositionToAxis();

        // Si el player se aleja demasiado, suelta (failsafe)
        float dist = Vector3.Distance(_rb.position, _grabber.position);
        if (dist > breakDistance)
        {
            Release();
        }
    }

    public void Grab(Transform grabber)
    {
        if (grabber == null) return;

        _grabber = grabber;

        // Busca "grabOrigin" en la jerarquía del player; si no existe no pasa nada
        _anchor = FindChildRecursive(grabber, "grabOrigin");

        _isHeld = true;

        // Fijamos el origen de la línea del eje en el momento de agarrar
        _axisOrigin = _rb.position;

        // Ajustes físicos para empuje estable
        _rb.linearDamping = grabbedDrag;

        if (freezeRotationWhileHeld)
        {
            _rb.constraints = _defaultConstraints |
                              RigidbodyConstraints.FreezeRotationX |
                              RigidbodyConstraints.FreezeRotationZ;
        }
    }

    public void Release()
    {
        _isHeld = false;
        _grabber = null;
        _anchor = null;

        _rb.linearDamping = _defaultDrag;
        _rb.angularDamping = _defaultAngularDrag;
        _rb.constraints = _defaultConstraints;
    }

    public void UpdateHoldPosition(Vector3 targetPosition, Quaternion targetRotation)
    {
        // Eje permitido (en plano XZ)
        Vector3 axis = GetAxisPlanarNormalized();
        if (axis.sqrMagnitude < 0.0001f) return;

        // Vector hacia el objetivo (plano XZ)
        Vector3 toTarget = targetPosition - _rb.position;
        Vector3 toTargetPlanar = new Vector3(toTarget.x, 0f, toTarget.z);

        // Proyección del "hacia target" sobre el eje permitido
        float along = Vector3.Dot(toTargetPlanar, axis);

        // Fuerza SOLO en el eje
        Vector3 force = axis * (along * followForce);
        _rb.AddForce(force, ForceMode.Acceleration);

        // Bloqueo de velocidad lateral: dejamos solo componente sobre el eje
        Vector3 v = _rb.linearVelocity;
        Vector3 planarVel = new Vector3(v.x, 0f, v.z);

        float alongVel = Vector3.Dot(planarVel, axis);
        alongVel = Mathf.Clamp(alongVel, -maxFollowSpeed, maxFollowSpeed);

        Vector3 lockedPlanarVel = axis * alongVel;
        _rb.linearVelocity = new Vector3(lockedPlanarVel.x, v.y, lockedPlanarVel.z);
    }

    public bool IsHeld() => _isHeld;

    private void LockPositionToAxis()
    {
        Vector3 axis = GetAxisPlanarNormalized();
        if (axis.sqrMagnitude < 0.0001f) return;

        Vector3 pos = _rb.position;

        Vector3 originPlanar = new Vector3(_axisOrigin.x, 0f, _axisOrigin.z);
        Vector3 posPlanar = new Vector3(pos.x, 0f, pos.z);

        // Proyecta la posición actual sobre la línea: origin + axis * t
        Vector3 diff = posPlanar - originPlanar;
        Vector3 projectedPlanar = originPlanar + axis * Vector3.Dot(diff, axis);

        Vector3 corrected = new Vector3(projectedPlanar.x, pos.y, projectedPlanar.z);

        // Corrección suave para evitar jitter
        float t = Mathf.Clamp01(axisCorrectionStrength * Time.fixedDeltaTime);
        _rb.MovePosition(Vector3.Lerp(pos, corrected, t));
    }

    private Vector3 GetAxisPlanarNormalized()
    {
        Vector3 axis = movementAxis;

        // Si el eje es local a la caja, pásalo a mundo
        if (axisInLocalSpace)
            axis = transform.TransformDirection(axis);

        axis.y = 0f;

        if (axis.sqrMagnitude < 0.0001f)
            return Vector3.zero;

        return axis.normalized;
    }

    private static Transform FindChildRecursive(Transform root, string childName)
    {
        if (root == null) return null;

        for (int i = 0; i < root.childCount; i++)
        {
            Transform c = root.GetChild(i);
            if (c.name == childName) return c;

            Transform found = FindChildRecursive(c, childName);
            if (found != null) return found;
        }

        return null;
    }
}
