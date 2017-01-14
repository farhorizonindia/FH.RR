using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Web.UI.WebControls;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using FarHorizon.Reservations.BusinessServices.Online;

public partial class response : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
         
    }

    public string CRCCode(String ClearString, String key)
    {
        
        Crc32 crc32 = new Crc32();
        String hash = String.Empty;
        byte[] mybytes = Encoding.UTF8.GetBytes(ClearString);
        foreach (byte b in crc32.ComputeHash(mybytes)) hash += b.ToString("x2");
        UInt32 Output = UInt32.Parse(hash, System.Globalization.NumberStyles.HexNumber);
        UInt32 Output1 = UInt32.Parse(key);
        //  Response.Write(Output);
        //  Response.Write(Output1);
        if (Output1 == Output)
        {
            // Response.Write("Secure Hash match.");
            // return true.ToString();

        }
        else
        {
            Response.Write("Secure Hash mismatch.");
            // return true.ToString();
           // Environment.Exit(0);
        }

        return hash;

    }

}
