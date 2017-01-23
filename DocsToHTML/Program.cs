using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace DocsToHTML
{
    /// <summary>
    /// Converts the XML documentation files from the other two projects to HTML
    /// </summary>
    /// <remarks>
    /// <para>
    /// If the other two projects have been built, the docs directory will contain the XML
    /// documentation. (At the very least, this project has itself been built, so you should see
    /// these documentation things). Running this program creates HTML files with the same content,
    /// but more appropriate for public display. They will appear in the docs directory with the
    /// same name but a .html extension, so the final contents of docs should be:
    /// <list type="bullet">
    /// <item><description>doc.css</description></item>
    /// <item><description>DocsToHTML.XML</description></item>
    /// <item><description>DocsToHTML.html</description></item>
    /// <item><description>GenerateSets.XML</description></item>
    /// <item><description>GenerateSets.html</description></item>
    /// <item><description>index.html</description></item>
    /// <item><description>MostCommonSet.XML</description></item>
    /// <item><description>MostCommonSet.html</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// TODO: Error handling: can't find the file, can't write to a new file, parse errors,
    /// character handling.
    /// TODO: Unit tests. Which tags are supported?
    /// <list type="bullet">
    /// <item><description>description (of an item)</description></item>
    /// <item><description>item (of a list)</description></item>
    /// <item><description>list (bullet)</description></item>
    /// <item><description>para</description></item>
    /// <item><description>param</description></item>
    /// <item><description>remarks</description></item>
    /// <item><description>returns</description></item>
    /// <item><description>summary</description></item>
    /// </list>
    /// TODO: Extend this list.
    /// TODO: Forbid documentation files larger than 1MB (since string length is ~2MB and this code
    /// roughly doubles the length). Could do sequential writing of top-level elements instead. How
    /// long are documentation files usually?
    /// TODO: Validate documentation. Are there any methods missing documentation? Any regular
    /// comments that should be promoted to XML ones?
    /// </para>
    /// </remarks>
    class Program
    {
        /// <summary>
        /// Paths (from working directory) of documents to convert.
        /// </summary>
        /// <remarks>
        /// TODO: Take this from arguments
        /// </remarks>
        private static String[] docsToConvert = { "../../../docs/DocsToHTML.XML", "../../../docs/GenerateSets.XML", "../../../docs/MostCommonSet.XML" };

        static void Main(string[] args)
        {
            foreach (String doc in docsToConvert)
            {
                //Will contain new html
                string text = "";

                //Read document sequentially (SAX-style)
                FileStream fs = new FileStream(doc, FileMode.Open, FileAccess.Read);
                XmlReader xReader = XmlReader.Create(fs);
                while (xReader.Read())
                {
                    switch (xReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            text += "Element:" + xReader.Value;
                            break;
                        case XmlNodeType.Text:
                            text += "Text:" + xReader.Value;
                            break;
                        case XmlNodeType.EndElement:
                            text += "/Element:" + xReader.Value;
                            break;
                    }
                }
                System.IO.File.WriteAllText(xmlToHTMLExtension(doc), text);
            }
        }

        /// <summary>
        /// Converts just the extension of the path from .XML to .html.
        /// </summary>
        /// <param name="path">A string that ends in .XML</param>
        /// <returns>The same path with the extension changed, if the extension is XML, otherwise the same path with an html extension.</returns>
        private static String xmlToHTMLExtension(String path)
        {
            if (path.EndsWith(".XML", StringComparison.OrdinalIgnoreCase))
            {
                return path.Substring(0, path.Length - 4) + ".html";
            }
            else
            {
                return path + ".html";
            }
        }
    }
}
