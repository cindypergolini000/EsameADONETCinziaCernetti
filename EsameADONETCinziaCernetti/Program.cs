using System;
using System.Data.SqlClient;

namespace EsameADONETCinziaCernetti
{
   public  class Program
    {
       public static void Main(string[] args)
        {
            ConnectedMode.InsertNewAgent();
            ConnectedMode.ShowAgents();
            //I primi due metodi servono per inserire un nuovo record in Agenti e per mostrare lalista dei record aggiornata
            ConnectedMode.GetInfoAgenti();
            //Ottenere le info dell'agente inserendo il codice dell'area
        }
    }
}
