namespace MiniHbys.DataAccess.Abstraction;

public interface ISystemManager
{
    void CreateBackup(string backupPath, string fileName);
    void RestoreBackup(string fullPath);
}