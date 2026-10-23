using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;

namespace KsaLocFramework.XmlPatch;

public sealed class XmlLocalizeOperation : XmlPatchOperation
{
    public string XPath { get; init; }

    public string Element { get; init; }

    public string Attribute { get; init; }

    public string Value { get; init; }

    public string Key { get; init; }

    public override int Apply(XDocument document, Func<string, string> translate)
    {
        return XPath == null ? ApplyByNames(document, translate) : ApplyByXPath(document, translate);
    }

    private int ApplyByNames(XDocument document, Func<string, string> translate)
    {
        int changed = 0;
        foreach (XElement element in Candidates(document))
        {
            changed += Localize(Attribute == null ? null : element.Attribute(Attribute), element, translate);
        }
        return changed;
    }

    private IEnumerable<XElement> Candidates(XDocument document)
    {
        if (Element != null)
        {
            return document.Descendants(Element);
        }
        if (Attribute != null)
        {
            return document.Descendants().Where(element => element.Attribute(Attribute) != null);
        }
        return Enumerable.Empty<XElement>();
    }

    private int ApplyByXPath(XDocument document, Func<string, string> translate)
    {
        if (document.XPathEvaluate(XPath) is not IEnumerable<object> targets)
        {
            return 0;
        }
        int changed = 0;
        foreach (object target in targets)
        {
            if (target is XAttribute attribute)
            {
                changed += Localize(attribute, null, translate);
            }
            else if (target is XElement element)
            {
                changed += Localize(null, element, translate);
            }
        }
        return changed;
    }

    private int Localize(XAttribute attribute, XElement element, Func<string, string> translate)
    {
        string original = attribute != null ? attribute.Value : element.Value;
        if (Value != null && original != Value)
        {
            return 0;
        }
        string translated = translate(Key ?? original);
        if (translated == null || translated == original)
        {
            return 0;
        }
        if (attribute != null)
        {
            attribute.Value = translated;
        }
        else
        {
            element.Value = translated;
        }
        return 1;
    }
}
