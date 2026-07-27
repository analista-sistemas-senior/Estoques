namespace Estoques.API.Services
{
    public class ArmazenamentoService(IWebHostEnvironment ambiente) : IArmazenamentoService
    {
        private readonly IWebHostEnvironment _ambiente = ambiente;

        public async Task<string> SalvarArquivo(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0) return string.Empty;

            var pastaUploads = Path.Combine(_ambiente.ContentRootPath, "Uploads", "Produtos");
            var extensao = Path.GetExtension(arquivo.FileName);
            var nomeUnico = $"{Guid.NewGuid()}{extensao}";
            var caminhoAbsoluto = Path.Combine(pastaUploads, nomeUnico);

            using (var stream = new FileStream(caminhoAbsoluto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return $"/Uploads/Produtos/{nomeUnico}";
        }

        public void ExcluirArquivo(string? caminhoRelativo)
        {
            if (string.IsNullOrEmpty(caminhoRelativo)) return;

            var caminhoAbsoluto = Path.Combine(_ambiente.ContentRootPath, caminhoRelativo.TrimStart('/', '\\'));
            if (File.Exists(caminhoAbsoluto)) File.Delete(caminhoAbsoluto);
        }
    }
}