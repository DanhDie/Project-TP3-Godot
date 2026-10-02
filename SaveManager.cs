using Godot;
using System;

public partial class SaveManager : Node
{
    const string SAVE_PATH = "user://game_save.tres";

    public static void saveGame(UserSaveData data)
    {
        ResourceSaver.Save(data, SAVE_PATH);
    }

    public static UserSaveData loadGame()
    {
        if (!FileAccess.FileExists(SAVE_PATH))
            return new UserSaveData();

        Resource resource = ResourceLoader.Load(SAVE_PATH);

        if (resource is UserSaveData data)
            return data;

        FileAccess file = FileAccess.Open(SAVE_PATH, FileAccess.ModeFlags.Write);
        file?.Close();

        return new UserSaveData();
    }
}