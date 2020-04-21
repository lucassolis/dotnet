using System;
using System.Collections.Generic;
using System.Linq;
using AttributeSolution.Attribute;
using AttributeSolution.Common;
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
                    Tipo = "01",
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

            var resultado = Util.ObjectToString(arquivosSaida);
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
                    Tipo = "01",
                    Nome = "Lucas Solis",
                    NumeroDaSorte = 25,
                    Aniversario = new DateTime(1995, 1, 2),
                    Divida = 7398.473m,
                    TipoPessoa = "F"
                }
            };

            var resultado = Util.ObjectToString(arquivosSaida);
            var enumerable = resultado as string[] ?? resultado.ToArray();
            
            Assert.AreEqual(1, enumerable.ToList().Count);
            Assert.AreEqual("01Lucas Solis                   1995-01-02F000000250000739847", enumerable.ToList()[0]);
        }
        
        [TestMethod]
        public void Formata_Header_Saida_Deve_Retornar_Posicoes_Conforme_Atributo()
        {
            var headerSaida = new HeaderSaida(new DateTime(2020, 04, 10), 123456789){ Tipo = "00"};

            var resultado = Util.ObjectToString(new List<HeaderSaida>() { headerSaida });
            var enumerable = resultado as string[] ?? resultado.ToArray();
            
            Assert.AreEqual(1, enumerable.ToList().Count);
            Assert.AreEqual("0020200410000123456789", enumerable.ToList()[0]);
        }
        
        [TestMethod]
        public void Formata_Footer_Saida_Deve_Retornar_Posicoes_Conforme_Atributo()
        {
            var footerSaida = new FooterSaida(15) { Tipo = "99" };

            var resultado = Util.ObjectToString(new List<FooterSaida>() { footerSaida });
            var enumerable = resultado as string[] ?? resultado.ToArray();
            
            Assert.AreEqual(1, enumerable.ToList().Count);
            Assert.AreEqual("99000000000015", enumerable.ToList()[0]);
        }
    }
}