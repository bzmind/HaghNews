namespace DataLayer.Repositories
{
    public interface ILoginRepository
    {
        bool UserExists(string username, string password);
    }
}
