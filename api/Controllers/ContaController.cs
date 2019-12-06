using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Domain.Models;
using api.Domain.Services;
using api.Domain.Exceptions;
using api.Domain.DTOs;

namespace api.Controllers
{
    [ApiController]
    [Route("api/contas/")]
    public class ContaController:ControllerBase
    {
        //get para obtenção de lista de todas as contas do banco
        //localhost:5000/api/contas
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Conta>>> Get([FromServices] IContaService service)
        {
            var result = await service.ListAsync();
            return Ok(result);
        }

        //get para obtenção de uma conta especifica por número da conta
        //localhost:5000/api/contas/1
        [HttpGet]
        [Route("{num:int}")]
        public async Task<ActionResult<Conta>> GetByNum([FromServices] IContaService service, int num)
        {
            try{
                var result = await service.FindByNumAsync(num);
                return Ok(result);
            }catch(ContaNaoEncontradaException e)
            {
                return NotFound(e.Message);
            }
        }

        //get para obtenção do extrato da conta
        //localhost:5000/api/contas/1/extratos
        [HttpGet]
        [Route("{num:int}/extratos")]
        public async Task<ActionResult<IEnumerable<Transacao>>> GetExtratos([FromServices] IContaService service, int num)
        {
            try{
                var result = await service.ListTransacoesAsync(num);
                return Ok(result);
            }catch(ContaNaoEncontradaException e)
            {
                return NotFound(e.Message);
            }
        }


        //Post para criação de conta
        //localhost:5000/api/contas
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Conta>> PostCriaConta(
            [FromServices] IContaService service,
            [FromBody] Conta model
        )
        {
            if (ModelState.IsValid)
            {                
                await service.SaveAsync(model);
                return Created($"api/contas/{model.Numero}", model);
            }else{
                return BadRequest(ModelState);
            }
        }

        //post para realização de depósito em conta
        //localhost:5000/api/contas/3356/depositos/
        [HttpPost]
        [Route("{num:int}/depositos")]
        public async Task<ActionResult<TransacaoRequest>> PostDeposito(
            [FromServices] IContaService service,
            [FromBody] TransacaoRequest model,
            int num
        )
        {
            if (ModelState.IsValid)
            {                
                try{
                    await service.DepositarAsync(model);
                }catch(ContaNaoEncontradaException e)
                {
                    return NotFound(e.Message);
                }
                return Created($"api/contas/{num}/extratos", model);
            }else{
                return BadRequest(ModelState);
            }
        }

        //post para realização de saque em conta
        //localhost:5000/api/contas/3356/saques/
        [HttpPost]
        [Route("{num:int}/saques")]
        public async Task<ActionResult<TransacaoRequest>> PostSaque(
            [FromServices] IContaService service,
            [FromBody] TransacaoRequest model,
            int num
        )
        {
            if (ModelState.IsValid)
            {
                try{
                    await service.SacarAsync(model);
                }catch(ContaNaoEncontradaException e)
                {
                    return NotFound(e.Message);
                }catch(SaldoInsuficienteException e)
                {
                    return Unauthorized(e.Message);
                }
                
                return Created($"api/contas/{num}/extratos", model);
            }else{
                return BadRequest(ModelState);
            }
        }
    }
}