using System.Text.Json;
using ConsumerViaCep.Models;
using static System.Console;

WriteLine("Digite o seu CEP: ");
var cep = ReadLine();

var enderecoUrl = $@"https://viacep.com.br/ws/{cep}/json/";

WriteLine($"Realizando requisição para o endpoint: {enderecoUrl}");

var client = new HttpClient();

try
{
    HttpResponseMessage? respostaApi = await client.GetAsync(enderecoUrl);
    respostaApi.EnsureSuccessStatusCode();

    string respostaApiJson = await respostaApi.Content.ReadAsStringAsync();

    Endereco? enderecoRetornadoDaApi = JsonSerializer.Deserialize<Endereco>(respostaApiJson);
    WriteLine($"CEP: {enderecoRetornadoDaApi.cep}");
    WriteLine($"Rua: {enderecoRetornadoDaApi.logradouro}");
    WriteLine($"Bairro: {enderecoRetornadoDaApi.bairro}");
    WriteLine($"Cidade: {enderecoRetornadoDaApi.localidade}");
}
catch (System.Exception e)
{

    WriteLine("Aconteceu um erro:\n" + e.Message);
}
