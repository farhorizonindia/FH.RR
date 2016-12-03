using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;

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
    public bool OnCredit { get; set; }
    public decimal CreditLimit { get; set; }
}