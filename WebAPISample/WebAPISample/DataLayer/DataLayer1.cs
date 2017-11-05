using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPISample.Models;

namespace WebAPISample.DataLayer
{
    public class DataLayer1 : IDataLayer
    {
        public IEnumerable<SampleData> GetAll()
        {
            //var connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=Temp;Integrated Security=True;";
            var connectionString = "Data Source=heegreen.cxexjzcnijap.us-east-1.rds.amazonaws.com;Initial Catalog=Temp;Persist Security Info=True;User ID=heegreen;Password=heegreen";
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter da;
            DataTable dt;
            con.Open();
            string qry = "select * from TempTable";
            //SqlCommand cmd = new SqlCommand(qry,con);
            da = new SqlDataAdapter(qry, con);
            dt = new DataTable();
            da.Fill(dt);

            List<SampleData> data = new List<SampleData>();
            dt.AsEnumerable().ToList().ForEach(x => {
                SampleData sd = new SampleData();
                sd.City = x.Field<string>("City");
                sd.LightSensorValue = x.Field<string>("LightSensorValue");
                sd.MotorTimeIntervalInMins = x.Field<int>("MotorTimeIntervalInMins").ToString();
                sd.SampleName = x.Field<string>("SampleName");
                sd.TemperatureSensorValue = x.Field<string>("TemperatureSensorValue");
                sd.Town = x.Field<string>("Town");

                data.Add(sd);
            });
            con.Dispose();
            con.Close();
            return data;
        }

        public bool PostData(SampleData sampleData)
        {
            //var connectionString = "Data Source=ADMIN-PC\\SQLEXPRESS;Initial Catalog=Temp;Integrated Security=True;";
            var connectionString = "Data Source=heegreen.cxexjzcnijap.us-east-1.rds.amazonaws.com;Initial Catalog=Temp;Persist Security Info=True;User ID=heegreen;Password=heegreen";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string qry = string.Format("INSERT INTO [dbo].[TempTable]([SampleName],[City],[Town],[CreatedDateTime],[TemperatureSensorValue],[LightSensorValue],[MotorTimeIntervalInMins]) VALUES ('{0}','{1}','{2}',{3},{4},{5},{6})", sampleData.SampleName, sampleData.City, sampleData.Town, "@create_date", sampleData.TemperatureSensorValue, sampleData.LightSensorValue, sampleData.MotorTimeIntervalInMins);
            

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.Add("@create_date", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close();
            return true;
        }
    }
}