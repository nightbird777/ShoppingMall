
using System.Data.SqlClient;

namespace ShoppingMall.Models
{
    public class EmployeeDB
    {
        public List<Employee> allEmployee()
        {
            string connectionString = "server=nyctotampa; initial catalog=personal; user id=raju; password=raju123";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "select * from employee";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            List<Employee> employees = new List<Employee>();

            while (reader.Read())
            {
                Employee employee = new Employee();

                employee.Id = Convert.ToInt32(reader[0].ToString());
                employee.Name = reader[1].ToString();
                employee.Phone = reader[2].ToString();
                employee.Email = reader[3].ToString();
                employee.Image = reader[4].ToString();
                employees.Add(employee);

            }
            reader.Close();
            conn.Close();
            return employees;
        }


        public Employee editEmployeeById(int id)
        {
            string connectionString = "server=nyctotampa; initial catalog=personal; user id=raju; password=raju123";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "select * from employee where id = " + id;
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Employee employee = new Employee();

            while (reader.Read())
            {
                employee.Id = Convert.ToInt32(reader[0].ToString());
                employee.Name = reader[1].ToString();
                employee.Phone = reader[2].ToString();
                employee.Email = reader[3].ToString();
                employee.Image = reader[4].ToString();
            }
            reader.Close();
            conn.Close();
            return employee;
        }


        public void saveEmployee(Employee employee)
        {
            string connectionString = "server=nyctotampa; initial catalog=personal; user id=raju; password=raju123";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "update employee set name = '" + employee.Name + "', Phone = '" + employee.Phone + "', Email = '" + employee.Email + "', Image = '" + employee.Image + "' where id = " + employee.Id;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }



        public void createEmployee(Employee employee)
        {
            string connectionString = "server=nyctotampa; initial catalog=personal; user id=raju; password=raju123";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "insert into employee (name, phone, email, Image) values ('" + employee.Name + "', '" + employee.Phone + "', '" + employee.Email + "', '" + employee.Image + "')";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public Employee deleteEmployee(int id)
        {
            string connectionString = "server=nyctotampa; initial catalog=personal; user id=raju; password=raju123";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "delete from employee where id = " + id;
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            Employee employee = new Employee();

            while (reader.Read())
            {
                employee.Id = Convert.ToInt32(reader[0].ToString());
                employee.Name = reader[1].ToString();
                employee.Phone = reader[2].ToString();
                employee.Email = reader[3].ToString();
                employee.Image = reader[4].ToString();
            }
            reader.Close();
            conn.Close();
            return employee;
        }

        public List<Employee> searchEmployee(string search)
        {
            string connectionString = "server=nyctotampa; initial catalog=personal; user id=raju; password=raju123";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "select * from employee where name like '%" + search + "%' ";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            List<Employee> employees = new List<Employee>();

            while (reader.Read())
            {
                Employee employee = new Employee();

                employee.Id = Convert.ToInt32(reader[0].ToString());
                employee.Name = reader[1].ToString();
                employee.Phone = reader[2].ToString();
                employee.Email = reader[3].ToString();
                employee.Image = reader[4].ToString();

                employees.Add(employee);

            }
            reader.Close();
            conn.Close();
            return employees;
        }


    }
}
