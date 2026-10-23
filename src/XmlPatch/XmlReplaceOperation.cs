using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace KsaLocFramework.XmlPatch;

public sealed class XmlReplaceOperation : XmlPatchOperation
{
    public string XPath { get; init; }

    public string Value { get; init; }

    public override int Apply(XDocument document, Func<string, string> translate)
    {
        if (XPath == null || Value == null || document.XPathEvaluate(XPath) is not IEnumerable<object> targets)
        {
            return 0;
        }
        int changed = 0;
        foreach (object target in targets)
        {
            if (target is XAttribute attribute)
            {
                attribute.Value = Value;
                changed++;
            }
            else if (target is XElement element)
            {
                element.Value = Value;
                changed++;
            }
        }
        return changed;
    }
}
