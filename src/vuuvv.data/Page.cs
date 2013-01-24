using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

using vuuvv.utils;

namespace vuuvv.data
{
    public class Page
    {
        public int? id;
        public int? parent_id;
        public int level = 1000;
        public int? tree_id;
        public int rght;
        public int lft;
        public bool active = true;
        public string title;
        public string meta_description;
        public string meta_keywords;
        public DateTime publish_date;
        public string content;
        public bool in_navigation = true;
        public bool banner = false;
        public int col;
        public string slug;
        public string url;

        public Page(int id)
        {
            string sql = "SELECT * FROM pages_page WHERE id=@id";
            var reader = DBHepler.Query(sql, new Dictionary<string,object>{{"id", id}});
            if (reader.Read())
            {
                DBHepler.FetchDataFromReader(this, reader);
                /*
                id = (int)reader.GetInt64(reader.GetOrdinal("id"));
                parent_id = (int)reader.GetInt64(reader.GetOrdinal("parent_id"));
                level = (int)reader.GetInt64(reader.GetOrdinal("level"));
                tree_id = (int)reader.GetInt64(reader.GetOrdinal("tree_id"));
                rght = (int)reader.GetInt64(reader.GetOrdinal("rght"));
                lft = (int)reader.GetInt64(reader.GetOrdinal("lft"));
                active = reader.GetBoolean(reader.GetOrdinal("active"));
                title = reader.GetString(reader.GetOrdinal("title"));
                meta_description = reader.GetString(reader.GetOrdinal("meta_description"));
                meta_keywords = reader.GetString(reader.GetOrdinal("meta_keywords"));
                content = reader.GetString(reader.GetOrdinal("content"));
                in_navigation = reader.GetBoolean(reader.GetOrdinal("in_navigation"));
                banner = reader.GetBoolean(reader.GetOrdinal("banner"));
                col = (int)reader.GetInt64(reader.GetOrdinal("col"));
                slug = reader.GetString(reader.GetOrdinal("slug"));
                url = reader.GetString(reader.GetOrdinal("url"));
                */
            }
        }

        public static Page ReadFrom(DbDataReader reader)
        {
            return null;
        }
    }
}
