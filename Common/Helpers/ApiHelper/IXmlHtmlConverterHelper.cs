namespace Common.Helpers.ApiHelper
{
    public interface IXmlHtmlConverterHelper
    {
        string TransformXMLToHTML(string inputXml, string xsltString);
    }
}
