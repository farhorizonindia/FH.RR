using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class BALDeparture
{
	public BALDeparture()
	{
	}

    public DateTime _checkInDate { get; set; }
    public DateTime _checkOutDate { get; set; }
    public int _DepartureId { get; set; }
    public string _UpdownStream { get; set; }
    public int AccomId { get; set; }
    public string _PackageId { get; set; }
    public string  _Action { get; set; }

}