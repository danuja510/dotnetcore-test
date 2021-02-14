using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FuelApp.DbHelper;
using FuelApp.Models;

namespace FuelApp.QueryHandler
{
    public class EmployeeData : DBConnection
    {
        public List<Employee> employeeView()
        {
            List<Employee> returnList = new List<Employee>();

            this.StoredProcedure = "EmployeeView";

            DataTable dt = this.ExecuteSelect();

            foreach (DataRow dr in dt.Select())
            {
                Employee item = new Employee();
                item.Id = Convert.ToInt32(dr["id"].ToString());
                item.Name = dr["name"].ToString();
                item.Email= dr["email"].ToString();
                item.Allowance = Convert.ToDouble(dr["allowance"].ToString());
                item.IsActive = Convert.ToInt32(dr["is_active"].ToString())==1?true:false;

                returnList.Add(item);
            }

            return returnList;
        }

        public bool employeeAdd(Employee item)
        {
            this.StoredProcedure = "EmployeeAdd";
            AddParameter("name", item.Name);
            AddParameter("email", item.Email);
            AddParameter("allowance", item.Allowance);
            AddParameter("is_active", item.IsActive);

            return ExecuteInsert();
        }
    }
}
