namespace MiniHbys.Business.Abstraction;

public interface ISystemService
{
    void BackupDatabase(string backupPath, string fileName);
    void RestoreDatabase(string fullPath);
}