using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 DefaultValue;

    public void OnAfterDeserialize()
    {
        DefaultValue = initialValue;
    }

    public void OnBeforeSerialize() { }
}
