using UnityEngine;
using System.Collections;

public class PullToRefreshRotateGizmo :PullToRefreshGizmo  {

#pragma warning disable 0649
    [SerializeField]
    RectTransform _StartingPoint, _EndingPoint;
#pragma warning restore 0649

    [SerializeField]
    [Range(0f, 1f)]
    float _ExcessPullRotationDamping = .95f;

    [SerializeField]
    float _AutoRotationDegreesPerSec = 200;

    bool _WaitingForManualHide;


    /// <summary>Calls base implementation + resets the rotation to default each time is assigned, regardless if true or false</summary>
    public override bool IsShown
    {
        get { return base.IsShown; }

        set
        {
            base.IsShown = value;

            // Reset to default rotation
            transform.localRotation = Quaternion.Euler(_InitialLocalRotation);

            if (!value)
                _WaitingForManualHide = false;
        }
    }



    Vector3 _InitialLocalRotation;


    public override void Awake()
    {
        base.Awake();
        _InitialLocalRotation = transform.localRotation.eulerAngles;
    }


    void Update()
    {
        if (_WaitingForManualHide)
        {
            SetLocalZRotation((transform.localEulerAngles.z - Time.deltaTime * _AutoRotationDegreesPerSec) % 360);
        }
    }

    public override void OnPull(float power)
    {
        base.OnPull(power);

        var powerClamped01 = Mathf.Clamp01(power);
        float excess = Mathf.Max(0f, power - 1f);

        float dampedExcess = excess * (1f - _ExcessPullRotationDamping);

        SetLocalZRotation((_InitialLocalRotation.z - 360 * (powerClamped01 + dampedExcess)) % 360);

        //transform.position = LerpUnclamped(_StartingPoint.position, _EndingPoint.position, power <= 1f ? (power - (1f - power/2)*(1f-power/2)) : (1 - 1/(1 + excess) ));
        transform.position = LerpUnclamped(_StartingPoint.position, _EndingPoint.position, 2 - 2 / (1 + powerClamped01));
    }

    public override void OnRefreshCancelled()
    {
        base.OnRefreshCancelled();

        _WaitingForManualHide = false;
    }

    public override void OnRefreshed(bool autoHide)
    {
        base.OnRefreshed(autoHide);

        _WaitingForManualHide = !autoHide;
    }

    Vector3 LerpUnclamped(Vector3 from, Vector3 to, float t) { return (1f - t) * from + t * to; }

    void SetLocalZRotation(float zRotation)
    {
        var rotE = transform.localRotation.eulerAngles;
        rotE.z = zRotation;
        transform.localRotation = Quaternion.Euler(rotE);
    }
}

