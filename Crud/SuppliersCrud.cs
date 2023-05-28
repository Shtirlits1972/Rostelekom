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
    public class SuppliersCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<Suppliers> GetAll()
        {
            List<Suppliers> list = new List<Suppliers>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Suppliers>("SELECT Id, SupplierName, ContactPerson, ContactNumber, Terms  FROM Suppliers").ToList();
            }
            return list;
        }
        public static Suppliers GetOne(int Id)
        {
            Suppliers model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Suppliers>("SELECT Id,  SupplierName, ContactPerson, ContactNumber, Terms FROM Suppliers WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Suppliers WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Suppliers model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Suppliers SET SupplierName = @SupplierName, ContactPerson = @ContactPerson, ContactNumber = @ContactNumber, Terms = @Terms WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Suppliers Add(Suppliers model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Suppliers ( SupplierName, ContactPerson, ContactNumber, Terms ) VALUES(@SupplierName, @ContactPerson, @ContactNumber, @Terms ); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
