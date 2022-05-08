using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavableEntity : MonoBehaviour
{
    [SerializeField] private string Id = string.Empty;
    public string ID { get { return Id; } }

    [ContextMenu("Generate ID")]
    private void GenerateId()
    {
        Id = System.Guid.NewGuid().ToString();
    }
    public object CaptureState()
    {
        
        Dictionary<string, object> data = new Dictionary<string, object>();
       
        foreach(var savable in GetComponents<ISavable>())
        {
            data[savable.GetType().ToString()]=savable.CaptureState();
        }
        return data;
    }
    public void RestoreState(object state)
    {
       
        var data = (Dictionary<string, object>)(state);
        foreach(var savable in GetComponents<ISavable>())
        {
            string typeName=savable.GetType().ToString();
            if(data.TryGetValue(typeName,out object value))
            {
                savable.RestoreState(value);
            }
        }

    }
}
