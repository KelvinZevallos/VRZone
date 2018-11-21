using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectFocus : MonoBehaviour
{
    [Serializable]
    public class FloatEvent : UnityEvent<float> { }

    [SerializeField] private Transform _cameraReference;

    [SerializeField] private readonly float _minAngle = 10;
    [SerializeField] private readonly float _maxAngle = 30;

    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private FloatEvent curveValueChanged;
    [SerializeField] private FloatEvent rawValueChanged;


    private float _fadeAmmount = -1;
    public float fadeAmmount
    {
        get { return _fadeAmmount; }
        set
        {
            if (value != _fadeAmmount)
            {
                _fadeAmmount = value;
                //To use the value interpolated into an Animation Curve.
                curveValueChanged.Invoke(_curve.Evaluate(_fadeAmmount));
                //To use the value as it is.
                rawValueChanged.Invoke(_fadeAmmount);
            }
        }
    }

    private float _delta = -1;
    /// <summary>
    /// Angle difference between
    /// </summary>
    public float delta
    {
        get { return _delta; }
        set
        {
            if (value != _delta)
            {
                _delta = value;
                //TODO : Update Fade Ammount Value.
                fadeAmmount = Mathf.InverseLerp(_maxAngle, _minAngle, _delta);

                if (_delta <= _minAngle)
                {
                    ObjectFocusManager.Add(this);
                }
                else
                {
                    ObjectFocusManager.Remove(this);
                }
            }
        }
    }

    private void Fade()
    {
        //Debug.DrawLine(_cameraReference.position, transform.position, Color.yellow);

        //TODO : Update Delta.
        Vector3 d = (transform.position - _cameraReference.position).normalized;

        Debug.DrawRay(_cameraReference.position, _cameraReference.forward, Color.cyan);
        Debug.DrawRay(_cameraReference.position, d, Color.yellow);

        delta = Vector3.Angle(d, _cameraReference.forward);

    }

    // Use this for initialization
    private void Start()
    {
        if (!_cameraReference)
            _cameraReference = Camera.main.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        Fade();
    }

#if DEBUG
    //private void OnGUI()
    //{
    //    GUILayout.Label("delta: " + delta.ToString());
    //    GUILayout.Label("fadeAmmount: " + fadeAmmount.ToString());
    //}
#endif

}
