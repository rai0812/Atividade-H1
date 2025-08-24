using Microsoft.AspNetCore.Mvc;

namespace SeuProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FreteController : ControllerBase
    {
        
        public class Produto
        {
            public string Nome { get; set; }
            public float Peso { get; set; }         
            public float Altura { get; set; }       
            public float Largura { get; set; }     
            public float Comprimento { get; set; }  
            public string UF { get; set; }          
        }

        
        private readonly Dictionary<string, decimal> tarifasEstado = new()
        {
            { "SP", 50.00m },
            { "RJ", 60.00m },
            { "MG", 55.00m }
        };

        private const decimal taxaPorCm3 = 0.01m; 
        
        [HttpPost("calcular")]
        public IActionResult CalcularFrete([FromBody] Produto produto)
        {
            if (produto.Altura <= 0 || produto.Largura <= 0 || produto.Comprimento <= 0)
                return BadRequest("Altura, largura e comprimento devem ser maiores que zero.");

            
            decimal volume = (decimal)(produto.Altura * produto.Largura * produto.Comprimento);

           
            decimal tarifaEstado = tarifasEstado.ContainsKey(produto.UF.ToUpper())
                ? tarifasEstado[produto.UF.ToUpper()]
                : 70.00m;

            
            decimal valorFrete = (volume * taxaPorCm3) + tarifaEstado;

            return Ok(new
            {
                produto.Nome,
                produto.Peso,
                produto.Altura,
                produto.Largura,
                produto.Comprimento,
                produto.UF,
                Volume = volume,
                ValorFrete = valorFrete.ToString("C2") 
            });
        }
    }
}
