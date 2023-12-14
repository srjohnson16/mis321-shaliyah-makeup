using api.Database;
using api.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace api
{
    public class CarUtility
    {
        public List<Car> GetAllCars()
        {
            ConnectionString db = new ConnectionString();
            using var con = new MySqlConnection(db.cs);

            List<Car> myCars = new List<Car>();
            con.Open();

            string stm = "SELECT * FROM cars;";
            using var cmd = new MySqlCommand(stm, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                System.Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)}");

                myCars.Add(new Car()
                {
                    id = rdr.GetInt32(0),
                    make = rdr.GetString(1),
                    model = rdr.GetString(2),
                    mileage = rdr.GetDouble(3),
                    enterdate = rdr.GetString(4),
                    isHold = rdr.GetBoolean(5), // Convert TINYINT to bool
                    deleted = rdr.GetBoolean(6) 
                });
            }

            con.Close();
            return myCars;
        }

        public void AddCars(Car myCar)
        {
               try
        {
               System.Console.WriteLine(myCar.make);
            ConnectionString db = new ConnectionString();
            using var con = new MySqlConnection(db.cs);
            con.Open();

            string stm = "INSERT INTO cars (make, model, mileage, enterdate, isHold, deleted) VALUES (@make, @model, @mileage, @enterdate, @isHold, @deleted)";
            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@make", myCar.make);
            cmd.Parameters.AddWithValue("@model", myCar.model);
            cmd.Parameters.AddWithValue("@mileage", myCar.mileage);
            cmd.Parameters.AddWithValue("@enterdate", myCar.enterdate);
            cmd.Parameters.AddWithValue("@isHold", myCar.isHold); 
            cmd.Parameters.AddWithValue("@deleted", myCar.deleted); 

            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
         Console.WriteLine($"Car added successfully: {myCar}");
         
        } catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.Error.WriteLine($"An error occurred in AddCars: {ex}");

            // Propagate the exception or handle it based on your requirements
            throw;
        }

        }

        public void DeleteCar(int id)
        {
            bool delete = true;

            ConnectionString db = new ConnectionString();
            using var con = new MySqlConnection(db.cs);
            con.Open();

            string stm = "UPDATE cars SET deleted = @delete WHERE id = @id";
            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@delete", delete); // Convert bool to TINYINT
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();
            
            Console.WriteLine($"Rows affected: {rowsAffected}"); // Add this line for debugging

            con.Close();
        }

        public void HoldCar(int id, bool isHold)
        {
            isHold = !(isHold);

            ConnectionString db = new ConnectionString();
            using var con = new MySqlConnection(db.cs);
            con.Open();

            string stm = "UPDATE cars SET isHold = @hold WHERE id = @id";
            using var cmd = new MySqlCommand(stm, con);

            cmd.Parameters.AddWithValue("@hold", isHold); // Convert bool to TINYINT
            cmd.Parameters.AddWithValue("@id", id);

            int rowsAffected = cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
