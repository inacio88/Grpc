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
    }
}