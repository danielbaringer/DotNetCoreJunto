
using coreApi.Models;
using coreApi.Helpers;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace coreApi.Models
{
     
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }


        public List<Usuario> verificaLoginSenha()  
        {  

            List<Usuario> list = new List<Usuario>();  
        
            using (SqlConnection conn = new AppDbContext().pegaConnBd())  
            {  
                conn.Open();  
                SqlCommand cmd = new SqlCommand("select * from tbl001sistemausuarios where tbl001Id < 10;", conn);  
        
                using (var reader = cmd.ExecuteReader())  
                {  
                    while (reader.Read())  
                    {  
                        list.Add(new Usuario()  
                        {  
                            Id = (int)reader["tbl001Id"] ,  
                            Nome = (string) reader["tbl001NomeUsuario"],  
                            Login = (string) reader["tbl001Login"],  
                            Senha = (string) reader["tbl001Senha"],  

                        }); 

                        System.Console.WriteLine(reader["tbl001Login"]);
                        
                    }  
                }  
            }  




            return list;
        }


    }
}