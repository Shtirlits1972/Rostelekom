using Rostelekom.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Rostelekom.Crud
{
    public class InventoryCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Inventory> GetAll()
        {
            List<Inventory> list = new List<Inventory>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Inventory>("SELECT Id, EquipmentId, EquipmentName, Quantity, Location, Availability  FROM InventoryView").ToList();
            }

            return list;
        }
        public static Inventory GetOne(int Id)
        {
            Inventory model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Inventory>(" SELECT Id, EquipmentId, EquipmentName, Quantity, Location, Availability FROM InventoryView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Inventory WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Inventory model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Inventory SET EquipmentId = @EquipmentId, Quantity = @Quantity, Location = @Location, Availability = @Availability WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Inventory Add(Inventory model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Inventory (EquipmentId, Quantity, Location, Availability) VALUES(@EquipmentId, @Quantity, @Location, @Availability); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
