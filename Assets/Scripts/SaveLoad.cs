using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

    public static int savedScore;

    public static void Save(int hiscore)
    {
        savedScore = hiscore;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/score.mne");
        bf.Serialize(file, savedScore);
        file.Close();

    }

    public static int Load()
    {
        if (File.Exists(Application.dataPath + "/score.mne"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/score.mne", FileMode.Open);
            savedScore = (int)bf.Deserialize(file);
            file.Close();

            return savedScore;
        }

        return 0;
    }
}
