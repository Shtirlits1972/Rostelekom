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
    public class EquipmentCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Equipment> GetAll()
        {
            List<Equipment> list = new List<Equipment>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Equipment>("SELECT Id, EquipmentName, EquipmentModel, SerialNumber, Manufacturer, EquipmentTypeId, EquipmentTypeName  FROM EquipmentView").ToList();
            }

            return list;
        }
        public static Equipment GetOne(int Id)
        {
            Equipment model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Equipment>("SELECT Id, EquipmentName, EquipmentModel, SerialNumber, Manufacturer, EquipmentTypeId, EquipmentTypeName FROM EquipmentView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Equipment WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Equipment model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Equipment SET EquipmentName = @EquipmentName, EquipmentModel = @EquipmentModel, SerialNumber = @SerialNumber, Manufacturer = @Manufacturer, EquipmentTypeId = @EquipmentTypeId WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Equipment Add(Equipment model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Equipment (EquipmentName, EquipmentModel, SerialNumber, Manufacturer, EquipmentTypeId) VALUES(@EquipmentName, @EquipmentModel, @SerialNumber, @Manufacturer, @EquipmentTypeId ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
