using System;

[Serializable]
public class SaveData {
	public int id;
	public string saveName;
	public int lvCount;

    public SaveData()
    {
    }

    public SaveData(int id, string saveName, int lvCount) {
        this.id = id;
        this.saveName = saveName;
        this.lvCount = lvCount;
    }
}
