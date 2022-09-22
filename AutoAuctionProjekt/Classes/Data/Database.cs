using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AutoAuctionProjekt.Classes.Vehicles;

namespace AutoAuctionProjekt.Classes.Data;

public class Database
{
    private static Database? _instance = null;
    private SqlConnection conn;

    private Database()
    {
        string connectionString = @"
            Server=docker.data.techcollege.dk,20003;
            Database=Auction_House;
            User Id=sa;
            Password=H2PD081122_Gruppe3;";
        conn = new SqlConnection(connectionString);
        conn.Open();
    }

    public static Database Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Database();
            return _instance;
        }
    }
    public void GetAllTest()
    {
        SqlCommand cmd = new(@"SELECT * FROM Vehicles", conn);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                for(int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write("{0} ", reader.GetValue(i));
                }
            }
        }
        else
        {
            Console.WriteLine("Couldn't find any vehicles.");
        }
        reader.Close();
    }
}