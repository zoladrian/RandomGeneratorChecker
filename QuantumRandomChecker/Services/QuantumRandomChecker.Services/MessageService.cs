using QuantumRandomChecker.Services.Interfaces;

namespace QuantumRandomChecker.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
