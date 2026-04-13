# Solução - Processamento de Pagamentos

## O que estava errado no código original

O código original apresentava alguns problemas importantes:

* Não tratava eventos duplicados, podendo processar o mesmo pagamento mais de uma vez;
* Não havia retentativa em caso de falha no processamento;
* As falhas não eram armazenadas, apenas exibidas no console;
* O valor total dependia de apenas uma tentativa de processamento.

---

## O que foi alterado e por quê

### 1. Controle de duplicidade

Foi adicionado um `HashSet` para armazenar os IDs já processados.

Antes de processar cada pagamento, é feita a verificação para evitar reprocessamento de eventos duplicados.

**Motivo:** garantir que cada pagamento seja processado apenas uma vez.

---

### 2. Implementação de retentativas (retry)

Foi implementado um loop que tenta processar cada pagamento até 3 vezes.

Caso ocorra falha, o sistema tenta novamente até atingir o limite.

**Motivo:** lidar com falhas temporárias de serviços externos.

---

### 3. Registro de falhas

Pagamentos que falham após 3 tentativas são armazenados em uma lista.

Ao final do processamento, esses pagamentos são exibidos no console.

**Motivo:** permitir análise e rastreabilidade das falhas.

---

### 4. Cálculo do total

O valor total é atualizado apenas quando o pagamento é processado com sucesso.

**Motivo:** garantir que o total represente apenas pagamentos efetivamente concluídos.

---

## O que faria a mais se tivesse mais tempo

* Criaria testes unitários para validar os cenários principais;
* Separaria melhor as responsabilidades do código (ex: classe de serviço);
* Implementaria backoff entre tentativas;
* Persistiria os IDs processados para garantir idempotência entre execuções;
* Retornaria um objeto mais estruturado com o total e a lista de falhas.

---

## Observação

O valor total pode variar entre execuções, pois o método de pagamento simula falhas aleatórias.

Isso é esperado dentro do contexto do desafio.
