using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AttributeSolution.Attribute;
using AttributeSolution.Model;

namespace AttributeSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var resultadoFinal = new List<string>();
            var header = new List<HeaderSaida>() { new HeaderSaida(DateTime.Now, 1234573321) };

            var arquivosSaida = new List<ArquivoSaida>()
            {
                new ArquivoSaida()
                {
                    Nome = "Lucas Solis",
                    NumeroDaSorte = 25,
                    Aniversario = new DateTime(1995, 1, 2),
                    Divida = 7398.473
                },
                new ArquivoSaida()
                {
                    Nome = "Chicão",
                    NumeroDaSorte = 21,
                    Aniversario = new DateTime(1998, 11, 1),
                    Divida = 22398.477
                }
            };
            
            var footer = new List<FooterSaida>() { new FooterSaida(arquivosSaida.Count) };

            resultadoFinal.AddRange(PositionalAttribute.Builder(header));
            resultadoFinal.AddRange(PositionalAttribute.Builder(arquivosSaida));
            resultadoFinal.AddRange(PositionalAttribute.Builder(footer));

            foreach (var linha in resultadoFinal)
            {
                Console.WriteLine(linha);
            }
        }
    }
}