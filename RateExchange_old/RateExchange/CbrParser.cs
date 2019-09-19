using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.Data.SqlClient;

namespace RateExchange
{
    class CbrParser
    {

        public static void ReaderCbr(string link)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(link);
            XmlNodeList nodeListCharCode = doc.GetElementsByTagName("CharCode");
            XmlNodeList elemListValues = doc.GetElementsByTagName("Value");
            XmlElement root = doc.DocumentElement;
            string date = root.GetAttribute("Date");

            Console.WriteLine(date);
            for (int i =0; i < elemListValues.Count;i++)
            {
                Console.WriteLine(elemListValues[i].InnerText);
                Console.WriteLine(nodeListCharCode[i].InnerText);
            }
            //поменять AttachDbFilename на директорию где будет находиться проект
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\vs_project\RateExchange_old\RateExchange\DatabaseCbr.mdf; Integrated Security = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = string.Format("INSERT INTO Course" + "(date, currency, value) VALUES(@date, @currency, @value)");

                for (int i = 0; i < nodeListCharCode.Count; i++)
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@date", Convert.ToDateTime(date));
                        cmd.Parameters.Add("@currency", nodeListCharCode[i].InnerText);
                        cmd.Parameters.Add("@value", (float)Convert.ToDouble(elemListValues[i].InnerText));
                        cmd.ExecuteNonQuery();
                    }
                }
            

            }
        }
    }
}
