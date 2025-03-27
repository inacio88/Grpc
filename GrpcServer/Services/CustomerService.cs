using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new();
            switch (request.UserId)
            {
                case 1:
                    output.FirstName = "jamie";
                    output.LastName = "Smith";
                    break;

                case 2:
                    output.FirstName = "Jane";
                    output.LastName = "Doe";
                    break;

                default:
                    output.FirstName = "Greg";
                    output.LastName = "Thomas";
                    break;
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                        new CustomerModel
                        {
                            FirstName = "João",
                            LastName = "Silva",
                            EmailAdress = "joao.silva@email.com",
                            IsAlive = true,
                            Age = 30
                        },
                        new CustomerModel
                        {
                            FirstName = "Maria",
                            LastName = "Oliveira",
                            EmailAdress = "maria.oliveira@email.com",
                            IsAlive = true,
                            Age = 25
                        },
                        new CustomerModel
                        {
                            FirstName = "Carlos",
                            LastName = "Santos",
                            EmailAdress = "carlos.santos@email.com",
                            IsAlive = false,
                            Age = 60
                        }
            };

            foreach (var item in customers)
            {
                await responseStream.WriteAsync(item);
                await Task.Delay(500);
            }
        }

    }
}