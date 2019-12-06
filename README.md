# Desafio Stone
> asp .net wep api

Esse projeto foi desenvolvido com C#, .Net Core 3.0.1 e persistência de dados em memória.

## Requisitos

1. .Net SDK - Disponível em: https://dotnet.microsoft.com/download

## Go!

Depois de instalar o sdk pra rodar a api entre na pasta do projeto principal ("..\challengeStone\api") e execute:

```sh
dotnet build & dotnet run
```

Para rodar os testes vá para "..\challengeStone\api-integration-test" ou "..\challengeStone\api-unit-test" e execute:

```sh
dotnet build & dotnet test
```


## Exemplo de uso

Nossa api possui os seguintes EndPoints:

- GET ***http://localhost:porta/api/contas***: Retorna todas as contas cadastradas;
- GET ***http://localhost:porta/api/contas/{num}***: Retorna conta pelo número;
- GET ***http://localhost:porta/api/contas/{num}/extratos***: Retorna o extrato de uma determinada conta;
- POST ***http://localhost:porta/api/contas***: Cadastra uma nova conta, exemplo de body:
```json
{
	"Numero": 3357,
	"Cliente" : "Adolfo",
	"Saldo" : 1000
}
```
- POST ***http://localhost:porta/api/contas/{num}/saques***: Realiza uma operação de saque em uma conta, exemplo de body:
```json
{
	"Descricao": "Saque em conta corrente",
	"Valor": 100
}
```
- POST ***http://localhost:porta/api/contas/{num}/depositos***: Realiza uma operação de deposito em uma conta, exemplo de body:
```json
{
	"Descricao": "Depósito em conta corrente",
	"Valor": 30000
}
```



## Sobre mim.
Davi Hagap – [@davihagap](https://twitter.com/davihagap) – dhagap@gmail.com

[https://github.com/davihagap/github-link](https://github.com/davihagap)
