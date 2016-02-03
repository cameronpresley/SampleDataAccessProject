using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SampleDataAccess.Models;

namespace SampleDatabase.DataAccess.Services
{
    public class AnimalDataAccess
    {
        private readonly string _connectionString;

        public AnimalDataAccess()
        {
            _connectionString = Connections.ConnectionString;
        }

        public AnimalDataAccess(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
            _connectionString = connectionString;
        }

        public void Create(Animal animal)
        {
            if (animal == null) throw new ArgumentNullException(nameof(animal));

            var query = "INSERT INTO Animals (Name, NumberOfLegs) VALUES (@Name, @NumberOfLegs)";
            var connection = CreateOpenConnection();

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@NumberOfLegs", animal.NumberOfLegs);
                command.ExecuteNonQuery();
            }
        }

        public Animal Read(int id)
        {
            var query = "SELECT * FROM Animals WHERE Id = @Id";

            var connection = CreateOpenConnection();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows) return null;
                    reader.Read();
                    var name = reader.GetString(reader.GetOrdinal("Name"));
                    var numberOfLegs = reader.GetInt32(reader.GetOrdinal("NumberOfLegs"));
                    return new Animal(id, name, numberOfLegs);
                }
            }
        }

        public List<Animal> ReadAll()
        {
            var query = "SELECT * FROM Animals";

            var connection = CreateOpenConnection();

            var animals = new List<Animal>();
            using (var command = new SqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows) return animals;
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(reader.GetOrdinal("Id"));
                        var name = reader.GetString(reader.GetOrdinal("Name"));
                        var numberOfLegs = reader.GetInt32(reader.GetOrdinal("NumberOfLegs"));
                        animals.Add(new Animal(id, name, numberOfLegs));
                    }
                    return animals;
                }
            }
        }

        private SqlConnection CreateOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
