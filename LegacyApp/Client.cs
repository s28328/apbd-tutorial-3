namespace LegacyApp
{
    public class Client
    {
        public Client()
        {
            Name = "Client";
            ClientId = 1;
            Email = "asd@email.com";
            Address = "adress";
            Type = "Normal";
        }

        public Client(string type)
        {
            Type = type;
        }

        public string Name { get; internal set; }
        public int ClientId { get; internal set; }
        public string Email { get; internal set; }
        public string Address { get; internal set; }
        public string Type { get; set; }
    }
}