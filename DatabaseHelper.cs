
using BtcCzkRates.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BtcCzkRates
{
    public class DatabaseHelper
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        /// <summary>
        /// Uložení dat do DB
        /// </summary>
        /// <param name="priceEUR"></param>
        /// <param name="priceCZK"></param>
        public void SaveData(decimal priceEUR, decimal priceCZK)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("stp_SaveBitcoinData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PriceEUR", priceEUR);
                    cmd.Parameters.AddWithValue("@PriceCZK", priceCZK);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// Načtení dat z DB
        /// </summary>
        /// <returns></returns>
        public List<BitcoinData> LoadData()
        {
            List<BitcoinData> bitcoinDataList = new List<BitcoinData>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("stp_LoadBitcoinData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BitcoinData data = new BitcoinData
                            {
                                Id = (int)reader["Id"],
                                PriceEUR = (decimal)reader["PriceEUR"],
                                PriceCZK = (decimal)reader["PriceCZK"],
                                Note = reader["Note"].ToString(),
                                Timestamp = (DateTime)reader["Timestamp"]
                            };
                            bitcoinDataList.Add(data);
                        }
                    }
                }
            }

            return bitcoinDataList;
        }

        /// <summary>
        /// Aktualizace poznámky v DB
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        public void UpdateNoteById(int id, string note)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("stp_UpdateNoteById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Note", note);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Mazání záznamu z DB
        /// </summary>
        /// <param name="id"></param>
        public void DeleteData(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("stp_DeleteData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
