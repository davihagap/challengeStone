using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [ApiController]
    [Route("api/contas/")]
    public class ContaController:ControllerBase
    {
        //get para obtenção de lista de todas as contas do banco
        //localhost:5000/contas
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Conta>>> Get([FromServices] DataContext context)
        {
            var contas = await context.Contas.ToListAsync();
            return contas;
        }

        //get para obtenção de uma conta especifica por número da conta
        //localhost:5000/contas/1
        [HttpGet]
        [Route("{num:int}")]
        public async Task<ActionResult<Conta>> GetByNum([FromServices] DataContext context, int num)
        {
            var conta = await context.Contas
                .AsNoTracking()
                .FirstOrDefaultAsync( c => c.Numero == num);
            if (conta != null)
                return conta;
            else
                return NotFound("Conta inexistente");
        }

        //get para obtenção do extrato da conta
        //localhost:5000/contas/1/extratos
        [HttpGet]
        [Route("{num:int}/extratos")]
        public async Task<ActionResult<List<Transacao>>> GetExtratos([FromServices] DataContext context, int num)
        {
            var extratos = await context.Transacoes
                .Where(t => t.ContaNum == num)
                .AsNoTracking()
                .ToListAsync();
            if (extratos != null)
                return extratos;
            else
                return NotFound("Conta inexistente");
        }


        //Post para criação de conta
        //localhost:5000/contas
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Conta>> PostCriaConta(
            [FromServices] DataContext context,
            [FromBody] Conta model
        )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Contas.Add(model);
                }catch(Exception e)
                {
                    return BadRequest("Dados imcompatíveis");
                }
                await context.SaveChangesAsync();
                return model;
            }else{
                return BadRequest(ModelState);
            }
        }

        //post para realização de depósito em conta
        //localhost:5000/contas/3356/depositos/
        [HttpPost]
        [Route("{num:int}/depositos")]
        public async Task<ActionResult<TransacaoViewModel>> PostDeposito(
            [FromServices] DataContext context,
            [FromBody] TransacaoViewModel model,
            int num
        )
        {
            if (ModelState.IsValid)
            {
                var conta = await context.Contas
                    .FirstOrDefaultAsync( c => c.Numero == num);
                if (conta != null){
                    var deposito = new Transacao{
                        Descricao = model.Descricao,
                        Tipo = "DEP",
                        Data = DateTime.Now,
                        Valor = model.Valor,
                        ContaNum = conta.Numero
                    };
                    try
                    {
                        context.Transacoes.Add(deposito);
                    }catch(Exception e)
                    {
                        return BadRequest("Dados imcompatíveis, transação não realizada");
                    }

                    var taxa = new Transacao{
                        Descricao = "Taxa sobre depósito #" +  deposito.Id,
                        Tipo = "TAXA",
                        Data = DateTime.Now,
                        Valor = model.Valor * new decimal(0.01),
                        ContaNum = conta.Numero
                    };
                    try
                    {
                        context.Transacoes.Add(taxa);
                    }catch(Exception e)
                    {
                        return BadRequest("Dados imcompatíveis, transação não realizada");
                    }                    

                    conta.Saldo += deposito.Valor - taxa.Valor;
                    await context.SaveChangesAsync();
                    return model;
                }
                else
                    return NotFound("Conta inexistente, depósito não efetuado.");
            }else{
                return BadRequest(ModelState);
            }
        }

        //post para realização de saque em conta
        //localhost:5000/contas/3356/saques/
        [HttpPost]
        [Route("{num:int}/saques")]
        public async Task<ActionResult<TransacaoViewModel>> PostSaque(
            [FromServices] DataContext context,
            [FromBody] TransacaoViewModel model,
            int num
        )
        {
            if (ModelState.IsValid)
            {
                var conta = await context.Contas
                    .FirstOrDefaultAsync( c => c.Numero == num);
                if (conta != null){
                    if (model.Valor>conta.Saldo-4){
                        return BadRequest("Saldo insuficiente");
                    }
                    var saque = new Transacao{
                        Descricao = model.Descricao,
                        Tipo = "SAQ",
                        Data = DateTime.Now,
                        Valor = model.Valor,
                        ContaNum = conta.Numero
                    };
                    try
                    {
                        context.Transacoes.Add(saque);
                    }catch(Exception e)
                    {
                        return BadRequest("Dados imcompatíveis, transação não realizada");
                    }

                    var taxa = new Transacao{
                        Descricao = "Taxa sobre saque #" +  saque.Id,
                        Tipo = "TAXA",
                        Data = DateTime.Now,
                        Valor = new decimal(4),
                        ContaNum = conta.Numero
                    };
                    try
                    {
                        context.Transacoes.Add(taxa);
                    }catch(Exception e)
                    {
                        return BadRequest("Dados imcompatíveis, transação não realizada");
                    }

                    conta.Saldo -= saque.Valor + taxa.Valor;
                    await context.SaveChangesAsync();
                    return model;
                }
                else
                    return NotFound("Conta inexistente, saque não efetuado.");
            }else{
                return BadRequest(ModelState);
            }
        }
    }
}