using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
            {
                new User
                {
                    Name = "Test User 1",
                    Email = "test1@test.com",
                    Address = new Address
                    {
                        Street = "New St",
                        City = "Dublin",
                        Zipcode = "1234"
                    }
                },
                new User
                {
                    Name = "Test User 2",
                    Email = "test2@test.com",
                    Address = new Address
                    {
                        Street = "Old St",
                        City = "Galway",
                        Zipcode = "1359"
                    }
                },
                new User
                {
                    Name = "Test User 3",
                    Email = "test3@test.com",
                    Address = new Address
                    {
                        Street = "Dublin Rd",
                        City = "Dundalk",
                        Zipcode = "7456"
                    }
                }
        };
    }
}
