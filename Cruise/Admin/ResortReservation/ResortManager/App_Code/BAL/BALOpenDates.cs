using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public class BALOpenDates
{
    public BALOpenDates()
    {
    }

    public string _Action { get; set; }
    public int _AccomId { get; set; }
    public int  _CountryId{ get; set; }
    public int _RiverId{ get; set; }
    public string _PackageId{ get; set; }
    public DateTime _checkInDate { get; set; }
    public DateTime _checkOutDate { get; set; }
    public bool Status { get; set; }
    public int Id { get; set; }

    public int Remdays { get; set; }


    public string selectedDate { get; set; }
}
