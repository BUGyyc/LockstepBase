namespace Entitas
{
    public class ContextInfoException : EntitasException
    {
        public ContextInfoException(IContext context, ContextInfo contextInfo)
            : base(string.Concat("Invalid ContextInfo for '", context, "'!\nExpected ", context.totalComponents, " componentName(s) but got ", contextInfo.componentNames.Length, ":"), string.Join("\n", contextInfo.componentNames))
        {
        }
    }
}

