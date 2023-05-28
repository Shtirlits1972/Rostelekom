using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Rostelekom.Models;

namespace Rostelekom.Crud
{
    public class PositionCrud
    {
        public static readonly string strConn = Ut.strConn;

        public static List<Position> GetAll()
        {
            List<Position> list = new List<Position>();

            using (IDbConnection db = new SqlConnection(strConn))
            {
                list = db.Query<Position>("SELECT Id, PositionName FROM Position").ToList();
            }

            return list;
        }
        public static Position GetOne(int Id)
        {
            Position model = null;

            using (IDbConnection db = new SqlConnection(strConn))
            {
                model = db.Query<Position>("SELECT Id, PositionName FROM Position WHERE Id = @Id;", new { Id }).FirstOrDefault();
            }

            return model;
        }
        public static void Del(int Id)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                db.Execute("DELETE FROM Position WHERE Id = @Id;", new { Id });
            }
        }
        public static void Edit(Position model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "UPDATE Position SET PositionName = @PositionName WHERE Id = @Id;";
                db.Execute(Query, model);
            }
        }
        public static Position Add(Position model)
        {
            using (IDbConnection db = new SqlConnection(strConn))
            {
                var Query = "INSERT INTO Position (PositionName) VALUES(@PositionName); SELECT CAST(SCOPE_IDENTITY() as int)";
                int Id = db.Query<int>(Query, model).FirstOrDefault();
                model.Id = Id;
            }
            return model;
        }
    }
}
