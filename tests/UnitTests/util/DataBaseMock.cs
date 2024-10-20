using Domain.Entities;

namespace UnitTests.util
{
    public static class DataBaseMock
    {
        private static List<Customer> _customerDBMock
        {
            get
            {
                return
                [
                    new Customer()
                    {
                        CustomerId = Guid.NewGuid(),
                        Name = "User_1",
                        Email = "User_1@gmail.com",
                        Age = 20,
                        Phone = 123456789,
                        Document = "cpf"
                    },
                    new Customer()
                    {
                        CustomerId = Guid.NewGuid(),
                        Name = "User_2",
                        Email = "User_2@gmail.com",
                        Age = 21,
                        Phone = 123456781,
                        Document = "cpf1"
                    },
                ];
            }
        }

        public static List<Customer> CustomerDBMock() => _customerDBMock;
    }
}
