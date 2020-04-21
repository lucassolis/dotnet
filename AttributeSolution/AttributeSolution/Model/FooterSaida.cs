using System;
using AttributeSolution.Attribute;

namespace AttributeSolution.Model
{
    public class FooterSaida
    {
        [Positional(posicao: 1, tamanho: 2, complemento: ' ')]
        public string Tipo { get; set; }
        
        [Positional(posicao: 2, tamanho: 12, complemento: '0')]
        public long QuantidadeRegistros { get; set; }

        public FooterSaida()
        {
            
        }
        
        public FooterSaida(long quantidadeRegistros)
        {
            QuantidadeRegistros = quantidadeRegistros;
        }
    }
}