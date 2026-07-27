using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Estoques.API.Controllers.Base
{
    [ApiController]
    [Route("api/")]
    public abstract class ControladorBase : ControllerBase
    {
        protected int IDUsuarioLogado
        {
            get
            {
                var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("id")?.Value;
                return int.TryParse(claim, out int id) ? id : 0;
            }
        }
    }
}