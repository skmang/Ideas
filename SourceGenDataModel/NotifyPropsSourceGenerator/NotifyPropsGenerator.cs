using System;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;

namespace SourceGen.NotifyPropsSourceGenerator
{
    [Generator(LanguageNames.CSharp)]
    public class NotifyPropsGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var markAttributeFiles = context.AdditionalTextsProvider.Where(file => file.Path.EndsWith("MarkAttributes.sbn", StringComparison.OrdinalIgnoreCase));
            var fileNameAndContents = markAttributeFiles.Select((file, cancelToken) => (Path.GetFileNameWithoutExtension(file.Path), file.GetText()?.ToString()));
            context.RegisterPostInitializationOutput(ctx =>
            {
                IncrementalValueProvider<ImmutableArray<(string, string?)>> data = fileNameAndContents.Collect();
            });
        }
    }
}
