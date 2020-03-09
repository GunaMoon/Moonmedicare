using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PONE
{
    public class Service1 : PONE
    { 
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();

         SqlConnection ConStr = new SqlConnection(ConfigurationManager.ConnectionStrings["GetConn4POne"].ConnectionString);



        public string POSItemList(string Clubid)
        {
            ConStr.Open();
            SqlCommand SC = new SqlCommand("select * from OP_Employee", ConStr);
            SqlDataAdapter oDa=new SqlDataAdapter(SC);

            DataSet oDs = new DataSet();      
            oDa.Fill(oDs, "FirstData");

            DataTable dt = oDs.Tables["FirstData"];
 
            List<Hashtable> HasahtableList = new List<Hashtable>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Hashtable ObjHashtable = new Hashtable();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ObjHashtable[dt.Columns[j].ToString()] = dt.Rows[i][j].ToString();
                }
                HasahtableList.Add(ObjHashtable);
            }

            List<Hashtable> startlist = new List<Hashtable>(HasahtableList); // CovertDatasetToHashTbl(DsDocument.Tables["MM_SEARCH"]);
            return js.Serialize(startlist);
        }

         
    }
}
