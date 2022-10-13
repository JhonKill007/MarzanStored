using MarzanStored.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarzanStored.Core.Inteface
{
    public interface IOrdenRepository
    {
        Task<List<OrdenesDto>> SearchOrden(string s, string email);
        Task<List<OrdenesDto>> GetOrdenes(string email);
        Task UpdateProducto(int id, int cantidad);
    }
}
