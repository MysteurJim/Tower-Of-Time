using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManagers
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
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
        Datas entity = (Datas)formatter.Deserialize(stream);
        stream.Close();
        return entity;
    }

}
