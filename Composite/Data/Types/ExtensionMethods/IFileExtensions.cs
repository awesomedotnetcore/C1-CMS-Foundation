using System.IO;
using Composite.Core.IO;
using Composite.Data.Streams;


namespace Composite.Data.Types
{
    /// <summary>
    /// Extension methods for IFile
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public static class IFileExtensions
    {
        public static Stream GetReadStream(this IFile file)
        {
            IFileStreamManager manager = FileStreamManagerLocator.GetFileStreamManager(file.GetType());

            return manager.GetReadStream(file);
        }



        public static Stream GetNewWriteStream(this IFile file)
        {
            IFileStreamManager manager = FileStreamManagerLocator.GetFileStreamManager(file.GetType());

            return manager.GetNewWriteStream(file);
        }

        public static void SubscribeOnChanged(this IFile file, OnFileChangedDelegate handler)
        {
            IFileStreamManager manager = FileStreamManagerLocator.GetFileStreamManager(file.GetType());

            manager.SubscribeOnFileChanged(file, handler);
        }

        /// <summary>
        /// Returns all text from the stream associated with the provided IFile
        /// </summary>
        public static string ReadAllText(this IFile file)
        {
            using (Stream fileStream = GetReadStream(file))
            {
                using (C1StreamReader sr = new C1StreamReader(fileStream))
                {
                    return sr.ReadToEnd();
                }
            }
        }



        /// <summary>
        /// Replaces the all files content with some new content
        /// </summary>
        /// <param name="file"></param>
        /// <param name="newContent"></param>
        public static void SetNewContent(this IFile file, string newContent)
        {
            using (C1StreamWriter sw = new C1StreamWriter(GetNewWriteStream(file)))
            {
                sw.Write(newContent);
            }
        }
    }
}