using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace NetVOD
{
    public static class DataHelper
    {
        public static IList<Song> Search(string search)
        {
            SQLiteDataReader reader = null;

            if (string.IsNullOrEmpty(search))
            {
                reader = DBUtility.SQLiteHelper.ExecuteReader("Data Source=song.tmp;Version=3", CommandType.Text, "SELECT * FROM Song");
            }
            else
            {
                var parms = new[] { new SQLiteParameter("@name", '%' + search + '%'), new SQLiteParameter("@singer", '%' + search + '%') };
                reader = DBUtility.SQLiteHelper.ExecuteReader("Data Source=song.tmp;Version=3", CommandType.Text, "SELECT * FROM Song WHERE Name LIKE @name OR Singer LIKE @singer", parms);
            }

           

            var list = new List<Song>();
            using (reader)
            {
                while (reader.Read())
                {
                    Song song = new Song();
                    song.ID = Convert.ToInt32(reader["ID"]);
                    song.Name = reader["Name"].ToString();
                    song.Singer = reader["Singer"].ToString();
                    list.Add(song);
                }
            }
            return list;
        }

        public static string GetPath(int id)
        {
            var obj = DBUtility.SQLiteHelper.ExecuteScalar("Data Source=song.tmp;Version=3", CommandType.Text, "SELECT Path FROM Song WHERE ID=@id",new SQLiteParameter("@id",id));
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            return obj.ToString();
        }

        public static Song GetById(int id)
        {
            var reader = DBUtility.SQLiteHelper.ExecuteReader("Data Source=song.tmp;Version=3", CommandType.Text, "SELECT * FROM Song WHERE ID=" + id);
            if (reader.Read())
            {
                Song song = new Song();
                song.ID = Convert.ToInt32(reader["ID"]);
                song.Name = reader["Name"].ToString();
                song.Singer = reader["Singer"].ToString();
                return song;
            }
            return null;
        }

        public static void Add(string name,string path)
        {
            var sql = "SELECT COUNT(ID) FROM Song WHERE Name=@name";

            var obj = DBUtility.SQLiteHelper.ExecuteScalar("Data Source=song.tmp;Version=3", CommandType.Text, sql, new SQLiteParameter("@name", name.Trim()));

            if (Convert.ToInt32(obj)>0)
            {
                return;
            }

            var sqlins = "INSERT INTO Song(Name,Singer,Path)VALUES(@name,@singer,@path)";

            var parms = new[] { 
                new SQLiteParameter("@name", name),
                new SQLiteParameter("@singer","不知"),
                new SQLiteParameter("@path", path)
            };

            var p = DBUtility.SQLiteHelper.ExecuteNonQuery("Data Source=song.tmp;Version=3", CommandType.Text, sqlins, parms);
        }



    }
}
