using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient; 
using System;    
using System.Collections.Generic; 

namespace coreApi.Models
{
    public class AppDbContext {
            
        public string connString { get; set; }    

        public AppDbContext()    
        {    
            
            //Console.WriteLine("Baringer! Conn: " + connectionString);
            //this.connString = connectionString;    
            
        }    

        public SqlConnection pegaConnBd()    
        {    
            this.connString = "Server=tcp:dbbaringer.database.windows.net,1433;Initial Catalog=dbDanielBaringer;Persist Security Info=False;User ID=dbaringer;Password=!dbadb1979@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connString);    
        }    
    }
}