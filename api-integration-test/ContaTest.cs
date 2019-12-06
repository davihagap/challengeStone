using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using api;
using api.Domain.Models;
using api.Domain.DTOs;
using Newtonsoft.Json;
using System.Linq; 

namespace api.IntegrationTests
{
    public class ContaTest: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public ContaTest(CustomWebApplicationFactory<Startup> factory) => this.client = factory.CreateClient();

        [Fact]
        public async void GetContas()
        {
            var response = await this.client.GetAsync("/api/contas");

            var respContent = await response.Content.ReadAsStringAsync();
            var contas = JsonConvert.DeserializeObject<IEnumerable<Conta>>(respContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains(contas, c => c.Numero.Equals(5564));
            Assert.Contains(contas, c => c.Numero.Equals(8897));
            Assert.Contains(contas, c => c.Numero.Equals(1223));
        }

        [Theory]
        [InlineData(1223)]
        [InlineData(8897)]
        public async void GetContaByNum(int num)
        {
            var response = await this.client.GetAsync($"/api/contas/{num}");

            var respContent = await response.Content.ReadAsStringAsync();

            var conta = JsonConvert.DeserializeObject<Conta>(respContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(num, conta.Numero);
        }

        [Fact]
        public async void PostConta()
        {
            var conta = new Conta(4554, "joao", 100);

            var response = await this.client.PostAsJsonAsync<Conta>("/api/contas", conta);

            var respContent = await response.Content.ReadAsStringAsync();

            conta = JsonConvert.DeserializeObject<Conta>(respContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(4554, conta.Numero);
            Assert.Equal("api/contas/4554", response.Headers.Location.OriginalString);
        }

        [Theory]
        [InlineData(1330, 2)]
        [InlineData(1, 0)]
        public async void GetExtrato(int num, int expected)
        {
            var response = await this.client.GetAsync($"/api/contas/{num}/extratos");

            var respContent = await response.Content.ReadAsStringAsync();
            var transacoes = JsonConvert.DeserializeObject<IEnumerable<Transacao>>(respContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(transacoes.Count(), expected);
        }

        [Fact]
        public async void GetContaByNumNaoAchaConta()
        {
            var response = await this.client.GetAsync("/api/contas/133");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void RealizaDep()
        {
            var deposito = new TransacaoRequest{NumConta=4554, Valor=50};

            var response = await this.client.PostAsJsonAsync<TransacaoRequest>("/api/contas/4554/depositos", deposito);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);           
            Assert.Equal("api/contas/4554/extratos", response.Headers.Location.OriginalString);
        }

        [Fact]
        public async void RealizaDepNaoAchaConta()
        {
            var deposito = new TransacaoRequest{NumConta=4, Valor=50};

            var response = await this.client.PostAsJsonAsync<TransacaoRequest>("/api/contas/4/depositos", deposito);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void RealizaDepInvalido()
        {
            var deposito = new TransacaoRequest{NumConta=4, Valor=50};

            var response = await this.client.PostAsJsonAsync<TransacaoRequest>("/api/contas/1/depositos", deposito);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void RealizaSaque()
        {
            var saque = new TransacaoRequest{NumConta=4554, Valor=50};

            var response = await this.client.PostAsJsonAsync<TransacaoRequest>("/api/contas/4554/saques", saque);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);        
            Assert.Equal("api/contas/4554/extratos", response.Headers.Location.OriginalString);
        }

        [Fact]
        public async void RealizaSaqNaoAchaConta()
        {
            var saque = new TransacaoRequest{NumConta=4, Valor=50};

            var response = await this.client.PostAsJsonAsync<TransacaoRequest>("/api/contas/4/saques", saque);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void RealizaSaqInvalido()
        {
            var saque = new TransacaoRequest{NumConta=4, Valor=50};

            var response = await this.client.PostAsJsonAsync<TransacaoRequest>("/api/contas/1/saques", saque);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void SaqueSaldoInsuficiente()
        {
            var saque = new TransacaoRequest{NumConta=4554, Valor=9999999};

            var response = await this.client.PostAsJsonAsync<TransacaoRequest>("/api/contas/4554/saques", saque);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}