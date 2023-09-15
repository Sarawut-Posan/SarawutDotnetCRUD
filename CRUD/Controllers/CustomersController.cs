using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CRUD.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            using SqlConnection conn = new Connect().GetConnection();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Customers";
            using SqlDataReader reader = cmd.ExecuteReader();
            List<Customerscs> customers = new List<Customerscs>();
            while (reader.Read())
            {
                customers.Add(new Customerscs
                {
                    CustomerID = reader.GetInt32(0),
                    FistName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    Address = reader.GetString(4)
                });
            }
            return View(customers);
        }

        public IActionResult Create( Customerscs customers)
        {
            try
            {
                using SqlConnection conn = new Connect().GetConnection();
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    "INSERT INTO Customers (FirstName, LastName, Email, Address) VALUES (@FirstName, @LastName, @Email, @Address)";
                cmd.Parameters.AddWithValue("@FirstName", customers.FistName);
                cmd.Parameters.AddWithValue("@LastName", customers.LastName);
                cmd.Parameters.AddWithValue("@Email", customers.Email);
                cmd.Parameters.AddWithValue("@Address", customers.Address);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int id)
        {
            using SqlConnection conn = new Connect().GetConnection();
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
            cmd.Parameters.AddWithValue("@CustomerID", id);
            using SqlDataReader reader = cmd.ExecuteReader();
            Customerscs customers = new Customerscs();
            while (reader.Read())
            {
                customers.CustomerID = reader.GetInt32(0);
                customers.FistName = reader.GetString(1);
                customers.LastName = reader.GetString(2);
                customers.Email = reader.GetString(3);
                customers.Address = reader.GetString(4);
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customerscs customers)
        {
            try
            {
                using SqlConnection conn = new Connect().GetConnection();
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Address = @Address WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", id);
                cmd.Parameters.AddWithValue("@FirstName", customers.FistName);
                cmd.Parameters.AddWithValue("@LastName", customers.LastName);
                cmd.Parameters.AddWithValue("@Email", customers.Email);
                cmd.Parameters.AddWithValue("@Address", customers.Address);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
