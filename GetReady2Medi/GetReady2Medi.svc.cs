﻿using System;
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

namespace GetReady2Medi
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : GetReady2Medi
    {
        System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
        SqlConnection ConStr = new SqlConnection(ConfigurationManager.ConnectionStrings["GetConn4POne"].ConnectionString);
        public string POSItemList(string Clubid)
        {
            ConStr.Open();
            SqlCommand SC = new SqlCommand("select * from OP_Employee", ConStr);
            SqlDataAdapter oDa = new SqlDataAdapter(SC);

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
