using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace Serializer
{
    public class Serialization
    {

        public static object Deserialize(XmlDocument xml, Type type)
        {
            XmlSerializer s = new XmlSerializer(type);
            string xmlString = xml.OuterXml.ToString();
            byte[] buffer = ASCIIEncoding.UTF8.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(buffer);
            XmlReader reader = new XmlTextReader(ms);
            Exception caught = null;

            try
            {
                object o = s.Deserialize(reader);
                return o;
            }
            catch (Exception e)
            {
                caught = e;
            }
            finally
            {
                reader.Close();
                if (caught != null)
                    throw caught;
            }
            return null;
        }

        public static XmlDocument Serialize(object o)
        {
            XmlSerializer s = new XmlSerializer(o.GetType());
            MemoryStream ms = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(ms, new UTF8Encoding());
            writer.Formatting = Formatting.Indented;
            writer.IndentChar = ' ';
            writer.Indentation = 5;
            Exception caught = null;
            try
            {
                s.Serialize(writer, o);
                XmlDocument xml = new XmlDocument();
                string xmlString = ASCIIEncoding.UTF8.GetString(ms.ToArray());
                xml.LoadXml(xmlString);
                return xml;
            }
            catch (Exception e)
            {
                caught = e;
            }
            finally
            {
                writer.Close();
                ms.Close();
                if (caught != null)
                    throw caught;
            }
            return null;
        }
        /// <summary>
        //    /// Method to convert a custom Object to XML string
        //    /// </summary>
        //    /// <param name="pObject">Object that is to be serialized to XML</param>
        //    /// <returns>XML string</returns>
        //    public String SerializeObject(Object pObject)
        //    {
        //        try
        //        {
        //            String XmlizedString = null;
        //            MemoryStream memoryStream = new MemoryStream();
        //            XmlSerializer xs = new XmlSerializer(pObject.GetType());
        //            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
        //            xs.Serialize(xmlTextWriter, pObject);
        //            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
        //            XmlizedString = Convert.ToString(memoryStream.ToArray());
        //            return XmlizedString;
        //        }
        //        catch (Exception e)
        //        {
        //            System.Console.WriteLine(e);
        //            return null;
        //        }
        //    }        

        //    public T DeSerializeAnObject<T>(string xmlOfAnObject) 
        //    {           
        //        StringReader read = new StringReader(xmlOfAnObject); 
        //        XmlSerializer serializer = new XmlSerializer(typeof(T)); 
        //        XmlReader reader = new XmlTextReader(read);
        //        try
        //        {
        //            return (T)serializer.Deserialize(reader);                 
        //        }
        //        catch
        //        {
        //            throw;
        //        }
        //        finally { reader.Close(); read.Close(); read.Dispose(); } }

        //}
    }
}
