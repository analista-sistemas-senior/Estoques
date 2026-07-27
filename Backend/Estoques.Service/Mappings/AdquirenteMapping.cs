using Estoques.Domain.Entities;
using Estoques.Service.DTOs;

namespace Estoques.Service.Mappings;

public static class AdquirenteMapping
{
    public static AdquirenteDTO ParaDTO(this Adquirente adquirente)
    {
        return new AdquirenteDTO(adquirente.IDAdquirente, adquirente.IDUsuario, adquirente.NMAdquirente, adquirente.TXEndereco, adquirente.TXAnotacao);
    }

    public static List<AdquirenteDTO> ParaDTOs(this List<Adquirente> adquirentes)
    {
        return [.. adquirentes.Select(a => a.ParaDTO()).ToList()];
    }

    public static Adquirente ParaEntidade(this AdquirenteDTO adquirenteDTO)
    {
        return new Adquirente(adquirenteDTO.IDAdquirente, adquirenteDTO.IDUsuario, adquirenteDTO.NMAdquirente, adquirenteDTO.TXEndereco, adquirenteDTO.TXAnotacao);
    }
}