using System;
using System.Xml.Linq;

namespace KsaLocFramework.XmlPatch;

public abstract class XmlPatchOperation
{
    public abstract int Apply(XDocument document, Func<string, string> translate);
}
