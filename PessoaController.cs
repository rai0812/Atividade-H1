using Microsoft.AspNetCore.Mvc;

namespace SeuProjeto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        public class Pessoa
        {
            public string Nome { get; set; }
            public double Peso { get; set; }
            public double Altura { get; set; }
        }

    
        [HttpPost("calcular-imc")]
        public IActionResult CalcularIMC([FromBody] Pessoa pessoa)
        {
            if (pessoa.Altura <= 0)
                return BadRequest("Altura deve ser maior que zero.");

            double imc = pessoa.Peso / (pessoa.Altura * pessoa.Altura);

            return Ok(new
            {
                pessoa.Nome,
                IMC = imc.ToString("F2")
            });
        }

      
        [HttpGet("consulta-tabela-imc")]
        public IActionResult ConsultaTabelaIMC(double imc)
        {
            string descricao;

            if (imc < 18.5)
                descricao = "Abaixo do peso";
            else if (imc < 24.9)
                descricao = "Peso normal";
            else if (imc < 29.9)
                descricao = "Sobrepeso";
            else if (imc < 34.9)
                descricao = "Obesidade Grau I";
            else if (imc < 39.9)
                descricao = "Obesidade Grau II";
            else
                descricao = "Obesidade Grau III (mórbida)";

            return Ok(new
            {
                IMC = imc.ToString("F2"),
                Classificacao = descricao
            });
        }
    }
}

