using MiniHbys.Business.Abstraction;
using MiniHbys.DataAccess.Abstraction;

namespace MiniHbys.Business.Services;

public class SystemService : ISystemService
{
    private readonly ISystemManager _systemManager;

    public SystemService(ISystemManager systemManager)
    {
        _systemManager = systemManager;
    }
    
    public void BackupDatabase(string backupPath,string fileName)
    {
        _systemManager.CreateBackup(backupPath,fileName);
    }

    public void RestoreDatabase(string fullPath)
    {
        _systemManager.RestoreBackup(fullPath);
    }
}