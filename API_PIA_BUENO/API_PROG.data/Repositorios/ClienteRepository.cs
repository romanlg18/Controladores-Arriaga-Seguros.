using Dapper;
using MySql.Data.MySqlClient;
using API_PROG.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_PIA_BUENO.API_PROG.model;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace API_PROG.data.Repositorios
{
    public class ClientesRepository : IClienteRepository
    {
        private readonly MySqlConfiguration _connectionString;

        public ClientesRepository(MySqlConfiguration ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<GetClienteCL>> GetAllClient()
        {
            var db = dbConnection();
            var sql = @"CALL SELECTCLIENTS";
            return await db.QueryAsync<GetClienteCL>(sql, new { });
        }

        public async Task<bool> InsertClientes(ClientesInsertar clientes)
        {
            var db = dbConnection();
            var sql = @"CALL AgregarCliente(@NombresP, @ApellidosP, @NumeroP, @CorreoP, @IdSegurosP, @FECHAP )";
            var result = await db.ExecuteAsync(sql, new { NombresP = clientes.CL_NOMBRES, ApellidosP = clientes.CL_aPELLIDOS, NumeroP = clientes.CL_NUMERO, CorreoP = clientes.CL_CORREO, IdSegurosP = clientes.IDSEGUROS, FECHAP = clientes.FECHA_ALTA });
            return result > 0;
        }

        public async Task<bool> BajaClientes(UpDeleteClient clientes)
        {
            var db = dbConnection();
            var sql = @"CALL delete_update_client(@idClienteP, @EstatusCLP)";
            var result = await db.ExecuteAsync(sql, new { idClienteP = clientes.idCliente, EstatusCLP = clientes.EstatusCL });
            return result > 0;
        }

        public async Task<IEnumerable<GetCredenciales>> GetCredentials()
        {
            var db = dbConnection();
            var sql = @"CALL LOGIN";
            return await db.QueryAsync<GetCredenciales>(sql, new { });
        }

        public async Task<IEnumerable<GetBuzon>> GetDudas()
        {
            var db = dbConnection();
            var sql = @"CALL BuzonDudas";
            return await db.QueryAsync<GetBuzon>(sql, new { });
        }

        public async Task<bool> UpdateClienteAdmin(UpdateClienteAdmin clientes)
        {
            var db = dbConnection();
            var sql = @"CALL ActualizarCliente(@IdClienteP, @NombreP, @ApellidoP, @NumeroP, @CorreoP )";
            var result = await db.ExecuteAsync(sql, new { IdClienteP = clientes.idCliente, NombreP = clientes.CL_NOMBRES, ApellidoP = clientes.CL_aPELLIDOS, NumeroP = clientes.CL_NUMERO, CorreoP = clientes.CL_CORREO });
            return result > 0;
        }
    }
}
