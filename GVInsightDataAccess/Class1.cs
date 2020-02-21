using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GVInsightDataAccess
{
	public class DataAccess
	{

        public DataSet dSet;
        public DataTable dt = new DataTable();

        public SqlCommand dbCommandsql; 
        public SqlConnection conSql = new SqlConnection(@"Data Source=GCLMEHJAM\SQLEXPRESS;Initial Catalog=GVInsights;Integrated Security=True");
        public SqlDataAdapter dbAdapterSql;
        public SqlDataReader dbReaderSql;

        
		//public List<EEmployee> GetCustomerData()
		//{

  //          const string SelectCommandText = "select * from Customer";
  //          dbAdapterSql = new SqlDataAdapter(SelectCommandText, conSql);
  //          dbAdapterSql.Fill(dt);
  //          List<EEmployee> customers = new List<EEmployee>();
  //         // EEmployee [] customers ;
  //          foreach (DataRow row in dt.Rows) {
  //              EEmployee obj = new EEmployee();
  //              obj.Id = (int)row["Id"];
  //              obj.Name = (string)row["Name"];
  //              obj.HostUrl = (string)row["HostUrl"];
  //              obj.ApiPasswrod = (string)row["ApiPassword"];
  //              obj.ApiUserName = (string)row["ApiUserName"];
  //              obj.Removed = Convert.ToBoolean(row["Removed"]);
  //              customers.Add(obj);
  //              //Console.WriteLine(row["Name"]);
  //          }
  //          //Console.ReadKey();
  //          return customers;
          
           
  //      }
        //public int InsertCustomerData(EEmployee obj) {

        //    string SelectCommandText = "insert into Customer Values('"+obj.Name+"','"+obj.HostUrl+"','"+obj.ApiUserName+"','"+obj.ApiPasswrod+"',"+obj.Removed+")";
        //    Console.WriteLine(SelectCommandText);

        //    conSql.Open();
        //    dbCommandsql = new SqlCommand(SelectCommandText,conSql);
        //    try
        //    {

        //        dbCommandsql.ExecuteNonQuery();
        //        conSql.Close();
        //        return 1;
        //    }
        //    catch (Exception ex) {
        //        Console.WriteLine(ex);
        //        conSql.Close();
        //        return 0;
        //    }

            
        //}

        public int deleteCustomerData(int id) {
            string SelectCommandText = "delete from Customer where Id = "+id;
            Console.WriteLine(SelectCommandText);

            conSql.Open();
            dbCommandsql = new SqlCommand(SelectCommandText, conSql);
            try
            {

                dbCommandsql.ExecuteNonQuery();
                conSql.Close();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                conSql.Close();
                return 0;
            }
        }
       

        //public int UpdateCustomerData(EEmployee obj,int id) {
        //    string SelectCommandText = "update Customer set Name= '"+obj.Name+ "', HostUrl=',"+obj.HostUrl+ "', ApiUserName='"+obj.ApiUserName+ "', ApiPassword='"+obj.ApiPasswrod+ "', Removed="+obj.Removed+"  where Id = " + id;
        //    Console.WriteLine(SelectCommandText);

        //    conSql.Open();
        //    dbCommandsql = new SqlCommand(SelectCommandText, conSql);
        //    try
        //    {
                
        //        int effectedRow = dbCommandsql.ExecuteNonQuery();
        //        conSql.Close();
        //        return effectedRow;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        conSql.Close();
        //        return 0;
        //    }
        //}

       
	}
}
