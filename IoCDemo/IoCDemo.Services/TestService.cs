namespace IoCDemo.Services
{
    public class TestService : IService
    {
        private readonly IDal dal;

        public TestService(IDal dal)
        {
            this.dal = dal;
        }

        public void Save()
        {
            dal.Save();
        }
    }
}
