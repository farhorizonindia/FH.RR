namespace FarHorizon.Reservations.BusinessServices.Online.BAL
{
    public class BALAgentPayment
    {
        public BALAgentPayment()
        {
        }

        public string _Action { get; set; }
        public string _FirstName { get; set; }
        public string _LastName { get; set; }
        public int _AgentCode { get; set; }
        public string _BillingAddress { get; set; }
        public string _PaymentMethod { get; set; }
        public string _EmailId { get; set; }
        public string _Password { get; set; }

        public int agentid;

        public bool OnCredit { get; set; }
        public decimal CreditLimit { get; set; }

        public string MarketId { get; set; }

        public string Phone { get; set; }
    }
}