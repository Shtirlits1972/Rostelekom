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
    public class RequestKitCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<RequestKit> GetAll()
        {
            List<RequestKit> list = new List<RequestKit>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<RequestKit>("SELECT Id, EmployeesId, EmployerName, RequestDate, CompletionDate, StatusRequestId, StatusRequestName FROM RequestKitView ; ").ToList();
            }
            return list;
        }
        public static RequestKit GetOne(int Id)
        {
            RequestKit model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<RequestKit>("SELECT Id,  EmployeesId, EmployerName, RequestDate, CompletionDate, StatusRequestId, StatusRequestName  FROM RequestKitView WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM RequestKit WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(RequestKit model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE RequestKit SET EmployeesId = @EmployeesId, RequestDate = @RequestDate, CompletionDate = @CompletionDate, StatusRequestId = @StatusRequestId WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static RequestKit Add(RequestKit model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO RequestKit (EmployeesId, RequestDate, CompletionDate, StatusRequestId ) VALUES(@EmployeesId, @RequestDate, @CompletionDate, @StatusRequestId); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
