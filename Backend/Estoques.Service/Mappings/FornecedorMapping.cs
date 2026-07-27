using Estoques.Domain.Entities;
using Estoques.Service.DTOs;

namespace Estoques.Service.Mappings;

public static class FornecedorMapping
{
    public static FornecedorDTO ParaDTO(this Fornecedor fornecedor)
    {
        return new FornecedorDTO(fornecedor.IDFornecedor, fornecedor.IDUsuario, fornecedor.NMFornecedor, fornecedor.TXEndereco, fornecedor.TXAnotacao);
    }

    public static List<FornecedorDTO> ParaDTOs(this List<Fornecedor> fornecedors)
    {
        return [.. fornecedors.Select(f => f.ParaDTO()).ToList()];
    }

    public static Fornecedor ParaEntidade(this FornecedorDTO fornecedorDTO)
    {
        return new Fornecedor(fornecedorDTO.IDFornecedor, fornecedorDTO.IDUsuario, fornecedorDTO.NMFornecedor, fornecedorDTO.TXEndereco, fornecedorDTO.TXAnotacao);
    }
}