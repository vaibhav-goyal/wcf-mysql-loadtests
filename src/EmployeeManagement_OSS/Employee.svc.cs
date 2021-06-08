using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace EmployeeManagement_OSS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Employee" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Employee.svc or Employee.svc.cs at the Solution Explorer and start debugging.
    public class Employee : IEmployee
    {
        private readonly string _connectionString;
        private const string _query = 
@"select year(hire_date) as Year, 
count(*) as TotalHires 
from employees.employees 
where @p0 = @p0
group by year(hire_date);";

        public Employee()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySQL_CS"].ConnectionString;
        }

        public List<GetHiresByYearResult> GetHiresByYear()
        {
            var result = new List<GetHiresByYearResult>();
            using (var con = new MySqlConnection(_connectionString))
            {
                con.Open();
                using (var cmd = new MySqlCommand(_query, con))
                {                    
                    cmd.Parameters.AddWithValue("p0", GetRandomNumber());
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                result.Add(new GetHiresByYearResult { Year = rdr.GetInt32(0), TotalHires = rdr.GetInt64(1) });
                            }
                        }
                    }
                }
            }
            return result;
        }

        public async Task<List<GetHiresByYearResult>> GetHiresByYearV2()
        {
            var result = new List<GetHiresByYearResult>();
            using (var con = new MySqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (var cmd = new MySqlCommand(_query, con))
                {
                    cmd.Parameters.AddWithValue("p0", GetRandomNumber());
                    using (var rdr = await cmd.ExecuteReaderAsync())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                result.Add(new GetHiresByYearResult { Year = rdr.GetInt32(0), TotalHires = rdr.GetInt64(1) });
                            }
                        }
                    }
                }
            }
            return result;
        }

        private int GetRandomNumber()
        {
            var rnd = new Random();
            return rnd.Next(1, int.MaxValue);
        }
    }
}
