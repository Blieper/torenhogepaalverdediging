using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistic : MonoBehaviour {

    private float _value;

    public float Value {
        get {
            return _value;
        }
        set {
            float Old = Value;
            _value = Mathf.Clamp(value, MinValue, MaxValue);

            print(_value);

            if (Old != Value)
                OnValueChanged(Old, Value);
        }
    }

    public float MaxValue = 100f;
    public float MinValue = 0f;
    public float Default = 100f;

    public virtual void Start () {
        Value = Default;
    }
	
    public virtual void OnValueChanged (float Old, float New) {

    }

    public void Reset() {
        Value = Default;
    }
}
