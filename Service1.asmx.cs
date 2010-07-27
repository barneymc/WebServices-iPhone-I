using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace Phone
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string HelloWorld()
        {
            return "Hello11";
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string HelloWorldToPerson(string name,string surname, int age)
        {
            return "Hello World to " + age.ToString() + " . Your name is " + name.ToString() + ". Your surname is " + surname.ToString();
        }


        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public int UserLogin(string username, string password)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            string sqlText;
            string strAnswer = null;
            sqlText = "exec UserLogin '" + username.ToString() + "','" + password.ToString() + "'";

            myConnection.Open();


            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(sqlText, myConnection);


            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                strAnswer = myReader["UserID"].ToString();

            }

            myConnection.Close();
            return Convert.ToInt16(strAnswer);
          
        }


        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetJobFileListJSON(int UserID)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            DataSet objDataSet = new DataSet();

            SqlCommand myCommand = new SqlCommand(" exec GetJobFileListJSON " + UserID.ToString());
            myConnection.Open();

            SqlDataAdapter objDataAdapter = new SqlDataAdapter(" exec GetJobFileListJSON " + UserID.ToString(), myConnection);
            objDataAdapter.Fill(objDataSet, "JobFiles");
            myConnection.Close();

            //Create the Multidimensional Array
            string[][] JobFileArray = new string[objDataSet.Tables[0].Rows.Count][];
            int i = 0;

            foreach (DataRow rs in objDataSet.Tables[0].Rows)
            {
                JobFileArray[i] = new string[] { rs["JobFileName"].ToString(), rs["JobFileID"].ToString(), rs["CustomerName"].ToString() };
                i = i + 1;

            }

            //return the JSON data
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(JobFileArray);
            myConnection.Close();
            
            return strJSON;

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetQuoteFilesFromJobJSON(int JobFileID)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            DataSet objDataSet = new DataSet();

            SqlCommand myCommand = new SqlCommand(" exec GetQuoteFilesFromJobJSON " + JobFileID.ToString());
            myConnection.Open();

            SqlDataAdapter objDataAdapter = new SqlDataAdapter(" exec GetQuoteFilesFromJobJSON " + JobFileID.ToString(), myConnection);
            objDataAdapter.Fill(objDataSet, "QuoteFilesFiles");
            myConnection.Close();

            //Create the Multidimensional Array
            string[][] QuoteFileFileArray = new string[objDataSet.Tables[0].Rows.Count][];
            int i = 0;

            foreach (DataRow rs in objDataSet.Tables[0].Rows)
            {
                QuoteFileFileArray[i] = new string[] { rs["QuoteFileID"].ToString(), rs["QuoteFileName"].ToString(), rs["CreatedDate"].ToString(), rs["UserID"].ToString() };
                i = i + 1;

            }

            //return the JSON data
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(QuoteFileFileArray);
            myConnection.Close();

            return strJSON;

        }

        //Added for the WhereDat Project 24-July-2010
        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetCounties()
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            DataSet objDataSet = new DataSet();

            SqlCommand myCommand = new SqlCommand(" select * from County " );
            myConnection.Open();

            SqlDataAdapter objDataAdapter = new SqlDataAdapter(" select * from County ", myConnection);
            objDataAdapter.Fill(objDataSet, "QuoteFilesFiles");
            myConnection.Close();

            //Create the Multidimensional Array
            string[][] QuoteFileFileArray = new string[objDataSet.Tables[0].Rows.Count][];
            int i = 0;

            foreach (DataRow rs in objDataSet.Tables[0].Rows)
            {
                QuoteFileFileArray[i] = new string[] { rs["ID"].ToString(), rs["CountyName"].ToString(), rs["SoftDelete"].ToString()};
                i = i + 1;

            }

            //return the JSON data
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(QuoteFileFileArray);
            myConnection.Close();

            return strJSON;

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetQuoteDetails(int QuoteFileID)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            DataSet objDataSet = new DataSet();

            SqlCommand myCommand = new SqlCommand(" exec GetQuoteDetail " + QuoteFileID.ToString());
            myConnection.Open();

            SqlDataAdapter objDataAdapter = new SqlDataAdapter(" exec GetQuoteDetail " + QuoteFileID.ToString(), myConnection);
            objDataAdapter.Fill(objDataSet, "QuoteFileDetail");
            myConnection.Close();

            //Create the Multidimensional Array
            string[][] QuoteFileDetailArray = new string[objDataSet.Tables[0].Rows.Count][];
            int i = 0;

            foreach (DataRow rs in objDataSet.Tables[0].Rows)
            {
                QuoteFileDetailArray[i] = new string[] { rs["QuoteStatusID"].ToString(), rs["refNum"].ToString(), rs["JobType"].ToString(), rs["Comments"].ToString(),rs["isTemplate"].ToString(),rs["jobFileID"].ToString(),rs["QuoteFileName"].ToString() };
                i = i + 1;

            }

            //return the JSON data
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(QuoteFileDetailArray);
            myConnection.Close();

            return strJSON;

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetShipAddressForQuote(Int64 QuoteFileID)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            DataSet objDataSet = new DataSet();

            SqlCommand myCommand = new SqlCommand(" exec LoadShipAddress " + QuoteFileID.ToString());
            myConnection.Open();

            SqlDataAdapter objDataAdapter = new SqlDataAdapter(" exec LoadShipAddress " + QuoteFileID.ToString(), myConnection);
            objDataAdapter.Fill(objDataSet, "QuoteFileDetail");
            myConnection.Close();

            //Create the Multidimensional Array
            string[][] QuoteFileDetailArray = new string[objDataSet.Tables[0].Rows.Count][];
            int i = 0;

            foreach (DataRow rs in objDataSet.Tables[0].Rows)
            {
                QuoteFileDetailArray[i] = new string[] { rs["FullShipAddress"].ToString() };
                i = i + 1;

            }

            //return the JSON data
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(QuoteFileDetailArray);
            myConnection.Close();

            return strJSON;

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetContactDetails(Int64 QuoteFileID)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            DataSet objDataSet = new DataSet();

            SqlCommand myCommand = new SqlCommand(" exec GetQuoteContacts " + QuoteFileID.ToString());
            myConnection.Open();

            SqlDataAdapter objDataAdapter = new SqlDataAdapter(" exec GetQuoteContacts " + QuoteFileID.ToString(), myConnection);
            objDataAdapter.Fill(objDataSet, "QuoteFileDetail");
            myConnection.Close();

            //Create the Multidimensional Array
            string[][] QuoteFileDetailArray = new string[objDataSet.Tables[0].Rows.Count][];
            int i = 0;

            foreach (DataRow rs in objDataSet.Tables[0].Rows)
            {
                QuoteFileDetailArray[i] = new string[] { rs["telephone"].ToString(), rs["CustomerName"].ToString(), rs["Address1"].ToString(), rs["City"].ToString() };
                i = i + 1;
            }
            //return the JSON data
            JavaScriptSerializer js = new JavaScriptSerializer();
            string strJSON = js.Serialize(QuoteFileDetailArray);
            myConnection.Close();

            return strJSON;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetJobFileList(int UserID)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            string sqlText;
            string strAnswer = null;
            sqlText = "exec GetJobFileList " + UserID.ToString();           //Proc returns RAW XML

            myConnection.Open();


            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(sqlText, myConnection);


            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                strAnswer = myReader[0].ToString();

            }

            myConnection.Close();
            return strAnswer;

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetQuoteFilesFromJob(string JobFileID)
        {
            //Connect to the database and execute the proc
            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=H_CONFIG;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);
            string sqlText;
            string strAnswer = null;
            sqlText = "exec GetQuoteFilesFromJob " + JobFileID.ToString();           //Proc returns RAW XML

            myConnection.Open();


            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand(sqlText, myConnection);


            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                strAnswer = myReader[0].ToString();

            }

            

            myConnection.Close();
            return strAnswer;

        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string GetDBMessage()
        {

            //Connection to database
            string _connstring = "Server=bwd-cs-m607;Database=SAF_PRODUCTION;uid=cpcuser;pwd=cpcuser2";

            SqlConnection myConnection = new SqlConnection(_connstring);

            //SqlConnection myConnection = new SqlConnection("user id=cpcuser;" +
            //                           "password=cpcuser2;server=206.44.199.41,1433" +
             //                          "Trusted_Connection=yes;" +
              //                         "database=SAF_PRODUCTION; " +
               //                        "connection timeout=30");

            string sqlText;
            string strAnswer = null;
            sqlText = "select [Message] from Zygone where ID=1";

            myConnection.Open();

            
            SqlDataReader myReader = null;
            SqlCommand myCommand=new SqlCommand(sqlText,myConnection);


            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                strAnswer=myReader["Message"].ToString();
                
            }

            myConnection.Close();

            return strAnswer;
        }

    }
}
