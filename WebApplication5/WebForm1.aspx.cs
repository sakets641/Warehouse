using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication5
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected String mutualfund()
        {
            var client = new RestClient("https://latest-mutual-fund-nav.p.rapidapi.com/fetchLatestNAV");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "latest-mutual-fund-nav.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "8c62ed044dmsh182a6cd1ac435c2p1a270fjsn208a1ffe79d1");
            IRestResponse response = client.Execute(request);
            var data1= JsonConvert.DeserializeObject(response.Content);
            String data2 = data1.ToString();
            return data2;

        }
        protected String Page_Load(int foliono)
        {
            string strurltest = String.Format("https://api.mfapi.in/mf/" + foliono);

            WebRequest requestObject = WebRequest.Create(strurltest);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = null;
            responseObject = (HttpWebResponse)requestObject.GetResponse();
            String responseObj = responseObject.ToString();
            string strresulttest = null;
            using (Stream stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                strresulttest = sr.ReadToEnd();
                return strresulttest;
                sr.Close();
            }
           // var details = JObject.Parse(responseObj);
            //Console.WriteLine(string.Concat("Hi ", details["fact"], " " + details["length"]));

            /* Console.ReadLine();
             var myDetails = JsonConvert.DeserializeObject< MyDetail >(jsonData);
             Console.WriteLine(string.Concat("Hi ", myDetails.fact, " " + myDetails.length));
             Console.ReadLine();
         }
             public class MyDetail
             {
             public string fact
             {
                 get;
                 set;
             }
             public string length
             {
                 get;
                 set;
             }*/
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String strresult= mutualfund();
            int folionumber= int.Parse(foliono.Text);
            //String  strresult = Page_Load(folionumber);
            Console.WriteLine(strresult);
            //String strresult = "";
            int userVal = int.Parse(foliono.Text);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["APICoreDBConnectionString2"].ConnectionString;

            SqlCommand cmd = new SqlCommand("insert_record_to", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Folionumber", SqlDbType.Int);
            cmd.Parameters.Add("@Detail", SqlDbType.VarChar);
            cmd.Parameters["@Folionumber"].Value = userVal;
            cmd.Parameters["@Detail"].Value = strresult;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            GridView1_SelectedIndexChanged1();

        }

        protected void GridView1_SelectedIndexChanged1()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["APICoreDBConnectionString2"].ConnectionString;
            int userVal = int.Parse(foliono.Text);
            SqlCommand cmd = new SqlCommand("get_record_to", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@foliono", SqlDbType.Int);
            cmd.Parameters["@foliono"].Value = userVal ;
            conn.Open();
            cmd.ExecuteNonQuery();
            TextBox1.Text = Convert.ToString(cmd.ExecuteScalar());
            String data1 = cmd.ToString();
            conn.Close();
        }

        protected void foliono_TextChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void EntityDataSource1_Selecting(object sender, EntityDataSourceSelectingEventArgs e)
        {

        }

        protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }
    }
}
