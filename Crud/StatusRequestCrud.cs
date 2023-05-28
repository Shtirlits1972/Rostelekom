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
    public class StatusRequestCrud
    {
        public static readonly string strConn = Ut.strConn;
        public static List<StatusRequest> GetAll()
        {
            List<StatusRequest> list = new List<StatusRequest>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<StatusRequest>("SELECT Id, StatusRequestName FROM StatusRequest").ToList();
            }

            return list;
        }
        public static StatusRequest GetOne(int Id)
        {
            StatusRequest model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<StatusRequest>("SELECT Id, StatusRequestName FROM StatusRequest WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }
            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM StatusRequest WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(StatusRequest model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE StatusRequest SET StatusRequestName = @StatusRequestName WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static StatusRequest Add(StatusRequest model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO StatusRequest (StatusRequestName) VALUES(@StatusRequestName); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
