using System.Collections.Generic;

namespace CuteS
{
    public class CompilerSettings
    {
        public bool Translation { get; private set; } = false;

        public bool ObjectFilesSave { get; private set; } = false;

        public List<string> SourceCodePaths { get; private set; } = new();

        /// <summary>
        /// Sets translation type
        /// </summary>
        /// <param name="translation">True - compilation, false - transpilation</param>
        public void TranslationType(bool translation) => Translation = translation;

        public void SaveObjectFiles() => ObjectFilesSave = true;

        public void AddSourceCodePath(string path) => SourceCodePaths.Add(path);
    }
}
