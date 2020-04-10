using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AttributeSolution.Attribute;

namespace AttributeSolution.Model
{
    public class ArquivoSaida
    {
        [Positional(posicao: 1, tamanho: 2, complemento: ' ')]
        public string  Tipo
        {
            get { return "01"; }
        }
        
        [Positional(posicao: 2, tamanho: 30, complemento: ' ')]
        public string Nome { get; set; }

        [Positional(posicao: 4, tamanho: 8, complemento:'0')]
        public int NumeroDaSorte { get; set; }
        
        [Positional(posicao: 3, tamanho: 10, complemento:' ', dataFormat: "yyyy-MM-dd")] 
        public DateTime Aniversario { get; set; }
        
        [Positional(posicao: 5, tamanho: 10, complemento:'0', precisao: 2)] 
        public double Divida { get; set; }
    }
}