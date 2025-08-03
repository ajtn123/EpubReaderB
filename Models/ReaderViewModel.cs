using EpubReaderB.Controllers;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using VersOne.Epub;

namespace EpubReaderB.Models;

public class ReaderViewModel(EpubBook? book) : BookViewModelBase(book)
{

}
