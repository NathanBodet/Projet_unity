using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager
{

    public static void Save(object entity, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Create(Application.persistentDataPath + "/" + fileName);
        formatter.Serialize(stream, entity);
        stream.Close();
    }

    public static object Load(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            Datas entity = (Datas)formatter.Deserialize(stream);
            stream.Close();
            return entity;
        }
        return null;
    }

    public static object LoadNames(string fileName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            DatasNames entity = (DatasNames)formatter.Deserialize(stream);
            stream.Close();
            return entity;
        }
        return null;
    }

}
