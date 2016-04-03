using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using CodeFirstStoreFunctions;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using PuzzleAdv.Backend.ViewModels.Shop;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace PuzzleAdv.Backend.Models
{
    public class PuzzleAdvDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Shop> Shop { get; set; }
        public DbSet<Puzzle> Puzzle { get; set; }
        public DbSet<Prize> Prize { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }

    }

    public static class DbContextExtension
    {
        public static List<ShopDistance> GetPuzzleDbFunction(this DbContext context, string query)
        {
            var command = context.Database.GetDbConnection().CreateCommand();

            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = query;

            command.Connection.Open();

            DbDataReader reader = command.ExecuteReader();

            var dt = new System.Data.DataTable();
            dt.Load(reader);

            List<ShopDistance> shopList = ConvertDataTable<ShopDistance>(dt);

            command.Connection.Close();

            return shopList;
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }

}
