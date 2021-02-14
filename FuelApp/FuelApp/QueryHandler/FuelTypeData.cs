using FuelApp.DbHelper;
using FuelApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FuelApp.QueryHandler
{
    public class FuelTypeData : DBConnection
    {
        public List<FuelType> fuelView()
        {
            List<FuelType> returnList = new List<FuelType>();

            this.StoredProcedure = "FuelView";

            DataTable dt = this.ExecuteSelect();

            foreach (DataRow dr in dt.Select())
            {
                FuelType item = new FuelType();
                item.Id = Convert.ToInt32(dr["id"].ToString());
                item.Name = dr["name"].ToString();
                item.CostPerLiter = Convert.ToDouble(dr["cost_per_liter"].ToString());
                item.Date = Convert.ToDateTime(dr["date"].ToString());
                item.IsActive = Convert.ToInt32(dr["is_active"].ToString()) == 1 ? true : false;

                returnList.Add(item);
            }

            return returnList;
        }

        public bool fuelAdd(FuelType item)
        {
            this.StoredProcedure = "FuelAdd";
            AddParameter("name", item.Name);
            AddParameter("cost_per_liter", item.CostPerLiter);
            AddParameter("date", item.Date);
            AddParameter("is_active", item.IsActive);

            return ExecuteInsert();
        }
    }
}
