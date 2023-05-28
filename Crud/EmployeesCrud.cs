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
    public class EmployeesCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Employees> GetAll()
        {
            List<Employees> list = new List<Employees>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Employees>("SELECT Id, EmployerName, PositionId, PositionName, Telephone, RoleEmployer FROM EmployeesView").ToList();
            }

            return list;
        }
        public static Position GetOne(int Id)
        {
            Position model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Position>("SELECT Id, EmployerName, PositionId, PositionName, Telephone, RoleEmployer FROM EmployeesView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Employees WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Employees model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Employees SET EmployerName = @EmployerName, PositionId = @PositionId,  Telephone = @Telephone, RoleEmployer = @RoleEmployer WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Employees Add(Employees model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Employees (EmployerName, PositionId, Telephone, RoleEmployer) VALUES(@EmployerName, @PositionId, @Telephone, @RoleEmployer); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
