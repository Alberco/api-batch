using DashboardBash.Models;
using System.Data;
using System.Data.SqlClient;

namespace DashboardBash.Services
{
    public class DBatch
    {
        private readonly string _connectionString;
        public DBatch(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<List<Batch>> ListarBatch()
        {
            var listBatch = new List<Batch>();

            using (var sql = new SqlConnection(_connectionString))
            {
                try
                {
                    using (var cmd = new SqlCommand("SP_ListarBatch", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var batch = new Batch
                                {
                                    Id = (int)reader["Id"],
                                    NameProgram = reader["NameProgram"].ToString(),
                                    Estado = (bool)reader["Estado"],
                                    HoraIni = (DateTime)reader["HoraIni"],
                                    HoraFin = (DateTime)reader["HoraFin"],
                                    HostName = reader["HostName"].ToString(),
                                    IpAddress = reader["IpAddress"].ToString()
                                };

                                listBatch.Add(batch);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return listBatch;
        }

        public async Task<List<Batch>> SearchProgram(string nameProgam,DateTime horaIni)
        {
            var listSerchBatch = new List<Batch>();

            using (var sql = new SqlConnection(_connectionString))
            {
                try
                {
                    await sql.OpenAsync();

                    using (var cmd = new SqlCommand("SP_SearchProgram", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NameProgram", nameProgam);
                        cmd.Parameters.AddWithValue("@HoraIni", horaIni.Date);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var batch = new Batch
                                {
                                    Id = (int)reader["Id"],
                                    NameProgram = reader["NameProgram"].ToString(),
                                    Estado = (bool)reader["Estado"],
                                    HoraIni = (DateTime)reader["HoraIni"],
                                    HoraFin = (DateTime)reader["HoraFin"],
                                    HostName = reader["HostName"].ToString(),
                                    IpAddress = reader["IpAddress"].ToString()
                                };

                                listSerchBatch.Add(batch);
                                Console.WriteLine(batch.HostName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return listSerchBatch;
        }

        public async Task<(List<Batch> Programs, int TotalPages)> GetProgramsAll(int pageNumber, int pageSize)
        {
            List<Batch> programs = new List<Batch>();

            int totalPages = 0;

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.OpenAsync();

                using (var sqlCommand = new SqlCommand("SP_GetPrograms", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@PageNumber", pageNumber);
                    sqlCommand.Parameters.AddWithValue("@PageSize", pageSize);

                    using (var reader = await sqlCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Batch program = new Batch
                            {
                                Id = (int)reader["Id"],
                                NameProgram = reader["NameProgram"].ToString(),
                                Estado = (bool)reader["Estado"],
                                HoraIni = (DateTime)reader["HoraIni"],
                                HoraFin = (DateTime)reader["HoraFin"],
                                HostName = reader["HostName"].ToString(),
                                IpAddress = reader["IpAddress"].ToString()
                            };

                            programs.Add(program);
                        }

                        if (await reader.NextResultAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                totalPages = Convert.ToInt32(reader["TotalPages"]);

                            }
                        }
                    }
                }
            }

            return (programs, totalPages);
        }

        public async Task<List<Batch>> TodayListBatch()
        {
            var listBatch = new List<Batch>();

            using (var sql = new SqlConnection(_connectionString))
            {
                try
                {
                    using (var cmd = new SqlCommand("SP_GetProgramsForCurrentDay", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var batch = new Batch
                                {
                                    Id = (int)reader["Id"],
                                    NameProgram = reader["NameProgram"].ToString(),
                                    Estado = (bool)reader["Estado"],
                                    HoraIni = (DateTime)reader["HoraIni"],
                                    HoraFin = (DateTime)reader["HoraFin"],
                                    HostName = reader["HostName"].ToString(),
                                    IpAddress = reader["IpAddress"].ToString()
                                };

                                listBatch.Add(batch);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return listBatch;
        }
    }
}
