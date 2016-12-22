using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class BALAgentRooms
{
    public BALAgentRooms()
    {

    }
    public string _Action { get; set; }
    public int _id { get; set; }
    public int _AccomId { get; set; }
    public int _AgentId { get; set; }
    public int _maxRooms { get; set; }
    public DateTime _Date { get; set; }
    public DateTime toDate { get; set; }
    public int RoomCategoryId { get; set; }

    public string ImagePath { get; set; }
}