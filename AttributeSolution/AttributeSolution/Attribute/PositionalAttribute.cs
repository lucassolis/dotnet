using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace AttributeSolution.Attribute
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class PositionalAttribute : System.Attribute
    {
        public int Posicao { get; }
        public int Tamanho { get; }
        public char Complemento { get; }
        public string DataFormat { get; }
        public Int16 Precisao { get; }

        public PositionalAttribute(int posicao, int tamanho, char complemento, string dataFormat = "", Int16 precisao = 0)
        {
            Posicao = posicao;
            Tamanho = tamanho;
            Complemento = complemento;
            DataFormat = dataFormat;
            Precisao = precisao;
        }
    }
}