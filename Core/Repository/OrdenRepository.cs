using Dapper;
using MarzanStored.Core.Inteface;
using MarzanStored.Data;
using MarzanStored.Dtos;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarzanStored.Core.Repository
{
  public class OrdenRepository : IOrdenRepository
  {
    private readonly IConnection _connection;
    readonly IConfiguration _configuration;

    public OrdenRepository(IConnection connection, IConfiguration configuration)
    {
      _connection = connection;
      _configuration = configuration;
    }

    public async Task<List<OrdenesDto>> SearchOrden(string s, string email)
    {
      using (var conn = _connection.GetConnection())
      {
        var query = $"select p.Nombre,p.Categoria,o.Cantidad,o.Fecha,o.Id from Ordenes as o inner join Productos as p on o.IdProductos = p.Id where o.Clientes='{email}' and p.Nombre LIKE '%{s}%' or p.Categoria  LIKE '%{s}%'";
        var reades = await conn.QueryAsync<OrdenesDto>(
            sql: query,
            commandType: System.Data.CommandType.Text
            );
        return reades.ToList();
      }
    }
    public async Task<List<OrdenesDto>> GetOrdenes(string email)
    {
      using (var conn = _connection.GetConnection())
      {
        var query = $"select p.Nombre,p.Categoria,o.Cantidad,o.Fecha,o.Id from Ordenes as o inner join Productos as p on o.IdProductos = p.Id where o.Clientes='{email}' ";
        var reades = await conn.QueryAsync<OrdenesDto>(
                    sql: query,
                    commandType: System.Data.CommandType.Text
                    );
        return reades.ToList();
      }
    }
    public async Task UpdateProducto(int id, int cantidad)
    {
      using (var conn = _connection.GetConnection())
      {
        var query = $"select Stock from Productos where Id={id}";
        var reades = await conn.QueryFirstOrDefaultAsync<int>(
            sql: query,
            commandType: System.Data.CommandType.Text
            );
        var NewCant = reades - cantidad;

        var query2 = $"UPDATE Productos SET Stock ={NewCant} where Id={id}";
        var reades2 = await conn.QueryFirstOrDefaultAsync<int>(
            sql: query2,
            commandType: System.Data.CommandType.Text
            );
      }
    }

  }
}
