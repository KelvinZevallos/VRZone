using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialGradientModifier : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;

    private Renderer _renderer;
    private float _gradientPosition;

    public float gradientPosition
    {
        get { return _gradientPosition; }
        set
        {
            if (_gradientPosition != value)
            {
                _gradientPosition = value;
                _renderer.material.color = _gradient.Evaluate(_gradientPosition);
            }
        }
    }

    

    //private void SetGradientPosition(float gradient)
    //{
    //    if (gradient == _gradientPosition)
    //        return;

    //    _gradientPosition = gradient;
    //    _renderer.material.color = _gradient.Evaluate(_gradientPosition);
    //}

    private void Awake()
    {
        if (_renderer == null) _renderer = GetComponent<Renderer>();
    }

    // Use this for initialization
    private void Start()
    {
        gradientPosition = 0;
    }

    // Update is called once per frame
    //private void Update()
    //{
    //    gradientPosition = (Mathf.Sin(Time.time) * 0.5f) + 0.5f;
    //    //SetGradientPosition((Mathf.Sin(Time.time) * 0.5f) + 0.5f);
    //}
}
