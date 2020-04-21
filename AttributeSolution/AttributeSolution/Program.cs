using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AttributeSolution.Attribute;
using AttributeSolution.Common;
using AttributeSolution.Model;

namespace AttributeSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
            var resultadoHeader = new List<string>();
            var resultadoConteudo = new List<string>();
            var resultadoFooter = new List<string>();
            var header = new List<HeaderSaida>() { new HeaderSaida(DateTime.Now, 1234573321) { Tipo = "00"} };

            var arquivosSaida = new List<ArquivoSaida>()
            {
                new ArquivoSaida()
                {
                    Tipo= "01",
                    Nome = "Lucas Solis",
                    NumeroDaSorte = 25,
                    Aniversario = new DateTime(1995, 1, 2),
                    Divida = 7398.473m,
                    TipoPessoa = "F"
                },
                new ArquivoSaida()
                {
                    Tipo = "01",
                    Nome = "Chicão",
                    NumeroDaSorte = 21,
                    Aniversario = new DateTime(1998, 11, 1),
                    Divida = 22398.477m,
                    TipoPessoa = "J"
                }
            };
            
            var footer = new List<FooterSaida>() { new FooterSaida(arquivosSaida.Count) { Tipo = "99"} };

            resultadoHeader.AddRange(Util.ObjectToString(header));
            resultadoConteudo.AddRange(Util.ObjectToString(arquivosSaida));
            resultadoFooter.AddRange(Util.ObjectToString(footer));

            foreach (var linha in resultadoHeader) Console.WriteLine(linha);
            foreach (var linha in resultadoConteudo) Console.WriteLine(linha);
            foreach (var linha in resultadoFooter) Console.WriteLine(linha);
            
            // Reversão do resultado
            var reverseHeader = Util.StringToObject(resultadoHeader, new HeaderSaida());
            var reverseConteudo = Util.StringToObject(resultadoConteudo, new ArquivoSaida());
            var reverseFooter = Util.StringToObject(resultadoFooter, new FooterSaida());
        }
    }
}