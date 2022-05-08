using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;
public class SavingAndLoading : MonoBehaviour
{
    //private string path => $"{Application.persistentDataPath} / sav.";
    private string saveFilePath = "sav";

    public static event Action InitializeValuesEvent;
    public void Save()
    {
        var state=LoadFile();
        CaptureState(state);
        SaveFile(state);
       
    }
    public void Load()
    {
        var state = LoadFile();
        RestoreState(state);
    }
   

    private void SaveFile(object state)
    {
        string saveFile=GetPathSaveFile(saveFilePath);
        using (var stream = File.Open(saveFile, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }
    private Dictionary<string, object> LoadFile()
    {
        string saveFile = GetPathSaveFile(saveFilePath);
        if (!File.Exists(saveFile))
        {
           
            InitializeValuesEvent?.Invoke();
          
            return new Dictionary<string, object>();
        }
        using (var stream = File.Open(saveFile, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            Dictionary<string,object> result = (Dictionary<string, object>)formatter.Deserialize(stream);
           
            return result;
        }

    }
    private string GetPathSaveFile(string saveFile)
    {
        return Path.Combine(Application.persistentDataPath, saveFile);
    }
    private void CaptureState(Dictionary<string ,object> state)
    {
        
        
        foreach (var savable in FindObjectsOfType<SavableEntity>())
        {
         
            state[savable.ID] = savable.CaptureState();
           
        }
     
    }
    private void RestoreState(Dictionary<string, object> state)
    {
       
        foreach (var savable in FindObjectsOfType<SavableEntity>())
        {
           
            if (state.TryGetValue(savable.ID, out object value))
            { 
                savable.RestoreState(value);
            }
         
        }
        
    }
    public void DeleteDataSaved()
    {
        File.Delete(GetPathSaveFile(saveFilePath));
    }
}
