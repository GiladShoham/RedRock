using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gif.Components
{
    public class DocToRTF
    {

        /// <summary>
        /// The metod convert from DOC to RTF
        /// </summary>
        /// <param name="Source"> The source doc file </param>
        /// <returns> The target file path </returns>
        public static string ConvertDOCtoRTF(string sSource)
        {
            //Creating the instance of Word Application
            Microsoft.Office.Interop.Word.Application newApp = new Microsoft.Office.Interop.Word.Application();

            // specifying the Source & Target file names
            object Source = sSource;
            object Target = Path.GetDirectoryName(sSource) + "\\" + Path.GetFileNameWithoutExtension(sSource) + ".TXT";

            // Use for the parameter whose type are not known or  
            // say Missing
            object Unknown = Type.Missing;

            // Source document open here
            // Additional Parameters are not known so that are  
            // set as a missing type
            newApp.Documents.Open(ref Source, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown,
                 ref Unknown, ref Unknown, ref Unknown, ref Unknown);

            // Specifying the format in which you want the output file 
            object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatText;

            //Changing the format of the document
            newApp.ActiveDocument.SaveAs(ref Target, ref format,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown, ref Unknown,
                    ref Unknown, ref Unknown);

            // for closing the application
            newApp.Quit(ref Unknown, ref Unknown, ref Unknown);

            return (Target.ToString());
        }

    }
}
