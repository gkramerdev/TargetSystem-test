using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Escolha um exercício para executar:");
            Console.WriteLine("1 - Cálculo da variável SOMA");
            Console.WriteLine("2 - Verificar número na sequência de Fibonacci");
            Console.WriteLine("3 - Analisar faturamento diário");
            Console.WriteLine("4 - Calcular percentual de faturamento por estado");
            Console.WriteLine("5 - Inverter uma string");
            Console.WriteLine("0 - Sair");
            Console.Write("\nDigite a opção desejada: ");

            int opcao = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (opcao)
            {
                case 1:
                    CalcularSoma();
                    break;
                case 2:
                    VerificarFibonacci();
                    break;
                case 3:
                    AnalisarFaturamento();
                    break;
                case 4:
                    CalcularPercentualEstados();
                    break;
                case 5:
                    InverterString();
                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    return;
                default:
                    Console.WriteLine("Opção inválida! Tente novamente.");
                    break;
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    static void CalcularSoma()
    {
        int INDICE = 13, SOMA = 0, K = 0;

        while (K < INDICE)
        {
            K = K + 1;
            SOMA = SOMA + K;
        }

        Console.WriteLine($"Valor final de SOMA: {SOMA}");
    }

    static void VerificarFibonacci()
    {
        Console.Write("Digite um número para verificar se está na sequência de Fibonacci: ");
        int numero = int.Parse(Console.ReadLine());

        int a = 0, b = 1, temp;
        while (b < numero)
        {
            temp = a + b;
            a = b;
            b = temp;
        }

        if (b == numero || numero == 0)
            Console.WriteLine($"O número {numero} pertence à sequência de Fibonacci.");
        else
            Console.WriteLine($"O número {numero} NÃO pertence à sequência de Fibonacci.");
    }

    static void AnalisarFaturamento()
    {
        string caminhoArquivo = "faturamento.json";

        if (!File.Exists(caminhoArquivo))
        {
            Console.WriteLine("Arquivo faturamento.json não encontrado!");
            return;
        }

        string json = File.ReadAllText(caminhoArquivo);
        List<Faturamento> dados = JsonConvert.DeserializeObject<List<Faturamento>>(json);

        var valores = dados.Where(d => d.Valor > 0).Select(d => d.Valor).ToList();

        double menor = valores.Min();
        double maior = valores.Max();
        double media = valores.Average();
        int diasAcimaMedia = valores.Count(v => v > media);

        Console.WriteLine($"Menor faturamento: {menor}");
        Console.WriteLine($"Maior faturamento: {maior}");
        Console.WriteLine($"Dias com faturamento acima da média: {diasAcimaMedia}");
    }

    static void CalcularPercentualEstados()
    {
        Dictionary<string, double> faturamentoEstados = new Dictionary<string, double>
        {
            {"SP", 67836.43},
            {"RJ", 36678.66},
            {"MG", 29229.88},
            {"ES", 27165.48},
            {"Outros", 19849.53}
        };

        double total = faturamentoEstados.Values.Sum();

        foreach (var estado in faturamentoEstados)
        {
            double percentual = (estado.Value / total) * 100;
            Console.WriteLine($"{estado.Key}: {percentual:F2}%");
        }
    }

    static void InverterString()
    {
        Console.Write("Digite uma string para inverter: ");
        string texto = Console.ReadLine();
        char[] caracteres = new char[texto.Length];

        for (int i = 0, j = texto.Length - 1; i < texto.Length; i++, j--)
        {
            caracteres[i] = texto[j];
        }

        Console.WriteLine("String invertida: " + new string(caracteres));
    }
}

class Faturamento
{
    public int Dia { get; set; }
    public double Valor { get; set; }
}
