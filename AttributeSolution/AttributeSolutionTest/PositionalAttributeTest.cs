using System;
using System.Collections.Generic;
using System.Linq;
using AttributeSolution.Attribute;
using AttributeSolution.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AttributeSolutionTest
{
    [TestClass]
    public class PositionalAttributeTest
    {
        [TestMethod]
        public void Formata_Arquivo_Saida_Deve_Retornar_Duas_Linhas()
        {
            var arquivosSaida = new List<ArquivoSaida>()
            {
                new ArquivoSaida()
                {
                    Nome = "Lucas Solis",
                    NumeroDaSorte = 25,
                    Aniversario = new DateTime(1995, 1, 2),
                    Divida = 7398.473,
                    TipoPessoa = "F"
                },
                new ArquivoSaida()
                {
                    Nome = "Chicão",
                    NumeroDaSorte = 21,
                    Aniversario = new DateTime(1998, 11, 1),
                    Divida = 22398.477,
                    TipoPessoa = "J"
                }
            };

            var resultado = PositionalAttribute.Builder(arquivosSaida);
            var enumerable = resultado as string[] ?? resultado.ToArray();
            
            Assert.AreEqual(2, enumerable.ToList().Count);
        }
        
        [TestMethod]
        public void Formata_Arquivo_Saida_Deve_Retornar_Posicoes_Conforme_Atributo()
        {
            var arquivosSaida = new List<ArquivoSaida>()
            {
                new ArquivoSaida()
                {
                    Nome = "Lucas Solis",
                    NumeroDaSorte = 25,
                    Aniversario = new DateTime(1995, 1, 2),
                    Divida = 7398.473,
                    TipoPessoa = "F"
                }
            };

            var resultado = PositionalAttribute.Builder(arquivosSaida);
            var enumerable = resultado as string[] ?? resultado.ToArray();
            
            Assert.AreEqual(1, enumerable.ToList().Count);
            Assert.AreEqual("01Lucas Solis                   1995-01-02F25000000739847JJJJ", enumerable.ToList()[0]);
        }
        
        [TestMethod]
        public void Formata_Header_Saida_Deve_Retornar_Posicoes_Conforme_Atributo()
        {
            var headerSaida = new HeaderSaida(new DateTime(2020, 04, 10), 123456789);

            var resultado = PositionalAttribute.Builder(new List<HeaderSaida>() { headerSaida });
            var enumerable = resultado as string[] ?? resultado.ToArray();
            
            Assert.AreEqual(1, enumerable.ToList().Count);
            Assert.AreEqual("0020200410123456789000", enumerable.ToList()[0]);
        }
        
        [TestMethod]
        public void Formata_Footer_Saida_Deve_Retornar_Posicoes_Conforme_Atributo()
        {
            var footerSaida = new FooterSaida(15);

            var resultado = PositionalAttribute.Builder(new List<FooterSaida>() { footerSaida });
            var enumerable = resultado as string[] ?? resultado.ToArray();
            
            Assert.AreEqual(1, enumerable.ToList().Count);
            Assert.AreEqual("99150000000000", enumerable.ToList()[0]);
        }
    }
}