using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

public class SqlConnectionClass {
    public SqlConnection sqlcon;
    public SqlCommand sqlcmd;
    public SqlDataAdapter sqlda;
    public DataSet ds;
    public SqlDataReader sqldr;

    public SqlConnectionClass(){}

    public void Connect() {
        sqlcon = new SqlConnection();
        sqlcmd = new SqlCommand();
        sqlda = new SqlDataAdapter();
        ds = new DataSet();
        try {
            //sqlcon.ConnectionString = "Initial Catalog=AuraInventarioProtoDB; Data Source=localhost\\SQLEXPRESS; Integrated Security=SSPI";
            sqlcon.ConnectionString = "Server = tcp:NB-BODEGA\\SQLEXPRESS; Database = AuraInventarioProtoDB; User ID = Aura; Password =aura";
            
            sqlcon.Open();
            sqlcon.Close();
        } catch (SqlException ex) {
            Debug.WriteLine(ex.Message);            
        }
    }
}
