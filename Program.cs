var pagamentos = new List<EventoPagamento>
{
    new(Id: "id-1", Valor: 100m),
    new(Id: "id-2", Valor: 200m),
    new(Id: "id-1", Valor: 100m),
    new(Id: "id-3", Valor: 300m),
    new(Id: "id-4", Valor: 400m),
    new(Id: "id-2", Valor: 200m),
};

var total = ProcessarPagamentos(pagamentos);

Console.WriteLine($"Valor total processado: {total}");

decimal ProcessarPagamentos(List<EventoPagamento> pagamentos)
{
    decimal total = 0;
    var idsProcessados = new HashSet<string>();
    var falha = new List<EventoPagamento>();

    foreach (var pagamento in pagamentos)
    {
        if (idsProcessados.Contains(pagamento.Id))
        {
            Console.WriteLine($"Pagamento {pagamento.Id} já processado, pulando.");
            continue;
        }
        idsProcessados.Add(pagamento.Id);

        var tentativas = 0;
        var sucesso = false;

        while (tentativas < 3 && !sucesso)
        {
            tentativas++;

            try
            {
                var valorPago = Pagar(pagamento);
                total += valorPago;
                sucesso = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar pagamento {pagamento.Id} na tentativa {tentativas}: {ex.Message}");

                if (tentativas == 3)
                {
                    falha.Add(pagamento);
                    Console.WriteLine($"Pagamento {pagamento.Id} falhou após 3 tentativas, adicionando à lista de falhas.");
                }
            }
        }

    }

    if (falha.Count > 0)
    {
        Console.WriteLine("Pagamentos que falharam após 3 tentativas:");
        foreach (var pagamento in falha)
        {
            Console.WriteLine($"- {pagamento.Id} com valor {pagamento.Valor}");
        }
    }

    return total;
}

decimal Pagar(EventoPagamento pagamento)
{
    // NÃO alterar este método
    // Ele simula um serviço externo que pode falhar

    if (Random.Shared.Next(0, 2) == 0)
        throw new Exception("Falha ao realizar pagamento.");

    // Simula o pagamento, retornando o valor do pagamento.
    // Imagine uma chamada a um serviço externo aqui, https://api-pagamento.com/pagar, por exemplo.

    return pagamento.Valor;
}

sealed record EventoPagamento(string Id, decimal Valor);