using Application.Models.CustomerModels.Response;
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
                        Document = "cpf",
                        Password = "123"
                    },
                    new Customer()
                    {
                        CustomerId = Guid.NewGuid(),
                        Name = "User_2",
                        Email = "User_2@gmail.com",
                        Age = 21,
                        Phone = 123456781,
                        Document = "cpf1",
                        Password = "1234"
                    },
                ];
            }
        }

        private static List<GetAllCustomersResponse> _getAllCustomersResponseMock
        {
            get
            {
                return
                [
                    new GetAllCustomersResponse()
                    {
                        CustomerId = Guid.NewGuid(),
                        Name = "User_1",
                        Email = "User_1@gmail.com",
                        Age = 20,
                        Phone = 123456789,
                        Document = "cpf"
                    },
                    new GetAllCustomersResponse()
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

        private static GetCustomerByIdResponse _getCustomerByIdResponseMock
        {
            get
            {
                return
                    new GetCustomerByIdResponse()
                    {
                        CustomerId = Guid.NewGuid(),
                        Name = "User_1",
                        Email = "User_1@gmail.com",
                        Age = 20,
                        Phone = 123456789,
                        Document = "cpf"
                    };
            }
        }

        public static List<Customer> CustomerDBMock() => _customerDBMock;

        public static List<GetAllCustomersResponse> GetAllCustomersResponseMock() => _getAllCustomersResponseMock;

        public static GetCustomerByIdResponse GetCustomerByIdResponseMock() => _getCustomerByIdResponseMock;
    }
}
