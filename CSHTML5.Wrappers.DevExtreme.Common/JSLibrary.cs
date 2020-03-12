using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHTML5.Wrappers.DevExtreme.Common
{
    /// <summary>  
    /// Class used to represent a set of JS and CSS files that should be loaded only once.
    /// </summary>
    public class JSLibrary
    {
        /// <value>
        /// Whether the library is already loaded.
        /// </value>
        public bool IsLoaded { get; private set; }

        /// <value>
        /// URLs pointing to the library's CSS files.
        /// </value>
        public Interop.ResourceFile[] CSS { get; private set; }

        /// <value>
        /// URLs pointing to the library's JS files.
        /// </value>
        public Interop.ResourceFile[] JS { get; private set; }

        public JSLibrary(Interop.ResourceFile[] css, Interop.ResourceFile[] js)
        {
            this.IsLoaded = false;
            this.CSS = css;
            this.JS = js;
        }

        /// <summary>
        /// Loads the library if it is not already loaded.
        /// </summary>
        /// <returns>
        /// A Task that should be awaited to wait until the library has finished loading.
        /// </returns>
        public async Task Load()
        {
            if (!this.IsLoaded)
            {
                bool wasLoadingSuccessful = true;
                foreach (var url in this.CSS)
                    await Interop.LoadCssFile(url);

                foreach (var url in this.JS)
                    try
                    {
                        await Interop.LoadJavaScriptFile(url);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        wasLoadingSuccessful = false;
                    }
                this.IsLoaded = wasLoadingSuccessful;
            }
        }

        /// <summary>
        /// Loads this library if it is not already loaded, and then executes a given callback.
        /// </summary>
        /// <param name="callback">
        /// The callback to execute when the library has finished loading.
        /// </param>
        public void LoadAsync(Action callback)
        {
            if (!this.IsLoaded)
            {
                Interop.LoadCssFilesAsync(this.CSS, () =>
                {
                    Interop.LoadJavaScriptFilesAsync(this.JS, () =>
                    {
                        this.IsLoaded = true;
                        callback();
                    });
                });
            }
        }
    }
}
