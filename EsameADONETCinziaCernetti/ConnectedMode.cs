using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace EsameADONETCinziaCernetti
{
    class ConnectedMode
    {
        const string connectionString = @"Persist Security Info = False; Integrated Security = True; Initial Catalog=Poliziadb; Server=WINAPE74KTQDFII\SQLEXPRESS";
        public static void ShowAgents()
        {
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT * FROM Agente";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0}, {1}, {2}, {3}",
                      reader["FirstName"],
                      reader["CodiceFiscale"],
                      reader["LastName"],
                      reader["ID"]);

                }
                connection.Close();
            }
            
        }
        public static void InsertNewAgent()
        {
            string nome, cognome, cf, data;
            int annidiservizio;
            Console.WriteLine("Prego, inserire credenziali nuovo agente:\nNome: ");
            nome = Console.ReadLine();
            Console.WriteLine("Cognome: ");
            cognome = Console.ReadLine();
            Console.WriteLine("Codice fiscale (16 caratteri): ");
            cf = Console.ReadLine();
            Console.WriteLine("Data di nascita (gg/mm/aaaa): ");
            data = Console.ReadLine();
            Console.WriteLine("Anni di servizio: ");
            annidiservizio = Int32.Parse(Console.ReadLine());


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "stpInsertAgent";
                command.Parameters.AddWithValue("@nome", nome);
                command.Parameters.AddWithValue("@cognome", cognome);
                command.Parameters.AddWithValue("@cf", cf);
                command.Parameters.AddWithValue("@data", data);
                command.Parameters.AddWithValue("@anniservizio", annidiservizio);
    
                command.ExecuteNonQuery();

                connection.Close();


            }


        }
        public static void GetInfoAgenti()
        {
            string area;
            Console.WriteLine("Prego, inserire 11010 per la prima zona OR 00001 per la seconda");
            //nel mio db esistono solo due zone contrassegnate da 11010 e 00001
            area = Console.ReadLine();
            if (!(area.Equals("11010") || area.Equals("00001"))) return;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "stpSelAgentUnderCondition";
                command.Parameters.AddWithValue("@codicearea", area);
                SqlDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                    Console.WriteLine("-----Agente n° {0}----- \n \n Nome:   {1} \n ID dell'agente:  {2}, \n La zona pattugliata da questo agente è ad alto Richio?  {3}, \n Anni di servizio: {4} \n ",
                       count,
                        reader["FirstName"],
                         reader["ID_AGENTE"],
                        reader["AltoRischio"],
                        reader["AnniServizio"],
                        reader["CodiceArea"]);

                }
                reader.Close();
                connection.Close();


            }

        }
    }
}
