using System;
using AttributeSolution.Attribute;

namespace AttributeSolution.Model
{
    public class HeaderSaida
    {
        [Positional(posicao: 1, tamanho: 2, complemento: ' ')]
        public string Tipo { get; set; }

        [Positional(posicao: 2, tamanho: 8, complemento: ' ', dataFormat: "yyyyMMdd")]
        public DateTime DataArquivo { get; set; }
        
        [Positional(posicao: 3, tamanho: 12, complemento: '0')]
        public long NumeroSequencial { get; set; }

        public HeaderSaida()
        {
            
        }

        public HeaderSaida(DateTime dataArquivo, long numeroSequencial)
        {
            DataArquivo = dataArquivo;
            NumeroSequencial = numeroSequencial;
        }
    }
}