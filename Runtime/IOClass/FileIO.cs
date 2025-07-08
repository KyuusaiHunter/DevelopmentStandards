using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class FileIO : MonoSingleton<FileIO>
{
    public void WriteTXT(string fileName, string newInfo)
    {
        string FILEPATH = "";
        string CONTENT = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            FILEPATH = Path.Combine(Application.persistentDataPath, fileName);
            CONTENT = File.ReadAllText(FILEPATH);
            CONTENT += '\n' + newInfo;
            File.WriteAllText(FILEPATH, CONTENT);
            //File.WriteAllText(url, string.Empty);
        }
    }
    public string LoadTXTtoString(string fileName)
    {
        string url = Path.Combine(Application.persistentDataPath, fileName);
        string s = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            s = File.ReadAllText(url);
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            FileStream fs = new FileStream(url, FileMode.Open);
            byte[] bytes = new byte[1024 * 8];
            fs.Read(bytes, 0, bytes.Length);
            //����ȡ���Ķ�����ת�����ַ���
            s = new UTF8Encoding().GetString(bytes);
            fs.Close();
        }
        return s;
    }
}
