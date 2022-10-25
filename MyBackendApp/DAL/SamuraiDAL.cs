using MyBackendApp.Models;
using System.Data.SqlClient;

namespace MyBackendApp.DAL
{
    public class SamuraiDAL : ISamurai
    {
        private readonly IConfiguration _config;
        public SamuraiDAL(IConfiguration config)
        {
            _config = config;
        }
        private string GetConn()
        {
            return _config.GetConnectionString("SamuraiConnection");
        }

        public IEnumerable<Samurai> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                List<Samurai> listSamurai = new List<Samurai>();
                string strSql = @"select * from Samurais order by Name asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listSamurai.Add(new Samurai()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return listSamurai;
            }
        }

        public Samurai GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                Samurai samurai = new Samurai();
                string strSql = @"select * from Samurais where Id=@Id";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    samurai.Id = Convert.ToInt32(dr["Id"]);
                    samurai.Name = dr["Name"].ToString();
                }

                dr.Close();
                cmd.Dispose();
                conn.Close();

                return samurai;
            }
        }

        public IEnumerable<Samurai> GetByName(string name)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                List<Samurai> listSamurai = new List<Samurai>();
                string strSql = @"select * from Samurais 
                                  where Name like @Name
                                  order by Name asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        listSamurai.Add(new Samurai()
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = dr["Name"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();

                return listSamurai;
            }
        }

        public Samurai Insert(Samurai samurai)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"insert into Samurais(Name) values(@Name);select @@identity";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Name", samurai.Name);
                try
                {
                    conn.Open();
                    int idNum = Convert.ToInt32(cmd.ExecuteScalar());
                    samurai.Id = idNum;
                    return samurai;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public Samurai Update(Samurai samurai)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"update Samurais set Name=@Name where Id=@Id";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Name", samurai.Name);
                cmd.Parameters.AddWithValue("@Id", samurai.Id);
                try
                {
                    conn.Open();
                    int status = cmd.ExecuteNonQuery();
                    if (status != 1)
                        throw new Exception("Gagal mengupdate, data tidak ditemukan..");
                    return samurai;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"Error: {sqlEx.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(GetConn()))
            {
                string strSql = @"delete from Samurais where Id=@Id";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    conn.Open();
                    int status = cmd.ExecuteNonQuery();
                    if (status != 1)
                        throw new Exception($"Gagal delete data dengan id {id}");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public IEnumerable<Samurai> GetAllWithQuote()
        {
            throw new NotImplementedException();
        }

        public void AddSamuraiToBattle(int samuraiId, int battleId)
        {
            throw new NotImplementedException();
        }

        public void AddHorse(Horse horse)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Samurai> GetAllSamuraiWithHorse()
        {
            throw new NotImplementedException();
        }

        public Samurai GetSamuraiWithBattle(int samuraiId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Samurai> GetAllSamuraisWithBattles()
        {
            throw new NotImplementedException();
        }

        public void RemoveBattleFromSamurai(int samuraiId, int battleId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Samurai> GetAllWithQuery()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Samurai> GetSamuraiWhoSaidWord(string text)
        {
            throw new NotImplementedException();
        }

        public void RemoveQuotesFromSamurai(int samuraiId)
        {
            throw new NotImplementedException();
        }

        public Samurai Samurai(Samurai samurai)
        {
            throw new NotImplementedException();
        }

        object ISamurai.Insert(Samurai samurai)
        {
            throw new NotImplementedException();
        }
    }
}