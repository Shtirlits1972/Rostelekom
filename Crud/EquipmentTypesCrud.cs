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
    public class EquipmentTypesCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<EquipmentTypes> GetAll()
        {
            List<EquipmentTypes> list = new List<EquipmentTypes>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<EquipmentTypes>("SELECT Id, EquipmentTypeName FROM EquipmentTypes").ToList();
            }

            return list;
        }
        public static EquipmentTypes GetOne(int Id)
        {
            EquipmentTypes model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<EquipmentTypes>("SELECT Id, EquipmentTypeName FROM EquipmentTypes WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM EquipmentTypes WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(EquipmentTypes model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE EquipmentTypes SET EquipmentTypeName = @EquipmentTypeName WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static EquipmentTypes Add(EquipmentTypes model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO EquipmentTypes (EquipmentTypeName) VALUES(@EquipmentTypeName); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
