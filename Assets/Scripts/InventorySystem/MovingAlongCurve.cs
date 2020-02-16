using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    class MovingAlongCurve : MonoBehaviour
    {
    private float _startTime;
    private float _duration;
    private Vector3 _from, _to, _through;

    private bool _started = false;
    private bool _removeWhenFinished;

    public MovingAlongCurve StartMoving(Vector3 from, Vector3 to, Vector3 through, float duration)
    {
        _from = from;
        _to = to;
        _through = through;
        _duration = duration;

        _started = false;
        _startTime = Time.time;

        return this;
    }

    private void Update()
    {
        if (_started)
        {
            var progress = (Time.time - _startTime) / _duration;

            transform.position = GetBezierPoint(_from, _through, _to, progress);

            if (progress >= 1 && _removeWhenFinished)
                Destroy(this);
        }
    }

    public void RemoveWhenFinished()
    {
        _removeWhenFinished = true;
    }

    private Vector3 GetBezierPoint(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        float negativeT = 1f - t,
            aCoef = negativeT * negativeT,
            bCoef = 2 * t * negativeT,
            cCoef = t * t;

        return new Vector3(a.x * aCoef + b.x * bCoef + c.x * cCoef,
                           a.y * aCoef + b.y * bCoef + c.y * cCoef,
                           a.z * aCoef + b.z * bCoef + c.z * cCoef);
    }
}

