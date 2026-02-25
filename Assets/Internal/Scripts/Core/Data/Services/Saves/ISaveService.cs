namespace Internal.Scripts.Core.Data.Services.Saves
{
    public interface ISaveService
    {
        public void Save(PlayerData from);
        public void Load(PlayerData to);
    }
}