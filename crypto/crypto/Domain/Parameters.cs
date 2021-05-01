using MatthiWare.CommandLine.Core.Attributes;

namespace crypto.Domain
{
    public class Parameters
    {
        [Required, Name("d", "diretorio"), Description("Diretório dos arquivos")]
        public string Directory { get; set; }

        [Required, Name("t", "type"), Description("crypt ou decrypt")]
        public string Type { get; set; }

        [Required, Name("k", "key"), Description("Chave de criptografia")]
        public string Key { get; set; }

        [Name("a", "archieve"), Description("Criptografa/Decriptografa arquivos"), DefaultValue(true)]
        public bool CryptArchieves { get; set; }

        [Name("sf", "subfolders"), Description("Criptografa/Decriptografa os arquivos das subpastas"), DefaultValue(true)]
        public bool CryptSubFolders { get; set; }

        [Name("fn", "foldername"), Description("Criptografa/Decriptografa o nome das pastas"), DefaultValue(true)]
        public bool CryptFoldersName { get; set; }

        [Name("an", "archievename"), Description("Criptografa/Decriptografa o nome dos arquivos"), DefaultValue(true)]
        public bool CryptArchieveName { get; set; }
    }
}
