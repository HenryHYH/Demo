namespace Model
{
    public class User
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("Name = {0}; Password = {1};", Name, Password);
        }

        public static User Instance
        {
            get
            {
                return new User
                {
                    Name = "HelloWorld",
                    Password = "Password"
                };
            }
        }
    }
}
