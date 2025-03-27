using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

Console.WriteLine("Inicio");





// var channel = GrpcChannel.ForAddress("http://localhost:5227");
// var client = new Greeter.GreeterClient(channel);

// var helloRequest = new HelloRequest {Name = "Inácio"};
// var reply = await client.SayHelloAsync(helloRequest);

// System.Console.WriteLine(reply.Message);


var channel = GrpcChannel.ForAddress("http://localhost:5227");
var CustomerClient = new Customer.CustomerClient(channel);
var clientRequest = new CustomerLookupModel { UserId = 5};
var customer = await CustomerClient.GetCustomerInfoAsync(clientRequest);

System.Console.WriteLine(customer.FirstName + " " + customer.LastName);

using (var call = CustomerClient.GetNewCustomers(new NewCustomerRequest()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var currentCustomer = call.ResponseStream.Current;
        Console.WriteLine(currentCustomer.EmailAdress + " --> " + currentCustomer.FirstName + " " + currentCustomer.LastName);
    }
}


Console.WriteLine("Fim");
