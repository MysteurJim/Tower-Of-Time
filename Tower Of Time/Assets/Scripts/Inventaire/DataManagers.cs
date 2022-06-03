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

    public static List<string> ReadSave()
    {
        string res;
        bool flag = false;
        List<string> l = new List<string>();
        string[] files = Directory.GetFiles(@"C:\Users\lepan\AppData\LocalLow\DefaultCompany\Test Unity", "*.Tot");
        foreach(string file in files)
        {
            res = "";
            flag = false;
            for(int i = file.Length-1;i>-1 ;i--)
            {
                if(file[i] == '.')
                {
                    flag = true;
                }
                else if (file[i] == '\\')
                {
                    if(res == "")
                    {
                        res = "Player";
                    }
                    l.Add(res);
                    break;
                }
                else if (flag)
                {
                    res = file[i]+res;
                }
                
                
            }

        }

        return l;
        
    }

}
